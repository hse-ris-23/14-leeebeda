using ClassLib;
using System.Linq;
using lab;
using System;
using System.Collections.Generic;

namespace lab_14
{
    public class Program
    {
        private static void CommandsForPart1()
        {
            Console.WriteLine("Меню часть 1.\n" +
                              "----------------------------------------------------------------------------\n" +
                              "1. На выборку данных(Where).\n" +
                              "2. Использование операций над множествами (Union,Except, Intersect).\n" +
                              "3. Агрегирование данных (Sum, Max, Min, Average).\n" +
                              "4. Группировка данных (Group by).\n" +
                              "5. Получение нового типа (Let).\n" +
                              "6. Переход к части 2.\n" +
                              "----------------------------------------------------------------------------\n");
        }

        private static void CommandsForPart2()
        {
            Console.WriteLine("Меню часть 2.\n" +
                              "----------------------------------------------------------------------------\n" +
                              "1. На выборку данных(Where).\n" +
                              "2. Получение счетчика(Count). \n" +
                              "3. Агрегирование данных (Sum, Max, Min, Average). \n" +
                              "4. Группировка данных (Group by).\n" +
                              "5. Завершение работы.\n" +
                              "----------------------------------------------------------------------------\n");
        }

        // Создание коллекции
        public static List<Instrument> MakeCollection(int length)
        {
            var list = new List<Instrument>();
            for (int i = 0; i < length; i++)
            {
                Guitar g = new Guitar();
                g.RandomInit();
                list.Add(g);
            }
            return list;
        }

        // На выборку данных (Where) - часть 1
        public static void WhereCasioYamaha(SortedDictionary<string, List<Instrument>> collection)
        {
            // Запрос LINQ на вывод музыкальных инструментов с названием Casio с отложенным выполнеием
            Console.WriteLine("\nЗапрос LINQ на вывод музыкальных инструментов с названием Casio");
            var res1 = from gr in collection
                       from mi in gr.Value
                       where mi is Guitar && mi.Name == "Casio"
                       select mi;

            // Вывод  
            Console.WriteLine("\nИнструменты с назвванием Casio");
            foreach (var item in res1)
                Console.WriteLine(item);


            // Методы расширения на вывод музыкальных инструментов с названием Casio с отложенным выполнеием
            Console.WriteLine("\nМетоды расширения на вывод музыкальных инструментов с названием Yamaha");
            var res2 = collection.SelectMany(gr => gr.Value)
                              .Where(mi => mi is Guitar && mi.Name == "Yamaha");

            // Вывод  
            Console.WriteLine("\nИнструменты с назвванием Yamaha");
            foreach (var item in res2)
                Console.WriteLine(item);
        }

        // На выборку данных (Where) - часть 2
        public static void WhereCasioYamaha2(MyCollection<Instrument> collection)
        {
            // Запрос LINQ на вывод музыкальных инструментов с названием Casio с отложенным выполнеием
            Console.WriteLine("\nЗапрос LINQ на вывод музыкальных инструментов с названием Casio");
            var res1 = from mi in collection
                       where mi?.Name?.ToString() == "Casio"
                       select mi;

            // Вывод  
            Console.WriteLine("\nИнструменты с назвванием Casio");
            foreach (var item in res1)
                Console.WriteLine(item.GetType());
            

            // Методы расширения на вывод музыкальных инструментов с названием Casio с отложенным выполнеием
            Console.WriteLine("\nМетоды расширения на вывод музыкальных инструментов с названием Yamaha");
            var res2 = collection.Select(gr => gr)
                              .Where(mi => mi?.id != null && mi?.Name == "Yamaha");

            // Вывод  
            Console.WriteLine("\nИнструменты с назвванием Yamaha");
            foreach (var item in res2)
                Console.WriteLine(item);
        }

        // Использование операций над множествами (Union,Except, Intersect) - часть 1
        public static void UnionGroup(SortedDictionary<string, List<Instrument>> collection)
        {
            // Запрос LINQ на объеденение музыкальных инструментов из двух групп
            Console.WriteLine("\nЗапрос LINQ на объеденение музыкальных инструментов из двух групп");
            var res1 = (from gr in collection
                        from mi in gr.Value 
                        select mi)
                        .Union(from gr in collection
                              from mi in gr.Value 
                              select mi);

            // Вывод  
            foreach (var item in res1)
                Console.WriteLine(item);

            // Методы расширения на объеденение музыкальных инструментов из двух групп
            Console.WriteLine("\nМетоды расширения на объеденение музыкальных инструментов из двух групп");
            var res2 = collection.SelectMany(gr => gr.Value)
                .Union(collection.SelectMany(gr => gr.Value));

            // Вывод  
            foreach (var item in res2)
                Console.WriteLine(item);
        }

