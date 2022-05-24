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
    }

    // Pikachu Subclass
    public class Pikachu : Pokemon
    {
        public Pikachu(){}
        public Pikachu(int HP, int EXP , int attack)
        {
            this.name = "pikachu";
            this.HP = HP;
            this.EXP = EXP;
            this.Skill = "Lightning Bolt";
            this.EvolveStatus = true;
            this.EvolveTo = "Raichu";
            this.NoToEvolve = 2;
            this.attack = attack;
        }

    }

    public class Charmander : Pokemon
    {
        public Charmander(){}
        public Charmander(int HP, int EXP , int attack)
        {
            this.name = "charmander";
            this.HP = HP;
            this.EXP = EXP;
            this.Skill = "Solar Power";
            this.EvolveStatus = true;
            this.EvolveTo = "Charmeleon";
            this.NoToEvolve = 1;
            this.attack = attack;
      
        }

    }

    public class Eevee : Pokemon
    {
        public Eevee(){}
        public Eevee(int HP, int EXP , int attack)
        {
            this.name = "eevee";
            this.HP = HP;
            this.EXP = EXP;
            this.Skill = "Run Away";
            this.EvolveStatus = true;
            this.EvolveTo = "Flareon";
            this.NoToEvolve = 3;
            this.attack = attack;
        }

    }

    public class Computer : Pokemon {
        public Computer(){}
        public Computer(string name , int HP , int attack){
            this.name = name;
            this.HP = HP;
            this.attack = attack;
        }
    }
}

