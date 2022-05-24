using System;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace PokemonPocket
{
    public class ContextPoke : DbContext
    {
        public DbSet<Pokemon> pokemons { get; set; }
        public DbSet<Charmander> charmanderz { get; set; }

        public DbSet<Eevee> eeveez { get; set; }

        public DbSet<Pikachu> pikachuz { get; set; }

        public DbSet<PokeItems> PokeItemz {get;set;}

        public DbSet<HealPotion> HealPotionz {get;set;}

        public DbSet<AttackBooster> AttackBoosterz{get;set;}

        public DbSet<Coins> Coinz {get;set;}
        public string DbPath { get; }


        public ContextPoke()
        {
            // var folder = Environment.SpecialFolder.LocalApplicationData;
            // var path = Environment.GetFolderPath(folder);
            // DbPath = System.IO.Path.Join(path, "poke.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={"poke.db"}");

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Pikachu>().Property(a => a.PokemonId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //     modelBuilder.Entity<Charmander>().Property(a => a.PokemonId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //     modelBuilder.Entity<Eevee>().Property(a => a.PokemonId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        // }
    }
}