        // Получение счетчика(Count) - часть 2
        public static void CountCasioYamaha(MyCollection<Instrument> collection)
        {
            // Запрос LINQ на вывод количества музыкальных инструментов с названием Casio с отложенным выполнеием
            Console.WriteLine("\nЗапрос LINQ на вывод количества музыкальных инструментов с названием Casio");
            var res1 = (from mi in collection
                       where mi?.Name?.ToString() == "Casio"
                       select mi).Count();

            // Вывод  
            Console.WriteLine($"Количество инструментов с названием Casio: {res1}");


            // Методы расширения на вывод музыкальных инструментов с названием Casio с отложенным выполнеием
            Console.WriteLine("\nМетоды расширения на вывод количества музыкальных инструментов с названием Yamaha");
            var res2 = collection.Select(gr => gr)
                              .Where(mi => mi?.Name == "Yamaha").Count();

            // Вывод  
            Console.WriteLine($"Количество инструментов с названием Yamaha: {res2}");
        }

        // Агрегирование данных (Sum, Max, Min, Average) - часть 1
        public static void MaxAverageId(SortedDictionary<string, List<Instrument>> collection)
        {
            // Запрос LINQ на вывод среднего значения показателя id всех инструментов
            Console.WriteLine("\nЗапрос LINQ на вывод среднего значения показателя id всех инструментов");
            var res1 = (from gr in collection
                       from mi in gr.Value
                       select mi.id)
                       .Average();
            // Вывод  
            Console.WriteLine($"Среднее значение показателя id всех инструментов: {res1}");

            // Методы расширения на вывод максимального значения показателя id всех инструментов
            Console.WriteLine("\nМетоды расширения на вывод среднего значения показателя id всех инструментов");
            var res2 = collection.SelectMany(gr => gr.Value)
                              .Max(mi => mi.id);

            // Вывод  
            Console.WriteLine($"Максимальное значение показателя id всех инструментов: {res2}");
        }

        // Агрегирование данных (Sum, Max, Min, Average) - часть 2
        public static void MaxAverageId2(MyCollection<Instrument> collection)
        {
            // Запрос LINQ на вывод среднего значения показателя id всех инструментов
            Console.WriteLine("\nЗапрос LINQ на вывод среднего значения показателя id всех инструментов");
            var res1 = (from mi in collection
                        select mi?.id)
                       .Average();
            // Вывод  
            Console.WriteLine($"Среднее значение показателя id всех инструментов: {res1}");

            // Методы расширения на вывод максимального значения показателя id всех инструментов
            Console.WriteLine("\nМетоды расширения на вывод среднего значения показателя id всех инструментов");
            var res2 = collection.Select(gr => gr)
                              .Max(mi => mi?.id);

            // Вывод  
            Console.WriteLine($"Максимальное значение показателя id всех инструментов: {res2}");
        }

        // Группировка данных (Group by) - часть 1
        public static void GroupNameAndStringCount(SortedDictionary<string, List<Instrument>> collection)
        {
            // Запросы LINQ на группировку по Названию мукзыкальных инструментов
            Console.WriteLine("\nЗапросы LINQ на группировку по Названию мукзыкальных инструментов ");
            var res1 = from gr in collection
                      from mi in gr.Value
                      group mi by mi.Name;

            // Вывод  
            Console.WriteLine("Группировка ");
            foreach (var gr in res1)
            {
                Console.WriteLine(gr.Key);
                foreach (var item in gr)
                {
                    Console.WriteLine(item);
                }
            }

            // Методы расширения на группировку по максимальному количеству струн гитар
            Console.WriteLine("\nМетоды расширения на группировку по максимальному количеству струн гитар ");
            var res2 = collection.SelectMany(gr => gr.Value)
                .Where(mi => mi is Guitar).GroupBy(x => ((Guitar)x).Strings);

            // Вывод  
            foreach (var gr in res2)
            {
                Console.WriteLine(gr.Key);
                foreach (var item in gr)
                {
                    Console.WriteLine(item);
                }
            }
        }

