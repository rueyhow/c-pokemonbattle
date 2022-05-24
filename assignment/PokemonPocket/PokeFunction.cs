using System;
using System.Collections.Generic;
using System.Linq;
using Figgle;
using ConsoleTables;
namespace PokemonPocket
{
    class functions
    {

        // menu function
        // create new table


        public static void Display()
        {
            string stars = String.Concat(Enumerable.Repeat("*", 29));
            string dash = String.Concat(Enumerable.Repeat("-", 29));
            Console.WriteLine($"{stars}\nWelcome to Pokemon Pocket App\n{stars}");
            Console.WriteLine("(1).  Add Pokemon To Pocket\n(2).  List Pokemon(s) in my pocket\n(3).  Check if i can evolve Pokemon\n(4).  Evolve Pokemon");
            Console.WriteLine($"(5).  Delete Pokemon from list\n(6).  Pokemon Battle\n(7)Display Number Of Coins");
            Console.Write("Please only enter [1,2,3,4] or Q to quit : ");
        }

        public static void PokeShopMenu()
        {
            Console.WriteLine("Welcome to the Pokemon Shop!");
            Console.WriteLine("Here are some items that are available for purchase");
            Console.WriteLine("1) Heal Potion - Heal your pokemon back to full health at any point in the fight");
        }
        public static void AddPokemon()
        {
            using (var db = new ContextPoke())
            {
                try
                {
                    Console.Write("Enter Pokemon's Name: ");
                    string Poke_Name = Console.ReadLine().ToLower();

                    Console.Write("Enter Pokemon's HP: ");
                    int Poke_HP = Int32.Parse(Console.ReadLine());

                    Console.Write("Enter Pokemon's EXP: ");
                    int Poke_EXP = Int32.Parse(Console.ReadLine());


                    // adding pokemons and their evol into object
                    if (Poke_Name == "pikachu")
                    {
                        db.Add(new Pikachu(Poke_HP, Poke_EXP, 10));
                        db.SaveChanges();
                    }

                    else if (Poke_Name == "charmander")
                    {
                        db.Add(new Charmander(Poke_HP, Poke_EXP, 10));
                        db.SaveChanges();
                    }
                    else if (Poke_Name == "eevee")
                    {
                        db.Add(new Eevee(Poke_HP, Poke_EXP, 10));
                        db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Invalid name entered");
                    }
                    Console.WriteLine($"{Poke_Name} has been added to your pocket!");
                }
                catch
                {
                    Console.WriteLine("Invalid datatype entered");
                }

            }
        }

        public static void DisplayPoke()
        {
            using (var db = new ContextPoke())
            {
                string dash = String.Concat(Enumerable.Repeat("-", 29));
                var PokeDb = db.pokemons.ToList();
                PokeDb.Sort(delegate (Pokemon x, Pokemon y)
                {
                    return x.HP.CompareTo(y.HP);
                });
                Console.WriteLine(dash);
                for (int i = 0; i < PokeDb.Count; i++)
                {
                    Console.WriteLine($"ID : {PokeDb[i].PokemonId}\nName : {PokeDb[i].name}\nHP : {PokeDb[i].HP}\nEXP : {PokeDb[i].EXP}\nSkill: {PokeDb[i].Skill}\nAttack : {PokeDb[i].attack}\n{dash}\n{dash}\n");
                    continue;
                }
            }
        }

