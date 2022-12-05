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
    public class CharacterLogicTester
    {
        CharacterLogic logic;

        Mock<IRepository<Character>> mockRepo;

        [SetUp]
        public void Init()
        {
            mockRepo = new Mock<IRepository<Character>>();

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
            };

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

            mockRepo.Setup(g => g.Create(It.IsAny<Character>()));
            mockRepo.Setup(c => c.ReadAll()).Returns(characters);

            logic = new CharacterLogic(mockRepo.Object);
        }

        #region Create tests
        [Test]
        public void CreateCharWithCorrectNameTest()
        {
            var created = new Character()
            {
                CharacterId = 1,
                CharacterName = "Test Character 1",
                Priority = 1,
                GameId = 1,
            };

            //ACT
            logic.Create(created);

            //ASSERT
            mockRepo.Verify(c => c.Create(created), Times.Once);
        }
        [Test]
        public void CreateCharWithNullNameTest()
        {
            var created = new Character()
            {
                CharacterId = 1,
                CharacterName = null,
                Priority = 1,
                GameId = 1,
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
        public void CreateCharWithStringEmptyNameTest()
        {
            var created = new Character()
            {
                CharacterId = 1,
                CharacterName = String.Empty,
                Priority = 1,
                GameId = 1,
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
        public void CreateCharWithIncorrectNameTest()
        {
            var created = new Character()
            {
                CharacterId = 1,
                CharacterName = "",
                Priority = 1,
                GameId = 1,
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
        #endregion

        #region ReadAll test
        [Test]
        public void ReadAllCharTest()
        {
            #region Expected
            var expected = new Character[]
                {
                    new Character("1#Test Character0#1#1"),
                    new Character("2#Test Character1#2#1"),
                    new Character("3#Test Character2#2#1"),
                    new Character("4#Test Character3#1#2"),
                    new Character("5#Test Character4#2#2"),
                    new Character("6#Test Character5#3#2"),
                    new Character("7#Test Character6#3#3"),
                    new Character("8#Test Character7#2#3"),
                    new Character("9#Test Character8#3#3"),
                }.AsQueryable();
            #endregion
            mockRepo
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
        public void ReadCharWithValidIDTest()
        {
            Character expected = new Character("2#Test Character1#2#1");

            mockRepo
                .Setup(d => d.Read(2))
                .Returns(expected);

            //ACT
            var actual = logic.Read(2);

            //ASSERT
            Assert.That(actual, Is.EqualTo(expected));
        }
        #endregion

        #region Update tests
        [Test]
        public void UpdateCharCorrectTest()
        {
            var updated = new Character()
            {
                CharacterId = 1,
                CharacterName = "Test Character 4",
                Priority = 1,
                GameId = 1,
            };

            //ACT
            logic.Update(updated);

            //ASSERT
            mockRepo.Verify(c => c.Update(updated), Times.Once);
        }
        [Test]
        public void UpdateCharWithNullTest()
        {
            var updated = new Character();

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
        public void UpdateCharWithIncorrectNameTest()
        {
            var updated = new Character()
            {
                CharacterId = 1,
                CharacterName = "",
                Priority = 1,
                GameId = 1,
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
        public void UpdateCharWithNullNameTest()
        {
            var updated = new Character()
            {
                CharacterId = 1,
                CharacterName = null,
                Priority = 1,
                GameId = 1,
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
        public void UpdateCharWithStringEmptyNameTest()
        {
            var updated = new Character()
            {
                CharacterId = 1,
                CharacterName = String.Empty,
                Priority = 1,
                GameId = 1,
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
        public void UpdateCharWithLongerThan50NameTest()
        {
            var updated = new Character()
            {
                CharacterId = 1,
                CharacterName = "   thisisarandomtestsentence" +
                                "   whichislongerthanfiftycha" +
                                "   racterslong",
                Priority = 1,
                GameId = 1,
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

    }
}
