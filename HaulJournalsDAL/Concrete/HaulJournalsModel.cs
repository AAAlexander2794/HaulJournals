using System.Data.Entity;
using HaulJournalsDAL.Entities;

namespace HaulJournalsDAL.Concrete
{
    public partial class HaulJournalsModel : DbContext
    {
        public HaulJournalsModel()
            : base("name=HaulJournalsModel")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public HaulJournalsModel(string nameOfConnectionString)
            : base($"name={nameOfConnectionString}")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Ages> Ages { get; set; }
        public virtual DbSet<CatchFactors> CatchFactors { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<FishTypes> FishTypes { get; set; }
        public virtual DbSet<Fishes> Fishes { get; set; }
        public virtual DbSet<GearTypes> GearTypes { get; set; }
        public virtual DbSet<Gears> Gears { get; set; }
        public virtual DbSet<GonadsStages> GonadsStages { get; set; }
        public virtual DbSet<Grounds> Grounds { get; set; }
        public virtual DbSet<Meats> Meats { get; set; }
        public virtual DbSet<PartitionVariants> PartitionVariants { get; set; }
        public virtual DbSet<Squares> Squares { get; set; }
        public virtual DbSet<StandartStations> StandartStations { get; set; }
        public virtual DbSet<Vessels> Vessels { get; set; }
        public virtual DbSet<VoyageTypes> VoyageTypes { get; set; }
        public virtual DbSet<WindDirects> WindDirects { get; set; }
        public virtual DbSet<BioAn> BioAn { get; set; }
        public virtual DbSet<BioAnOld> BioAnOld { get; set; }
        public virtual DbSet<Hauls> Hauls { get; set; }
        public virtual DbSet<Measurements> Measurements { get; set; }
        public virtual DbSet<Stations> Stations { get; set; }
        public virtual DbSet<VarSeries> VarSeries { get; set; }
        public virtual DbSet<Voyages> Voyages { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //В БД все названия столбцов маленькими буквами
            modelBuilder.Properties().Configure(c =>
            {
                var name = c.ClrPropertyInfo.Name;
                var newName = name.ToLower();
                c.HasColumnName(newName);
            });

            modelBuilder.Entity<Ages>()
                .HasMany(e => e.BioAn)
                .WithOptional(e => e.Age)
                .HasForeignKey(e => e.AgeId);

            modelBuilder.Entity<Districts>()
                .HasMany(e => e.Squares)
                .WithRequired(e => e.Districts)
                .HasForeignKey(e => e.DistrictId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FishTypes>()
                .HasMany(e => e.Fishes)
                .WithOptional(e => e.FishTypes)
                .HasForeignKey(e => e.FishTypeId);

            modelBuilder.Entity<Fishes>()
                .HasMany(e => e.CatchFactors)
                .WithRequired(e => e.Fishes)
                .HasForeignKey(e => e.FishId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fishes>()
                .HasMany(e => e.Hauls)
                .WithRequired(e => e.Fish)
                .HasForeignKey(e => e.FishId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GearTypes>()
                .HasMany(e => e.Gears)
                .WithOptional(e => e.GearTypes)
                .HasForeignKey(e => e.GearTypeId);

            modelBuilder.Entity<Gears>()
                .HasMany(e => e.CatchFactors)
                .WithRequired(e => e.Gears)
                .HasForeignKey(e => e.GearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Gears>()
                .HasMany(e => e.Stations)
                .WithRequired(e => e.Gear)
                .HasForeignKey(e => e.GearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GonadsStages>()
                .HasMany(e => e.BioAn)
                .WithOptional(e => e.GonadsStage)
                .HasForeignKey(e => e.GonadsStageId);

            modelBuilder.Entity<Grounds>()
                .HasMany(e => e.Stations)
                .WithOptional(e => e.Ground)
                .HasForeignKey(e => e.GroundId);

            modelBuilder.Entity<PartitionVariants>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.PartitionVariants)
                .HasForeignKey(e => e.PartitionVariantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Squares>()
                .HasMany(e => e.StandartStations)
                .WithOptional(e => e.Squares)
                .HasForeignKey(e => e.SquareId);

            modelBuilder.Entity<Squares>()
                .HasMany(e => e.Stations)
                .WithOptional(e => e.Square)
                .HasForeignKey(e => e.SquareId);

            modelBuilder.Entity<Vessels>()
                .HasMany(e => e.Voyages)
                .WithRequired(e => e.Vessel)
                .HasForeignKey(e => e.VesselId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VoyageTypes>()
                .HasMany(e => e.Voyages)
                .WithRequired(e => e.VoyageType)
                .HasForeignKey(e => e.VoyageTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WindDirects>()
                .HasMany(e => e.Stations)
                .WithOptional(e => e.WindDirect)
                .HasForeignKey(e => e.WindDirectId);

            modelBuilder.Entity<BioAn>()
                .HasMany(e => e.BioAnOld)
                .WithRequired(e => e.BioAn)
                .HasForeignKey(e => e.BioAnId);

            modelBuilder.Entity<Hauls>()
                .HasMany(e => e.BioAn)
                .WithRequired(e => e.Haul)
                .HasForeignKey(e => e.HaulId);

            modelBuilder.Entity<Hauls>()
                .HasMany(e => e.Measurements)
                .WithRequired(e => e.Hauls)
                .HasForeignKey(e => e.HaulId);

            modelBuilder.Entity<Hauls>()
                .HasMany(e => e.VarSeries)
                .WithRequired(e => e.Hauls)
                .HasForeignKey(e => e.HaulId);

            modelBuilder.Entity<Stations>()
                .Property(e => e.TempAir)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Stations>()
                .Property(e => e.TempWaterSurface)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Stations>()
                .Property(e => e.TempWater_5M)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Stations>()
                .Property(e => e.TempWaterBottom)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Stations>()
                .Property(e => e.CoordsStartN)
                .HasPrecision(7, 5);

            modelBuilder.Entity<Stations>()
                .Property(e => e.CoordsStartE)
                .HasPrecision(7, 5);

            modelBuilder.Entity<Stations>()
                .Property(e => e.CoordsEndN)
                .HasPrecision(7, 5);

            modelBuilder.Entity<Stations>()
                .Property(e => e.CoordsEndE)
                .HasPrecision(7, 5);

            modelBuilder.Entity<Stations>()
                .Property(e => e.TimeStart)
                .HasPrecision(0);

            modelBuilder.Entity<Stations>()
                .Property(e => e.TimeEnd)
                .HasPrecision(0);

            modelBuilder.Entity<Stations>()
                .HasMany(e => e.Hauls)
                .WithRequired(e => e.Stations)
                .HasForeignKey(e => e.StationId);

            modelBuilder.Entity<Voyages>()
                .HasMany(e => e.Stations)
                .WithRequired(e => e.Voyage)
                .HasForeignKey(e => e.VoyageId);

            modelBuilder.Entity<Groups>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Groups)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<Groups>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("refs_group_role", "properties").MapLeftKey("group_id").MapRightKey("role_id"));

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Voyages)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
