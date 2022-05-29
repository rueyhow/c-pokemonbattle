﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokemonPocket;

#nullable disable

namespace PokemonPocket.Migrations
{
    [DbContext(typeof(ContextPoke))]
    partial class ContextPokeModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("PokemonPocket.PokeItems", b =>
                {
                    b.Property<int>("PokeItemsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PokeItemCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PokeItemName")
                        .HasColumnType("TEXT");

                    b.Property<int>("buff")
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.HasKey("PokeItemsId");

                    b.ToTable("PokeItemz");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PokeItems");
                });

            modelBuilder.Entity("PokemonPocket.Pokemon", b =>
                {
                    b.Property<int>("PokemonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EXP")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EvolveStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EvolveTo")
                        .HasColumnType("TEXT");

                    b.Property<int>("HP")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NoToEvolve")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PokeLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Skill")
                        .HasColumnType("TEXT");

                    b.Property<int>("attack")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.HasKey("PokemonId");

                    b.ToTable("pokemons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Pokemon");
                });

            modelBuilder.Entity("PokemonPocket.AttackBooster", b =>
                {
                    b.HasBaseType("PokemonPocket.PokeItems");

                    b.HasDiscriminator().HasValue("AttackBooster");
                });

            modelBuilder.Entity("PokemonPocket.Charmander", b =>
                {
                    b.HasBaseType("PokemonPocket.Pokemon");

                    b.HasDiscriminator().HasValue("Charmander");
                });

            modelBuilder.Entity("PokemonPocket.Coins", b =>
                {
                    b.HasBaseType("PokemonPocket.PokeItems");

                    b.HasDiscriminator().HasValue("Coins");
                });

            modelBuilder.Entity("PokemonPocket.Eevee", b =>
                {
                    b.HasBaseType("PokemonPocket.Pokemon");

                    b.HasDiscriminator().HasValue("Eevee");
                });

            modelBuilder.Entity("PokemonPocket.HealPotion", b =>
                {
                    b.HasBaseType("PokemonPocket.PokeItems");

                    b.HasDiscriminator().HasValue("HealPotion");
                });

            modelBuilder.Entity("PokemonPocket.Pikachu", b =>
                {
                    b.HasBaseType("PokemonPocket.Pokemon");

                    b.HasDiscriminator().HasValue("Pikachu");
                });
#pragma warning restore 612, 618
        }
    }
}
