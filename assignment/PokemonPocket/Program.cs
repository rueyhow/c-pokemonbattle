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
                List<string> options = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Q" };
                // to create new table
                // delete database and current migrations folder
                // dotnet ef migrations add InitialCreate
                // dotnet ef database update
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
                        if (PokeDb.Count > 0)
                        {
                            functions.DisplayPoke();
                        }
                        else
                        {
                            Console.WriteLine("Your pocket is empty");
                        }
                    }
                    if (response == "3")
                    {
                        if (PokeDb.Count > 0)
                        {
                            functions.CheckEvolve();
                        }
                        else
                        {
                            Console.WriteLine("Your pocket is empty");
                        }
                    }
                    if (response == "4")
                    {
                        if (PokeDb.Count > 0)
                        {
                            functions.EvolvePokemon();
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Your pocket is empty");
                        }
                    }
                    if (response == "5")
                    {
                        if (PokeDb.Count > 0)
                        {
                            functions.DeletePoke();
                        }
                        else
                        {
                            Console.WriteLine("You have no pokemon to battle with");
                        }
                    }
                    if (response == "6")
                    {
                        if (PokeDb.Count > 0)
                        {
                            functions.PokemonBattle();
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Your pocket is empty");
                        }
                    }
                    if (response == "7")
                    {
                        functions.DisplayPokeItems();
                    }
                    if (response == "8")
                    {
                        functions.PokemonShop();
                    }
                    if (response == "9")
                    {
                        functions.PokemonBattleRules();
                    }
                    if (response == "10")
                    {
                        if (PokeDb.Count > 0)
                        {
                            functions.LevelUpPokemon();
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Your pocket is empty");
                        }

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
