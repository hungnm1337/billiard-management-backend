using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Models;

public partial class Prn232ProjectContext : DbContext
{
    public Prn232ProjectContext()
    {
    }

    public Prn232ProjectContext(DbContextOptions<Prn232ProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<RewardPoint> RewardPoints { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__account__46A222CD87F2F432");

            entity.ToTable("account");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Avt)
                .IsUnicode(false)
                .HasColumnName("avt");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__invoice__F58DFD49F088EC21");

            entity.ToTable("invoice");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("payment_status");
            entity.Property(e => e.TimeEnd)
                .HasColumnType("datetime")
                .HasColumnName("time_end");
            entity.Property(e => e.TimeStart)
                .HasColumnType("datetime")
                .HasColumnName("time_start");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invoice_employee");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.ServiceTableId).HasName("PK__invoice___617753E11272C5D4");

            entity.ToTable("invoice_detail");

            entity.Property(e => e.ServiceTableId).HasColumnName("service_table_id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invoice_detail_invoice");

            entity.HasOne(d => d.Service).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invoice_detail_service");

            entity.HasOne(d => d.Table).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invoice_detail_table");
        });

        modelBuilder.Entity<RewardPoint>(entity =>
        {
            entity.HasKey(e => e.RewardPointsId).HasName("PK__reward_p__1240C568FBC32ADA");

            entity.ToTable("reward_points");

            entity.Property(e => e.RewardPointsId).HasColumnName("reward_points_id");
            entity.Property(e => e.Points).HasColumnName("points");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.RewardPoints)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reward_points_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__role__760965CCF23A09E4");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__service__3E0DB8AF780287AB");

            entity.ToTable("service");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("service_name");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("PK__table__B21E8F2440BACD5E");

            entity.ToTable("table");

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.HourlyRate)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("hourly_rate");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TableName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("table_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370FCB127F4F");

            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("salary");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Account).WithMany(p => p.Users)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_account");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
