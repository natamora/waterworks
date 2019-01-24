using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Waterworks.Models.Db.Waterworks;
using Waterworks.Models.View.Client;
using Waterworks.Models.View.PriceList;
using Waterworks.Models.View.Object;
using Waterworks.Models.View.WaterMeter;
using Waterworks.Models.View.Contract;
namespace Waterworks.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<AdresKorespondencyjny> AdresKorespondencyjny { get; set; }
        public virtual DbSet<AdresObiektu> AdresObiektu { get; set; }
        public virtual DbSet<AktywnyOdbiorca> AktywnyOdbiorca { get; set; }
        public virtual DbSet<CennikScieki> CennikScieki { get; set; }
        public virtual DbSet<CennikWoda> CennikWoda { get; set; }
        public virtual DbSet<DaneKlienta> DaneKlienta { get; set; }
        public virtual DbSet<IdentyfikatorPodatkowy> IdentyfikatorPodatkowy { get; set; }
        public virtual DbSet<Klasyfikatory> Klasyfikatory { get; set; }
        public virtual DbSet<KlasyfikatoryObiektu> KlasyfikatoryObiektu { get; set; }
        public virtual DbSet<Klient> Klient { get; set; }
        public virtual DbSet<NormyZuzyciaWody> NormyZuzyciaWody { get; set; }
        public virtual DbSet<Obiekt> Obiekt { get; set; }
        public virtual DbSet<OplatyAbonamentoweScieki> OplatyAbonamentoweScieki { get; set; }
        public virtual DbSet<OplatyAbonamentoweWoda> OplatyAbonamentoweWoda { get; set; }
        public virtual DbSet<RodzajKlienta> RodzajKlienta { get; set; }
        public virtual DbSet<WartosciKlasyfikatorow> WartosciKlasyfikatorow { get; set; }
        public virtual DbSet<FormulyZmniejszajace> FormulyZmniejszajace { get; set; }
        public virtual DbSet<Wodomierz> Wodomierz { get; set; }

        public virtual DbSet<TypUmowy> TypUmowy { get; set; }
        public virtual DbSet<TytulPrawny> TytulPrawny { get; set; }
        public virtual DbSet<GranicaOdpowiedzialnosci> GranicaOdpowiedzialnosci { get; set; }
        public virtual DbSet<Umowa> Umowa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("pg_buffercache")
                .HasPostgresExtension("pg_stat_statements")
                .HasPostgresExtension("postgis");

            modelBuilder.Entity<AdresKorespondencyjny>(entity =>
            {
                entity.ToTable("adres_korespondencyjny");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.KodPocztowy)
                    .HasColumnName("kod_pocztowy")
                    .HasMaxLength(6);

                entity.Property(e => e.Miejscowosc)
                    .HasColumnName("miejscowosc")
                    .HasMaxLength(40);

                entity.Property(e => e.NrDomu)
                    .HasColumnName("nr_domu")
                    .HasMaxLength(5);

                entity.Property(e => e.NrLokalu)
                    .HasColumnName("nr_lokalu")
                    .HasMaxLength(5);

                entity.Property(e => e.Poczta)
                    .HasColumnName("poczta")
                    .HasMaxLength(20);

                entity.Property(e => e.Ulica)
                    .HasColumnName("ulica")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<AdresObiektu>(entity =>
            {
                entity.ToTable("adres_obiektu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.KodPocztowy)
                    .IsRequired()
                    .HasColumnName("kod_pocztowy")
                    .HasMaxLength(6);

                entity.Property(e => e.Miejscowosc)
                    .IsRequired()
                    .HasColumnName("miejscowosc")
                    .HasMaxLength(40);

                entity.Property(e => e.NrDomu)
                    .IsRequired()
                    .HasColumnName("nr_domu")
                    .HasMaxLength(5);

                entity.Property(e => e.NrDzialki)
                    .IsRequired()
                    .HasColumnName("nr_dzialki")
                    .HasMaxLength(30);

                entity.Property(e => e.NrLokalu)
                    .HasColumnName("nr_lokalu")
                    .HasMaxLength(5);

                entity.Property(e => e.ObiektId).HasColumnName("obiekt_id");

                entity.Property(e => e.Poczta)
                    .HasColumnName("poczta")
                    .HasMaxLength(20);

                entity.Property(e => e.Ulica)
                    .IsRequired()
                    .HasColumnName("ulica")
                    .HasMaxLength(40);

                entity.HasOne(d => d.Obiekt)
                    .WithMany(p => p.AdresObiektu)
                    .HasForeignKey(d => d.ObiektId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("adres_obiektu_obiekt");
            });

            modelBuilder.Entity<FormulyZmniejszajace>(entity =>
            {
                entity.ToTable("formuly_zmniejszajace");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ObiektId).HasColumnName("obiekt_id");

                entity.Property(e => e.WodomierzId).HasColumnName("wodomierz_id");

                entity.HasOne(d => d.Obiekt)
                   .WithMany(p => p.FormulyZmniejszajace)
                   .HasForeignKey(d => d.ObiektId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("formuly_zmniejszajace_obiekt");


                entity.HasOne(d => d.Wodomierz)
                    .WithMany(p => p.FormulyZmniejszajace)
                    .HasForeignKey(d => d.WodomierzId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("formuly_zmniejszajace_wodomierz");
            });

            modelBuilder.Entity<Wodomierz>(entity =>
            {
                entity.ToTable("wodomierz");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DataEwidencji)
                    .HasColumnName("data_ewidencji")
                    .HasColumnType("date");

                entity.Property(e => e.DataLegalizacji)
                    .HasColumnName("data_legalizacji")
                    .HasColumnType("date");

                entity.Property(e => e.NrWodomierza).HasColumnName("nr_wodomierza");

                entity.Property(e => e.ObiektId).HasColumnName("obiekt_id");

                entity.Property(e => e.TypWodomierza)
                    .IsRequired()
                    .HasColumnName("typ_wodomierza")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Obiekt)
                    .WithMany(p => p.Wodomierz)
                    .HasForeignKey(d => d.ObiektId)
                    .HasConstraintName("wodomierz_obiekt");
            });

            modelBuilder.Entity<AktywnyOdbiorca>(entity =>
            {
                entity.ToTable("aktywny_odbiorca");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(20);

                entity.Property(e => e.Email2)
                    .HasColumnName("email_2")
                    .HasMaxLength(20);

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasColumnName("imie")
                    .HasMaxLength(40);

                entity.Property(e => e.KlientIdKlienta).HasColumnName("klient_id_klienta");

                entity.Property(e => e.KodPocztowy)
                    .IsRequired()
                    .HasColumnName("kod_pocztowy")
                    .HasMaxLength(6);

                entity.Property(e => e.Miejscowosc)
                    .IsRequired()
                    .HasColumnName("miejscowosc")
                    .HasMaxLength(40);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasColumnName("nazwisko")
                    .HasMaxLength(40);

                entity.Property(e => e.NrDomu)
                    .IsRequired()
                    .HasColumnName("nr_domu")
                    .HasMaxLength(5);

                entity.Property(e => e.NrLokalu)
                    .HasColumnName("nr_lokalu")
                    .HasMaxLength(5);

                entity.Property(e => e.Poczta)
                    .IsRequired()
                    .HasColumnName("poczta")
                    .HasMaxLength(20);

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasColumnName("telefon")
                    .HasMaxLength(15);

                entity.Property(e => e.Telefon2)
                    .HasColumnName("telefon_2")
                    .HasMaxLength(15);

                entity.Property(e => e.Ulica)
                    .IsRequired()
                    .HasColumnName("ulica")
                    .HasMaxLength(40);

                entity.HasOne(d => d.KlientIdKlientaNavigation)
                    .WithMany(p => p.AktywnyOdbiorca)
                    .HasForeignKey(d => d.KlientIdKlienta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("aktywny_odbiorca_klient");
            });

            modelBuilder.Entity<CennikScieki>(entity =>
            {
                entity.ToTable("cennik_scieki");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CenaZaM3).HasColumnName("cena_za_m3");

                entity.Property(e => e.NazwaCennika)
                    .IsRequired()
                    .HasColumnName("nazwa_cennika")
                    .HasMaxLength(40);

                entity.Property(e => e.RodzajKlientaId).HasColumnName("rodzaj_klienta_id");

                entity.Property(e => e.TypCennika)
                    .IsRequired()
                    .HasColumnName("typ_cennika")
                    .HasMaxLength(30);

                entity.HasOne(d => d.RodzajKlienta)
                    .WithMany(p => p.CennikScieki)
                    .HasForeignKey(d => d.RodzajKlientaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cennik_scieki_rodzaj_klienta");
            });

            modelBuilder.Entity<CennikWoda>(entity =>
            {
                entity.ToTable("cennik_woda");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CenaZaM3).HasColumnName("cena_za_m3");

                entity.Property(e => e.NazwaCennika)
                    .IsRequired()
                    .HasColumnName("nazwa_cennika")
                    .HasMaxLength(40);

                entity.Property(e => e.RodzajKlientaId).HasColumnName("rodzaj_klienta_id");

                entity.Property(e => e.TypCennika)
                    .IsRequired()
                    .HasColumnName("typ_cennika")
                    .HasMaxLength(40);

                entity.HasOne(d => d.RodzajKlienta)
                    .WithMany(p => p.CennikWoda)
                    .HasForeignKey(d => d.RodzajKlientaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cennik_woda_rodzaj_klienta");
            });

            modelBuilder.Entity<DaneKlienta>(entity =>
            {
                entity.ToTable("dane_klienta");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdresKorespondencyjnyId).HasColumnName("adres_korespondencyjny_id");

                entity.Property(e => e.NazwaFirmy)
                    .HasColumnName("nazwa_firmy")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(20);

                entity.Property(e => e.Email2)
                    .HasColumnName("email_2")
                    .HasMaxLength(20);

                entity.Property(e => e.IdKlienta).HasColumnName("id_klienta");

                entity.Property(e => e.Imie)
                    .IsRequired()
                    .HasColumnName("imie")
                    .HasMaxLength(40);

                entity.Property(e => e.KodPocztowy)
                    .IsRequired()
                    .HasColumnName("kod_pocztowy")
                    .HasMaxLength(6);

                entity.Property(e => e.Miejscowosc)
                    .IsRequired()
                    .HasColumnName("miejscowosc")
                    .HasMaxLength(40);

                entity.Property(e => e.Nazwisko)
                    .IsRequired()
                    .HasColumnName("nazwisko")
                    .HasMaxLength(40);

                entity.Property(e => e.NrDomu)
                    .IsRequired()
                    .HasColumnName("nr_domu")
                    .HasMaxLength(5);

                entity.Property(e => e.NrLokalu)
                    .HasColumnName("nr_lokalu")
                    .HasMaxLength(5);

                entity.Property(e => e.Poczta)
                    .IsRequired()
                    .HasColumnName("poczta")
                    .HasMaxLength(20);

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasColumnName("telefon")
                    .HasMaxLength(15);

                entity.Property(e => e.Telefon2)
                    .HasColumnName("telefon_2")
                    .HasMaxLength(15);

                entity.Property(e => e.Ulica)
                    .IsRequired()
                    .HasColumnName("ulica")
                    .HasMaxLength(40);

                entity.HasOne(d => d.AdresKorespondencyjny)
                    .WithMany(p => p.DaneKlienta)
                    .HasForeignKey(d => d.AdresKorespondencyjnyId)
                    .HasConstraintName("dane_klienta_adres_korespondencyjny");

                entity.HasOne(d => d.IdKlientaNavigation)
                    .WithMany(p => p.DaneKlienta)
                    .HasForeignKey(d => d.IdKlienta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dane_klienta_klient");
            });

            modelBuilder.Entity<IdentyfikatorPodatkowy>(entity =>
            {
                entity.ToTable("identyfikator_podatkowy");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.KlientIdKlienta).HasColumnName("klient_id_klienta");

                entity.Property(e => e.Krs)
                    .HasColumnName("krs")
                    .HasMaxLength(10);

                entity.Property(e => e.Nip)
                    .HasColumnName("nip")
                    .HasMaxLength(12);

                entity.Property(e => e.Pesel)
                    .HasColumnName("pesel")
                    .HasMaxLength(11);

                entity.Property(e => e.Regon)
                    .HasColumnName("regon")
                    .HasMaxLength(9);

                entity.HasOne(d => d.KlientIdKlientaNavigation)
                    .WithMany(p => p.IdentyfikatorPodatkowy)
                    .HasForeignKey(d => d.KlientIdKlienta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("identyfikator_podatkowy_klient");
            });

            modelBuilder.Entity<Klasyfikatory>(entity =>
            {
                entity.ToTable("klasyfikatory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NazwaKlasyfikatora)
                    .IsRequired()
                    .HasColumnName("nazwa_klasyfikatora")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<KlasyfikatoryObiektu>(entity =>
            {
                entity.HasKey(e => e.ObiektId);

                entity.ToTable("klasyfikatory_obiektu");

                entity.Property(e => e.ObiektId)
                    .HasColumnName("obiekt_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.KlasyfikatoryId).HasColumnName("klasyfikatory_id");

                entity.Property(e => e.WartosciKlasyfikatorowId).HasColumnName("wartosci_klasyfikatorow_id");

                entity.HasOne(d => d.Klasyfikatory)
                    .WithMany(p => p.KlasyfikatoryObiektu)
                    .HasForeignKey(d => d.KlasyfikatoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("klasyfikatory_obiektu_klasyfikatory");

                entity.HasOne(d => d.Obiekt)
                    .WithOne(p => p.KlasyfikatoryObiektu)
                    .HasForeignKey<KlasyfikatoryObiektu>(d => d.ObiektId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("klasyfikatory_obiektu_obiekt");

                entity.HasOne(d => d.WartosciKlasyfikatorow)
                    .WithMany(p => p.KlasyfikatoryObiektu)
                    .HasForeignKey(d => d.WartosciKlasyfikatorowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("klasyfikatory_obiektu_wartosci_klasyfikatorow");
            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasKey(e => e.IdKlienta);

                entity.ToTable("klient");

                entity.Property(e => e.IdKlienta).HasColumnName("id_klienta");

                entity.Property(e => e.CzyAktywny).HasColumnName("czy_aktywny");

                entity.Property(e => e.RodzajKlientaId).HasColumnName("rodzaj_klienta_id");

                entity.HasOne(d => d.RodzajKlienta)
                    .WithMany(p => p.Klient)
                    .HasForeignKey(d => d.RodzajKlientaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("klient_rodzaj_klienta");
            });

            modelBuilder.Entity<NormyZuzyciaWody>(entity =>
            {
                entity.ToTable("normy_zuzycia_wody");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NazwaNormy).HasColumnName("nazwa_normy");

                entity.Property(e => e.OpisNormy)
                    .IsRequired()
                    .HasColumnName("opis_normy")
                    .HasMaxLength(40);

                entity.Property(e => e.RodzajKlientaId).HasColumnName("rodzaj_klienta_id");

                entity.Property(e => e.TypNormy)
                    .IsRequired()
                    .HasColumnName("typ_normy")
                    .HasMaxLength(40);

                entity.Property(e => e.ZuzycieDm3NaDobe).HasColumnName("zuzycie_dm3_na_dobe");

                entity.Property(e => e.ZuzycieM3NaMiesiac).HasColumnName("zuzycie_m3_na_miesiac");

                entity.HasOne(d => d.RodzajKlienta)
                    .WithMany(p => p.NormyZuzyciaWody)
                    .HasForeignKey(d => d.RodzajKlientaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("normy_zuzycia_wody_rodzaj_klienta");
            });

            modelBuilder.Entity<Obiekt>(entity =>
            {
                entity.ToTable("obiekt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AktywnyOdbiorcaId).HasColumnName("aktywny_odbiorca_id");

                entity.Property(e => e.KlientIdKlienta).HasColumnName("klient_id_klienta");

                entity.Property(e => e.Geometria).HasColumnName("geometria");

                entity.Property(e => e.Woda).HasColumnName("woda");
                entity.Property(e => e.Scieki).HasColumnName("scieki");
                entity.Property(e => e.AbonamentScieki).HasColumnName("abonament_woda");
                entity.Property(e => e.AbonamentWoda).HasColumnName("abonament_scieki");

                entity.Property(e => e.SposobRozliczenia)
                    .IsRequired()
                    .HasColumnName("sposob_rozliczenia")
                    .HasMaxLength(20);

                entity.HasOne(d => d.AktywnyOdbiorca)
                    .WithMany(p => p.Obiekt)
                    .HasForeignKey(d => d.AktywnyOdbiorcaId)
                    .HasConstraintName("obiekt_aktywny_odbiorca");

                entity.HasOne(d => d.KlientIdKlientaNavigation)
                    .WithMany(p => p.Obiekt)
                    .HasForeignKey(d => d.KlientIdKlienta)
                    .HasConstraintName("obiekt_klient");
            });

            modelBuilder.Entity<OplatyAbonamentoweScieki>(entity =>
            {
                entity.ToTable("oplaty_abonamentowe_scieki");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(30);

                entity.Property(e => e.OkresRozliczeniowy)
                    .IsRequired()
                    .HasColumnName("okres_rozliczeniowy")
                    .HasMaxLength(40);

                entity.Property(e => e.Oplata).HasColumnName("oplata");

                entity.Property(e => e.RodzajKlientaId).HasColumnName("rodzaj_klienta_id");

                entity.Property(e => e.TypCennika)
                    .IsRequired()
                    .HasColumnName("typ_cennika")
                    .HasMaxLength(30);

                entity.HasOne(d => d.RodzajKlienta)
                    .WithMany(p => p.OplatyAbonamentoweScieki)
                    .HasForeignKey(d => d.RodzajKlientaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("oplaty_abonamentowe_scieki_rodzaj_klienta");
            });

            modelBuilder.Entity<OplatyAbonamentoweWoda>(entity =>
            {
                entity.ToTable("oplaty_abonamentowe_woda");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(30);

                entity.Property(e => e.OkresRozliczeniowy)
                    .IsRequired()
                    .HasColumnName("okres_rozliczeniowy")
                    .HasMaxLength(40);

                entity.Property(e => e.Oplata).HasColumnName("oplata");

                entity.Property(e => e.RodzajKlientaId).HasColumnName("rodzaj_klienta_id");

                entity.Property(e => e.TypCennika)
                    .IsRequired()
                    .HasColumnName("typ_cennika")
                    .HasMaxLength(30);

                entity.HasOne(d => d.RodzajKlienta)
                    .WithMany(p => p.OplatyAbonamentoweWoda)
                    .HasForeignKey(d => d.RodzajKlientaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("oplaty_abonamentowe_woda_rodzaj_klienta");
            });

            modelBuilder.Entity<RodzajKlienta>(entity =>
            {
                entity.ToTable("rodzaj_klienta");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TypKlienta)
                    .IsRequired()
                    .HasColumnName("typ_klienta")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<WartosciKlasyfikatorow>(entity =>
            {
                entity.ToTable("wartosci_klasyfikatorow");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.KlasyfikatoryId).HasColumnName("klasyfikatory_id");

                entity.HasOne(d => d.Klasyfikatory)
                    .WithMany(p => p.WartosciKlasyfikatorow)
                    .HasForeignKey(d => d.KlasyfikatoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wartosci_klasyfikatorow_klasyfikatory");
            });

            modelBuilder.Entity<GranicaOdpowiedzialnosci>(entity =>
            {
                entity.ToTable("granica_odpowiedzialnosci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(10);

                entity.Property(e => e.Typ)
                    .IsRequired()
                    .HasColumnName("typ")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TypUmowy>(entity =>
            {
                entity.ToTable("typ_umowy");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Typ)
                    .IsRequired()
                    .HasColumnName("typ")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TytulPrawny>(entity =>
            {
                entity.ToTable("tytul_prawny");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nazwa)
                    .IsRequired()
                    .HasColumnName("nazwa")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Umowa>(entity =>
            {
                entity.HasKey(e => e.NrUmowy);

                entity.ToTable("umowa");

                entity.Property(e => e.NrUmowy)
                    .HasColumnName("nr_umowy")
                    .HasMaxLength(9)
                    .ValueGeneratedNever();

                entity.Property(e => e.DataObowiazywaniaDo)
                    .HasColumnName("data_obowiazywania_do")
                    .HasColumnType("date");

                entity.Property(e => e.DataObowiazywaniaOd)
                    .HasColumnName("data_obowiazywania_od")
                    .HasColumnType("date");

                entity.Property(e => e.DataZawarcia)
                    .HasColumnName("data_zawarcia")
                    .HasColumnType("date");

                entity.Property(e => e.DeklarowanaIloscM3).HasColumnName("deklarowana_ilosc_m3");

                entity.Property(e => e.GranicaOdpowiedzialnosciSciekiId).HasColumnName("granica_odpowiedzialnosci_scieki_id");

                entity.Property(e => e.GranicaOdpowiedzialnosciWodaId).HasColumnName("granica_odpowiedzialnosci_woda_id");

                entity.Property(e => e.KlientIdKlienta).HasColumnName("klient_id_klienta");

                entity.Property(e => e.LokalizacjaUmowy)
                    .IsRequired()
                    .HasColumnName("lokalizacja_umowy")
                    .HasMaxLength(200);

                entity.Property(e => e.ObiektId).HasColumnName("obiekt_id");

                entity.Property(e => e.TypUmowyId).HasColumnName("typ_umowy_id");

                entity.Property(e => e.TytulPrawnyId).HasColumnName("tytul_prawny_id");

                entity.Property(e => e.Uwagi)
                    .IsRequired()
                    .HasColumnName("uwagi")
                    .HasMaxLength(200);

                entity.HasOne(d => d.GranicaOdpowiedzialnosciScieki)
                    .WithMany(p => p.UmowaGranicaOdpowiedzialnosciScieki)
                    .HasForeignKey(d => d.GranicaOdpowiedzialnosciSciekiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("umowa_granica_odpowiedzialnosci_scieki");

                entity.HasOne(d => d.GranicaOdpowiedzialnosciWoda)
                    .WithMany(p => p.UmowaGranicaOdpowiedzialnosciWoda)
                    .HasForeignKey(d => d.GranicaOdpowiedzialnosciWodaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("umowa_granica_odpowiedzialnosci_woda");

                entity.HasOne(d => d.KlientIdKlientaNavigation)
                    .WithMany(p => p.Umowa)
                    .HasForeignKey(d => d.KlientIdKlienta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("umowa_klient");

                entity.HasOne(d => d.Obiekt)
                    .WithMany(p => p.Umowa)
                    .HasForeignKey(d => d.ObiektId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("umowa_obiekt");

                entity.HasOne(d => d.TypUmowy)
                    .WithMany(p => p.Umowa)
                    .HasForeignKey(d => d.TypUmowyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("umowa_typ_umowy");

                entity.HasOne(d => d.TytulPrawny)
                    .WithMany(p => p.Umowa)
                    .HasForeignKey(d => d.TytulPrawnyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("umowa_tytul_prawny");
            });
        }

        public DbSet<Waterworks.Models.View.PriceList.SubscriptionPriceViewModel> SubscriptionPriceViewModel { get; set; }

        public DbSet<Waterworks.Models.View.Client.BasicCreateObjectViewModel> BasicCreateObjectViewModel { get; set; }

        public DbSet<Waterworks.Models.View.Object.ObjectInListViewModel> ObjectInListViewModel { get; set; }

        public DbSet<Waterworks.Models.View.PriceList.PriceViewModel> PriceViewModel { get; set; }

        public DbSet<Waterworks.Models.View.WaterMeter.WaterMeterViewModel> WaterMeterViewModel { get; set; }

        public DbSet<Waterworks.Models.View.Contract.ContractTypeViewModel> ContractTypeViewModel { get; set; }

        public DbSet<Waterworks.Models.View.Contract.LiabilityLimitViewModel> LiabilityLimitViewModel { get; set; }

        public DbSet<Waterworks.Models.View.Contract.LegalTitleViewModel> LegalTitleViewModel { get; set; }

    }
}
