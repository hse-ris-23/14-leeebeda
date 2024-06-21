using ClassLib;
using System.Linq;
using lab;
using System;
using System.Collections.Generic;

namespace lab_14
{
    public class Program
    {
        // Отображение меню для первой части
        private static void DisplayMenuPart1()
        {
            Console.WriteLine("\nМеню часть 1.\n" +
                              "1. Выборка данных (Where).\n" +
                              "2. Операции над множествами (Union, Except, Intersect).\n" +
                              "3. Агрегирование данных (Sum, Max, Min, Average).\n" +
                              "4. Группировка данных (Group by).\n" +
                              "5. Получение нового типа (Let).\n" +
                              "6. Переход к части 2.\n");
        }

        // Отображение меню для второй части
        private static void DisplayMenuPart2()
        {
            Console.WriteLine("\nМеню часть 2.\n" +
                              "1. Выборка данных (Where).\n" +
                              "2. Получение счетчика (Count).\n" +
                              "3. Агрегирование данных (Sum, Max, Min, Average).\n" +
                              "4. Группировка данных (Group by).\n" +
                              "5. Завершение работы.\n");
        }

        // Создание коллекции инструментов
        public static List<Instrument> CreateCollection(int length)
        {
            var instruments = new List<Instrument>();
            for (int i = 0; i < length; i++)
            {
                var guitar = new Guitar();
                guitar.RandomInit();
                instruments.Add(guitar);
            }
            return instruments;
        }

        // Where, часть 1 - Выборка инструментов Casio и Yamaha
        public static void SelectCasioYamaha(SortedDictionary<string, List<Instrument>> collection)
        {
            Console.WriteLine("\nЗапрос LINQ на вывод инструментов Casio");
            var casioInstruments = from grp in collection
                from instrument in grp.Value
                where instrument is Guitar && instrument.Name == "Casio"
                select instrument;

            Console.WriteLine("\nИнструменты Casio");
            foreach (var item in casioInstruments)
                Console.WriteLine(item);

            Console.WriteLine("\nМетоды расширения на вывод инструментов Yamaha");
            var yamahaInstruments = collection.SelectMany(group => group.Value)
                                              .Where(instrument => instrument is Guitar && instrument.Name == "Yamaha");

            Console.WriteLine("\nИнструменты Yamaha");
            foreach (var item in yamahaInstruments)
                Console.WriteLine(item);
        }

        // Where, часть 2 - Выборка инструментов Casio и Yamaha с MyCollection
        public static void SelectCasioYamaha2(MyCollection<Instrument> collection)
        {
            Console.WriteLine("\nЗапрос LINQ на вывод инструментов Casio");
            var casioInstruments = from instrument in collection
                                   where instrument?.Name?.ToString() == "Casio"
                                   select instrument;

            Console.WriteLine("\nИнструменты Casio");
            foreach (var item in casioInstruments)
            {
                if (item != null)
                {
                    Console.WriteLine($"Name: {item.Name}, Id: {item.id}");
                }
            }

            Console.WriteLine("\nМетоды расширения на вывод инструментов Yamaha");
            var yamahaInstruments = collection.Select(instrument => instrument)
                                              .Where(instrument => instrument?.id != null && instrument?.Name == "Yamaha");

            Console.WriteLine("\nИнструменты Yamaha");
            foreach (var item in yamahaInstruments)
            {
                if (item != null)
                {
                    Console.WriteLine($"Name: {item.Name}, Id: {item.id}");
                }
            }
        }

        // Union, часть 1 - Объединение групп инструментов
        public static void UnionGroups(SortedDictionary<string, List<Instrument>> collection)
        {
            Console.WriteLine("\nЗапрос LINQ на объединение инструментов из двух групп");
            var unionInstrumentsLinq = 
                (from grp in collection
                    from instrument in grp.Value
                    select instrument)
                .Union(from grp in collection
                    from instrument in grp.Value
                    select instrument);


            foreach (var item in unionInstrumentsLinq)
                Console.WriteLine(item);

            Console.WriteLine("\nМетоды расширения на объединение инструментов из двух групп");
            var unionInstrumentsExtensions = collection.SelectMany(group => group.Value)
                                                        .Union(collection.SelectMany(group => group.Value));

            foreach (var item in unionInstrumentsExtensions)
                Console.WriteLine(item);
        }

