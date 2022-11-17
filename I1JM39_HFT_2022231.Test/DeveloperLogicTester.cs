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
    public class DeveloperLogicTester
    {
        DeveloperLogic logic;

        Mock<IRepository<Developer>> mockRepo;

        [SetUp]
        public void Init()
        {
            mockRepo = new Mock<IRepository<Developer>>();
            mockRepo.Setup(d => d.ReadAll()).Returns(new List<Developer>()
            {
                new Developer("1#Valve Corporation"),
                new Developer("2#Riot Games Inc."),
                new Developer("3#Blizzard Entertainment"),
                new Developer("4#Psyonix Inc."),
                new Developer("5#Behaviour Interactive"),
                new Developer("6#Rockstar Games")
            }.AsQueryable());
            logic = new DeveloperLogic(mockRepo.Object);
        }

        #region Create tests
        [Test]
        public void CreateDevWithCorrectNameTest()
        {
            var created = new Developer()
            {
                DeveloperId = 7,
                DeveloperName = "Bethesda Inc."
            };

            //ACT
            logic.Create(created);

            //ASSERT
            mockRepo.Verify(c => c.Create(created), Times.Once);
        }
        [Test]
        public void CreateDevWithNullNameTest()
        {
            var created = new Developer()
            {
                DeveloperId = 7,
                DeveloperName = null,
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
        public void CreateDevWithIncorrectNameTest()
        {
            var created = new Developer()
            {
                DeveloperId = 7,
                DeveloperName = "",
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
        public void CreateDevWithStringEmptyNameTest()
        {
            var created = new Developer()
            {
                DeveloperId = 7,
                DeveloperName = String.Empty,
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
        public void CreateDevWithLongerThan100NameTest()
        {
            var created = new Developer()
            {
                DeveloperId= 7,
                DeveloperName = "imtryingtowritearandomunittest" +
                                "sentencewhichislongerthanahund" +
                                "redcharacterslongtotestmytestc" +
                                "aseifitworksgood"
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
        public void ReadAllDevTest()
        {
            #region Expected
            var expected = new Developer[]
                {
                    new Developer()
                    { 
                        DeveloperId = 1,
                        DeveloperName = "Valve Corporation",
                    },
                    new Developer()
                    {
                        DeveloperId = 2,
                        DeveloperName = "Riot Games Inc.",
                    },
                    new Developer()
                    {
                        DeveloperId = 3,
                        DeveloperName = "Blizzard Entertainment",
                    },
                    new Developer()
                    {
                        DeveloperId = 4,
                        DeveloperName = "Psyonix Inc.", 
                    },
                    new Developer()
                    {
                        DeveloperId = 5,
                        DeveloperName = "Behaviour Interactive",
                    },
                    new Developer()
                    {
                        DeveloperId = 6,
                        DeveloperName = "Rockstar Games",
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
        public void ReadDevWithValidIDTest()
        {
            Developer expected = new Developer()
            {
                DeveloperId = 3,
                DeveloperName = "Blizzard Entertainment",
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
        public void ReadDevWithInvalidIDTest()
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
        public void UpdateDevCorrectTest()
        {
            var updated = new Developer()
            {
                DeveloperId = 1,
                DeveloperName = "Bethesda Inc."
            };

            //ACT
            logic.Update(updated);

            //ASSERT
            mockRepo.Verify(c => c.Update(updated), Times.Once);
        }
        [Test]
        public void UpdateDevWithNullTest()
        {
            var updated = new Developer();

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
        public void UpdateDevWithNullNameTest()
        {
            var updated = new Developer()
            {
                DeveloperId = 1,
                DeveloperName = null,
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
        public void UpdateDevWithIncorrectNameTest()
        {
            var updated = new Developer()
            {
                DeveloperId = 1,
                DeveloperName = "",
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
        public void UpdateDevWithStringEmptyNameTest()
        {
            var updated = new Developer()
            {
                DeveloperId = 1,
                DeveloperName = String.Empty,
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
        public void UpdateDevWithLongerThan100NameTest()
        {
            var updated = new Developer()
            {
                DeveloperId = 1,
                DeveloperName = "imtryingtowritearandomunittest" +
                                "sentencewhichislongerthanahund" +
                                "redcharacterslongtotestmytestc" +
                                "aseifitworksgood",
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
        public void DeleteDevTest()
        {
            //ACT
            logic.Delete(1);

            //ASSERT
            mockRepo
                .Verify(d => d.Delete(It.IsAny<int>()), Times.Once);
        }
        [Test]
        public void DeleteDevWithIncorrectIDTest()
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

