using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace PokemonPocket;

public class PokemonforDB : DbContext
{
    public DbSet<Pokemon> MyPokemons { get; set; }
    public DbSet<ShopItem> MyItems { get; set; }
    public DbSet<Coin> MyCoins { get; set; }

    public string DbPath { get; }

    public PokemonforDB()
    {
        var path = Directory.GetCurrentDirectory();
        DbPath = System.IO.Path.Join(path, "database.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Pokemon{

    public int Id { get; set; }
    public string Name { get; set;}
    public int HP { get; set;}
    public int Exp { get; set;}
    public string Skill { get; set;}
    public int Attack { get; set;}
    public string sound { get; set;}
    
}

class Pikachu : Pokemon{
    public Pikachu(string Name, int Hp, int Exp){
        this.Name = Name;
        this.HP = Hp;
        this.Exp = Exp;
        this.Skill = "Lightning Bolt";
    } 
}

class Eevee : Pokemon{
    public Eevee(string Name, int Hp, int Exp){
        this.Name = Name;
        this.HP = Hp;
        this.Exp = Exp;
        this.Skill = "Run Away";
    }
}

class Charmander : Pokemon{
    public Charmander(string Name, int Hp, int Exp){
        this.Name = Name;
        this.HP = Hp;
        this.Exp = Exp;
        this.Skill = "Solar Power";
    }
}  

public class Coin{
    public int Id { get; set; }
    public int coins {get;set;}
}

// class Coins : Coin{
//     public Coins(int coins){
//         this.coins = coins;
//     }
// }

public class ShopItem{
    public int Id { get; set; }
    public string Name {get;set;}
    public int Price {get; set;}

    public string Picture {get;set;}
    public string Description {get;set;}
}
class Pokeball : ShopItem{
    public Pokeball(string Name, int Price, string description){
        this.Name = Name;
        this.Price = Price;
        this.Description = description;
        this.Picture = "";
    }
}

class HPPortion : ShopItem{
    public HPPortion(string Name, int Price, string description){
        this.Name = Name;
        this.Price = Price;
        this.Description = description;
        this.Picture = "";
    }
} 

class AttackPortion : ShopItem{
    public AttackPortion(string Name, int Price, string description){
        this.Name = Name;
        this.Price = Price;
        this.Description = description;
        this.Picture = "";
    }
} 