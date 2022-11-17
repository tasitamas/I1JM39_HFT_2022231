using ConsoleTools;
using I1JM39_HFT_2022231.Models;
using System;
using System.Collections.Generic;

namespace I1JM39_HFT_2022231.Client
{
    internal class rsMenu
    {
        RestService rest;

        public rsMenu(RestService restService)
        {
            this.rest = restService;
        }

        public void MenuStart()
        {
            #region Console Menu
            ConsoleMenu menu = new ConsoleMenu();
            menu.Add("Game",
                () => new ConsoleMenu()
                .Add("List of games", () => ReadAll<Game>(rest.Get<Game>("game")))
                .Add("Read one game", () => ReadGame("game"))
                .Add("Update game", () => UpdateGame("game"))
                .Add("Delete game", () => DeleteGame("game"))
                .Add("Go back", ConsoleMenu.Close)
                .Show()
                );
            menu.Add("Developer",
                () => new ConsoleMenu()
                .Add("List of developers", () => ReadAll<Developer>(rest.Get<Developer>("developer")))
                .Add("Read one developer", () => ReadDeveloper("developer"))
                .Add("Update developer", () => UpdateDeveloper("developer"))
                .Add("Delete developer", () => DeleteDeveloper("developer"))
                .Add("Go back", ConsoleMenu.Close)
                .Show()
                );
            menu.Add("Character",
                () => new ConsoleMenu()
                .Add("List of characters", () => ReadAll<Character>(rest.Get<Character>("character")))
                .Add("Read one character", () => ReadCharacter("character"))
                .Add("Update character", () => UpdateCharacter("character"))
                .Add("Delete character", () => DeleteCharacter("character"))
                .Add("Go back", ConsoleMenu.Close)
                .Show()
                );
            menu.Add("Non CRUD Methods",
                () => new ConsoleMenu()
                .Add("Highest rated game", () => HighestRatedGame("stat/highestratedgame"))
                .Add("Lowest rated game", () => LowestRatedGame("stat/lowestratedgame"))
                .Add("Games older than 10 years", () => OlderThan10("stat/olderthan10"))
                .Add("Games with NPCs", () => GamesWithNpc("stat/gameswithnpc"))
                .Add("Oldest game", () => OldestGame("stat/oldestgame"))
                .Add("Youngest game", () => YoungestGame("stat/youngestgame"))
                .Add("Free games", () => FreeGames("stat/freegames"))
                .Add("Paid games", () => PaidGames("stat/paidgames"))
                .Add("Go back", ConsoleMenu.Close)
                .Show()
                );
            menu.Add("Exit", ConsoleMenu.Close);

            menu.Show();
            #endregion
        }

        #region CRUD
        void ReadAll<T>(List<T> list)
        {
            Console.Clear();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }

        void ReadGame<T>(T item)
        {
            Console.Clear();
            Console.WriteLine("ID of the required item: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine(rest.Get<Game>(id,"game").ToString());
            Console.ReadKey();
        }
        void ReadDeveloper<T>(T item)
        {
            Console.Clear();
            Console.WriteLine("ID of the required item: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine(rest.Get<Developer>(id, "developer").ToString());
            Console.ReadKey();
        }
        void ReadCharacter<T>(T item)
        {
            Console.Clear();
            Console.WriteLine("ID of the required item: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine(rest.Get<Character>(id, "character").ToString());
            Console.ReadKey();
        }

        void CreateGame(string endpoint)
        {
            Console.Clear();
            Game createdGame = new Game();
            Console.WriteLine("Name of the game: ");
            string gameName = Console.ReadLine();
            Console.WriteLine("Price of the game: ");
            int price = int.Parse(Console.ReadLine());
            Console.WriteLine("Rating of the game: ");
            int rating = int.Parse(Console.ReadLine());
            Console.WriteLine("Release year of the game: ");
            int release = int.Parse(Console.ReadLine());
            createdGame.GameName = gameName;
            createdGame.Price = price;
            createdGame.Rating = rating;
            createdGame.Release = release;
            Console.Clear();
            Console.WriteLine("Creating the game...");
            rest.Post<Game>(createdGame, endpoint);
            Console.WriteLine("Game added successfully!");
            Console.ReadKey();
        }
        void CreateDeveloper(string endpoint)
        {
            Console.Clear();
            Developer createdDeveloper = new Developer();
            Console.WriteLine("Name of the Developer: ");
            string devName = Console.ReadLine();
            createdDeveloper.DeveloperName = devName;
            Console.Clear();
            Console.WriteLine("Creating the developer...");
            rest.Post<Developer>(createdDeveloper, endpoint);
            Console.WriteLine("Developer added successfully!");
            Console.ReadKey();
        }
        void CreateCharacter(string endpoint)
        {
            Console.Clear();
            Character createdChar = new Character();
            Console.WriteLine("Name of the Character: ");
            string charName = Console.ReadLine();
            Console.WriteLine("Priority of the Character: ");
            int priority = int.Parse(Console.ReadLine());
            createdChar.CharacterName = charName;
            createdChar.Priority = priority;
            Console.Clear();
            Console.WriteLine("Creating the character...");
            rest.Post<Character>(createdChar, endpoint);
            Console.WriteLine("Character added successfully!");
            Console.ReadKey();
        }

        void UpdateGame(string endpoint)
        {
            Console.Clear();
            Game updated = new Game();
            Console.WriteLine("ID of the game: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Name of the game: ");
            string gameName = Console.ReadLine();
            Console.WriteLine("Price of the game: ");
            int price = int.Parse(Console.ReadLine());
            Console.WriteLine("Rating of the game: ");
            int rating = int.Parse(Console.ReadLine());
            Console.WriteLine("Release year of the game: ");
            int release = int.Parse(Console.ReadLine());
            updated.GameName = gameName;
            updated.Price = price;
            updated.Rating = rating;
            updated.Release = release;
            Console.Clear();
            Console.WriteLine("Updating...");
            rest.Put<Game>(updated,endpoint);
            Console.WriteLine("Game updated successfully!");
            Console.ReadKey();
        }
        void UpdateDeveloper(string endpoint)
        {
            Console.Clear();
            Developer updated = new Developer();
            Console.WriteLine("ID of the developer: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Name of the Developer: ");
            string devName = Console.ReadLine();
            updated.DeveloperName = devName;
            Console.Clear();
            Console.WriteLine("Updating...");
            rest.Post<Developer>(updated, endpoint);
            Console.WriteLine("Developer updated successfully!");
            Console.ReadKey();
        }
        void UpdateCharacter(string endpoint)
        {
            Console.Clear();
            Character updated = new Character();
            Console.WriteLine("ID of the character: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Name of the Character: ");
            string charName = Console.ReadLine();
            Console.WriteLine("Priority of the Character: ");
            int priority = int.Parse(Console.ReadLine());
            updated.CharacterName = charName;
            updated.Priority = priority;
            Console.Clear();
            Console.WriteLine("Updating...");
            rest.Post<Character>(updated, endpoint);
            Console.WriteLine("Character updated successfully!");
            Console.ReadKey();
        }

        void DeleteGame(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("ID of the game you want to delete: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Deleting...");
            rest.Delete(id, endpoint);
            Console.WriteLine("Game deleted successfully!");
            Console.ReadKey();
        }
        void DeleteDeveloper(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("ID of the developer you want to delete: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Deleting...");
            rest.Delete(id, endpoint);
            Console.WriteLine("Developer deleted successfully!");
            Console.ReadKey();
        }
        void DeleteCharacter(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("ID of the character you want to delete: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Deleting...");
            rest.Delete(id, endpoint);
            Console.WriteLine("character deleted successfully!");
            Console.ReadKey();
        }
        #endregion

        #region Non-CRUD
        void HighestRatedGame(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("The highest rated game: \n");
            var highest = rest.Get<object>(endpoint);
            foreach (var item in highest)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        void LowestRatedGame(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("The lowest rated game: \n");
            var lowest = rest.Get<object>(endpoint);
            foreach (var item in lowest)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        void OldestGame(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("The oldest game: \n");
            var oldest = rest.Get<object>(endpoint);
            foreach (var item in oldest)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        void YoungestGame(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("The youngest game: \n");
            var youngest = rest.Get<object>(endpoint);
            foreach (var item in youngest)
            { 
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        void OlderThan10(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("Games that are older than 10 years: \n");
            var olderGames = rest.Get<object>(endpoint);
            foreach (var item in olderGames)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        void GamesWithNpc(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("Games with NPCs: \n");
            var gamesWithNpc = rest.Get<object>(endpoint);
            foreach (var item in gamesWithNpc)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        void FreeGames(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("Free games: \n");
            var freeGames = rest.Get<object>(endpoint);
            foreach (var item in freeGames)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        void PaidGames(string endpoint)
        {
            Console.Clear();
            Console.WriteLine("Paid games: \n");
            var paidGames = rest.Get<object>(endpoint);
            foreach (var item in paidGames)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadKey();
        }
        #endregion
    }
}