        public static void CheckEvolve()
        {
            using (var db = new ContextPoke())
            {
                var PokeDb = db.pokemons.ToList();
                DisplayPoke();
                Console.Write("Enter Pokemon ID that you want to evolve (to check):");
                int PokeID = Convert.ToInt32(Console.ReadLine());
                for (int g = 0; g < PokeDb.Count; g++)
                {
                    if (PokeID == PokeDb[g].PokemonId)
                    {
                        if (PokeDb[g].EvolveStatus == true)
                        {
                            Console.WriteLine("Yes the pokemon you have selected can be evolved");
                            Console.WriteLine($"{PokeDb[g].name} --> {PokeDb[g].EvolveTo}");
                            Console.WriteLine($"Amount of pokemon required to evolve : {PokeDb[g].NoToEvolve}");
                            Console.WriteLine($"You have currently : {PokeDb.Where(y => y.name == PokeDb[g].name).Count()} amount of {PokeDb[g].name} pokemon");
                        }
                        else if (PokeDb[g].EvolveStatus != true)
                        {
                            Console.WriteLine("Your pokemon cannot be evolved any furthur");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID Entered does not belong to any pokemon");
                    }
                }
            }
        }

        public static void EvolvePokemon()
        {
            using (var db = new ContextPoke())
            {
                var PokeDb = db.pokemons.ToList();
                List<int> PokemonToDie = new List<int>() { };
                DisplayPoke();
                Console.Write("Select which pokemon you would like to evolve:\n");
                int evolve = Int32.Parse(Console.ReadLine());
                // run selected id thru db list
                int ValidID = PokeDb.Where(x => x.PokemonId == evolve).First().PokemonId;
                if (ValidID >= 0)
                {
                    Pokemon selected = PokeDb.Where(x => x.PokemonId == evolve).First(); // store selected pokemon as object var
                    if (PokeDb.Where(y => y.name == selected.name).Count() >= selected.NoToEvolve && selected.EvolveStatus == true)
                    {
                        foreach (Pokemon m in PokeDb.OrderBy(m => m.HP))
                        {
                            if (m.name == selected.name)
                            {
                                Console.WriteLine("Available Pokemons");
                                Console.WriteLine($"ID of Pokemon {m.PokemonId} , Name : {m.name} , HP : {m.HP} , EXP : {m.EXP}");
                            }
                        }
                        for (int y = 0; y < selected.NoToEvolve - 1; y++)
                        { // keep asking which pokemons they want to merge till notoevolve number is met
                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("Enter ID of pokemon you want to merge with the selected Pokemon");
                                    int merge = Int32.Parse(Console.ReadLine());
                                    // store selected merge to object var
                                    Pokemon lamb = PokeDb.Where(x => x.PokemonId == merge).First();

                                    int LambID = PokeDb.Where(x => x.PokemonId == merge).First().PokemonId;

                                    if (LambID == selected.PokemonId)
                                    {
                                        Console.WriteLine("You cannot merge the pokemon you want to evolve"); continue;
                                    }

                                    if (lamb.name != selected.name)
                                    {
                                        Console.WriteLine("Enter valid ID"); continue;
                                    }

                                    PokemonToDie.Add(LambID);
                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("Error has occured");
                                    continue;
                                }

                            }
                        }

                        db.RemoveRange(PokeDb.Where(x => PokemonToDie.Contains(x.PokemonId))); //remove id of merge
                        Pokemon Evolved = PokeDb.Where(x => x.PokemonId == evolve).First(); // create evolved pokemon object to change its attributes

                        Evolved.name = Evolved.EvolveTo;
                        Evolved.EvolveStatus = false;
                        Evolved.HP *= 3;
                        Evolved.EXP = 0;
                        Evolved.attack *= 2;
                        Evolved.EvolveTo = "Evolution completed";
                        db.SaveChanges();
                        Console.WriteLine($"Your Pokemon has been evolved to {Evolved.name}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Your Pokemon cannot be evolved");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.WriteLine("Enter valid ID");
                }
            }
        }