        // Count, часть 2 - Подсчет инструментов Casio и Yamaha
        public static void CountCasioYamaha(MyCollection<Instrument> collection)
        {
            Console.WriteLine("\nЗапрос LINQ на вывод количества инструментов Casio");
            var casioCount = (from instrument in collection
                              where instrument?.Name?.ToString() == "Casio"
                              select instrument).Count();

            Console.WriteLine($"Количество инструментов Casio: {casioCount}");

            Console.WriteLine("\nМетоды расширения на вывод количества инструментов Yamaha");
            var yamahaCount = collection.Select(instrument => instrument)
                                        .Count(instrument => instrument?.Name == "Yamaha");

            Console.WriteLine($"Количество инструментов Yamaha: {yamahaCount}");
        }

        // Sum, Max, Min, Average, часть 1 - Агрегирование данных
        public static void MaxAverageId(SortedDictionary<string, List<Instrument>> collection)
        {
            Console.WriteLine("\nЗапрос LINQ на вывод среднего значения показателя id всех инструментов");
            var averageIdLinq = (from grp in collection
                                 from instrument in grp.Value
                                 select instrument.id).Average();

            Console.WriteLine($"Среднее значение показателя id всех инструментов: {averageIdLinq}");

            Console.WriteLine("\nМетоды расширения на вывод максимального значения показателя id всех инструментов");
            var maxIdExtensions = collection.SelectMany(group => group.Value)
                                            .Max(instrument => instrument.id);

            Console.WriteLine($"Максимальное значение показателя id всех инструментов: {maxIdExtensions}");
        }

        // Sum, Max, Min, Average, часть 2 - Агрегирование данных с MyCollection
        public static void MaxAverageId2(MyCollection<Instrument> collection)
        {
            Console.WriteLine("\nЗапрос LINQ на вывод среднего значения показателя id всех инструментов");
            var averageIdLinq = (from instrument in collection
                                 select instrument?.id).Average();

            Console.WriteLine($"Среднее значение показателя id всех инструментов: {averageIdLinq}");

            Console.WriteLine("\nМетоды расширения на вывод максимального значения показателя id всех инструментов");
            var maxIdExtensions = collection.Select(instrument => instrument)
                                            .Max(instrument => instrument?.id);

            Console.WriteLine($"Максимальное значение показателя id всех инструментов: {maxIdExtensions}");
        }

        // Group by, часть 1 - Группировка данных
        public static void GroupByInstrumentName(SortedDictionary<string, List<Instrument>> collection)
        {
            // LINQ запрос для группировки
            Console.WriteLine("\nЗапросы LINQ на группировку по названию инструментов");
            var groupedByLinq = from grp in collection
                from instrument in grp.Value
                group instrument by instrument.Name into instrumentGroup
                select instrumentGroup;

            // Вывод результатов
            foreach (var group in groupedByLinq)
            {
                Console.WriteLine(group.Key);
                foreach (var instrument in group)
                {
                    Console.WriteLine($"{instrument.id} Инструмент {instrument.Name}.");
                }
            }

            // Методы расширения для группировки
            Console.WriteLine("\nМетоды расширения на группировку по названию инструментов");
            var groupedByExtensions = collection.SelectMany(grp => grp.Value)
                .GroupBy(instrument => instrument.Name);

            // Вывод результатов
            foreach (var group in groupedByExtensions)
            {
                Console.WriteLine(group.Key);
                foreach (var instrument in group)
                {
                    Console.WriteLine($"{instrument.id} Инструмент {instrument.Name}.");
                }
            }
        }

