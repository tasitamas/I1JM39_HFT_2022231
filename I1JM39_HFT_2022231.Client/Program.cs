using System;
using System.Linq;
using System.Collections.Generic;
using I1JM39_HFT_2022231.Models;
using ConsoleTools;
using I1JM39_HFT_2022231.Repository;
using I1JM39_HFT_2022231.Logic;

namespace I1JM39_HFT_2022231.Client
{
    internal class Program
    {
        static RestService rest;
        #region CRUD
        static void Create(string entity)
        {
            if (entity == "Game")
            {
                Console.Write("Enter game name:");
                string name = Console.ReadLine();
                rest.Post(new Game() { GameName = name }, "game");
            }
            else if (entity == "Developer")
            {
                Console.Write("Enter a developer name:");
                string name = Console.ReadLine();
                rest.Post(new Developer() { DeveloperName = name }, "developer");
            }
            else if (entity == "Character")
            {

                Console.Write("Enter a character name:");
                string name = Console.ReadLine();
                rest.Post(new Character() { CharacterName = name }, "character");
            }
        }
        static void List(string entity)
        {
            if (entity == "Game")
            {
                List<Game> games = rest.Get<Game>("actor");
                foreach (var item in games)
                {
                    Console.WriteLine($"{item.GameId}: {item.GameName}");
                }
            }
            else if (entity == "Developer")
            {
                List<Developer> devs = rest.Get<Developer>("developer");
                foreach (var item in devs)
                {
                    Console.WriteLine($"{item.DeveloperId}: {item.DeveloperName}");
                }
            }
            else if (entity == "Character")
            {
                List<Character> characters = rest.Get<Character>("character");
                foreach (var item in characters)
                {
                    Console.WriteLine($"{item.CharacterId}: {item.CharacterName}");
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Game")
            {
                Console.Write("Enter Game's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Game one = rest.Get<Game>(id, "game");
                Console.Write($"New name [old: {one.GameName}]: ");
                string name = Console.ReadLine();
                one.GameName = name;
                rest.Put(one, "game");
            }
            else if (entity == "Developer")
            {
                Console.Write("Enter Dev's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Developer one = rest.Get<Developer>(id, "developer");
                Console.Write($"New name [old: {one.DeveloperName}]: ");
                string name = Console.ReadLine();
                one.DeveloperName = name;
                rest.Put(one, "developer");
            }
            else if (entity == "Character")
            {
                Console.Write("Enter Character's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Character one = rest.Get<Character>(id, "character");
                Console.Write($"New name [old: {one.CharacterName}]: ");
                string name = Console.ReadLine();
                one.CharacterName = name;
                rest.Put(one, "character");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Game")
            {
                Console.Write("Enter Game's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "game");
            }
            else if (entity == "Developer")
            {
                Console.Write("Enter Dev's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "developer");
            }
            else if (entity == "Character")
            {
                Console.Write("Enter Character's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "character");
            }
        }
        #endregion
        #region Non-CRUD

        #endregion
        static void Main(string[] args)
        {
            //rest = new RestService("http://localhost:23247", "game");

            //#region Console Menu
            //var gameSubMenu = new ConsoleMenu(args, level: 1)
            //    .Add("List", () => List("Game"))
            //    .Add("Create", () => Create("Game"))
            //    .Add("Delete", () => Delete("Game"))
            //    .Add("Update", () => Update("Game"))
            //    .Add("Exit", ConsoleMenu.Close);

            //var devSubMenu = new ConsoleMenu(args, level: 1)
            //    .Add("List", () => List("Developer"))
            //    .Add("Create", () => Create("Developer"))
            //    .Add("Delete", () => Delete("Developer"))
            //    .Add("Update", () => Update("Developer"))
            //    .Add("Exit", ConsoleMenu.Close);

            //var characterSubMenu = new ConsoleMenu(args, level: 1)
            //    .Add("List", () => List("Character"))
            //    .Add("Create", () => Create("Character"))
            //    .Add("Delete", () => Delete("Character"))
            //    .Add("Update", () => Update("Character"))
            //    .Add("Exit", ConsoleMenu.Close);

            //var menu = new ConsoleMenu(args, level: 0)
            //    .Add("Games", () => gameSubMenu.Show())
            //    .Add("Developers", () => devSubMenu.Show())
            //    .Add("Characters", () => characterSubMenu.Show())
            //    .Add("Exit", ConsoleMenu.Close);

            //menu.Show();
            //#endregion


            #region Noncrud tests TODELETE
            var ctx = new GameDbContext();

            var gameRepo = new GameRepository(ctx);
            var devRepo = new DeveloperRepository(ctx);
            var charRepo = new CharacterRepository(ctx);

            var gameLogic = new GameLogic(gameRepo, devRepo, charRepo);

            var q1 = gameLogic.OldestGameWithDeveloperName();
            var q2 = gameLogic.YoungestGameWithDeveloperName();
            var q3 = gameLogic.OlderThan10YearsGames();
            var q4 = gameLogic.GamesWithNpc();
            var q5 = gameLogic.HighestRatingGameWithDevName();
            var q6 = gameLogic.LowestRatingGameWithDevName();
            var q7 = gameLogic.FreeGames();
            var q8 = gameLogic.PaidGames();
            ;
            #endregion
        }
    }
}