        public static void DeletePoke()
        {
            using (var db = new ContextPoke())
            {
                try
                {
                    string dash = String.Concat(Enumerable.Repeat("-", 29));
                    Console.WriteLine("List of Pokemon");
                    var PokeDb = db.pokemons.ToList();
                    PokeDb.Sort(delegate (Pokemon x, Pokemon y)
                    {
                        return x.HP.CompareTo(y.HP);
                    });
                    Console.WriteLine(dash);
                    for (int i = 0; i < PokeDb.Count; i++)
                    {
                        Console.WriteLine($"ID : {PokeDb[i].PokemonId}\nName : {PokeDb[i].name}\nHP : {PokeDb[i].HP}\nEXP : {PokeDb[i].EXP}\nSkill: {PokeDb[i].Skill}\n{dash}\n{dash}");
                        continue;
                    }
                    Console.WriteLine("Select The ID of Pokemon you want to delete");
                    int delete = Int32.Parse(Console.ReadLine());
                    int DeleteID = PokeDb.Where(x => x.PokemonId == delete).First().PokemonId;
                    Pokemon DeletePokemon = PokeDb.Where(y => y.PokemonId == DeleteID).First();
                    if (DeleteID >= 0)
                    {
                        Console.WriteLine($"Pokemon selected : {DeletePokemon.name}");
                        Console.WriteLine("Are you sure you want to delete this pokemon?");
                        string DeleteDecison = Convert.ToString(Console.Read()).ToUpper();
                        if (DeleteDecison == "Y")
                        {
                            Console.WriteLine($"{DeletePokemon.name} was deleted");
                            db.RemoveRange(delete);
                            db.SaveChanges();
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid response entered");
                }
            }
        }
        public static void PokemonBattle()
        {
            using (var db = new ContextPoke())
            {
                var PokeDb = db.pokemons.ToList();
                var bagpack = db.PokeItemz.ToList();
                int savedLevel = 0;
                Random rnd = new Random(); // generate random hp / exp values
                List<Computer> GeneratedPokemon = new List<Computer>() { };
                List<string> PokemonNames = new List<string>(){
                "Charizard",
                "Wartortle",
                "Venusaur",
                "Weedle",
                "Rattata",
                "Ekans",
                "Nidoqueen",
                "Ninetales",
                "Eevee",
                "Charmander",
                "Pikachu",
            };
                int ComputerHP = 0;
                int ComputerAttack = 0;
                while (true)
                {
                    Console.WriteLine("Choose your level!");
                    Console.WriteLine("1)Level 1: \n2)Level 2: \n3)Level 3: ");
                    int level = Int32.Parse(Console.ReadLine());
                    // levels
                    if (level == 1)
                    {
                        savedLevel = 1;
                        ComputerHP = rnd.Next(50, 200); ComputerAttack = rnd.Next(5, 20); break;
                    }

                    if (level == 2)
                    {
                        savedLevel = 2;
                        ComputerHP = rnd.Next(200, 400); ComputerAttack = rnd.Next(20, 40); break;
                    }
                    if (level == 3)
                    {
                        savedLevel = 3;
                        ComputerHP = rnd.Next(400, 600); ComputerAttack = rnd.Next(60, 80); break;
                    }
                    else
                    {
                        Console.WriteLine("Enter valid level");
                        continue;
                    }
                }
                string GenerateName = PokemonNames[rnd.Next(1, PokemonNames.Count)];
                GeneratedPokemon.Add(new Computer(GenerateName, ComputerHP, ComputerAttack));
                for (var i = 0; i < GeneratedPokemon.Count; i++)
                {
                    Console.WriteLine($"A wild {GeneratedPokemon[i].name} has appeared!");
                    Console.WriteLine($"HP : {GeneratedPokemon[i].HP}");
                    Console.WriteLine($"Attack : {GeneratedPokemon[i].attack}");
                }
                DisplayPoke();
                bool battleStatus = true;
                while (battleStatus == true)
                {
                    try
                    {
                        Console.WriteLine("Select the ID of the pokemon you want to battle with : (99 to Exit)");
                        int battlePokemon = Int32.Parse(Console.ReadLine());
                        if (PokeDb.Where(x => x.PokemonId == battlePokemon).First().PokemonId == battlePokemon)
                        {
                            Pokemon SelectedForBattle = PokeDb.Where(x => x.PokemonId == battlePokemon).First();
                            Console.WriteLine(FiggleFonts.Standard.Render($"{SelectedForBattle.name}     VS     {GeneratedPokemon[0].name}"));
                            Console.WriteLine($"You have selected : {SelectedForBattle.name}");
                            Console.WriteLine($"Your stats : HP : {SelectedForBattle.HP}\nAttack : {SelectedForBattle.attack}");

                            // Pokemon Battle Portion
                            int savedHP = SelectedForBattle.HP;
                            int charge = 0;
                            for (var i = 0; battleStatus == true; i++)
                            {
                                // display stats
                                var table1 = new ConsoleTable("Name", "HP", "Attack");
                                table1.
                                    AddRow(SelectedForBattle.name, SelectedForBattle.HP, SelectedForBattle.attack);
                                var table2 = new ConsoleTable("Name", "HP", "Attack");
                                table2.
                                    AddRow(GeneratedPokemon[0].name, GeneratedPokemon[0].HP, GeneratedPokemon[0].attack);
                                table1.Options.EnableCount = false;
                                table2.Options.EnableCount = false;
                                Console.WriteLine(table1);
                                Console.WriteLine(table2);


                                if (SelectedForBattle.HP > 0 && GeneratedPokemon[0].HP > 0)
                                {
                                    if (charge >= 3)
                                    {
                                        Console.WriteLine("You have collected enough charges to use your main skill!");
                                        Console.Write("Use main skill? Y/N: ");
                                        var UseSkill = Convert.ToString(Console.ReadLine());
                                        if (UseSkill.ToUpper() == "Y")
                                        {
                                            GeneratedPokemon[0].HP -= 50;
                                            charge -= 3;
                                            continue;
                                        }
                                        else if (UseSkill.ToUpper() != "N")
                                        {
                                            Console.WriteLine("Enter valid choice"); continue;

                                        }
                                    }
                                    // main attack function
                                    Console.Write("Attack(Y/N): ");
                                    var attackDecision = Convert.ToChar(Console.ReadLine().ToUpper());
                                    if (attackDecision == 'Y')
                                    {
                                        GeneratedPokemon[0].HP -= SelectedForBattle.attack;
                                    }
                                    else if (attackDecision == 'Q')
                                    {
                                        Console.WriteLine("Battle Stopped");
                                        break;
                                    }
                                    SelectedForBattle.HP -= GeneratedPokemon[0].attack;
                                    // after every attack , get one charge
                                    charge += 1;
                                }
                                else if (SelectedForBattle.HP <= 0)
                                {
                                    SelectedForBattle.HP = savedHP;
                                    SelectedForBattle.EXP += 15;
                                    Console.WriteLine("Oh no, you lost! Better Luck Next Time!");
                                    bagpack[0].PokeItemCount += 10;
                                    battleStatus = false;
                                    db.SaveChanges();
                                    break;
                                }
                                else if (GeneratedPokemon[0].HP <= 0)
                                {
                                    SelectedForBattle.HP = savedHP;


                                    if (savedLevel == 1)
                                    {
                                        SelectedForBattle.EXP += rnd.Next(30, 100);
                                        bagpack[0].PokeItemCount += 50;
                                    }

                                    if (savedLevel == 2)
                                    {
                                        SelectedForBattle.EXP += rnd.Next(100, 200);
                                        bagpack[0].PokeItemCount += 100;
                                    }

                                    if (savedLevel == 3)
                                    {
                                        SelectedForBattle.EXP += rnd.Next(250, 350);
                                        bagpack[0].PokeItemCount += 500;
                                    }

                                    Console.WriteLine("Congrats! You won!");
                                    battleStatus = false;
                                    db.SaveChanges();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Pokemon ID not found in database");
                            continue;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input format entered");
                        continue;
                    }
                }

            }
        }
        public static void CreateBagPack()
        {
            using (var db = new ContextPoke())
            {
                var bagpack = db.PokeItemz.ToList();
                if (bagpack.Count <= 0)
                {
                    db.Add(new Coins(0));
                    db.SaveChanges();
                }
            }
        }
        public static void DisplayPokeItems()
        {
            using (var db = new ContextPoke())
            {
                var bagpack = db.PokeItemz.ToList();
                foreach (PokeItems i in bagpack)
                {
                    Console.WriteLine($"You have {i.PokeItemCount} amount of coins!");
                }
            }
        }

        public static void PokemonShop()
        {
            using (var db = new ContextPoke())
            {
                var PokeDb = db.pokemons.ToList();
            }
            functions.PokeShopMenu();
            List<int> purchaseChoices = new List<int>(){
            1,
            2
        };
            try
            {
                Console.Write("Please select your choice of purchase");
                int purchase = Int32.Parse(Console.ReadLine());
                if (purchaseChoices.Contains(purchase))
                {
                    if (purchase == 1)
                    {
                        Console.Write("You have selected the heal potion!");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid choice");
                }
            }
            catch
            {
                Console.WriteLine("Incorrect Format Entered");
            }

        }
    }
}