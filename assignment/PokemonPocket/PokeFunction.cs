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
        public static int FindCoin()
        {
            using (var db = new ContextPoke())
            {
                var bagpack = db.PokeItemz.ToList();
                int index = bagpack.FindIndex(a => a.PokeItemName == "Coins");
                return index;
            }
        }

        public static int FindHeal()
        {
            using (var db = new ContextPoke())
            {
                var bagpack = db.PokeItemz.ToList();
                int index = bagpack.FindIndex(a => a.PokeItemName == "Heal Potion");
                return index;
            }
        }

        public static int FindAttack()
        {
            using (var db = new ContextPoke())
            {
                var bagpack = db.PokeItemz.ToList();
                int index = bagpack.FindIndex(a => a.PokeItemName == "AttackBooster");
                return index;
            }
        }
        public static void Display()
        {
            string stars = String.Concat(Enumerable.Repeat("*", 29));
            string dash = String.Concat(Enumerable.Repeat("-", 29));
            Console.WriteLine($"{stars}\nWelcome to Pokemon Pocket App\n{stars}");
            Console.WriteLine("(1).  Add Pokemon To Pocket\n(2).  List Pokemon(s) in my pocket\n(3).  Check if i can evolve Pokemon\n(4).  Evolve Pokemon");
            Console.WriteLine($"(5).  Delete Pokemon from list\n(6).  Pokemon Battle\n(7).  Display Your Items in BagPack\n(8).  PokeShop\n(9).  Pokemon Battle Rules");
            Console.Write("Please only enter [1,2,3,4] or Q to quit : ");
        }

        public static void PokeLevelMenu()
        {
            Console.WriteLine("EXP Table for levelling up Pokemon!");
            Console.WriteLine("Level 1 : 200 EXP\nLevel 2 : 400 EXP\nLevel 3 : 600 EXP\nLevel 4 : 800 EXP\nLevel 5 : 1000 EXP");
        }

        public static void PokeShopMenu()
        {
            Console.WriteLine("Welcome to the Pokemon Shop!");
            Console.WriteLine("Here are some items that are available for purchase");
            Console.WriteLine("1) Heal Potion - Heal your pokemon back to full health at any point in the fight - 100 Coins");
            Console.WriteLine("2) Attack Booster - Give your pokemon a 20% attack boost in battle(Lasts only 3 rounds) - 100 Coins");
        }

        public static void BattleMenu()
        {
            using (var db = new ContextPoke())
            {
                var bagpack = db.PokeItemz.ToList();
                Console.Write($"1).  Attack Opponent\n");
                Console.Write($"2).  Remaining: {bagpack[FindHeal()].PokeItemCount}   Use Heal Potion\n");
                Console.Write($"3).  Remaining: {bagpack[FindAttack()].PokeItemCount} Use Attack Booster\n");
            }
        }

        public static void PokemonBattleRules()
        {
            Console.WriteLine("1).   Every Attack will deal the amount of damage stated on the attack column of selected pokemon\n");
            Console.WriteLine("2).   Main Skill can be used after 3 basic attacks (Deals 50 Damage)\n");
            Console.WriteLine("3).   When PokeItems such as (Heal Potion,Attack Booster) is used, damage will be dealt TO you and no damage will be dealt BY you\n");
            Console.WriteLine("4).   You can opt to not use main skill and just use basic attack\n");
            Console.WriteLine("5).   You as the player will ALWAYS attack first\n");
            Console.WriteLine("6).   Battles can result in 3 manners (win/lose/tie). EXP/Coins will be distributed accordingly\n");
            Console.WriteLine("7).   PokeItems Attributes can be read and found at PokeShop(8)\n");
            Console.WriteLine("8).   Pokemon Battle comes with 3 different levels (1,2,3) EXP/Coins will be distributed accordingly");
            Console.WriteLine("Enjoy your Pokemon Battles!");

        }

        public static void DisplayPokeItems()
        {
            using (var db = new ContextPoke())
            {
                var bagpack = db.PokeItemz.ToList();
                foreach (PokeItems g in bagpack)
                {
                    Console.WriteLine($"{g.PokeItemName} :  {g.PokeItemCount}");
                }
            }
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
                        db.Add(new Pikachu(Poke_Name, Poke_HP, Poke_EXP, 10));
                        db.SaveChanges();
                    }

                    else if (Poke_Name == "charmander")
                    {
                        db.Add(new Charmander(Poke_Name, Poke_HP, Poke_EXP, 10));
                        db.SaveChanges();
                    }
                    else if (Poke_Name == "eevee")
                    {
                        db.Add(new Eevee(Poke_Name, Poke_HP, Poke_EXP, 10));
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
                    Console.WriteLine($"ID : {PokeDb[i].PokemonId}\nName : {PokeDb[i].name}\nHP : {PokeDb[i].HP}\nEXP : {PokeDb[i].EXP}\nSkill: {PokeDb[i].Skill}\nAttack : {PokeDb[i].attack}\nLevel : {PokeDb[i].PokeLevel}\n{dash}\n{dash}\n");
                    continue;
                }
            }
        }

        public static void CheckEvolve()
        {
            using (var db = new ContextPoke())
            {

                List<PokemonMaster> pokemonMasters = new List<PokemonMaster>(){
            new PokemonMaster("pikachu" , 2 , "Raichu"),
            new PokemonMaster("eevee" , 3 , "Flareon"),
            new PokemonMaster("charmander" , 1 , "Charmeleon")
                };

                var PokeDb = db.pokemons.ToList();
                DisplayPoke();
                Console.Write("Enter Pokemon ID that you want to evolve (to check):");
                int PokeID = Convert.ToInt32(Console.ReadLine()); // entered by user
                string checking = PokeDb.Where(x => x.PokemonId == PokeID).First().name;

                for (int i = 0; i < pokemonMasters.Count; i++)
                {
                    if (checking == pokemonMasters[i].Name)
                    {
                        if (PokeDb.Where(x => x.name == checking).Count() == pokemonMasters[i].NoToEvolve)
                        {
                            Console.WriteLine($"Yes , your pokemon {pokemonMasters[i].Name} can be evolved to {pokemonMasters[i].EvolveTo}");
                            Console.WriteLine($"You have currently {pokemonMasters[i].NoToEvolve} of {pokemonMasters[i].Name} pokemon");
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Yes , your pokemon {pokemonMasters[i].Name} can be evolved to {pokemonMasters[i].EvolveTo}");
                            Console.WriteLine("Your selected pokemon does not have enough pokemon!");
                            break;
                        }
                    }
                    else if (PokeDb.Where(x => x.PokemonId == PokeID).First().EvolveStatus == false)
                    {
                        Console.WriteLine("Your pokemon has been evolved fully");
                        break;
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
                while (true)
                {
                    List<int> PokemonToDelete = new List<int>() { };
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
                    Console.WriteLine("Select The ID of Pokemon you want to delete(Q to exit)");
                    var delete = Int32.Parse(Console.ReadLine());
                    int DeleteID = PokeDb.Where(x => x.PokemonId == delete).First().PokemonId;
                    Pokemon DeletePokemon = PokeDb.Where(y => y.PokemonId == DeleteID).First();
                    if (DeleteID == delete)
                    {
                        Console.WriteLine($"Pokemon selected : {DeletePokemon.name}");
                        Console.WriteLine("Are you sure you want to delete this pokemon?(Y/N):");
                        string DeleteDecison = Convert.ToString(Console.ReadLine().ToUpper());
                        if (DeleteDecison == "Y")
                        {
                            PokemonToDelete.Add(DeleteID);
                            Console.WriteLine($"{DeletePokemon.name} was deleted");
                            db.RemoveRange(PokeDb.Where(x => PokemonToDelete.Contains(x.PokemonId)));
                            db.SaveChanges();
                            break;
                        }
                        else if (DeleteDecison == "N") break;
                        else Console.WriteLine("Enter valid ID and try again"); continue;
                    }
                    else if (Convert.ToString(delete) == "Q")
                    {
                        break;
                    }
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
                GeneratedPokemon.Add(new Computer(GenerateName, ComputerHP, ComputerAttack, 0));
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
                            int savedAttack = SelectedForBattle.attack;
                            bool AttackBoost = false;
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
                                    BattleMenu();
                                    Console.Write("Select a battle option [1,2,3,Q]");
                                    List<char> BattleOptions = new List<char>() { '1', '2', '3', 'Q' };
                                    var attackDecision = Convert.ToChar(Console.ReadLine().ToUpper());
                                    if (attackDecision == '1')
                                    {
                                        GeneratedPokemon[0].HP -= SelectedForBattle.attack;
                                    }
                                    if (attackDecision == '2')
                                    {
                                        if (bagpack[FindHeal()].PokeItemCount > 0)
                                        {
                                            SelectedForBattle.HP += savedHP;
                                            bagpack[FindHeal()].PokeItemCount -= 1;
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            Console.WriteLine("You do not have heal potions");
                                        }
                                    }
                                    if (attackDecision == '3')
                                    {
                                        if (bagpack[FindAttack()].PokeItemCount > 0)
                                        {
                                            if (AttackBoost == false)
                                            {
                                                SelectedForBattle.attack *= bagpack[FindAttack()].buff;
                                                AttackBoost = true;
                                                bagpack[FindAttack()].PokeItemCount -= 1;
                                                db.SaveChanges();
                                            }
                                            else
                                            {
                                                Console.WriteLine("You already have an attack booster in play");
                                            }
                                        }

                                    }
                                    else if (attackDecision == 'Q')
                                    {
                                        Console.WriteLine("Battle Stopped");
                                        break;
                                    }
                                    else if (BattleOptions.Contains(attackDecision) == false)
                                    {
                                        Console.WriteLine("Enter valid battle option"); continue;
                                    }

                                    SelectedForBattle.HP -= GeneratedPokemon[0].attack;
                                    // after every attack , get one charge
                                    charge += 1;
                                }
                                // lose
                                else if (SelectedForBattle.HP <= 0)
                                {
                                    SelectedForBattle.HP = savedHP;
                                    SelectedForBattle.attack = savedAttack;
                                    SelectedForBattle.EXP += 15;
                                    Console.WriteLine("Oh no, you lost! Better Luck Next Time!");
                                    bagpack[FindCoin()].PokeItemCount += 10;
                                    battleStatus = false;
                                    db.SaveChanges();
                                    break;
                                }
                                // win
                                else if (GeneratedPokemon[0].HP <= 0)
                                {
                                    SelectedForBattle.HP = savedHP;
                                    SelectedForBattle.attack = savedAttack;

                                    if (savedLevel == 1)
                                    {
                                        SelectedForBattle.EXP += rnd.Next(30, 100);
                                        bagpack[FindCoin()].PokeItemCount += 50;
                                    }

                                    if (savedLevel == 2)
                                    {
                                        SelectedForBattle.EXP += rnd.Next(100, 200);
                                        bagpack[FindCoin()].PokeItemCount += 100;
                                    }

                                    if (savedLevel == 3)
                                    {
                                        SelectedForBattle.EXP += rnd.Next(250, 350);
                                        bagpack[FindCoin()].PokeItemCount += 500;
                                    }

                                    Console.WriteLine("Congrats! You won!");
                                    battleStatus = false;
                                    db.SaveChanges();
                                    break;
                                }
                                // tie
                                else if (GeneratedPokemon[0].HP <= 0 && SelectedForBattle.HP <= 0)
                                {
                                    Console.WriteLine("Wow , A Tie!");
                                    SelectedForBattle.EXP += rnd.Next(15, 20);
                                    bagpack[FindCoin()].PokeItemCount += 15;
                                    battleStatus = false;
                                    db.SaveChanges();
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
                    db.Add(new HealPotion(0));
                    db.Add(new AttackBooster(0));
                    db.SaveChanges();
                }
            }
        }
        public static void PokemonShop()
        {
            using (var db = new ContextPoke())
            {
                var PokeDb = db.pokemons.ToList();
                var bagpack = db.PokeItemz.ToList();
                functions.PokeShopMenu();
                List<int> purchaseChoices = new List<int>(){
            1,
            2
        };
                try
                {
                    DisplayPokeItems();
                    Console.Write("Please select your choice of purchase");
                    int purchase = Int32.Parse(Console.ReadLine());
                    if (purchaseChoices.Contains(purchase))
                    {
                        if (purchase == 1)
                        {
                            Console.Write("You have selected the heal potion!");
                            Console.WriteLine("Price : 300 Coins");
                            Console.WriteLine("Are you sure you want to purchase a heal potion?");
                            char buy = Convert.ToChar(Console.ReadLine().ToUpper());
                            foreach (PokeItems i in bagpack)
                            {
                                if (buy == 'Y')
                                {
                                    if (i.PokeItemName == "Coins")
                                    {
                                        if (i.PokeItemCount >= 300)
                                        {
                                            foreach (PokeItems g in bagpack)
                                            {
                                                if (g.PokeItemName == "Heal Potion")
                                                {
                                                    g.PokeItemCount += 1;
                                                    db.SaveChanges();
                                                }

                                            }
                                            i.PokeItemCount -= 300;
                                            db.SaveChanges();
                                            Console.WriteLine("You have purchased a heal potion!");

                                        }
                                        else
                                        {
                                            Console.WriteLine("You do not have enough coins!");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid option");
                                }
                            }
                        }
                        if (purchase == 2)
                        {
                            Console.WriteLine("You have selected an attack booster");
                            Console.WriteLine("Price : 100 Coins");
                            Console.WriteLine("Are you sure you want to purchase an Attack Booster?");
                            char buy = Convert.ToChar(Console.ReadLine().ToUpper());
                            foreach (PokeItems i in bagpack)
                            {
                                if (buy == 'Y')
                                {
                                    if (i.PokeItemName == "Coins")
                                    {
                                        if (i.PokeItemCount >= 100)
                                        {
                                            foreach (PokeItems g in bagpack)
                                            {
                                                if (g.PokeItemName == "AttackBooster")
                                                {
                                                    g.PokeItemCount += 1;
                                                    db.SaveChanges();
                                                }

                                            }
                                            i.PokeItemCount -= 100;
                                            db.SaveChanges();
                                            Console.WriteLine("You have purchased an AttackBooster!");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("You do not have enough coins!");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please enter a valid option");
                                    continue;
                                }
                            }
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

        public static void LevelUpPokemon()
        {
            using (var db = new ContextPoke())
            {
                Dictionary<int, int> EXPLevels = new Dictionary<int, int>();
                EXPLevels.Add(1, 200);
                EXPLevels.Add(2, 400);
                EXPLevels.Add(3, 600);
                EXPLevels.Add(4, 800);
                EXPLevels.Add(5, 1000);
                try
                {
                    while (true)
                    {
                        var PokeDb = db.pokemons.ToList();
                        DisplayPoke();
                        Console.WriteLine("To level up your pokemon, you will need to use the pokemon's saved EXP");
                        Console.WriteLine("The pokemon must also be fully evoled before it can be levelled up");
                        Console.WriteLine("Please select the ID of pokemon you want to level up!(Q to exit)");
                        var levelUp = Console.ReadLine().ToUpper();
                        if (Convert.ToString(levelUp) == "Q" || Convert.ToString(levelUp) == "q")
                        {
                            break;
                        }
                        else if (PokeDb.Where(x => x.PokemonId == Int32.Parse(levelUp)).First().PokemonId == Int32.Parse(levelUp))
                        {
                            Pokemon SelectedLevel = PokeDb.Where(x => x.PokemonId == Int32.Parse(levelUp)).First();
                            if (SelectedLevel.EvolveStatus == false)
                            {
                                Console.WriteLine($"You have selected {SelectedLevel.name} to be levelled up!");
                                Console.WriteLine($"{SelectedLevel.name} currently has {SelectedLevel.EXP} EXP and is level {SelectedLevel.PokeLevel}");
                                PokeLevelMenu();
                                for (var i = 0; i <= EXPLevels.Count; i++)
                                {
                                    if (SelectedLevel.PokeLevel == i && SelectedLevel.PokeLevel != 5)
                                    {
                                        if (SelectedLevel.EXP >= EXPLevels[i + 1])
                                        {
                                            Console.WriteLine("Your pokemon has enough EXP!");
                                            Console.WriteLine("Are you sure you want to level this pokemon up?(Y/N)");
                                            string LevelDecison = Convert.ToString(Console.ReadLine().ToUpper());
                                            if (LevelDecison == "Y")
                                            {
                                                SelectedLevel.PokeLevel += 1;
                                                SelectedLevel.HP += EXPLevels[i + 1] / 2;
                                                SelectedLevel.EXP -= EXPLevels[i + 1];
                                                Console.WriteLine("Your Pokemon has been levelled up!");
                                                db.SaveChanges();
                                                break;
                                            }
                                            else if (LevelDecison == "N") break;
                                            else Console.WriteLine("Please enter valid choice"); continue;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Your pokemon does not have enough EXP to level up");
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Your pokemon has been fully levelled up!");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Pokemon must be fully evolved before they can be levelled up!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid pokemon ID");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid Format entered");
                }

            }

        }
    }
}