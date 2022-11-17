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
            mockRepo.Setup(c => c.ReadAll()).Returns(new List<Character>()
            {
                new Character("1#Counter Terrorist#1#1"),
                new Character("2#Terrorist#1#1"),
                new Character("3#Jett#2#2"),
                new Character("4#Reyna#2#2"),
                new Character("5#Raze#1#2"),
                new Character("6#Omen#1#2"),
                new Character("7#Brimstone#1#2"),
            }.AsQueryable());
            logic = new CharacterLogic(mockRepo.Object);
        }

        #region Create tests
        [Test]
        public void CreateCharWithCorrectNameTest()
        {
            var created = new Character() 
            { 
                CharacterName = "Michael Myers", 
                Priority = 1 
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
                CharacterName = "", 
                Priority = 1 
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
            var created = new Character() { CharacterName = null, Priority = 1 };

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
            var created = new Character() { CharacterName = String.Empty, Priority = 1 };

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
        public void CreateCharWithLongerThan50NameTest()
        {
            var created = new Character()
            {
                CharacterName = "   thisisarandomtestsentence" +
                                "   whichislongerthanfiftycha" +
                                "   racterslong",
                Priority = 1
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
        public void CreateCharWithIncorrectPriorityTest()
        {
            var created = new Character() 
            { 
                CharacterName = "Michael Myers", 
                Priority = 0 
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
        public void CreateCharWithIncorrectPriorityTest2()
        {
            var created = new Character() { CharacterName = "Michael Myers", Priority = 4 };

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
        public void ReadAllCharTest()
        {
            #region Expected
            var expected = new Character[]
                {
                new Character()
                {
                    CharacterId = 1,
                    CharacterName = "Counter Terrorist",
                    Priority = 1
                },
                new Character()
                {
                    CharacterId = 2,
                    CharacterName = "Terrorist",
                    Priority = 1
                },
                new Character()
                {
                    CharacterId = 3,
                    CharacterName = "Jett",
                    Priority = 2
                },
                new Character()
                {
                    CharacterId = 4,
                    CharacterName = "Reyna",
                    Priority = 2
                },
                new Character()
                {
                    CharacterId = 5,
                    CharacterName = "Raze",
                    Priority = 1
                },
                new Character()
                {
                    CharacterId = 6,
                    CharacterName = "Omen",
                    Priority = 1
                },
                new Character()
                {
                    CharacterId = 7,
                    CharacterName = "Brimstone",
                    Priority = 1
                },
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
            Character expected = new Character()
            {
                CharacterId = 3,
                CharacterName = "Jett",
                Priority = 2
            };

            mockRepo
                .Setup(d => d.Read(3))
                .Returns(expected);

            //ACT
            var actual = logic.Read(3);

            //ASSERT
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ReadCharWithInvalidIDTest()
        {
            try
            {
                logic.Read(0);
            }
            catch{}

            //ACT + ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => logic.Read(0));
        }
        #endregion

        #region Update tests
        [Test]
        public void UpdateCharCorrectTest()
        {
            var updated = new Character()
            { 
                CharacterId = 1,
                CharacterName = "Terrorist 2",
                Priority = 2
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
            catch{}

            //ASSERT
            Assert.Throws<NullReferenceException>(() => logic.Update(updated));
        }
        [Test]
        public void UpdateCharWithIncorrectNameTest()
        {
            var updated = new Character()
            { 
                CharacterId= 1,
                CharacterName = "",
                Priority = 1
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
                Priority = 1
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
                Priority = 1
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
        public void UpdateCharWithIncorrectPriorityTest()
        {
            var updated = new Character()
            {
                CharacterName = "Michael Myers",
                Priority = 0
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
        public void UpdateCharWithIncorrectPriorityTest2()
        {
            var updated = new Character()
            {
                CharacterName = "Michael Myers",
                Priority = 4
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
        public void UpdateCharWithLongerThan50NameTest()
        {
            var updated = new Character()
            {
                CharacterName = "   thisisarandomtestsentence" +
                                "   whichislongerthanfiftycha" +
                                "   racterslong",
                Priority = 1
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
        public void DeleteCharWithCorrectIdTest()
        {
            //ACT
            logic.Delete(1);

            //ASSERT
            mockRepo
                .Verify(c => c.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void DeleteCharWithIncorrectIdTest()
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
    }
}
