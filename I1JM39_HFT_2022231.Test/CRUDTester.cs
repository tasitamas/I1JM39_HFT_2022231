using I1JM39_HFT_2022231.Logic;
using I1JM39_HFT_2022231.Models;
using I1JM39_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace I1JM39_HFT_2022231.Test
{
    [TestFixture]
    public class CRUDTester
    {
        GameLogic gameLogic;
        DeveloperLogic devLogic;
        CharacterLogic charLogic;

        Mock<IRepository<Game>> mockGameRepo;
        Mock<IRepository<Developer>> mockDevRepo;
        Mock<IRepository<Character>> mockCharRepo;

        [SetUp]
        public void Init()
        {
            #region GameRepoSetup
            mockGameRepo = new Mock<IRepository<Game>>();
            mockGameRepo.Setup(g => g.ReadAll()).Returns(new List<Game>()
            {
                new Game("1#Counter Strike:Global Offensive#3500#8.0#2012*08*21#1"),
                new Game("2#VALORANT#0.00#9.2#2020*06*02#2"),
                new Game("3#World Of Warcraft#10000#8.1#2004*11*23#3"),
                new Game("4#League of Legends#0.00#5.0#2009*10*27#2"),
                new Game("5#Half Life#1500#9.5#1998*11*19#1"),
            }.AsQueryable());
            gameLogic = new GameLogic(mockGameRepo.Object);

            #endregion

            #region DevRepoSetup
            mockDevRepo = new Mock<IRepository<Developer>>();
            mockDevRepo.Setup(d => d.ReadAll()).Returns(new List<Developer>()
            {
                new Developer("1#Valve Corporation"),
                new Developer("2#Riot Games Inc."),
                new Developer("3#Blizzard Entertainment"),
                new Developer("4#Psyonix Inc."),
                new Developer("5#Behaviour Interactive"),
                new Developer("6#Rockstar Games")
            }.AsQueryable());
            devLogic = new DeveloperLogic(mockDevRepo.Object);
            #endregion

            #region CharRepoSetup
            mockCharRepo = new Mock<IRepository<Character>>();
            mockCharRepo.Setup(c => c.ReadAll()).Returns(new List<Character>()
            {
                new Character("1#Counter Terrorist#1#1"),
                new Character("2#Terrorist#1#1"),
                new Character("3#Jett#2#2"),
                new Character("4#Reyna#2#2"),
                new Character("5#Raze#1#2"),
                new Character("6#Omen#1#2"),
                new Character("7#Brimstone#1#2"),
            }.AsQueryable());
            charLogic = new CharacterLogic(mockCharRepo.Object);
            #endregion
        }

        #region Game tests
        [Test]
        public void CreateGameWithCorrectNameTest()
        {
            var game = new Game() { GameName = "Overwatch 2" };

            //ACT
            gameLogic.Create(game);

            //ASSERT
            mockGameRepo.Verify(g => g.Create(game), Times.Once);
        }
        [Test]
        public void CreateGameWithIncorrectNameTest()
        {
            var game = new Game() { GameName = "G"};

            try
            {
                //ACT
                gameLogic.Create(game);
            }
            catch{}

            //ASSERT
            mockGameRepo.Verify(g => g.Create(game), Times.Never);
        }
        [Test]
        public void DeleteGameTest()
        {
            //ACT
            gameLogic.Delete(1);

            //ASSERT
            mockGameRepo
                .Verify(g => g.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void ReadGameWithValidIDTest()
        {
            Game expected = new Game()
            {
                GameId = 3,
                GameName = "World of Warcraft",
                Price = 10000
            };

            mockGameRepo
                .Setup(g => g.Read(3))
                .Returns(expected);

            //ACT
            var actual = gameLogic.Read(3);

            //ASSERT
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ReadGameWithInvalidIDTest()
        {
            //ARRANGE
            mockGameRepo
                .Setup(g => g.Read(It.IsAny<int>()))
                .Returns(value: null);

            //ACT + ASSERT
            Assert.Throws<ArgumentException>(() => gameLogic.Read(100));
        }
        #endregion

        #region Developer tests
        [Test]
        public void CreateDevWithCorrectNameTest()
        {
            var dev = new Developer() { DeveloperName = "Psyonix Inc" };

            //ACT
            devLogic.Create(dev);

            //ASSERT
            mockDevRepo.Verify(d => d.Create(dev), Times.Once);
        }
        [Test]
        public void CreateDevWithIncorrectNameTest()
        {
            var dev = new Developer() { DeveloperName = "D" };

            try
            {
                //ACT
                devLogic.Create(dev);
            }
            catch { }

            //ASSERT
            mockDevRepo.Verify(d => d.Create(dev), Times.Never);
        }
        [Test]
        public void DeleteDevTest()
        {
            //ACT
            devLogic.Delete(1);

            //ASSERT
            mockDevRepo
                .Verify(d => d.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void ReadDevWithValidIDTest()
        {
            Developer expected = new Developer()
            {
                DeveloperId = 3,
                DeveloperName = "Blizzard Entertainment",
            };

            mockDevRepo
                .Setup(d => d.Read(3))
                .Returns(expected);

            //ACT
            var actual = devLogic.Read(3);

            //ASSERT
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ReadDevWithInvalidIDTest()
        {
            //ARRANGE
            mockDevRepo
                .Setup(d => d.Read(It.IsAny<int>()))
                .Returns(value: null);

            //ACT + ASSERT
            Assert.Throws<ArgumentException>(() => devLogic.Read(100));
        }
        #endregion

        #region Character tests
        [Test]
        public void CreateCharWithCorrectNameTest()
        {
            var character = new Character() { CharacterName = "Michael Myers" };

            //ACT
            charLogic.Create(character);

            //ASSERT
            mockCharRepo.Verify(c => c.Create(character), Times.Once);
        }
        [Test]
        public void CreateCharWithIncorrectNameTest()
        {
            var character = new Character() { CharacterName = "C" };

            try
            {
                //ACT
                charLogic.Create(character);
            }
            catch { }

            //ASSERT
            mockCharRepo.Verify(c => c.Create(character), Times.Never);
        }
        [Test]
        public void DeleteCharTest()
        {
            //ACT
            charLogic.Delete(1);

            //ASSERT
            mockCharRepo
                .Verify(c => c.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void ReadCharWithValidIDTest()
        {
            Character expected = new Character()
            {
                CharacterName = "Michael Myers",
                Priority = 1
            };

            mockCharRepo
                .Setup(d => d.Read(3))
                .Returns(expected);

            //ACT
            var actual = charLogic.Read(3);

            //ASSERT
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ReadCharWithInvalidIDTest()
        {
            //ARRANGE
            mockCharRepo
                .Setup(d => d.Read(It.IsAny<int>()))
                .Returns(value: null);

            //ACT + ASSERT
            Assert.Throws<ArgumentException>(() => charLogic.Read(100));
        }
        #endregion
    }
}
