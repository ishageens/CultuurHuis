using System;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Microsoft.Extensions.Configuration;
using System.IO;

#nullable disable

namespace Model.Repositories
{
    public partial class CultuurHuisMVCContext : DbContext
    {
        public static IConfigurationRoot configuration;
        public CultuurHuisMVCContext()
        {
        }

        public CultuurHuisMVCContext(DbContextOptions<CultuurHuisMVCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Klant> Klanten { get; set; }
        public virtual DbSet<Reservatie> Reservaties { get; set; }
        public virtual DbSet<Voorstelling> Voorstellingen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Zoek de naam in de connectionStrings section - appsettings.json
            configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json", false)
            .Build();
            var connectionString = configuration.GetConnectionString("cultuurhuis");
            if (connectionString != null) // Indien de naam is gevonden
            {
                optionsBuilder.UseSqlServer(connectionString, options => options.MaxBatchSize(150)); // Max aantal SQL commands die kunnen doorgestuurd worden
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.GenreNr);

                entity.Property(e => e.GenreNr).ValueGeneratedNever();

                entity.Property(e => e.GenreNaam)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Genre");
            });

            modelBuilder.Entity<Klant>(entity =>
            {
                entity.HasKey(e => e.KlantNr);

                entity.ToTable("Klanten");

                entity.Property(e => e.Familienaam)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GebruikersNaam)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gemeente)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HuisNr)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Paswoord)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Straat)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Voornaam)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Reservatie>(entity =>
            {
                entity.HasKey(e => e.ReservatieNr);

                entity.HasOne(d => d.KlantNrNavigation)
                    .WithMany(p => p.Reservaties)
                    .HasForeignKey(d => d.KlantNr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservaties_Klanten");

                entity.HasOne(d => d.VoorstellingsNrNavigation)
                    .WithMany(p => p.Reservaties)
                    .HasForeignKey(d => d.VoorstellingsNr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservaties_Voorstellingen");
            });

            modelBuilder.Entity<Voorstelling>(entity =>
            {
                entity.HasKey(e => e.VoorstellingsNr);

                entity.ToTable("Voorstellingen");

                entity.Property(e => e.VoorstellingsNr).ValueGeneratedNever();

                entity.Property(e => e.Datum).HasColumnType("datetime");

                entity.Property(e => e.Prijs).HasColumnType("money");

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Uitvoerders)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.GenreNrNavigation)
                    .WithMany(p => p.Voorstellingen)
                    .HasForeignKey(d => d.GenreNr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voorstellingen_Genres");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
