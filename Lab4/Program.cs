using System.Runtime.InteropServices;
using McMaster.Extensions.CommandLineUtils;
using ClassLibraryForLab4;

namespace Lab4
{

    [Command(Name = "Lab4", Description = "Консольний застосунок для лабораторних робіт")]
    [Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
    class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication<Program>();
            app.Conventions.UseDefaultConventions();

            try
            {
                return app.Execute(args);
            }
            catch (CommandParsingException)
            {
                ShowUsageGuide();
                return 0;
            }
        }

        private int OnExecute(CommandLineApplication app)
        {
            ShowUsageGuide();
            return 0;
        }

        private static void ShowUsageGuide()
        {
            Console.WriteLine("Доступні команди:");
            Console.WriteLine("  version       Показує інформацію про програму");
            Console.WriteLine("  run           Запускає обрану лабораторну роботу");
            Console.WriteLine("                Приклади використання:");
            Console.WriteLine("                Lab4 run lab1 -i input.txt -o output.txt");
            Console.WriteLine("                Lab4 run lab2 --input=input.txt --output=output.txt");
            Console.WriteLine("  set-path      Задає шлях до папки з інпут та аутпут файлами для всіх підтримуваних ОС");
            Console.WriteLine("                Приклад використання:");
            Console.WriteLine("                Lab4 set-path -p /path/to/folder");
            Console.WriteLine("  help          Виводить цю інформацію");
        }

        [Command("version", Description = "Показує інформацію про програму")]
        class VersionCommand
        {
            private int OnExecute()
            {
                Console.WriteLine("Автор: Круценко Микита");
                Console.WriteLine("Версія: 1.0.0");
                return 0;
            }
        }

        [Command("run", Description = "Запускає обрану лабораторну роботу")]
        class RunCommand
        {
            [Argument(0, "lab", "Лабораторна для запуску (lab1, lab2, lab3)")]
            public string Lab { get; set; }

            [Option("-i|--input", "Інпут файл", CommandOptionType.SingleValue)]
            public string InputFile { get; set; }

            [Option("-o|--output", "Аутпут файл", CommandOptionType.SingleValue)]
            public string OutputFile { get; set; }

            private int OnExecute()
            {
                Console.WriteLine(Environment.GetEnvironmentVariable("LAB_PATH"));
                string inputPath = InputFile ?? Environment.GetEnvironmentVariable("LAB_PATH") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                string outputPath = OutputFile ?? Environment.GetEnvironmentVariable("LAB_PATH") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                inputPath = Path.Combine(inputPath, "INPUT.txt");
                outputPath = Path.Combine(outputPath, "OUTPUT.txt");
                if (!File.Exists(inputPath))
                {
                    Console.WriteLine($"Файл {inputPath} не знайдено.");
                    return 1;
                }

                switch (Lab?.ToLower())
                {
                    case "lab1":
                        Lab1Lib.RunLab1(inputPath, outputPath);
                        break;
                    case "lab2":
                        Lab2Lib.RunLab2(inputPath, outputPath);
                        break;
                    case "lab3":
                        Lab3Lib.RunLab3(inputPath, outputPath);
                        break;
                    default:
                        Console.WriteLine("Невірна лабораторна робота. Вкажіть lab1, lab2 або lab3.");
                        return 1;
                }

                Console.WriteLine($"Роботу завершено. Результат записано у {outputPath}");
                return 0;
            }
        }

        [Command("set-path", Description = "Задає шлях до папки з інпут та аутпут файлами для всіх підтримуваних ОС")]
        class SetPathCommand
        {
            [Option("-p|--path", "Шлях до папки", CommandOptionType.SingleValue)]
            public string Path { get; set; }

            private int OnExecute()
            {
                if (string.IsNullOrEmpty(Path))
                {
                    Console.WriteLine("Шлях не вказано.");
                    return 1;
                }

                try
                {
                    SetEnvironmentVariable("LAB_PATH", Path);
                    Console.WriteLine($"Змінна LAB_PATH встановлена на: {Path}");
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Не вдалося встановити змінну середовища: {ex.Message}");
                    return 1;
                }
            }

            private void SetEnvironmentVariable(string variable, string value)
            {
                if (OperatingSystem.IsWindows())
                {
                    Environment.SetEnvironmentVariable(variable, value, EnvironmentVariableTarget.Machine);
                }
                else if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                {
                    string profilePath = OperatingSystem.IsLinux() ? "/etc/environment" : "/etc/paths";

                    if (File.Exists(profilePath))
                    {
                        using (StreamWriter sw = File.AppendText(profilePath))
                        {
                            sw.WriteLine($"{variable}={value}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Системний файл для змінних середовища не знайдено.");
                        throw new InvalidOperationException("Невдале встановлення змінної середовища.");
                    }
                }
            }
        }

    }

    

}