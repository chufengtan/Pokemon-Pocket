using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonPocket
{
    class Program
    {
        static void Main(string[] args)
        {   
            //PokemonMaster list for checking pokemon evolution availability.    
            List<PokemonMaster> pokemonMasters = new List<PokemonMaster>(){
            new PokemonMaster("Pikachu", 2, "Raichu"),
            new PokemonMaster("Eevee", 3, "Flareon"),
            new PokemonMaster("Charmander", 1, "Charmeleon")
            };

            List<ShopItem> Shop = new List<ShopItem>(){
            new Pokeball("Pokeball", 2, "Pokeball is used to capture Pokemon"),
            new HPPortion("Potion", 3, "Potion is used to heal and increase pokemon HP"),
            new AttackPortion("Attack Upgrade", 4, "This allow you to upgrade your Pokemon Attack ability")
            };

        //Use "Environment.Exit(0);" if you want to implement an exit of the console program
        //Start your assignment 1 requirements below.
            using var db = new PokemonforDB();

            // Print message in red colour
            static void Red_Msg(string Message)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Message);
                Console.ResetColor();
            }

            // Print message in red colour
            static void Yellow_Msg(string Message)
            {
                int msgcount = Message.Count();
                Console.WriteLine(string.Concat(Enumerable.Repeat("*", msgcount)));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Message);
                Console.ResetColor();
                Console.WriteLine(string.Concat(Enumerable.Repeat("*", msgcount)));
            }

            // Print message in green colour
            static void Green_Msg(string Message)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Message);
                Console.ResetColor();
            }

            static void Menu()
            {
                string welcome_msg = "Welcome to Pokemon Pocket App";
                int count_welcome = welcome_msg.Count();
                List<string> options_menu = new List<string>()
                {
                    "Add pokemon to my pocket",
                    "List pokemon(s) in my pocket",
                    "Check if I can evolve any pokemon",
                    "Evolve pokemon",
                    "Sell pokemon",
                    "Battle pokemon",
                    "Shop",
                    "My inventory"
                };
                Yellow_Msg(welcome_msg);
                for (int i = 0; i < options_menu.Count; i++)
                {
                    Console.WriteLine("(" + (i + 1) + "). " + options_menu[i]);
                }
                Console.Write("Please only enter [1, 2, 3, 4] or Q to quit: ");
            }

            void add_pokemon()
            {
                string name;
                bool NameIsNotValid = true;
                while (NameIsNotValid)
                {
                    Console.Write("Enter Pokemon Name: ");
                    name = Console.ReadLine().Trim();
                    name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
                    foreach (PokemonMaster pokemon in pokemonMasters)
                    {
                        if (pokemon.Name == name)
                        {
                            NameIsNotValid = false;
                            break;
                        }
                    }
                    if (NameIsNotValid == false)
                    {
                        while (true)
                        {
                            try
                            {
                                Console.Write("Enter Pokemon HP: ");
                                int HP = Convert.ToInt32(Console.ReadLine());
                                if (HP < 0)
                                {
                                    Red_Msg("Pokemon HP input is invalid as it cannot be negative. Please try again!");
                                    continue;
                                }
                                else
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("Enter Pokemon Exp: ");
                                            int Exp = Convert.ToInt32(Console.ReadLine());
                                            if (Exp < 0)
                                            {
                                                Red_Msg("Pokemon EXP input is invalid as it cannot be negative. Please try again!");
                                                continue;
                                            }
                                            else
                                            {
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        Console.Write("Enter Pokemon Attack: ");
                                                        int Attack = Convert.ToInt32(Console.ReadLine());
                                                        if (Attack < 0)
                                                        {
                                                            Red_Msg("Pokemon Attack input is invalid as it cannot be negative. Please try again!");
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            if (name == "Pikachu")
                                                            {
                                                                var pikachu = new Pikachu(name, HP, Exp);
                                                                pikachu.Attack = Attack;
                                                                db.Add(pikachu);
                                                                db.SaveChanges();
                                                            }
                                                            else if (name == "Eevee")
                                                            {
                                                                var eevee = new Eevee(name, HP, Exp);
                                                                eevee.Attack = Attack;
                                                                db.Add(eevee);
                                                                db.SaveChanges();
                                                            }
                                                            else if (name == "Charmander")
                                                            {
                                                                var charmander = new Charmander(name, HP, Exp);
                                                                charmander.Attack = Attack;
                                                                db.Add(charmander);
                                                                db.SaveChanges();
                                                            }
                                                            Green_Msg("Pokemon added sucessfully");
                                                            break;
                                                        }
                                                    }
                                                    catch (System.Exception)
                                                    {
                                                        Red_Msg("Pokemon Attack input is invalid. Please try again!");
                                                        continue;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                        catch (System.Exception)
                                        {
                                            Red_Msg("Pokemon Exp input is invalid. Please try again!");
                                            continue;
                                        }
                                    }
                                    break;
                                }
                            }
                            catch (System.Exception)
                            {
                                Red_Msg("Pokemon HP is invalid. Please try again!");
                                continue;
                            }
                        }
                        break;
                    }
                    else
                    {
                        Red_Msg("Pokemon name is invalid. Please try again!");
                        continue;
                    }
                }
            }
            void MyPokemon()
            {
                int number = 0;
                foreach (Pokemon pokemon in db.MyPokemons.ToList())
                {
                    number++;
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine(number + ") Name: " + pokemon.Name);
                    Console.WriteLine("   HP: " + pokemon.HP);
                    Console.WriteLine("   Exp: " + pokemon.Exp);
                    Console.WriteLine("   Attack: " + pokemon.Attack);
                    Console.WriteLine("   Skill: " + pokemon.Skill);
                    Console.WriteLine("--------------------------------");
                }
                if (number == 0)
                {
                    Red_Msg("You do not have any pokemon.");
                }
            }

            void EvolutionCheck()
            {
                int countevolve = 0;
                foreach(var evolvingpokemon in pokemonMasters)
                {
                    var count = db.MyPokemons.Where(i => i.Name == evolvingpokemon.Name).Count();
                    if (count >= evolvingpokemon.NoToEvolve)
                    {
                        countevolve ++;
                        Console.WriteLine(evolvingpokemon.Name + " --> " + evolvingpokemon.EvolveTo);
                    }
                }
                if (countevolve == 0)
                {
                    Red_Msg("You do not have any pokemon(s) that can be evolved");
                }
            }

            void EvolvePokemon()
            {
                foreach(var evolvingpokemon in pokemonMasters)
                {
                    var count = db.MyPokemons.Where(mypokemon => mypokemon.Name == evolvingpokemon.Name).Count();
                    if (count >= evolvingpokemon.NoToEvolve)
                    {
                        if (evolvingpokemon.Name == "Pikachu")
                        {
                            var raichu = new Pikachu("Raichu", 0, 0);
                            raichu.Attack = 0;
                            db.MyPokemons.Add(raichu);
                            db.SaveChanges();
                            Green_Msg(evolvingpokemon.Name + " has successfully evolved into " + evolvingpokemon.EvolveTo);
                            int remove_count = 0;
                            var Samepokemon = db.MyPokemons.Where(a => a.Name == evolvingpokemon.Name).ToList();
                            foreach (var pokemon in Samepokemon)
                            {
                                if (remove_count != evolvingpokemon.NoToEvolve)
                                {
                                    db.MyPokemons.Remove(pokemon);
                                    db.SaveChanges();
                                    remove_count ++;
                                }
                                else
                                {
                                    break;
                                }
                            } 
                        }
                        if (evolvingpokemon.Name == "Eevee")
                        {
                            var flareon = new Eevee("Flareon", 0, 0);
                            flareon.Attack = 0;
                            db.MyPokemons.Add(flareon);
                            db.SaveChanges();
                            Green_Msg(evolvingpokemon.Name + " has successfully evolved into " + evolvingpokemon.EvolveTo);
                            int remove_count = 0;
                            var Samepokemon = db.MyPokemons.Where(a => a.Name == evolvingpokemon.Name).ToList();
                            foreach (var pokemon in Samepokemon)
                            {
                                if (remove_count != evolvingpokemon.NoToEvolve)
                                {
                                    db.MyPokemons.Remove(pokemon);
                                    db.SaveChanges();
                                    remove_count ++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        if (evolvingpokemon.Name == "Charmander")
                        {
                            var charmeleon = new Charmander("Charmeleon", 0, 0);
                            charmeleon.Attack = 0;
                            db.MyPokemons.Add(charmeleon);
                            db.SaveChanges();
                            Green_Msg(evolvingpokemon.Name + " has successfully evolved into " + evolvingpokemon.EvolveTo);
                            int remove_count = 0;
                            var Samepokemon = db.MyPokemons.Where(a => a.Name == evolvingpokemon.Name).ToList();
                            foreach (var pokemon in Samepokemon)
                            {
                                if (remove_count != evolvingpokemon.NoToEvolve)
                                {
                                    db.MyPokemons.Remove(pokemon);
                                    db.SaveChanges();
                                    remove_count ++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            void SellPokemon()
            {
                int number = 0;
                foreach (Pokemon pokemon in db.MyPokemons.ToList())
                {
                    number++;
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine(number + ") Name: " + pokemon.Name);
                    Console.WriteLine("   HP: " + pokemon.HP);
                    Console.WriteLine("   Exp: " + pokemon.Exp);
                    Console.WriteLine("   Skill: " + pokemon.Skill);
                    Console.WriteLine("--------------------------------");
                }
                if (number != 0)
                {
                    while (true)
                {
                    try
                    {
                        Console.Write("Please only enter Pokemon Number or Q to go back: ");
                        string input = Console.ReadLine();
                        if (input == "Q" || input == "q")
                        {
                            break;
                        }
                        int pokemonnumber = Convert.ToInt32(input);
                        if (pokemonnumber <= 0)
                        {
                            Red_Msg("Pokemon number input is invalid as it cannot be negative. Please try again!");
                            continue;
                        }
                        else if (pokemonnumber > db.MyPokemons.Count())
                        {
                            Red_Msg("Pokemon number input is invalid. Please try again!");
                            continue;
                        }
                        else
                        {
                            db.MyPokemons.Remove(db.MyPokemons.ToList()[pokemonnumber - 1]);
                            db.SaveChanges();
                            Green_Msg("Pokemon sold successfully for 5 coins");
                            db.MyCoins.First().coins += 5;
                            db.SaveChanges();
                        }
                        break;
                    }
                    catch (System.Exception)
                    {
                        Red_Msg("Pokemon number input is invalid. Please try again!");
                        continue;
                    }
                }
                }
                else
                {
                    Red_Msg("You do not have any pokemon to sell.");
                }
            }

            void PokemonShop()
            {
                Yellow_Msg("Welcome to the Pokemon Item Store!");
                Console.WriteLine("Currently you have " + db.MyCoins.First().coins + " Coins");
                int number = 0;
                foreach (ShopItem item in Shop)
                {
                    number++;
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine(number + ") Name: " + item.Name);
                    Console.WriteLine("   Price: " + item.Price + " Coins");
                    Console.WriteLine("   Description: " + item.Description);
                    Console.WriteLine("--------------------------------");
                }
                while (true)
                {
                    try
                    {
                        Console.Write("Please enter the item number to purchase or Q to go back: ");
                        string input = Console.ReadLine();
                        if (input == "Q" || input == "q")
                        {
                            break;
                        }
                        int itemnumber = Convert.ToInt32(input);
                        if (itemnumber <= 0 || itemnumber > Shop.Count)
                        {
                            Red_Msg("Item number input is invalid. Please try again!");
                            continue;
                        }
                        else if (db.MyCoins.First().coins >= Shop[itemnumber - 1].Price)
                        {
                            if (Shop[itemnumber - 1].Name == "Potion")
                            {
                                var potion = new HPPortion("Potion", 3, "Potion is used to heal and increase pokemon HP");
                                db.MyItems.Add(potion);
                                db.SaveChanges();
                                db.MyCoins.First().coins -= Shop[itemnumber - 1].Price;
                                db.SaveChanges();
                            }
                            else if (Shop[itemnumber - 1].Name == "Pokeball")
                            {
                                var pokeball = new Pokeball("Pokeball", 2, "Pokeball is used to capture Pokemon");
                                db.MyItems.Add(pokeball);
                                db.SaveChanges();
                                db.MyCoins.First().coins -= Shop[itemnumber - 1].Price;
                                db.SaveChanges();
                            }
                            else if (Shop[itemnumber - 1].Name == "Attack Upgrade")
                            {
                                var attack = new AttackPortion("Attack Upgrade", 4, "This allow you to upgrade your Pokemon Attack ability");
                                db.MyItems.Add(attack);
                                db.SaveChanges();
                                db.MyCoins.First().coins -= Shop[itemnumber - 1].Price;
                                db.SaveChanges();
                            }
                            Green_Msg("Item purchased successfully for " + Shop[itemnumber - 1].Price + " coins");
                            break;
                        }
                        else
                        {
                            Red_Msg("Purchased unsuccesfully. You do not have enough coins.");
                            continue;
                        }
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }

            void ViewItem()
            {
                int number = 0;
                foreach(ShopItem item in db.MyItems.ToList())
                {
                    number ++;
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine(number + ") Name: " + item.Name);
                    Console.WriteLine("   Description: " + item.Description);
                    Console.WriteLine("--------------------------------");
                }
                if (number != 0)
                {
                    while (true)
                    {
                        Console.Write("Do you want to use any of the item (Yes or No): ");
                        string input = Console.ReadLine();
                        if (input.ToLower() == "no")
                        {
                            break;
                        }
                        else if (input.ToLower() == "yes")
                        {
                            while (true)
                            {
                                try
                                {
                                    Console.Write("Please enter the item number to use: ");
                                    int iteminput = Convert.ToInt32(Console.ReadLine().Trim());
                                    if (iteminput <= 0 || iteminput > number)
                                    {
                                        Red_Msg("Please enter a valid item number to use.");
                                        continue;
                                    }
                                    else if (db.MyItems.ToList()[iteminput - 1].Name == "Pokeball")
                                    {
                                        Red_Msg("Pokeball is only used for battle game.");
                                        break;
                                    }
                                    else if (db.MyItems.ToList()[iteminput - 1].Name == "Potion")
                                    {
                                        MyPokemon();
                                        while (true)
                                        {
                                            try
                                            {
                                                int count = db.MyPokemons.ToList().Count();
                                                if (count > 0)
                                                {
                                                    Console.Write("Which pokemon would you like the potion to be used on. Please enter pokemon number: ");
                                                    int pokemonnumber = Convert.ToInt32(Console.ReadLine());
                                                    int originalHP = db.MyPokemons.ToList()[pokemonnumber - 1].HP;
                                                    int newHP = originalHP + 20;
                                                    db.MyPokemons.ToList()[pokemonnumber - 1].HP = newHP;
                                                    db.SaveChanges();
                                                    Green_Msg(db.MyPokemons.ToList()[pokemonnumber - 1].Name + " have successfully added 20 HP");
                                                    for (int i = 0; i < db.MyItems.ToList().Count(); i++)
                                                    {
                                                        if (db.MyItems.ToList()[i].Name == "Potion")
                                                        {
                                                            db.MyItems.Remove(db.MyItems.ToList()[i]);
                                                            db.SaveChanges();
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            catch (System.Exception)
                                            {
                                                Red_Msg("Please enter a valid pokemon number to use.");
                                                continue;
                                            }
                                        }
                                        break;
                                    }
                                    else if (db.MyItems.ToList()[iteminput - 1].Name == "Attack Upgrade")
                                    {
                                        MyPokemon();
                                        while (true)
                                        {
                                            try
                                            {
                                                int count = db.MyPokemons.ToList().Count();
                                                if (count > 0)
                                                {
                                                    Console.Write("Which pokemon would you like the attack upgrade to be used on. Please enter pokemon number: ");
                                                    int pokemonnumber = Convert.ToInt32(Console.ReadLine());
                                                    int originalattack = db.MyPokemons.ToList()[pokemonnumber - 1].Attack;
                                                    int newattack = originalattack + 20;
                                                    db.MyPokemons.ToList()[pokemonnumber - 1].Attack = newattack;
                                                    db.SaveChanges();
                                                    Green_Msg(db.MyPokemons.ToList()[pokemonnumber - 1].Name + " have successfully added 20 attack point");
                                                    for (int i = 0; i < db.MyItems.ToList().Count(); i++)
                                                    {
                                                        if (db.MyItems.ToList()[i].Name == "Attack Upgrade")
                                                        {
                                                            db.MyItems.Remove(db.MyItems.ToList()[i]);
                                                            db.SaveChanges();
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            catch (System.Exception)
                                            {
                                                Red_Msg("Please enter a valid pokemon number to use.");
                                                continue;
                                            }
                                        }
                                        break;
                                    }
                                }
                                catch (System.Exception)
                                {
                                    Red_Msg("Please enter a valid item number to use.");
                                    continue;
                                }
                            }
                            break;
                        }
                        else
                        {
                            Red_Msg("Please only enter [Yes, No].");
                            continue;
                        }
                    }
                }
                else
                {
                    Red_Msg("You do not have any items in your inventory.");
                }
            }

            static void battlemenu()
            {
                Yellow_Msg("Welcome to the game of battle");
                Console.WriteLine("(1) Start Game");
                Console.WriteLine("(2) How to play");
                Console.Write("Please only enter [1, 2] or Q to go back: ");
            }

            static void battleinstruction()
            {
                Console.WriteLine("For this battle, there are two part.");
                Console.WriteLine("First is to catch the pokemon using your Pokeball");
                Console.WriteLine("Second is to fight with the Pokemon");
                Console.WriteLine("Hope you will have fun with the game!");
            }

            void fightpokemon()
            {
                Yellow_Msg("Welcome to this second part of the game: fighting Pokemon");
                while (true)
                {
                    int count = 0;
                    foreach (Pokemon pokemon in db.MyPokemons.ToList())
                    {
                        count++;
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine(count + ") Name: " + pokemon.Name);
                        Console.WriteLine("   HP: " + pokemon.HP);
                        Console.WriteLine("   Exp: " + pokemon.Exp);
                        Console.WriteLine("   Attack: " + pokemon.Attack);
                        Console.WriteLine("   Skill: " + pokemon.Skill);
                        Console.WriteLine("--------------------------------");
                    }
                    try
                    {
                        Console.Write("Please only enter Pokemon Number that you want to use to fight or enter Q to go home: ");
                        string input = Console.ReadLine().Trim();
                        if (input.ToLower() == "q")
                        {
                            break;
                        }
                        int pokemonnumber = Convert.ToInt32(input);
                        if (pokemonnumber <= 0)
                        {
                            Red_Msg("Pokemon number input is invalid as it cannot be negative. Please try again!");
                            continue;
                        }
                        else if (pokemonnumber > count)
                        {
                            Red_Msg("Pokemon number input is invalid. Please try again!");
                            continue;
                        }
                        else if(db.MyPokemons.ToList()[pokemonnumber - 1].Attack <= 60 || db.MyPokemons.ToList()[pokemonnumber - 1].HP <= 60)
                        {
                            Red_Msg("Pokemon health or attack is not enought to fight. Health and attack need above 60. Please pick another pokemon!");
                            continue;
                        }
                        else
                        {   
                            Random rnd = new Random();                      
                            List<Pokemon> ComputerPokemon = new List<Pokemon>(){
                            new Pikachu("Pikachu", rnd.Next(db.MyPokemons.ToList()[pokemonnumber - 1].HP - 20, db.MyPokemons.ToList()[pokemonnumber - 1].HP + 21), 4),
                            new Eevee("Eevee", rnd.Next(db.MyPokemons.ToList()[pokemonnumber - 1].HP - 20, db.MyPokemons.ToList()[pokemonnumber - 1].HP + 21), 4),
                            new Charmander("Charmander", rnd.Next(db.MyPokemons.ToList()[pokemonnumber - 1].HP - 20, db.MyPokemons.ToList()[pokemonnumber - 1].HP + 21), 4),
                            new Pikachu("Raichu", rnd.Next(db.MyPokemons.ToList()[pokemonnumber - 1].HP - 50, db.MyPokemons.ToList()[pokemonnumber - 1].HP + 101), 4),
                            new Eevee("Flareon", rnd.Next(db.MyPokemons.ToList()[pokemonnumber - 1].HP - 50, db.MyPokemons.ToList()[pokemonnumber - 1].HP + 101), 4),
                            new Charmander("Charmeleon", rnd.Next(db.MyPokemons.ToList()[pokemonnumber - 1].HP - 50, db.MyPokemons.ToList()[pokemonnumber - 1].HP + 101), 4)
                            };
                            foreach (var item in ComputerPokemon)
                            {
                                if (item.Name == "Pikachu" || item.Name == "Eevee" || item.Name == "Charmander")
                                {
                                    item.Attack = rnd.Next(db.MyPokemons.ToList()[pokemonnumber - 1].HP - 20, db.MyPokemons.ToList()[pokemonnumber - 1].HP + 21);
                                }
                                else
                                {
                                    item.Attack = rnd.Next(db.MyPokemons.ToList()[pokemonnumber - 1].HP - 50, db.MyPokemons.ToList()[pokemonnumber - 1].HP + 101);
                                }
                            }
                            var computerpokemongenerate = ComputerPokemon[rnd.Next(ComputerPokemon.Count())];
                            Console.WriteLine("In order for you to attack the pokemon, you need to win the computer in Scissors, Paper, and Stone game.");
                            Console.WriteLine("If you loss, you will get attacked by the pokemon");
                            List<string> Game = new List<string>(){
                            "scissors",
                            "paper",
                            "stone"
                            };
                            while (true)
                            {
                                Yellow_Msg("Your Pokemon Target");
                                Console.WriteLine("Name: " + computerpokemongenerate.Name);
                                Console.WriteLine("Hp: " + computerpokemongenerate.HP);
                                Console.WriteLine("Attack: " + computerpokemongenerate.Attack);
                                Yellow_Msg("Your Pokemon");
                                Console.WriteLine("Name: " + db.MyPokemons.ToList()[pokemonnumber - 1].Name);
                                Console.WriteLine("Hp: " + db.MyPokemons.ToList()[pokemonnumber - 1].HP);
                                Console.WriteLine("Attack: " + db.MyPokemons.ToList()[pokemonnumber - 1].Attack);
                                Console.Write("Scissors, Paper or Stone: ");
                                string userinput = Console.ReadLine().Trim().ToLower();
                                if (userinput == "scissors" || userinput == "paper" || userinput == "stone")
                                {
                                    string computergame = Game[rnd.Next(Game.Count())];
                                    if (computergame == userinput)
                                    {
                                        Console.WriteLine("Its a tie, No damaged");
                                        continue;
                                    }
                                    else if(computergame == "scissors" && userinput == "paper")
                                    {
                                        Red_Msg("You lost, and you got attacked. Computer chose scissors");
                                        var original = db.MyPokemons.ToList()[pokemonnumber - 1];
                                        original.HP = original.HP - computerpokemongenerate.Attack;
                                        db.SaveChanges();
                                        if (original.HP < 0)
                                        {
                                            original.HP = 0;
                                            db.SaveChanges();
                                            Red_Msg("You lost this game");
                                            break;
                                        }
                                        else
                                        {
                                            Red_Msg("Pokemon attacked you");
                                            continue;
                                        } 
                                    }
                                    else if(computergame == "paper" && userinput == "stone")
                                    {
                                        Red_Msg("You lost, and you got attacked. Computer chose paper");
                                        var original = db.MyPokemons.ToList()[pokemonnumber - 1];
                                        original.HP = original.HP - computerpokemongenerate.Attack;
                                        db.SaveChanges();
                                        if (original.HP < 0)
                                        {
                                            original.HP = 0;
                                            db.SaveChanges();
                                            Red_Msg("You lost this game");
                                            break;
                                        }
                                        else
                                        {
                                            Red_Msg("Pokemon attacked you");
                                            continue;
                                        } 
                                    }
                                    else if(computergame == "stone" && userinput == "scissors")
                                    {
                                        Red_Msg("You lost, and you got attacked. Computer chose stone");
                                        var original = db.MyPokemons.ToList()[pokemonnumber - 1];
                                        original.HP = original.HP - computerpokemongenerate.Attack;
                                        db.SaveChanges();
                                        if (original.HP < 0)
                                        {
                                            original.HP = 0;
                                            db.SaveChanges();
                                            Red_Msg("You lost this game");
                                            break;
                                        }
                                        else
                                        {
                                            Red_Msg("Pokemon attacked you");
                                            continue;
                                        } 
                                    }
                                    else if(computergame == "scissors" && userinput == "stone")
                                    {
                                        Green_Msg("You win, and you attacked the enemy. Computer chose scissors");
                                        var original = db.MyPokemons.ToList()[pokemonnumber - 1];
                                        computerpokemongenerate.HP = computerpokemongenerate.HP - original.Attack;
                                        if (computerpokemongenerate.HP < 0)
                                        {
                                            Green_Msg("You won this game");
                                            break;
                                        }
                                        else
                                        {
                                            Green_Msg("You attacked the pokemon");
                                            continue;
                                        } 
                                    }
                                    else if(computergame == "stone" && userinput == "paper")
                                    {
                                        Green_Msg("You win, and you attacked the enemy. Computer chose stone");
                                        var original = db.MyPokemons.ToList()[pokemonnumber - 1];
                                        computerpokemongenerate.HP = computerpokemongenerate.HP - original.Attack;
                                        if (computerpokemongenerate.HP < 0)
                                        {
                                            Green_Msg("You won this game");
                                            break;
                                        }
                                        else
                                        {
                                            Green_Msg("You attacked the pokemon");
                                            continue;
                                        } 
                                    }
                                    else if(computergame == "paper" && userinput == "scissors")
                                    {
                                        Green_Msg("You win, and you attacked the enemy. Computer chose paper");
                                        var original = db.MyPokemons.ToList()[pokemonnumber - 1];
                                        computerpokemongenerate.HP = computerpokemongenerate.HP - original.Attack;
                                        if (computerpokemongenerate.HP < 0)
                                        {
                                            Green_Msg("You won this game");
                                            break;
                                        }
                                        else
                                        {
                                            Green_Msg("You attacked the pokemon");
                                            continue;
                                        } 
                                    }
                                }
                                else
                                {
                                    Red_Msg("Please only enter [Scissors, Paper, Stone].");
                                    continue;
                                }
                            }
                        }
                    }
                    catch (System.Exception)
                    {
                        Red_Msg("Pokemon number input is invalid. Please try again!");
                        continue;
                    }
                    break;
                }
            }

            void CatchPokemon()
            {
                List<Coordinate> Coordinates = new List<Coordinate>(){
                    new Coordinate("A1"),
                    new Coordinate("A2"),
                    new Coordinate("A3"),
                    new Coordinate("B1"),
                    new Coordinate("B2"),
                    new Coordinate("B3"),
                    new Coordinate("C1"),
                    new Coordinate("C2"),
                    new Coordinate("C3"),
                };
                Random rnd = new Random();
                string ComputerPokemonLocation = Coordinates[rnd.Next(10)].Location;
                Yellow_Msg("Welcome to this first part of the game: Catching Pokemon");                
                while (true)
                {
                    if (db.MyItems.ToList().Where(i => i.Name == "Pokeball").Count() > 0)
                    {
                        Console.WriteLine("                                   ");
                        Console.WriteLine("          1         2         3    ");
                        Console.WriteLine("     │---------│---------│---------│");
                        Console.WriteLine("     │" + Coordinates[0].found + "│" + Coordinates[1].found + "│" + Coordinates[2].found + "│");
                        Console.WriteLine("  A  │" + Coordinates[0].found + "│" + Coordinates[1].found + "│" + Coordinates[3].found + "│");
                        Console.WriteLine("     │---------│---------│---------│");
                        Console.WriteLine("     │" + Coordinates[3].found + "│" + Coordinates[4].found + "│" + Coordinates[5].found + "│");
                        Console.WriteLine("  B  │" + Coordinates[3].found + "│" + Coordinates[4].found + "│" + Coordinates[5].found + "│");
                        Console.WriteLine("     │---------│---------│---------│");
                        Console.WriteLine("     │" + Coordinates[6].found + "│" + Coordinates[7].found + "│" + Coordinates[8].found + "│");
                        Console.WriteLine("  C  │" + Coordinates[6].found + "│" + Coordinates[7].found + "│" + Coordinates[8].found + "│");
                        Console.WriteLine("     │---------│---------│---------│");
                        Console.Write("Using a Pokeball, please choose a location to guess if the pokemon is there: ");
                        string userguess = Console.ReadLine().Trim();
                        if (userguess.ToUpper() == ComputerPokemonLocation)
                        {
                            for (int i = 0; i < db.MyItems.ToList().Count(); i++)
                            {
                                if (db.MyItems.ToList()[i].Name == "Pokeball")
                                {
                                    db.MyItems.Remove(db.MyItems.ToList()[i]);
                                    db.SaveChanges();
                                    break;
                                }
                            }
                            Green_Msg("Congrats on finding the pokemon. Now it's time for you to fight with the pokemon.");
                            fightpokemon();
                            break;
                        }
                        else if (userguess.ToUpper() == "A1" || userguess.ToUpper() == "A2" || userguess.ToUpper() == "A3" || userguess.ToUpper() == "B1" || userguess.ToUpper() == "B2" || userguess.ToUpper() == "B3" || userguess.ToUpper() == "C1" || userguess.ToUpper() == "C2" || userguess.ToUpper() == "C3")
                        {
                            for (int i = 0; i < Coordinates.Count(); i++)
                            {
                                if (Coordinates[i].Location == userguess.ToUpper())
                                {
                                    Coordinates[i].found = "---------";
                                    break;
                                }
                            }
                            for (int i = 0; i < db.MyItems.ToList().Count(); i++)
                            {
                                if (db.MyItems.ToList()[i].Name == "Pokeball")
                                {
                                    db.MyItems.Remove(db.MyItems.ToList()[i]);
                                    db.SaveChanges();
                                    break;
                                }
                            }
                            Red_Msg("You did not catch until the Pokemon. Please try again");
                            continue;
                        }
                        else
                        {
                            Red_Msg("Please enter a valid location [A1, A2, A3 etc]: ");
                            continue;
                        }
                    }
                    else
                    {
                        Red_Msg("You do not have anymore Pokeball. Please purchase it from the store.");
                        break;
                    }
                }
            }

            void battlePokemon()
            {
                var count = db.MyItems.Where(item => item.Name == "Pokeball").Count();
                while (true)
                {
                    if (db.MyPokemons.ToList().Count() == 0)
                    {
                        Red_Msg("You do not have any Pokemon in your Pocket");
                        break;
                    }
                    else if (count == 0)
                    {
                        Red_Msg("You do not have any Pokeball in your inventory");
                        break;
                    }
                    else
                    {
                        while (db.MyItems.Where(item => item.Name == "Pokeball").Count() > 0)
                        {
                            battlemenu();
                            string user_input = Console.ReadLine().Trim();
                            if (user_input.ToLower() == "q")
                            {
                                break;
                            }
                            switch (user_input)
                            {
                                case "1":
                                    CatchPokemon();
                                    break;

                                case "2":
                                    battleinstruction();
                                    continue;
                                
                                default:
                                    Red_Msg("Please enter a valid input");
                                    break;
                            }
                            break;
                        }
                        break;
                    }
                }
            }

            while (true)
            {
                try
                {
                    var MyCoins = db.MyCoins
                    .OrderBy(b => b.Id)
                    .First();
                }
                catch (System.Exception)
                {
                    var coin = new Coin();
                    coin.coins = 0;
                    db.Add(coin);
                    db.SaveChanges();
                }
                Menu();               
                string user_input = Console.ReadLine().Trim();
                if (user_input == "Q" || user_input == "q")
                {
                    Green_Msg("Thankyou for visiting My Pokemon Pocket. Goodbye!");
                    Environment.Exit(0);
                }
                else
                {
                    switch (user_input)
                    {
                        case "1":
                            add_pokemon();
                            break;

                        case "2":
                            MyPokemon();
                            break;

                        case "3":
                            EvolutionCheck();
                            break;

                        case "4":
                            EvolvePokemon();
                            break;

                        case "5":
                            SellPokemon();
                            break;

                        case "6":
                            battlePokemon();
                            break;

                        case "7":
                            PokemonShop();
                            break;

                        case "8":
                            ViewItem();
                            break;

                        default:
                            Red_Msg("Please enter a valid input.");
                            break;
                    }
                }
            }
        }
    }
}
