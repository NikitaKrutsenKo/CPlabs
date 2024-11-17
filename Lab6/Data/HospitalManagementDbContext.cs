using Lab6.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Data
{
    public class HospitalManagementDbContext : DbContext
    {
        public HospitalManagementDbContext(DbContextOptions<HospitalManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<RefTimeOffReason> RefTimeOffReasons { get; set; }
        public DbSet<StaffTimeOff> StaffTimeOffs { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<RosterOfStaffOnShift> RosterOfStaffOnShifts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<StaffWardAssignment> StaffWardAssignments { get; set; }
        public DbSet<StaffPay> StaffPay { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Address_ID);
                entity.Property(e => e.Line1).IsRequired();
                entity.Property(e => e.City).IsRequired();
                entity.Property(e => e.Country).IsRequired();
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.HasKey(e => e.Hospital_ID);
                entity.Property(e => e.HospitalName).IsRequired();

                entity.HasOne(h => h.Address)
                      .WithMany()
                      .HasForeignKey(h => h.Address_ID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.Staff_ID);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();

                entity.HasOne(s => s.Address)
                      .WithMany()
                      .HasForeignKey(s => s.Address_ID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Hospital)
                      .WithMany()
                      .HasForeignKey(s => s.Hospital_ID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RefTimeOffReason>(entity =>
            {
                entity.HasKey(e => e.ReasonCode);
                entity.Property(e => e.ReasonDescription).IsRequired();
            });

            modelBuilder.Entity<StaffTimeOff>(entity =>
            {
                entity.HasKey(e => e.StaffTimeOff_ID);

                entity.HasOne(sto => sto.Staff)
                      .WithMany()
                      .HasForeignKey(sto => sto.Staff_ID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(sto => sto.RefTimeOffReason)
                      .WithMany()
                      .HasForeignKey(sto => sto.ReasonCode)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasKey(e => e.Shift_ID);
                entity.Property(e => e.ShiftName).IsRequired();
                entity.Property(e => e.DayOrNight).IsRequired();
            });

            modelBuilder.Entity<RosterOfStaffOnShift>(entity =>
            {
                entity.HasKey(e => e.Roster_ID);

                entity.HasOne(r => r.Staff)
                      .WithMany()
                      .HasForeignKey(r => r.Staff_ID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Shift)
                      .WithMany()
                      .HasForeignKey(r => r.Shift_ID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => e.Ward_ID);
                entity.Property(e => e.WardName).IsRequired();
            });

            modelBuilder.Entity<StaffWardAssignment>(entity =>
            {
                entity.HasKey(e => e.Assignment_ID);

                entity.HasOne(swa => swa.Staff)
                      .WithMany()
                      .HasForeignKey(swa => swa.Staff_ID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(swa => swa.Ward)
                      .WithMany()
                      .HasForeignKey(swa => swa.Ward_ID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StaffPay>(entity =>
            {
                entity.HasKey(e => e.Pay_ID);

                entity.HasOne(sp => sp.Staff)
                      .WithMany()
                      .HasForeignKey(sp => sp.Staff_ID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.GrossPay).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Deductions).HasColumnType("decimal(18,2)");
                entity.Property(e => e.NetPay).HasColumnType("decimal(18,2)");
            });
        }


        public static void Seed(HospitalManagementDbContext context)
        {
            // Заповнюємо Address
            if (!context.Addresses.Any())
            {
                context.Addresses.AddRange(
                    new Address
                    {
                        Line1 = "123 Main St",
                        Line2 = "Apt 4B",
                        Area = "Downtown",
                        City = "Kyiv",
                        ZipCode = "01001",
                        StateProvince = "Kyiv Oblast",
                        Country = "Ukraine",
                        OtherAddressDetails = "Near the park"
                    },
                    new Address
                    {
                        Line1 = "456 Elm St",
                        Line2 = "Suite 22",
                        Area = "Suburb",
                        City = "Lviv",
                        ZipCode = "79000",
                        StateProvince = "Lviv Oblast",
                        Country = "Ukraine",
                        OtherAddressDetails = "Next to the school"
                    }
                );
                context.SaveChanges();
            }

            // Заповнюємо Hospital
            if (!context.Hospitals.Any())
            {
                context.Hospitals.AddRange(
                    new Hospital
                    {
                        Address_ID = context.Addresses.OrderBy(a => a.Address_ID).First().Address_ID,
                        HospitalName = "Central Hospital",
                        OtherDetails = "24/7 Emergency Services"
                    },
                    new Hospital
                    {
                        Address_ID = context.Addresses.OrderBy(a => a.Address_ID).Last().Address_ID,
                        HospitalName = "City Clinic",
                        OtherDetails = "Specialized in Cardiology and Pediatrics"
                    }
                );
                context.SaveChanges();
            }

            // Заповнюємо Staff
            if (!context.Staff.Any())
            {
                var hospitalId1 = context.Hospitals.OrderBy(h => h.Hospital_ID).First().Hospital_ID;
                var hospitalId2 = context.Hospitals.OrderBy(h => h.Hospital_ID).Last().Hospital_ID;

                context.Staff.AddRange(
                    new Staff
                    {
                        Address_ID = context.Addresses.OrderBy(a => a.Address_ID).First().Address_ID,
                        Hospital_ID = hospitalId1,
                        FirstName = "Ivan",
                        MiddleName = "Oleksandrovych",
                        LastName = "Petrenko",
                        BirthDate = new DateTime(1985, 4, 12),
                        Gender = "Male",
                        JobTitle = "Doctor",
                        DateJoinedHospital = DateTime.UtcNow.AddYears(-5),
                        DateLeftHospital = null,
                        Qualifications = "MD, PhD",
                        OtherDetails = "Specialist in Internal Medicine"
                    },
                    new Staff
                    {
                        Address_ID = context.Addresses.OrderBy(a => a.Address_ID).Last().Address_ID,
                        Hospital_ID = hospitalId2,
                        FirstName = "Anna",
                        MiddleName = "Mykolaivna",
                        LastName = "Shevchenko",
                        BirthDate = new DateTime(1990, 7, 22),
                        Gender = "Female",
                        JobTitle = "Nurse",
                        DateJoinedHospital = DateTime.UtcNow.AddYears(-3),
                        DateLeftHospital = null,
                        Qualifications = "Certified Nurse",
                        OtherDetails = "Specialist in ICU"
                    }
                );
                context.SaveChanges();
            }

            // Заповнюємо RefTimeOffReason
            if (!context.RefTimeOffReasons.Any())
            {
                context.RefTimeOffReasons.AddRange(
                    new RefTimeOffReason { ReasonCode = "VAC", ReasonDescription = "Vacation Leave" },
                    new RefTimeOffReason { ReasonCode = "SICK", ReasonDescription = "Sick Leave" },
                    new RefTimeOffReason { ReasonCode = "PER", ReasonDescription = "Personal Leave" }
                );
                context.SaveChanges();
            }

            // Заповнюємо StaffTimeOff
            if (!context.StaffTimeOffs.Any())
            {
                var staffId = context.Staff.OrderBy(s => s.Staff_ID).First().Staff_ID;

                context.StaffTimeOffs.AddRange(
                    new StaffTimeOff
                    {
                        Staff_ID = staffId,
                        ReasonCode = "VAC",
                        DateFrom = DateTime.UtcNow.AddDays(-30),
                        DateTo = DateTime.UtcNow.AddDays(-25)
                    },
                    new StaffTimeOff
                    {
                        Staff_ID = staffId,
                        ReasonCode = "SICK",
                        DateFrom = DateTime.UtcNow.AddDays(-10),
                        DateTo = DateTime.UtcNow.AddDays(-5)
                    }
                );
                context.SaveChanges();
            }

            // Заповнюємо Shift
            if (!context.Shifts.Any())
            {
                context.Shifts.AddRange(
                    new Shift
                    {
                        DayOrNight = "Day",
                        ShiftName = "Morning Shift",
                        ShiftDescription = "Regular shift",
                        StartTime = new DateTime(1, 1, 1, 8, 0, 0),
                        EndTime = new DateTime(1, 1, 1, 16, 0, 0),
                        OtherShiftDetails = "Includes lunch break"
                    },
                    new Shift
                    {
                        DayOrNight = "Night",
                        ShiftName = "Night Shift",
                        ShiftDescription = "Overnight shift",
                        StartTime = new DateTime(1, 1, 1, 22, 0, 0),
                        EndTime = new DateTime(1, 1, 2, 6, 0, 0),
                        OtherShiftDetails = "Additional night allowance"
                    }
                );
                context.SaveChanges();
            }

            // Заповнюємо RosterOfStaffOnShift
            if (!context.RosterOfStaffOnShifts.Any())
            {
                var staffId = context.Staff.OrderBy(s => s.Staff_ID).First().Staff_ID;
                var shiftId = context.Shifts.OrderBy(s => s.Shift_ID).First().Shift_ID;

                context.RosterOfStaffOnShifts.Add(
                    new RosterOfStaffOnShift
                    {
                        Staff_ID = staffId,
                        Shift_ID = shiftId,
                        StartDate = DateTime.UtcNow.AddDays(-7),
                        EndDate = DateTime.UtcNow.AddDays(-5)
                    }
                );
                context.SaveChanges();
            }

            // Заповнюємо Ward
            if (!context.Wards.Any())
            {
                context.Wards.AddRange(
                    new Ward
                    {
                        WardName = "ICU",
                        WardLocation = "Building A, Floor 2",
                        WardDescription = "Intensive Care Unit"
                    },
                    new Ward
                    {
                        WardName = "Pediatrics",
                        WardLocation = "Building B, Floor 1",
                        WardDescription = "Childcare ward"
                    }
                );
                context.SaveChanges();
            }

            // Заповнюємо StaffWardAssignment
            if (!context.StaffWardAssignments.Any())
            {
                var staffId = context.Staff.OrderBy(s => s.Staff_ID).First().Staff_ID;
                var wardId = context.Wards.OrderBy(w => w.Ward_ID).First().Ward_ID;

                context.StaffWardAssignments.Add(
                    new StaffWardAssignment
                    {
                        Staff_ID = staffId,
                        Ward_ID = wardId,
                        DateFrom = DateTime.UtcNow.AddDays(-30),
                        DateTo = DateTime.UtcNow.AddDays(-15)
                    }
                );
                context.SaveChanges();
            }

            // Заповнюємо StaffPay
            if (!context.StaffPay.Any())
            {
                var staffId = context.Staff.OrderBy(s => s.Staff_ID).First().Staff_ID;

                context.StaffPay.AddRange(
                    new StaffPay
                    {
                        Staff_ID = staffId,
                        PaymentDate = DateTime.UtcNow.AddMonths(-1),
                        GrossPay = 5000m,
                        Deductions = 1000m,
                        NetPay = 4000m
                    },
                    new StaffPay
                    {
                        Staff_ID = staffId,
                        PaymentDate = DateTime.UtcNow,
                        GrossPay = 5500m,
                        Deductions = 1200m,
                        NetPay = 4300m
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
