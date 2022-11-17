using I1JM39_HFT_2022231.Logic;
using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I1JM39_HFT_2022231.Test
{
    [TestFixture]
    public class GameLogicTester
    {
        GameLogic logic;
        Mock<IRepository<Game>> mockGameRepo;
        Mock<IRepository<Developer>> mockDevRepo;
        Mock<IRepository<Character>> mockCharRepo;

        [SetUp]
        public void Init()
        {
            mockGameRepo = new Mock<IRepository<Game>>();
            mockDevRepo = new Mock<IRepository<Developer>>();
            mockCharRepo = new Mock<IRepository<Character>>();

            mockGameRepo.Setup(g => g.ReadAll()).Returns(new List<Game>()
            {
                new Game("1#Counter Strike:Global Offensive#3500#8.0#2012#1"),
                new Game("2#VALORANT#0.00#9.2#2020#2"),
                new Game("3#World Of Warcraft#10000#8.1#2004#3"),
                new Game("4#League of Legends#0.00#5.0#2009#2"),
                new Game("5#Half Life#1500#9.5#1998#1"),
                new Game("6#Team Fortress 2#0.00#7.9#2007#1"),
                new Game("7#Rocket League#3000#9.7#2015#4"),
            }.AsQueryable());

            mockDevRepo.Setup(d => d.ReadAll()).Returns(new List<Developer>()
            {
                new Developer("1#Valve Corporation"),
                new Developer("2#Riot Games Inc."),
                new Developer("3#Blizzard Entertainment"),
                new Developer("4#Psyonix Inc."),
                new Developer("5#Behaviour Interactive"),
                new Developer("6#Rockstar Games")
            }.AsQueryable());

            mockCharRepo.Setup(c => c.ReadAll()).Returns(new List<Character>()
            {
                new Character("1#Counter Terrorist#1#1"),
                new Character("2#Terrorist#1#1"),

                new Character("3#Jett#2#2"),
                new Character("6#Omen#1#2"),


                new Character("12#Kil'jaeden#2#3"),
                new Character("13#Archimonde#2#3"),
                new Character("16#Headless Horseman#3#3"),
                new Character("17#Chub#3#3"),

                new Character("23#G-Man#1#5"),
                new Character("24#Wallace Breen#3#5"),


            }.AsQueryable());

            logic = new GameLogic(mockGameRepo.Object, mockDevRepo.Object, mockCharRepo.Object);
        }

        [Test]
        public void OldestGameTest()
        {
            var actual = logic.OldestGameWithDeveloperName().ToList();
            var expected = new List<BasicGameInfo>()
            {
                new BasicGameInfo()
                { 
                    GameName = "Half Life",
                    DevName = "Valve Corporation",
                    Age = 24,
                }
            };

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void YoungestGameTest()
        {
            var actual = logic.YoungestGameWithDeveloperName().ToList();
            var expected = new List<BasicGameInfo>()
            {
                new BasicGameInfo()
                {
                    GameName = "VALORANT",
                    DevName = "Riot Games Inc.",
                    Age = 2,
                }
            };

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void OlderThan10GamesTest()
        {
            var actual = logic.OlderThan10YearsGames().ToList();

            var expected = new List<BasicGameInfo>()
            {
                new BasicGameInfo()
                {
                    DevName = "Blizzard Entertainment",
                    GameName = "World Of Warcraft",
                    Age = (int)DateTime.Now.Year - 2004,
                },
                new BasicGameInfo()
                {
                    DevName = "Riot Games Inc.",
                    GameName = "League of Legends",
                    Age = (int)DateTime.Now.Year - 2009,
                },
                new BasicGameInfo()
                {
                    DevName = "Valve Corporation",
                    GameName = "Half Life",
                    Age = (int)DateTime.Now.Year - 1998,
                },
                new BasicGameInfo()
                {
                    DevName = "Valve Corporation",
                    GameName = "Team Fortress 2",
                    Age = (int)DateTime.Now.Year - 2007,
                }
            };

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GamesWithNPCTest()
        {
            var actual = logic.GamesWithNpc().ToList();

            var expected = new List<BasicGameInfo>()
            { 
                new BasicGameInfo()
                { 
                    DevName = "Blizzard Entertainment",
                    GameName = "World Of Warcraft",
                    Age = 18,
                },
                new BasicGameInfo()
                { 
                    DevName = "Valve Corporation",
                    GameName = "Half Life",
                    Age = 24,
                },
            };

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void HighestRatingGameTest()
        {
            var actual = logic.HighestRatingGameWithDevName().ToList();

            var expected = new List<RatingInfo>()
            {
                new RatingInfo()
                {
                    GameName = "Rocket League",
                    DevName = "Psyonix Inc.",
                    Rating = 9.7,
                    Price = 3000,
                }
            };

            Assert.AreEqual(expected,actual);
        }
        [Test]
        public void LowestRatingGameTest()
        {
            var actual = logic.LowestRatingGameWithDevName().ToList();

            var expected = new List<RatingInfo>()
            {
                new RatingInfo()
                {
                    GameName = "League of Legends",
                    DevName = "Riot Games Inc.",
                    Rating = 5.0,
                    Price = 0,
                }
            };

            Assert.AreEqual(expected, actual);
        }
    }
}
