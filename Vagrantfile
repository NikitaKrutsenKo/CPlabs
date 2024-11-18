Vagrant.configure("2") do |config|
  
  # Визначення конфігурації для Ubuntu
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "bento/ubuntu-22.04"
    ubuntu.vm.hostname = "VagrantVM"
    ubuntu.vm.network "forwarded_port", guest: 7031, host: 7031
    ubuntu.vm.network "forwarded_port", guest: 1433, host: 1433
    ubuntu.vm.network "forwarded_port", guest: 7106, host: 7106
    ubuntu.vm.network "private_network", ip: "192.168.67.26"
    ubuntu.vm.provider "virtualbox" do |vb|
      vb.name = "VagrantVM"
      vb.gui = false
      vb.memory = "15360"
      vb.cpus = 5
    end
    ubuntu.vm.synced_folder ".", "/home/vagrant/project"
    ubuntu.ssh.insert_key = false

    ubuntu.vm.provision "shell", inline: <<-SHELL
      # Видалення існуючих пакетів .NET, якщо вони є
      sudo apt remove -y 'dotnet*' 'aspnet*' 'netstandard*' || true

      # Налаштування apt preferences для ігнорування системних .NET пакетів
      echo "Package: dotnet* aspnet* netstandard*" | sudo tee /etc/apt/preferences.d/dotnet
      echo "Pin: origin \"archive.ubuntu.com\"" | sudo tee -a /etc/apt/preferences.d/dotnet
      echo "Pin-Priority: -10" | sudo tee -a /etc/apt/preferences.d/dotnet
      echo "" | sudo tee -a /etc/apt/preferences.d/dotnet
      echo "Package: dotnet* aspnet* netstandard*" | sudo tee -a /etc/apt/preferences.d/dotnet
      echo "Pin: origin \"security.ubuntu.com\"" | sudo tee -a /etc/apt/preferences.d/dotnet
      echo "Pin-Priority: -10" | sudo tee -a /etc/apt/preferences.d/dotnet

      # Додавання Microsoft репозиторію
      wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
      sudo dpkg -i packages-microsoft-prod.deb
      rm packages-microsoft-prod.deb

      # Оновлення репозиторіїв
      sudo apt-get update

      # Встановлення необхідних інструментів і .NET SDK
      sudo apt-get install -y gpg curl wget apt-transport-https software-properties-common
      sudo apt-get install -y dotnet-sdk-6.0

      # Перевірка встановлення
      dotnet --info || { echo "Помилка встановлення .NET SDK"; exit 1; }
    SHELL

    ubuntu.vm.provision "shell", privileged: false, inline: <<-SHELL
      # Додавання BaGet як джерела NuGet
      dotnet nuget add source http://10.0.2.2:5000/v3/index.json -n "BaGet"
      
      # Встановлення інструмента MKrutsenko
      dotnet tool install -g MKrutsenko --version 1.0.6 --add-source http://10.0.2.2:5000/v3/index.json

      # Додавання інструментів до PATH
      echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.bashrc
      source ~/.bashrc

      # Перевірка встановлення інструмента
      MKrutsenko --help || { echo "Інструмент MKrutsenko не встановлено коректно"; exit 1; }
    SHELL
  end
  
  # Визначення конфігурації для Windows
  config.vm.define "windows" do |windows|
    windows.vm.box = "StefanScherer/windows_2019"
    windows.vm.communicator = "winrm"
    
    windows.vm.provider "virtualbox" do |vb|
      vb.name = "WindowsVM"
      vb.gui = true
      vb.memory = "10240"
      vb.cpus = 5
      vb.customize ["modifyvm", :id, "--vram", "128"]
      vb.customize ["modifyvm", :id, "--natdnshostresolver1", "on"]
      vb.customize ["modifyvm", :id, "--natdnsproxy1", "on"]
      vb.customize ["modifyvm", :id, "--clipboard", "bidirectional"]
    end
    
    # Налаштування портів для Windows
    windows.vm.network "forwarded_port", guest: 5050, host: 5052, auto_correct: true
    windows.vm.network "forwarded_port", guest: 5000, host: 5003, auto_correct: true
    windows.vm.network "forwarded_port", guest: 3389, host: 33389, auto_correct: true
    windows.vm.network "forwarded_port", guest: 5985, host: 55985, auto_correct: true
    
    windows.winrm.username = "vagrant"
    windows.winrm.password = "vagrant"
    windows.winrm.transport = :negotiate
    windows.winrm.basic_auth_only = false
    
    windows.vm.provision "shell", inline: <<-SHELL
      Set-ExecutionPolicy Bypass -Scope Process -Force
      [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072
      iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
      
      choco install dotnet-sdk -y --no-progress
      
      refreshenv
      
      dotnet nuget add source http://10.0.2.2:5000/v3/index.json -n "BaGet"
      dotnet tool install -g MKrutsenko --version 1.0.6 --add-source http://10.0.2.2:5000/v3/index.json
    SHELL
  end

  config.vm.define "macos" do |macos|
    macos.vm.box = "ramsey/macos-catalina"
    macos.vm.hostname = "MacOSVM"
    
    macos.vm.provider "virtualbox" do |vb|
      vb.name = "MacOSVM"
      vb.gui = true
      vb.memory = "10240"
      vb.cpus = 4
    end

    macos.vm.provision "shell", inline: <<-SHELL
      # Встановлення Homebrew для macOS
      /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
      brew update
      brew install --cask dotnet-sdk
      
      # Перевірка доступності BaGet
      curl -I http://10.0.2.2:5000/v3/index.json || echo "BaGet server is not accessible"
    SHELL

    macos.vm.provision "shell", privileged: false, inline: <<-SHELL
      dotnet nuget add source http://10.0.2.2:5000/v3/index.json -n "BaGet"
      dotnet tool install -g MKrutsenko --version 1.0.6 --add-source http://10.0.2.2:5000/v3/index.json

      echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.zshrc
      export PATH="$PATH:$HOME/.dotnet/tools"
    SHELL
  end
end
