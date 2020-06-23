using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HardGame.Models;

/// <summary>
/// Код, который создается EntityFramework. В данном классе описаны связи между моделями.
/// Содержит настройки и создание DbContext (см. документацию EntityFramework)
/// </summary>
namespace HardGame.Data
{
    public partial class BeerHouseGameContext : DbContext
    {
        public BeerHouseGameContext()
        {
        }

        public BeerHouseGameContext(DbContextOptions<BeerHouseGameContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GameData> GameData { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-UDF24JU;Initial Catalog=BeerHouseGame;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Uq_Name")
                    .IsUnique();

                entity.HasOne(d => d.GameData)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.GameDataId)
                    .HasConstraintName("FK_User_GameData");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_User_Room");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
