using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace PokemonPocket
{
    public class PokemonMaster
    {
        public string Name { get; set; }
        public int NoToEvolve { get; set; }
        public string EvolveTo { get; set; }

        public int HP { get; set; }

        public int EXP { get; set; }
        public PokemonMaster(string Name, int noToEvolve, string evolveTo)
        {
            this.Name = Name;
            this.NoToEvolve = noToEvolve;
            this.EvolveTo = evolveTo;
        }
    }

        public class Pokemon
    {
        public int PokemonId {get;set;}
        public int NoToEvolve{get;set;}
        
        public string name { get; set;}
        public int attack {get;set;}
        public int HP { get; set; }

        public int EXP { get; set; }

        public string EvolveTo { get; set; }

        public bool EvolveStatus { get; set; }

        public string Skill { get; set; }

        public int PokeLevel {get;set;}

        public Pokemon(string name , int HP , int EXP , int attack){
            this.name = name;
            this.HP = HP;
            this.EXP = EXP;
            this.attack = attack;
            this.EvolveStatus = true;
            this.PokeLevel  = 0;
        }
    }

    // Pikachu Subclass
    public class Pikachu : Pokemon
    {
        public Pikachu(string name , int HP, int EXP , int attack) : base(name,HP , EXP , attack)
        {
            this.Skill = "Lightning Bolt";
            this.EvolveTo = "Raichu";
            this.NoToEvolve = 2;
        }

    }

    public class Charmander : Pokemon
    {
        public Charmander(string name , int HP, int EXP , int attack) : base(name,HP , EXP , attack)
        {
            this.Skill = "Solar Power";
            this.EvolveTo = "Charmeleon";
            this.NoToEvolve = 1;
        }

    }

    public class Eevee : Pokemon
    {
        public Eevee(string name , int HP, int EXP , int attack) : base(name,HP , EXP , attack)
        {
            this.Skill = "Run Away";
            this.EvolveTo = "Flareon";
            this.NoToEvolve = 3;
        }

    }

    public class Computer : Pokemon {

        public Computer(string name , int HP ,int attack , int EXP): base(name , HP ,attack , EXP){
            this.name = name;
            this.HP = HP;
            this.attack = attack;
        }
    }
}

