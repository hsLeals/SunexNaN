using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Sunex.Models
{
    public partial class CargoContext : DbContext
    {
        public CargoContext()
        {
        }

        public CargoContext(DbContextOptions<CargoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branchs { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NF2OE20\\SQLEXPRESS;Database=Cargo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.BranchAddress).HasMaxLength(300);

                entity.Property(e => e.BranchName).HasMaxLength(50);
            });

            modelBuilder.Entity<Connection>(entity =>
            {
                entity.ToTable("connection");

                entity.Property(e => e.ConnectionDescription).HasMaxLength(300);

                entity.Property(e => e.ConnectionEmail).HasMaxLength(50);

                entity.Property(e => e.ConnectionTitle).HasMaxLength(50);

                entity.Property(e => e.ConnectionUser).HasMaxLength(50);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contacts");

                entity.Property(e => e.ContactId).HasColumnName("contactId");

                entity.Property(e => e.ContactDescription)
                    .HasMaxLength(400)
                    .HasColumnName("contactDescription");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(20)
                    .HasColumnName("contactName");

                entity.Property(e => e.ContactSurname)
                    .HasMaxLength(20)
                    .HasColumnName("contactSurname");

                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(100)
                    .HasColumnName("contactTitle");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.Property(e => e.LevelName).HasMaxLength(100);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NotificationDescription).HasMaxLength(400);

                entity.Property(e => e.NotificationTitle).HasMaxLength(100);

                entity.HasOne(d => d.NotificationClient)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationClientId)
                    .HasConstraintName("FK__Notificat__Notif__4CA06362");

                entity.HasOne(d => d.NotificationOrder)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationOrderId)
                    .HasConstraintName("FK__Notificat__Notif__4BAC3F29");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.OrderBranch)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderBranchId)
                    .HasConstraintName("FK__Orders__OrderBra__45F365D3");

                entity.HasOne(d => d.OrderClient)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderClientId)
                    .HasConstraintName("FK__Orders__OrderCli__4316F928");

                entity.HasOne(d => d.OrderLevel)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderLevelId)
                    .HasConstraintName("FK__Orders__OrderLev__44FF419A");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.HasOne(d => d.ProductOrder)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductOrderId)
                    .HasConstraintName("FK__Products__Produc__6E01572D");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("shops");

                entity.Property(e => e.Shopid).HasColumnName("shopid");

                entity.Property(e => e.ShopLogo)
                    .IsUnicode(false)
                    .HasColumnName("shopLogo");

                entity.Property(e => e.ShopName)
                    .HasMaxLength(20)
                    .HasColumnName("shopName");

                entity.Property(e => e.ShopUrl)
                    .IsUnicode(false)
                    .HasColumnName("shopUrl");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("statuses");

                entity.Property(e => e.Statusid).HasColumnName("statusid");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(20)
                    .HasColumnName("statusName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.UserBranchId).HasColumnName("userBranchId");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(80)
                    .HasColumnName("userEmail");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .HasColumnName("userName");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(20)
                    .HasColumnName("userPassword");

                entity.Property(e => e.UserPhone).HasColumnName("userPhone");

                entity.Property(e => e.UserStatusId).HasColumnName("userStatusId");

                entity.Property(e => e.UserSurname)
                    .HasMaxLength(20)
                    .HasColumnName("userSurname");

                entity.HasOne(d => d.UserBranch)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserBranchId)
                    .HasConstraintName("FK__users__userBranc__3C69FB99");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatusId)
                    .HasConstraintName("FK__users__userStatu__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
