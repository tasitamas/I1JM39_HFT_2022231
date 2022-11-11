using I1JM39_HFT_2022231.Repository;
using I1JM39_HFT_2022231.Logic;
using System;
using System.Linq;
using System.Collections.Generic;

namespace I1JM39_HFT_2022231.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ctx = new GameDbContext();

            var gameRepo = new GameRepository(ctx);
            var devRepo = new DeveloperRepository(ctx);
            var charRepo = new CharacterRepository(ctx);

            var gameLogic = new GameLogic(gameRepo, devRepo, charRepo);

            var oldest = gameLogic.OldestGameWithDeveloperName();
            var youngest = gameLogic.YoungestGameWithDeveloperName();
            var olderThan10 = gameLogic.OlderThan10YearsGames();
            var highestRating = gameLogic.HighestRatingGameWithDevName();
            var gamesWithNpc = gameLogic.GamesWithNpc();
            var freeGames = gameLogic.FreeGames();
            var paidGames = gameLogic.PaidGames();
            ;
        }
    }
}
