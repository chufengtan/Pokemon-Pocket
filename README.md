# Pokemon Pocket

## Running the program

Use the terminal to build the program.

```bash
dotnet build
```

Use the terminal to run the program.

```bash
dotnet run
```

## Program
### Game Menu
```
*****************************
Welcome to Pokemon Pocket App
*****************************
(1). Add pokemon to my pocket
(2). List pokemon(s) in my pocket
(3). Check if I can evolve any pokemon
(4). Evolve pokemon
(5). Sell pokemon
(6). Battle pokemon
(7). Shop
(8). My inventory
Please only enter [1, 2, 3, 4] or Q to quit:
```
### Adding Pokemon
```
Enter Pokemon Name: Pikachu
Enter Pokemon HP: 90
Enter Pokemon Exp: 29
Enter Pokemon Attack: 39
Pokemon added sucessfully
```
### Battle
#### Battle Menu
```
*****************************
Welcome to the game of battle
*****************************
(1) Start Game
(2) How to play
Please only enter [1, 2] or Q to go back:
```
#### How to play
```
For this battle, there are two part.
First is to catch the pokemon using your Pokeball
Second is to fight with the Pokemon
Hope you will have fun with the game!
```

#### Battle Part 1 (Guessing Coordinates)
```
********************************************************
Welcome to this first part of the game: Catching Pokemon
********************************************************
                                   
          1         2         3    
     │---------│---------│---------│
     │         │         │         │
  A  │         │         │         │
     │---------│---------│---------│
     │         │         │         │
  B  │         │         │         │
     │---------│---------│---------│
     │         │         │         │
  C  │         │         │         │
     │---------│---------│---------│
Using a Pokeball, please choose a location to guess if the pokemon is there: 
```
#### Battle Part 2 (Scissors Paper Stone with Pokemon)
### Game Item Shop
```
**********************************
Welcome to the Pokemon Item Store!
**********************************
Currently you have 3 Coins
--------------------------------
1) Name: Pokeball
   Price: 2 Coins
   Description: Pokeball is used to capture Pokemon
--------------------------------
--------------------------------
2) Name: Potion
   Price: 3 Coins
   Description: Potion is used to heal and increase pokemon HP
--------------------------------
--------------------------------
3) Name: Attack Upgrade
   Price: 4 Coins
   Description: This allow you to upgrade your Pokemon Attack ability
--------------------------------
Please enter the item number to purchase or Q to go back: 
```

### Game Inventory
```
--------------------------------
1) Name: Pokeball
   Description: Pokeball is used to capture Pokemon
--------------------------------
--------------------------------
2) Name: Pokeball
   Description: Pokeball is used to capture Pokemon
--------------------------------
Do you want to use any of the item (Yes or No):
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.
