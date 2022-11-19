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

            var chars1 = new List<Character>()
            {
                new Character("1#Test Character0#1#1"),
                new Character("2#Test Character1#2#1"),
                new Character("3#Test Character2#2#1"),
            };
            var chars2 = new List<Character>()
            {
                new Character("4#Test Character3#1#2"),
                new Character("5#Test Character4#2#2"),
                new Character("6#Test Character5#3#2"),
            };
            var chars3 = new List<Character>()
            {
                new Character("7#Test Character6#3#3"),
                new Character("8#Test Character7#2#3"),
                new Character("9#Test Character8#3#3"),
                new Character("10#Test Character8#3#3"),
            };

            Developer dev1 = new Developer
            {
                DeveloperId = 1,
                DeveloperName = "Test Developer 1",
                GameId = 1,
            };
            Developer dev2 = new Developer
            {
                DeveloperId = 2,
                DeveloperName = "Test Developer 2",
                GameId = 2,
            };
            Developer dev3 = new Developer
            {
                DeveloperId = 3,
                DeveloperName = "Test Developer 3",
                GameId = 3,
            };
            var devs = new List<Developer>() { dev1, dev2, dev2 }.AsQueryable();

            Game game1 = new Game
            {
                GameId = 1,
                GameName = "Test Game",
                Price = 5000,
                Rating = 7.1,
                Release = 2020,
                Developer = dev1,
                Characters = chars1,
            };
            Game game2 = new Game
            {
                GameId = 2,
                GameName = "Test Game 2",
                Price = 0,
                Rating = 9.2,
                Release = 2000,
                Developer = dev2,
                Characters = chars2,
            };
            Game game3 = new Game
            {
                GameId = 3,
                GameName = "Test Game 3",
                Price = 20000,
                Rating = 5.7,
                Release = 2011,
                Developer = dev3,
                Characters = chars3,
            };
            var games = new List<Game>() { game1, game2, game3 }.AsQueryable();

            dev1.Game = game1;
            dev2.Game = game2;
            dev3.Game = game3;

            foreach (var item in chars1)
            {
                item.Game = game1;
            }
            foreach (var item in chars2)
            {
                item.Game = game2;
            }
            foreach (var item in chars3)
            {
                item.Game = game3;
            }

            var cs = new List<Character>();
            foreach (var item in chars1)
            {
                cs.Add(item);
            }
            foreach (var item in chars2)
            {
                cs.Add(item);
            }
            foreach (var item in chars3)
            {
                cs.Add(item);
            }

            var characters = cs.AsQueryable();
            mockDevRepo.Setup(d => d.ReadAll()).Returns(devs);
            mockCharRepo.Setup(c => c.ReadAll()).Returns(characters);

            mockGameRepo.Setup(g => g.Create(It.IsAny<Game>()));
            mockGameRepo.Setup(g => g.ReadAll()).Returns(games);

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
                GameName = "This is basically a filling sentence, " +
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
                    new Game
                    {
                        GameId = 1,
                        GameName = "Test Game",
                        Price = 5000,
                        Rating = 7.1,
                        Release = 2020,
                    },
                    new Game
                    {
                        GameId = 2,
                        GameName = "Test Game 2",
                        Price = 0,
                        Rating = 9.2,
                        Release = 2000,
                    },
                    new Game
                    {
                        GameId = 3,
                        GameName = "Test Game 3",
                        Price = 20000,
                        Rating = 5.7,
                        Release = 2011,
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
            var expected = new Game
            {
                GameId = 1,
                GameName = "Test Game",
                Price = 5000,
                Rating = 7.1,
                Release = 2020,
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
                GameId = 1,
                GameName = "Test Game 3",
                Price = 5000,
                Rating = 7.1,
                Release = 2020,
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
                GameId = 1,
                GameName = null,
                Price = 5000,
                Rating = 7.1,
                Release = 2020,
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
                GameId = 1,
                GameName = "",
                Price = 5000,
                Rating = 7.1,
                Release = 2020,
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
                GameId = 1,
                GameName = String.Empty,
                Price = 5000,
                Rating = 7.1,
                Release = 2020,
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
                GameId = 1,
                GameName = "Test Game 3",
                Price = -1,
                Rating = 7.1,
                Release = 2020,
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
                GameId = 1,
                GameName = "Test Game 3",
                Price = 500000,
                Rating = 7.1,
                Release = 2020,
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
                GameId = 1,
                GameName = "Test Game 3",
                Price = 5000,
                Rating = -1,
                Release = 2020,
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
                GameId = 1,
                GameName = "Test Game 3",
                Price = 5000,
                Rating = 11,
                Release = 2020,
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
                GameId = 1,
                GameName = "Test Game 3",
                Price = 5000,
                Rating = 7.1,
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
                GameId = 1,
                GameName = "Test Game 3",
                Price = 5000,
                Rating = 7.1,
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
        public void HighestRatingGameTest()
        {
            var expected = logic.HighestRatingGameWithDevName().ToList();

            Assert.That(expected[0], Is.EqualTo(new KeyValuePair<string, string>("Test Game 2", "Test Developer 2")));
        }
        [Test]
        public void LowestRatingGameTest()
        {
            var expected = logic.LowestRatingGameWithDevName().ToList();

            Assert.That(expected[0], Is.EqualTo(new KeyValuePair<string, string>("Test Game 3", "Test Developer 3")));
        }
        [Test]
        public void OldestGameTest()
        {
            var actual = logic.OldestGameWithDeveloperName().ToList();

            Assert.That(actual[0].GetHashCode(), Is.EqualTo(new { _GameName = "Test Game 2", _DevName = "Test Developer 2", _Age = 22 }.GetHashCode()));
        }
        [Test]
        public void YoungestGameTest()
        {
            var actual = logic.YoungestGameWithDeveloperName().ToList();

            Assert.That(actual[0].GetHashCode(), Is.EqualTo(new { _GameName = "Test Game", _DevName = "Test Developer 1", _Age = 2 }.GetHashCode()));
        }
        [Test]
        public void OlderThan10YearsGamesTest()
        {
            var actual = logic.OlderThan10YearsGames().ToList();
            var expected = new List<GameInfo>()
            {
                new GameInfo()
                {
                    GameName = "Test Game 2",
                    DevName = "Test Developer 2",
                    Age = 22,
                },
                new GameInfo()
                {
                    GameName = "Test Game 3",
                    DevName = "Test Developer 3",
                    Age = 11,
                },
            };

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void FreeGamesTest()
        {
            var expected = new List<RatingInfo>()
            {
                new RatingInfo()
                {
                    GameName = "Test Game 2",
                    DevName = "Test Developer 2",
                    Rating = 9.2
                }
            };

            var actual = logic.FreeGames().ToList();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void PaidGamesTest()
        {
            var expected = new List<RatingInfo>()
            {
                new RatingInfo()
                {
                    GameName = "Test Game",
                    DevName = "Test Developer 1",
                    Rating = 7.1
                },
                new RatingInfo()
                {
                    GameName = "Test Game 3",
                    DevName = "Test Developer 3",
                    Rating = 5.7
                }
            };

            var actual = logic.PaidGames().ToList();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GamesCharactersCountTest()
        {
            var expected = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>
                ("Test Game 3", 4),
                new KeyValuePair<string, int>
                ("Test Game", 3),
                new KeyValuePair<string, int>
                ("Test Game 2", 3)
            };

            var actual = logic.GamesCharactersCount().ToList();

            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
