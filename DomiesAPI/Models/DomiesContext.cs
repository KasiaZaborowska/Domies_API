using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DomiesAPI.Models;

public partial class DomiesContext : DbContext
{
    public DomiesContext()
    {
    }

    public DomiesContext(DbContextOptions<DomiesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<AnimalType> AnimalTypes { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<OfferAnimalType> OfferAnimalTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-TLIAKDF\\SQLEXPRESS;Database=Domies;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Addresse__3213E83FF9B41D90");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Animals__3213E83F4D203094");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnimalType).HasColumnName("animal_type");
            entity.Property(e => e.Owner)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("owner");
            entity.Property(e => e.PetName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("pet_name");
            entity.Property(e => e.SpecificDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("specific_description");

            entity.HasOne(d => d.AnimalTypeNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.AnimalType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Animals__animal___4E88ABD4");

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Animals__owner__4D94879B");
        });

        modelBuilder.Entity<AnimalType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AnimalTy__3213E83FB9FC04F0");

            entity.ToTable("AnimalType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("animal_type");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Offers__3213E83FA0EF4E10");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.DateAdd)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_add");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Host)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("host");
            entity.Property(e => e.Photo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("photo");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Address).WithMany(p => p.Offers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Offers__address___47DBAE45");

            entity.HasOne(d => d.HostNavigation).WithMany(p => p.Offers)
                .HasForeignKey(d => d.Host)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Offers__date_add__46E78A0C");
        });

        modelBuilder.Entity<OfferAnimalType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OfferAnimalType");

            entity.Property(e => e.AnimalTypeId).HasColumnName("animal_type_id");
            entity.Property(e => e.OfferId).HasColumnName("offer_id");

            entity.HasOne(d => d.AnimalType).WithMany()
                .HasForeignKey(d => d.AnimalTypeId)
                .HasConstraintName("FK__OfferAnim__anima__4AB81AF0");

            entity.HasOne(d => d.Offer).WithMany()
                .HasForeignKey(d => d.OfferId)
                .HasConstraintName("FK__OfferAnim__offer__49C3F6B7");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CCEFD7DF48");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Users__AB6E616591478EA9");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.DateAdd)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_add");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__role_id__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
