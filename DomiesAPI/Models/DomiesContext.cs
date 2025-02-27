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

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Opinion> Opinions { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<OfferFacility> OfferFacilities { get; set; }



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

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3213E83F1905EB2F");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApplicationDateAdd)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("application_date_add");
            entity.Property(e => e.ApplicationStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                 .HasDefaultValue("Oczekująca")
                .HasColumnName("application_status");
            entity.Property(e => e.DateEnd)
                .HasColumnType("datetime")
                .HasColumnName("date_end");
            entity.Property(e => e.DateStart)
                .HasColumnType("datetime")
                .HasColumnName("date_start");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.OfferId).HasColumnName("offer_id");
            entity.Property(e => e.Applicant)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("applicant");

            entity.HasOne(d => d.Offer).WithMany(p => p.Applications)
                .HasForeignKey(d => d.OfferId)
                .HasConstraintName("FK__Applicati__offer__2A164134");

            entity.HasOne(d => d.ApplicantNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.Applicant)
                .HasConstraintName("FK__Applicati__applicant__2B0A656D");

            entity.HasMany(d => d.Animals).WithMany(p => p.Applications)
                .UsingEntity<Dictionary<string, object>>(
                    "ApplicationsAnimal",
                    r => r.HasOne<Animal>().WithMany()
                        .HasForeignKey("AnimalId")
                        .HasConstraintName("FK__Applicati__anima__51300E55"),
                    l => l.HasOne<Application>().WithMany()
                        .HasForeignKey("ApplicationId")
                        .HasConstraintName("FK__Applicati__appli__503BEA1C"),
                    j =>
                    {
                        j.HasKey("ApplicationId", "AnimalId").HasName("PK__Applicat__062D5C0BE76D7449");
                        j.ToTable("ApplicationsAnimals");
                        j.IndexerProperty<int>("ApplicationId").HasColumnName("application_id");
                        j.IndexerProperty<int>("AnimalId").HasColumnName("animal_id");
                    });
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Faciliti__3213E83FA72E8885");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FacilitiesDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("facilities_description");
            entity.Property(e => e.FacilitiesType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("facilities_type");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Offers__3213E83FA0EF4E10");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.DateAdd)
                .HasDefaultValueSql("CONVERT(date, GETDATE())")
                .HasColumnType("date")
                .HasColumnName("date_add");
            entity.Property(e => e.OfferDescription)
                .IsUnicode(false)
                .HasColumnName("offer_description");
            entity.Property(e => e.PetSitterDescription)
                .IsUnicode(false)
                .HasColumnName("petSitterDescription");
            entity.Property(e => e.Host)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("host");
            entity.Property(e => e.PhotoId).HasColumnName("photo_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.Address).WithMany(p => p.Offers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Offers__address___47DBAE45");

            entity.HasOne(d => d.HostNavigation).WithMany(p => p.Offers)
                .HasForeignKey(d => d.Host)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Offers__date_add__46E78A0C");

            entity.HasOne(d => d.Photo).WithMany(p => p.Offers)
                .HasForeignKey(d => d.PhotoId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Offers_Photo");

             entity.HasMany(d => d.Facilities).WithMany(p => p.Offers)
                .UsingEntity<Dictionary<string, object>>(
                    "OfferFacility",
                    r => r.HasOne<Facility>().WithMany()
                        .HasForeignKey("FacilitieId")
                        .HasConstraintName("FK__OfferFaci__facil__09746778"),
                    l => l.HasOne<Offer>().WithMany()
                        .HasForeignKey("OfferId")
                        .HasConstraintName("FK__OfferFaci__offer__0880433F"),
                    j =>
                    {
                        j.HasKey("OfferId", "FacilitieId").HasName("PK__OfferFac__5A847DB762566F5A");
                        j.ToTable("OfferFacilities");
                        j.IndexerProperty<int>("OfferId").HasColumnName("offer_id");
                        j.IndexerProperty<int>("FacilitieId").HasColumnName("facilitie_id");
                    });


        });

        modelBuilder.Entity<OfferFacility>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("OfferFacility");

            entity.Property(e => e.FacilitieId).HasColumnName("facilitie_id");
            entity.Property(e => e.OfferId).HasColumnName("offer_id");

            entity.HasOne(d => d.Facilitie).WithMany()
                .HasForeignKey(d => d.FacilitieId)
                .HasConstraintName("FK__OfferFaci__facil__793DFFAF");

            entity.HasOne(d => d.Offer).WithMany()
                .HasForeignKey(d => d.OfferId)
                .HasConstraintName("FK__OfferFaci__offer__7849DB76");
        });

        modelBuilder.Entity<OfferAnimalType>(entity =>
        {
            entity.HasKey(e => new { e.OfferId, e.AnimalTypeId });

            entity.ToTable("OfferAnimalType");

            entity.Property(e => e.AnimalTypeId).HasColumnName("animal_type_id");
            entity.Property(e => e.OfferId).HasColumnName("offer_id");

            entity.HasOne(d => d.AnimalType).WithMany(p => p.OfferAnimalTypes)
                .HasForeignKey(d => d.AnimalTypeId)
                .HasConstraintName("FK__OfferAnim__anima__06CD04F7");

            entity.HasOne(d => d.Offer).WithMany(p => p.OfferAnimalTypes)
                .HasForeignKey(d => d.OfferId)
                .HasConstraintName("FK__OfferAnim__offer__05D8E0BE");
        });

        modelBuilder.Entity<Opinion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Opinions__3213E83F1A4255FB");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OpinionDateAdd)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("opinion_date_add");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_email");

            entity.HasOne(d => d.Application).WithMany(p => p.Opinions)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Opinions__applic__2EDAF651");

            entity.HasOne(d => d.UserEmailNavigation).WithMany(p => p.Opinions)
                .HasForeignKey(d => d.UserEmail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Opinions__user_e__2FCF1A8A");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Photo__3213E83FFD9BBC63");

            entity.ToTable("Photo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BinaryData).HasColumnName("binary_data");
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .HasColumnName("extension");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
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
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("phone_number");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.IsEmailVerified)
                .IsUnicode(false)
                .HasColumnName("email_verified");
            entity.Property(e => e.EmailVerificationToken)
                .IsUnicode(false)
                .HasColumnName("email_verification_token");

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