        // Group by, часть 2 - Группировка данных с MyCollection
        public static void GroupByInstrumentName2(MyCollection<Instrument> collection)
        {
            // LINQ запрос для группировки
            Console.WriteLine("\nЗапросы LINQ на группировку по названию инструментов");
            var groupedByLinq = from instrument in collection
                group instrument by instrument?.Name into instrumentGroup
                select instrumentGroup;

            // Вывод результатов
            foreach (var group in groupedByLinq)
            {
                Console.WriteLine(group.Key);
                foreach (var instrument in group)
                {
                    if (instrument != null)
                    {
                        Console.WriteLine($"{instrument.id} Инструмент {instrument.Name}.");
                    }
                }
            }

            // Методы расширения для группировки
            Console.WriteLine("\nМетоды расширения на группировку по названию инструментов");
            var groupedByExtensions = collection
                .GroupBy(instrument => instrument?.Name);

            // Вывод результатов
            foreach (var group in groupedByExtensions)
            {
                Console.WriteLine(group.Key);
                foreach (var instrument in group)
                {
                    if (instrument != null)
                    {
                        Console.WriteLine($"{instrument.id} Инструмент {instrument.Name}.");
                    }
                }
            }
        }

        // Let, часть 1 - Получение нового типа ID
        public static void CreateNewId(SortedDictionary<string, List<Instrument>> collection)
        {
            Console.WriteLine("\nЗапросы LINQ на получение нового типа ID");
            var newIdLinq = from grp in collection
                            from instrument in grp.Value
                            let newId = instrument.id * 0.1
                            select new { instrument.Name, OldId = instrument.id, NewID = newId };

            foreach (var item in newIdLinq)
                Console.WriteLine($"{{ Name = {item.Name}, OldId = {item.OldId}, NewID = {item.NewID:F1} }}");

            Console.WriteLine("\nМетоды расширения на получение нового типа ID");
            var newIdExtensions = collection.SelectMany(group => group.Value)
                                            .Select(instrument => new { instrument.Name, OldId = instrument.id, NewID = instrument.id * 0.5 });

            foreach (var item in newIdExtensions)
                Console.WriteLine($"{{ Name = {item.Name}, OldId = {item.OldId}, NewID = {item.NewID:F1} }}");
        }

        // Основной метод программы
        static void Main(string[] args)
        {
            Console.WriteLine("\nЧАСТЬ 1");

            // Создание двух групп инструментов
            var group1 = CreateCollection(10);
            var group2 = CreateCollection(10);

            var participants = new SortedDictionary<string, List<Instrument>>
            {
                { "Музыкальная группа 1", group1 },
                { "Музыкальная группа 2", group2 }
            };

            // Вывод инструментов в группах
            Console.WriteLine("Инструменты:");
            foreach (var participant in participants)
            {
                Console.WriteLine($"\nГруппа: {participant.Key}");
                foreach (var instrument in participant.Value)
                {
                    Console.WriteLine(instrument);
                }
            }

            // Меню для первой части
            int choice = 1;
            while (choice != 6)
            {
                try
                {
                    DisplayMenuPart1();
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            SelectCasioYamaha(participants);
                            break;
                        case 2:
                            UnionGroups(participants);
                            break;
                        case 3:
                            MaxAverageId(participants);
                            break;
                        case 4:
                            GroupByInstrumentName(participants);
                            break;
                        case 5:
                            CreateNewId(participants);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            Console.WriteLine("\nЧАСТЬ 2\n");

            // Инициализация коллекции инструментов
            var myCollection = new MyCollection<Instrument>(10);
            for (int i = 0; i < 10; i++)
            {
                var instrument = new Instrument();
                instrument.RandomInit();
                myCollection.Add(instrument);
            }

            myCollection.Print();

            // Меню для второй части
            int choice2 = 1;
            while (choice2 != 5)
            {
                try
                {
                    DisplayMenuPart2();
                    choice2 = int.Parse(Console.ReadLine());
                    switch (choice2)
                    {
                        case 1:
                            SelectCasioYamaha2(myCollection);
                            break;
                        case 2:
                            CountCasioYamaha(myCollection);
                            break;
                        case 3:
                            MaxAverageId2(myCollection);
                            break;
                        case 4:
                            GroupByInstrumentName2(myCollection);
                            break;
                        case 5:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
