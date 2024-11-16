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
    }
}
