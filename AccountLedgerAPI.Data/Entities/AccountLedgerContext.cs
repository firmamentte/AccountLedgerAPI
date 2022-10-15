using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AccountLedgerAPI.Data.Entities
{
    public partial class AccountLedgerContext : DbContext
    {
        public AccountLedgerContext()
        {
        }

        public AccountLedgerContext(DbContextOptions<AccountLedgerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public virtual DbSet<ApplicationUserAccount> ApplicationUserAccounts { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<PersonalDetail> PersonalDetails { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<TransactionType> TransactionTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(StaticClass.DatabaseHelper.ConnectionString);
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("ApplicationUser");

                entity.Property(e => e.ApplicationUserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccessToken)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AccessTokenExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.ApplicationUserCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PersonalDetail)
                    .WithMany(p => p.ApplicationUsers)
                    .HasForeignKey(d => d.PersonalDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUser_PersonalDetail");
            });

            modelBuilder.Entity<ApplicationUserAccount>(entity =>
            {
                entity.ToTable("ApplicationUserAccount");

                entity.Property(e => e.ApplicationUserAccountId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountBalance).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.ApplicationUserAccounts)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserAccount_ApplicationUser");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.ContactId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CityOrTown)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateOrProvince)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SuburbOrTownship)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PersonalDetail>(entity =>
            {
                entity.ToTable("PersonalDetail");

                entity.Property(e => e.PersonalDetailId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.PersonalDetails)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonalDetail_Contact");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.TransactionId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TransactionAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.ApplicationUserAccount)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.ApplicationUserAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_ApplicationUserAccount");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_TransactionType");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("TransactionType");

                entity.Property(e => e.TransactionTypeId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TransactionTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
