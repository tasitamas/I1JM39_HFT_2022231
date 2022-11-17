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

        #region CRUD Methods tests

        #region Create tests
        [Test]
        public void CreateGameWithCorrectNameTest()
        {
            var created = new Game()
                { 
                    GameId = 8,
                    GameName = "Overwatch 2",
                    Price = 0.00,
                    Rating = 7.8,
                    Release = 2022,
                };

            //ACT
            logic.Create(created);

            //ASSERT
            mockGameRepo
                .Verify(c => c.Create(created), Times.Once);
        }
        [Test]
        public void CreateGameWithNullNameTest()
        {
            var created = new Game()
            {
                GameId = 8,
                GameName = null,
                Price = 0.00,
                Rating = 7.8,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Create(created);
            }
            catch { }

            //ASSERT
            Assert.Throws<NullReferenceException>(() => logic.Create(created));
        }
        [Test]
        public void CreateGameWithIncorrectNameTest()
        {
            var created = new Game()
            {
                GameId = 8,
                GameName = "",
                Price = 0.00,
                Rating = 7.8,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Create(created);
            }
            catch { }

            //ASSERT
            Assert.Throws<NullReferenceException>(() => logic.Create(created));
        }
        [Test]
        public void CreateGameWithStringEmptyNameTest()
        {
            var created = new Game()
            {
                GameId = 8,
                GameName = String.Empty,
                Price = 0.00,
                Rating = 7.8,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Create(created);
            }
            catch { }

            //ASSERT
            Assert.Throws<NullReferenceException>(() => logic.Create(created));
        }
        [Test]
        public void CreateGameWithIncorrectPriceTest()
        {
            var created = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = -1,
                Rating = 7.8,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Create(created);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Create(created));
        }
        [Test]
        public void CreateGameWithIncorrectPriceTest2()
        {
            var created = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 100000,
                Rating = 7.8,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Create(created);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Create(created));
        }
        [Test]
        public void CreateGameWithIncorrectRatingTest()
        {
            var created = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 0.00,
                Rating = -1,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Create(created);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Create(created));
        }
        [Test]
        public void CreateGameWithIncorrectRatingTest2()
        {
            var created = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 0.00,
                Rating = 11,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Create(created);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Create(created));
        }
        [Test]
        public void CreateGameWithIncorrectReleaseTest()
        {
            var created = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 0.00,
                Rating = 0.00,
                Release = 1500,
            };

            try
            {
                //ACT
                logic.Create(created);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Create(created));
        }
        [Test]
        public void CreateGameWithIncorrectReleaseTest2()
        {
            var created = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 0.00,
                Rating = 0.00,
                Release = 2050,
            };

            try
            {
                //ACT
                logic.Create(created);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Create(created));
        }
        [Test]
        public void CreateGameWithLongerThan150NameTest()
        {
            var created = new Game()
            {
                GameId = 8,
                GameName =  "This is basically a filling sentence, " +
                            "that I'm trying to write to test my te" +
                            "stcases to see if they are working cor" +
                            "rectly or not. I'm hoping for it that " +
                            "this will be longer than 150 characters.",
                Price = 0.00,
                Rating = 0.00,
                Release = 2020,
            };

            try
            {
                //ACT
                logic.Create(created);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Create(created));
        }
        #endregion

        #region ReadAll test
        [Test]
        public void ReadAllGameTest()
        {
            #region Expected
            var expected = new Game[]
                { 
                    new Game()
                    {
                        GameId = 1,
                        GameName = "Counter Strike:Global Offensive",
                        Price = 3500,
                        Rating = 8.0,
                        Release = 2012,
                    },
                    new Game()
                    {
                        GameId = 2,
                        GameName = "VALORANT",
                        Price = 0.00,
                        Rating = 9.2,
                        Release = 2020,
                    },
                    new Game()
                    {
                        GameId = 3,
                        GameName = "World Of Warcraft",
                        Price = 10000,
                        Rating = 8.1,
                        Release = 2004,
                    },
                    new Game()
                    {
                        GameId = 1,
                        GameName = "League of Legends",
                        Price = 0.00,
                        Rating = 5.0,
                        Release = 2009,
                    },
                    new Game()
                    {
                        GameId = 5,
                        GameName = "Half Life",
                        Price = 1500,
                        Rating = 9.5,
                        Release = 1998,
                    },
                    new Game()
                    {
                        GameId = 6,
                        GameName = "Team Fortress 2",
                        Price = 0.00,
                        Rating = 7.9,
                        Release = 2007,
                    },
                    new Game()
                    {
                        GameId = 1,
                        GameName = "Rocket League",
                        Price = 3000,
                        Rating = 9.7,
                        Release = 2015,
                    },
                }.AsQueryable();
            #endregion
            mockGameRepo
                   .Setup(d => d.ReadAll())
                   .Returns(expected);

            //ACT
            var actual = logic.ReadAll();

            //ASSERT
            Assert.That(actual, Is.EqualTo(expected));
        }
        #endregion

        #region Read tests
        [Test]
        public void ReadGameWithCorrectIDTest()
        {
            Game expected = new Game()
            {
                GameId = 1,
                GameName = "Counter Strike:Global Offensive",
                Price = 3500,
                Rating = 8.0,
                Release = 2012
            };

            mockGameRepo
                .Setup(d => d.Read(1))
                .Returns(expected);

            //ACT
            var actual = logic.Read(1);

            //ASSERT
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ReadGameWithIncorrectIDTest()
        {
            try
            {
                logic.Read(0);
            }
            catch { }

            //ACT + ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Read(0));
        }
        #endregion

        #region Update tests
        [Test]
        public void UpdateGameCorrectTest()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 0.00,
                Rating = 7.8,
                Release = 2022,
            };

            //ACT
            logic.Update(updated);

            //ASSERT
            mockGameRepo.Verify(c => c.Update(updated), Times.Once);
        }
        [Test]
        public void UpdateGameWithNullTest()
        {
            var updated = new Game();

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<NullReferenceException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateGameWithNullNameTest()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = null,
                Price = 0.00,
                Rating = 7.8,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<NullReferenceException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateGameWithIncorrectNameTest()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = "",
                Price = 0.00,
                Rating = 7.8,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<NullReferenceException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateGameWithStringEmptyNameTest()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = String.Empty,
                Price = 0.00,
                Rating = 7.8,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<NullReferenceException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateGameWithIncorrectPriceTest()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = -1,
                Rating = 7.8,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateGameWithIncorrectPriceTest2()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 60000,
                Rating = 7.8,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateGameWithIncorrectRatingTest()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 1500,
                Rating = -1,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateGameWithIncorrectRatingTest2()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 1500,
                Rating = 11,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateGameWithIncorrectReleaseTest()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 1500,
                Rating = 5,
                Release = 1500,
            };

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateGameWithIncorrectReleaseTest2()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = "Overwatch 2",
                Price = 1500,
                Rating = 5,
                Release = 2500,
            };

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateGameWithLongerThan150NameTest()
        {
            var updated = new Game()
            {
                GameId = 8,
                GameName = "This is basically a filling sentence, " +
                            "that I'm trying to write to test my te" +
                            "stcases to see if they are working cor" +
                            "rectly or not. I'm hoping for it that " +
                            "this will be longer than 150 characters.",
                Price = 1500,
                Rating = 5,
                Release = 2022,
            };

            try
            {
                //ACT
                logic.Update(updated);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Update(updated));
        }
        #endregion

        #region Delete tests
        [Test]
        public void DeleteGameWithCorrectIDTest()
        {
            //ACT
            logic.Delete(1);

            //ASSERT
            mockGameRepo
                .Verify(d => d.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void DeleteGameWithIncorrectIDTest()
        {
            try
            {
                //ACT
                logic.Delete(0);
            }
            catch { }

            //ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Delete(0));
        }
        #endregion

        #endregion

        #region Non CRUD Methods tests
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
        #endregion
    }
}
