using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        { }

        public DbSet<Users> Users { get; set; }
        public DbSet<PushNotifications> PushNotifications { get; set; }
        public DbSet<UserNotifications> UserNotifications { get; set; }
        public DbSet<Records> Records { get; set; }
        public DbSet<UserRecords> UserRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* ========== Users =========== */
            modelBuilder.Entity<Users>()
                .HasMany(u => u.PushNotifications)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(u => u.UserNotifications)
                .WithOne(un => un.User)
                .HasForeignKey(un => un.UserId);

            modelBuilder.Entity<Users>()
                .HasMany(u => u.UserRecords)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Users>()
                .Property(u => u.CreatedDate)
                .HasColumnType("datetime2");
            /* ========== End Users =========== */

            /* ========== PushNotifications =========== */
            modelBuilder.Entity<PushNotifications>()
                .HasMany(p => p.UserNotifications)
                .WithOne(un => un.PushNotification)
                .HasForeignKey(un => un.PushId);

            modelBuilder.Entity<PushNotifications>()
                .Property(pN => pN.CreatedDate)
                .HasColumnType("datetime2");

            modelBuilder.Entity<PushNotifications>()
                .Property(pN => pN.UpdatedDate)
                .HasColumnType("datetime2");
            /* ========== End PushNotifications =========== */

            /* ========== UserNotifications =========== */
            modelBuilder.Entity<UserNotifications>()
                .Property(uN => uN.CreatedDate)
                .HasColumnType("datetime2");

            modelBuilder.Entity<UserNotifications>()
                .Property(uN => uN.UpdatedDate)
                .HasColumnType("datetime2");
            /* ========== End UserNotifications =========== */

            /* ========== Records =========== */
            modelBuilder.Entity<Records>()
                .HasMany(r => r.UserRecords)
                .WithOne(ur => ur.Record)
                .HasForeignKey(ur => ur.RecordId);

            modelBuilder.Entity<Records>()
                .Property(r => r.CreatedDate)
                .HasColumnType("datetime2");
            /* ========== End Records =========== */

            /* ========== UserRecords =========== */
            modelBuilder.Entity<UserRecords>()
                .HasKey(ur => new { ur.UserId, ur.RecordId });
            /* ========== End UserRecords =========== */
        }
    }
}