        // Группировка данных (Group by) - часть 2
        public static void GroupNameAndStringCount2(MyCollection<Instrument> collection)
        {
            // Запросы LINQ на группировку по названию мукзыкальных инструментов
            Console.WriteLine("\nЗапросы LINQ на группировку по названию мукзыкальных инструментов ");
            var res1 = from mi in collection
                       group mi by mi?.Name;

            // Вывод  
            foreach (var gr in res1)
            {
                Console.WriteLine(gr.Key);
                foreach (var item in gr)
                {
                    Console.WriteLine(item);
                }
            }

            // Методы расширения на группировку по максимальному количеству струн гитар
            Console.WriteLine("\nМетоды расширения на группировку по максимальному количеству струн гитар ");
            var res2 = collection
                .Select(gr => gr)
                .GroupBy(mi => mi?.Name);

            // Вывод  
            foreach (var gr in res2)
            {
                Console.WriteLine(gr.Key);
                foreach (var item in gr)
                {
                    Console.WriteLine(item);
                }
            }
        }

        // Получение нового типа(Let) - часть 1
        public static void NewName(SortedDictionary<string, List<Instrument>> collection)
        {
            // Запросы LINQ на получение нового типа ID
            Console.WriteLine("\nЗапросы LINQ на получение нового типа ID");
            var res1 = from gr in collection
                       from mi in gr.Value
                       let newId = mi.id * 0.1
                       select new { mi.Name, OldId = mi.id, NewID = mi.id * 0.1 };

            // Вывод  
            foreach (var item in res1)
                Console.WriteLine(item);

            // Методы расширения на получение нового типа
            Console.WriteLine("\nМетоды расширения на получение нового типа ID");
            var res2 = collection.SelectMany(gr => gr.Value)
                .Select(mi => new { mi.Name, OldId = mi.id, NewID = mi.id * 0.5 });

            // Вывод  
            foreach (var item in res2)
                Console.WriteLine(item);
        }



        static void Main(string[] args)
        {
            Console.WriteLine("\nЧАСТЬ 1");

            List<Instrument> group1 = MakeCollection(10);
            List<Instrument> group2 = MakeCollection(10);

            SortedDictionary<string, List<Instrument>> participants = new SortedDictionary<string, List<Instrument>>();

            participants.Add("Музыкальная группа 1", group1);
            participants.Add("Музыкальная группа 2", group2);

            Console.WriteLine("Музыкальные инструменты");

            foreach (var participant in participants)
            {
                Console.WriteLine($"\nГруппа: {participant.Key}");
                foreach (var musicalInstrument in participant.Value)
                {
                    Console.WriteLine(musicalInstrument);
                }
            }

            // Меню часть 1
            int answer = 1;
            while (answer != 6)
            {
                try
                {
                    CommandsForPart1();
                    answer = int.Parse(Console.ReadLine());
                    switch (answer)
                    {
                        case 1:
                            {
                                WhereCasioYamaha(participants);
                                break;
                            }
                        case 2:
                            {
                                UnionGroup(participants);
                                break;
                            }
                        case 3:
                            {
                                MaxAverageId(participants);
                                break;
                            }
                        case 4:
                            {
                                GroupNameAndStringCount(participants);
                                break;
                            }
                        case 5:
                            {
                                NewName(participants);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            Console.WriteLine("\nЧАСТЬ 2\n");
            MyCollection<Instrument> myCollection = new MyCollection<Instrument>(10); // Создание хеш-таблицы музыкальных инструментов

            for (int i = 0; i < 10; i++)
            {
                Instrument a = new();
                a.RandomInit();
                myCollection.Add(a);
            }

            myCollection.Print();
            // Меню часть 2
            int answer2 = 1;
            while (answer2 != 5)
            {
                try
                {
                    CommandsForPart2();
                    answer2 = int.Parse(Console.ReadLine());
                    switch (answer2)
                    {
                        case 1:
                            {
                                WhereCasioYamaha2(myCollection);
                                break;
                            }
                        case 2:
                            {
                                CountCasioYamaha(myCollection);
                                break;
                            }
                        case 3:
                            {
                                MaxAverageId2(myCollection);
                                break;
                            }
                        case 4:
                            {
                                GroupNameAndStringCount2(myCollection);
                                break;
                            }
                        case 5:
                            {
                                break;
                            }
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