using System;
using System.Linq;
using System.Collections.Generic;
using I1JM39_HFT_2022231.Models;
using ConsoleTools;

namespace I1JM39_HFT_2022231.Client
{
    internal class Program
    {
        static RestService rest;
        
        #region CRUD
        static void Create(string entity)
        {
            Console.Clear();
            Console.WriteLine("Enter the requested param(s): ");
            if (entity.ToLower() == "game")
            {
                Console.Write("GameID: ");
                int gameId = int.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Price: ");
                double price = double.Parse(Console.ReadLine());
                Console.Write("Rating: ");
                double rating = double.Parse(Console.ReadLine());
                Console.Write("Release: ");
                int release = int.Parse(Console.ReadLine());
                
                rest.Post(new Game() { GameId = gameId ,GameName = name, Price = price, Rating = rating, Release = release }, "game");
            }
            else if (entity.ToLower() == "developer")
            {
                Console.Write("DeveloperID: ");
                int devId = int.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("GameID: ");
                int gameId = int.Parse(Console.ReadLine());
                rest.Post(new Developer() { DeveloperId = devId , DeveloperName = name, GameId = gameId }, "developer");
            }
            else if (entity.ToLower() == "character")
            {
                Console.Write("CharactedID: ");
                int charId = int.Parse(Console.ReadLine());
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Priority: ");
                int priority = int.Parse(Console.ReadLine());
                Console.Write("GameID: ");
                int gameId = int.Parse(Console.ReadLine());
                rest.Post(new Character() { CharacterId = charId , CharacterName = name, Priority = priority, GameId = gameId }, "character");
            }
            Console.WriteLine("\n" + entity.ToUpper() + " added successfully!");
            Console.Write("\nPress a button to continue...");
            Console.ReadKey();
        }
        static void ReadAll<T>(List<T> list, string entity)
        {
            Console.Clear();
            Console.WriteLine($"List of {entity.ToUpper()}'s in the database: \n");
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.Write("\nPress a button to continue...");
            Console.ReadKey();
        }
        static void ReadGame<T>(T item)
        {
            Console.Clear();
            Console.WriteLine("ID of the required item: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(rest.Get<Game>(id,"game").ToString());
            Console.Write("\nPress a button to continue...");
            Console.ReadKey();
        }
        static void ReadCharacter<T>(T item)
        {
            Console.Clear();
            Console.WriteLine("ID of the required item: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(rest.Get<Character>(id, "character").ToString());
            Console.Write("\nPress a button to continue...");
            Console.ReadKey();
        }
        static void ReadDeveloper<T>(T item)
        {
            Console.Clear();
            Console.WriteLine("ID of the required item: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(rest.Get<Developer>(id, "developer").ToString());
            Console.Write("\nPress a button to continue...");
            Console.ReadKey();
        }
        static void Update(string entity)
        {
            Console.WriteLine("Enter the requested param(s): ");
            if (entity.ToLower() == "game")
            {
                Console.Write("Enter the id of the game, that you want to update: ");
                int id = int.Parse(Console.ReadLine());
                Game one = rest.Get<Game>(id, "game");
                Console.Write($"New name [old: {one.GameName}]: ");
                string name = Console.ReadLine();
                one.GameName = name;
                rest.Put(one, "game");
            }
            else if (entity.ToLower() == "developer")
            {
                Console.Write("Enter the id of the developer, that you want to update: ");
                int id = int.Parse(Console.ReadLine());
                Developer one = rest.Get<Developer>(id, "developer");
                Console.Write($"New name [old: {one.DeveloperName}]: ");
                string name = Console.ReadLine();
                one.DeveloperName = name;
                rest.Put(one, "developer");
            }
            else if (entity.ToLower() == "character")
            {
                Console.Write("Enter the id of the character, that you want to update: ");
                int id = int.Parse(Console.ReadLine());
                Character one = rest.Get<Character>(id, "character");
                Console.Write($"New name [old: {one.CharacterName}]: ");
                string name = Console.ReadLine();
                one.CharacterName = name;
                rest.Put(one, "character");
            }
            Console.WriteLine("\n" + entity.ToUpper() + " updated successfully!");
            Console.Write("\nPress a button to continue...");
            Console.ReadKey();
        }
        static void Delete(string entity)
        {
            if (entity.ToLower() == "game")
            {
                Console.Clear();
                Console.Write("Enter the game id, that you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "game");
            }
            else if (entity.ToLower() == "developer")
            {
                Console.Clear();
                Console.Write("Enter the developer id, that you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "developer");
            }
            else if (entity.ToLower() == "character")
            {
                Console.Clear();
                Console.Write("Enter the character id, that you want to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "character");
            }
            Console.WriteLine($"\n{entity.ToUpper()} deleted succcessfully!");
            Console.Write("\nPress a button to continue...");
            Console.ReadKey();
        }
        #endregion

        #region Non-CRUD
        static void HighestRatedGame(string entity)
        {
            Console.WriteLine("The highest rated game: ");
            var games = rest.Get<object>(entity);
            foreach (var item in games)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void LowestRatedGame(string entity)
        {
            Console.WriteLine("The lowest rated game: ");
            var games = rest.Get<object>(entity);
            foreach (var item in games)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void OldestGame(string entity)
        {
            Console.WriteLine("The oldest game: ");
            var games = rest.Get<object>(entity);
            foreach (var item in games)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void YoungestGame(string entity)
        {
            Console.WriteLine("The youngest game: ");
            var games = rest.Get<object>(entity);
            foreach (var item in games)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void FreeGames(string entity)
        {
            Console.WriteLine("The list of free game(s): ");
            var games = rest.Get<object>(entity);
            foreach (var item in games)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void PaidGames(string entity)
        {
            Console.WriteLine("The list of paid game(s): ");
            var games = rest.Get<object>(entity);
            foreach (var item in games)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void OlderThan10(string entity)
        {
            Console.WriteLine("The list of game(s) that are older, than 10 years: ");
            var games = rest.Get<object>(entity);
            foreach (var item in games)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        static void GamesCharactersCount(string entity)
        {
            Console.WriteLine("The count of each game's characters: ");
            var games = rest.Get<object>(entity);
            foreach (var item in games)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }
        #endregion

        static void Main(string[] args)
        {
            rest  = new RestService("http://localhost:23247/","game");

            ConsoleMenu menu = new ConsoleMenu();

            menu.Add("Game",
                () => new ConsoleMenu()
                .Add("Create", () => Create("game"))
                .Add("List", () => ReadAll<Game>(rest.Get<Game>("game"),"game"))
                .Add("Read by ID", () => ReadGame("game"))
                .Add("Update", () => Update("game"))
                .Add("Delete", () => Delete("game"))
                .Add("Go back to main menu", ConsoleMenu.Close)
                .Show()
                );

            menu.Add("Developer",
                () => new ConsoleMenu()
                .Add("Create", () => Create("developer"))
                .Add("List", () => ReadAll<Developer>(rest.Get<Developer>("developer"), "developer"))
                .Add("Read by ID", () => ReadDeveloper("developer"))
                .Add("Update", () => Update("developer"))
                .Add("Delete", () => Delete("developer"))
                .Add("Go back to main menu", ConsoleMenu.Close)
                .Show()
                );

            menu.Add("Character",
                () => new ConsoleMenu()
                .Add("Create", () => Create("character"))
                .Add("List", () => ReadAll<Character>(rest.Get<Character>("character"), "character"))
                .Add("Read by ID", () => ReadCharacter("character"))
                .Add("Update", () => Update("character"))
                .Add("Delete", () => Delete("character"))
                .Add("Go back to main menu", ConsoleMenu.Close)
                .Show()
                );

            menu.Add("Non CRUD Methods",
                () => new ConsoleMenu()
                .Add("Highest rated game", () => HighestRatedGame("stat/highestratedgame"))
                .Add("Lowest rated game", () => LowestRatedGame("stat/lowestratedgame"))
                .Add("List of 10 years older games", () => OlderThan10("stat/olderthan10"))
                .Add("Character count per game", () => GamesCharactersCount("stat/gamescharacterscount"))
                .Add("Oldest game", () => OldestGame("stat/oldestgame"))
                .Add("Youngest game", () => YoungestGame("stat/youngestgame"))
                .Add("List of free games", () => FreeGames("stat/freegames"))
                .Add("List of paid games", () => PaidGames("stat/paidgames"))
                .Add("Go back to main menu", ConsoleMenu.Close)
                .Show()
                );
            menu.Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
