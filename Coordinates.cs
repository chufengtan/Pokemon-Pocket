using System;
using System.Collections.Generic;

namespace PokemonPocket{
    public class Coordinate{
        public string Location {get;set;}
        public string found {get; set;}
        public Coordinate(string name){
            this.Location = name;
            this.found = "         ";
        }
    }
}