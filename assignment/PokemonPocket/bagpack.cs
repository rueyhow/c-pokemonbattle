using System;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace PokemonPocket
{
    public class PokeItems{
        public int PokeItemsId{get;set;}

        public string PokeItemName{get;set;}

        public string description {get;set;}

        public int PokeItemCount{get;set;}

    }

    public class HealPotion : PokeItems{
        public HealPotion(){}

        public HealPotion(int PokeItemCount){
            this.PokeItemName = "Heal Potion";
            this.PokeItemCount = PokeItemCount;
        }
    }

    public class AttackBooster : PokeItems {
        public double buff {get;set;}
        public AttackBooster(){}
        public AttackBooster(int PokeItemCount){
            this.PokeItemName = "AttackBooster";
            this.buff = 1.2;
            this.PokeItemCount = PokeItemCount;

        }
    }

    public class Coins : PokeItems {
        public Coins(){}

        public Coins(int PokeItemCount){
            this.PokeItemName = "Coins";
            this.PokeItemCount = PokeItemCount;
        }
    }
}