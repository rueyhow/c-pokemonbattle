using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonPocket
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ContextPoke())
            {
                var PokeDb = db.pokemons.ToList();
                // list of valid input options
                List<string> options = new List<string>() { "1", "2", "3", "4", "5", "6" ,"7", "Q" };

                // List<Pokemon> pokemons = new List<Pokemon>() { };
                // Random rnd = new Random();

                // Printing of the menu contents
                // string stars = String.Concat(Enumerable.Repeat("*", 29));
                // string dash = String.Concat(Enumerable.Repeat("-", 29));
                // default add bc im lazy
                // pokemons.Add(new Pikachu(34, 16));
                // pokemons.Add(new Charmander(100,12));
                // pokemons.Add(new Eevee(71, 8));
                functions.CreateBagPack();
                TextArt.title();
                while (true)
                {
                    functions.Display();
                    string response = Console.ReadLine()!.ToUpper();
                    if (response == "1")
                    {
                        functions.AddPokemon();
                        continue;
                    }
                    if (response == "2")
                    {
                        functions.DisplayPoke();
                    }
                    if (response == "3")
                    {
                        functions.CheckEvolve();
                    }
                    if (response == "4")
                    {
                        functions.EvolvePokemon();
                        continue;
                    }
                    if (response == "5")
                    {
                        if (PokeDb.Count > 0){
                            functions.DeletePoke();
                        } else{
                            Console.WriteLine("You have no pokemon to battle with");
                        }
                    }
                    if (response == "6")
                    {
                        functions.PokemonBattle();
                    }
                    if (response == "7"){
                        functions.DisplayPokeItems();
                    }
                    // exit application
                    if (response == "Q")
                    {
                        Environment.Exit(0);
                    }
                    // check whether options entered are valid
                    if (options.Contains(response) == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid reponse entered");
                        Console.ResetColor();
                        continue;
                    }
                }

            }
        }
    }
}
