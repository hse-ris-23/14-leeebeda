using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab_14;
using System;
using System.Collections.Generic;
using System.Linq;
using ClassLib;
using lab;

namespace lab_14_tests
{
    [TestClass]
    public class ProgramTests
    {
        private SortedDictionary<string, List<Instrument>> GetSampleCollection()
        {
            var instruments1 = new List<Instrument>
            {
                new Guitar { Name = "Casio", id = 1 },
                new Guitar { Name = "Yamaha", id = 2 },
                new Guitar { Name = "Casio", id = 3 },
                new Guitar { Name = "Yamaha", id = 4 },
                new Guitar { Name = "Fender", id = 5 }
            };

            var instruments2 = new List<Instrument>
            {
                new Guitar { Name = "Casio", id = 6 },
                new Guitar { Name = "Yamaha", id = 7 },
                new Guitar { Name = "Casio", id = 8 },
                new Guitar { Name = "Yamaha", id = 9 },
                new Guitar { Name = "Gibson", id = 10 }
            };

            return new SortedDictionary<string, List<Instrument>>
            {
                { "Музыкальная группа 1", instruments1 },
                { "Музыкальная группа 2", instruments2 }
            };
        }

        private MyCollection<Instrument> GetSampleMyCollection()
        {
            var collection = new MyCollection<Instrument>()
            {
                new Instrument { Name = "Casio", id = 1 },
                new Instrument { Name = "Yamaha", id = 2 },
                new Instrument { Name = "Casio", id = 3 },
                new Instrument { Name = "Yamaha", id = 4 },
                new Instrument { Name = "Fender", id = 5 },
                new Instrument { Name = "Casio", id = 6 },
                new Instrument { Name = "Yamaha", id = 7 },
            };

            return collection;
        }

        [TestMethod]
        public void Test_SelectCasioYamaha()
        {
            var collection = GetSampleCollection();

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.SelectCasioYamaha(collection);

                var output = sw.ToString();

                Assert.IsTrue(output.Contains("Casio"));
                Assert.IsTrue(output.Contains("Yamaha"));
            }
        }

        [TestMethod]
        public void Test_SelectCasioYamaha2()
        {
            var collection = GetSampleMyCollection();

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.SelectCasioYamaha2(collection);

                var output = sw.ToString();
                var expectedCasioInstruments = new List<string>
                {
                    "Name: Casio, Id: 1",
                    "Name: Casio, Id: 3",
                    "Name: Casio, Id: 6"
                };
                var expectedYamahaInstruments = new List<string>
                {
                    "Name: Yamaha, Id: 2",
                    "Name: Yamaha, Id: 4",
                    "Name: Yamaha, Id: 7"
                };

                // Проверяем инструменты Casio
                foreach (var expected in expectedCasioInstruments)
                {
                    Assert.IsTrue(output.Contains(expected));
                }

                // Проверяем инструменты Yamaha
                foreach (var expected in expectedYamahaInstruments)
                {
                    Assert.IsTrue(output.Contains(expected));
                }
            }
        }

        [TestMethod]
        public void Test_UnionGroups()
        {
            var collection = GetSampleCollection();

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.UnionGroups(collection);

                var output = sw.ToString();

                Assert.IsTrue(output.Contains("Casio"));
                Assert.IsTrue(output.Contains("Yamaha"));
                Assert.IsTrue(output.Contains("Fender"));
                Assert.IsTrue(output.Contains("Gibson"));
            }
        }

        [TestMethod]
        public void Test_CountCasioYamaha()
        {
            var collection = GetSampleMyCollection();

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.CountCasioYamaha(collection);

                var output = sw.ToString();
                var expectedCasioCount = "Количество инструментов Casio: 3";
                var expectedYamahaCount = "Количество инструментов Yamaha: 3";

                // Находим строки с количеством инструментов
                var casioCountLine = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault(line => line.Contains("Количество инструментов Casio:"));
                var yamahaCountLine = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault(line => line.Contains("Количество инструментов Yamaha:"));

                // Проверяем найденные строки
                Assert.AreEqual(expectedCasioCount, casioCountLine?.Trim());
                Assert.AreEqual(expectedYamahaCount, yamahaCountLine?.Trim());
            }
        }


        [TestMethod]
        public void Test_MaxAverageId()
        {
            var collection = GetSampleCollection();

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.MaxAverageId(collection);

                var output = sw.ToString();

                Assert.IsTrue(output.Contains("Среднее значение показателя id всех инструментов:"));
                Assert.IsTrue(output.Contains("Максимальное значение показателя id всех инструментов: 10"));
            }
        }

        [TestMethod]
        public void Test_MaxAverageId2()
        {
            var collection = GetSampleMyCollection();

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.MaxAverageId2(collection);

                var output = sw.ToString();
                var expectedAverageId = "Среднее значение показателя id всех инструментов: 4";
                var expectedMaxId = "Максимальное значение показателя id всех инструментов: 7";

                // Проверяем среднее значение id
                Assert.IsTrue(output.Contains(expectedAverageId));

                // Проверяем максимальное значение id
                Assert.IsTrue(output.Contains(expectedMaxId));
            }
        }

        [TestMethod]
        public void Test_GroupByInstrumentName()
        {
            var collection = GetSampleCollection();

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.GroupByInstrumentName(collection);

                var output = sw.ToString();

                Assert.IsTrue(output.Contains("Casio"));
                Assert.IsTrue(output.Contains("Yamaha"));
                Assert.IsTrue(output.Contains("Fender"));
                Assert.IsTrue(output.Contains("Gibson"));
            }
        }

        [TestMethod]
        public void Test_GroupByInstrumentName2()
        {
            var collection = GetSampleMyCollection();

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.GroupByInstrumentName2(collection);

                var output = sw.ToString();

                Assert.IsTrue(output.Contains("Casio"));
                Assert.IsTrue(output.Contains("Yamaha"));
                Assert.IsTrue(output.Contains("Fender"));
            }
        }

        [TestMethod]
        public void Test_CreateNewId()
        {
            var collection = GetSampleCollection();

            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);
                Program.CreateNewId(collection);

                var output = sw.ToString();

                Assert.IsTrue(output.Contains("NewID"));
            }
        }
    }
}
