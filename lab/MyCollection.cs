using ClassLib;
using System;
using System.Collections;
using System.Collections.Generic;

namespace lab
{
    public class MyCollection<T> : ICollection<T>, IEnumerable<T> where T : IInit, IComparable, new()
    {
        private T[] table; // таблица
        private int count = 0; // количество записей
        private double fillRatio; // коэффициент заполняемости таблицы
        private int capacity;

        public int Capacity => table.Length; // ёмкость, количество выделенной памяти
        public int Count => count; // текущее количество элементов

        public bool IsReadOnly => false; // коллекция не только для чтения

        // Конструктор без параметров
        public MyCollection() 
        {
            capacity = 10;
            table = new T[10];
            fillRatio = 0.72;
        }

        public MyCollection(int capacity, double fillRatio = 0.72) 
        {
            if (capacity <= 0) throw new Exception("В хеш-таблице должен быть хотя бы 1 элемент.");
            this.capacity = capacity;
            table = new T[capacity];
            this.fillRatio = fillRatio;

            // Заполняем таблицу элементами через RandomInit
            Random random = new Random();
            int elementsToAdd = (int)(capacity * fillRatio);
            for (int i = 0; i < elementsToAdd; i++)
            {
                T item = new T();
                item.RandomInit();
                Add(item);
            }
        }

        // Конструктор копирования
        public MyCollection(MyCollection<T> collection)
        {
            MyCollection<T> temp = (MyCollection<T>)collection.Clone();
            capacity = temp.capacity;
            table = temp.table;
            fillRatio = temp.fillRatio;
            count = temp.count;
        }

        // Клонирование коллекции
        public object Clone()
        {
            MyCollection<T> clone = new MyCollection<T>();
            foreach (T item in this)
            {
                if (item is ICloneable cloneable)
                {
                    clone.Add((T)cloneable.Clone());
                }
                else
                {
                    clone.Add(item);
                }
            }
            return clone;
        }

        // Проверка на наличие элемента
        public bool Contains(T data)
        {
            return !(FindItem(data) < 0);
        }

        // Печать хеш-таблицы
        public void Print()
        {
            int i = 0;
            foreach (T item in table)
            {
                Console.WriteLine($"{i} : {item}");
                i++;
            }
        }

        // Получение индекса элемента
        private int GetIndex(T data)
        {
            return Math.Abs(data.GetHashCode()) % Capacity;
        }

        // Добавление элемента в таблицу
        private void AddData(T data)
        {
            if (data == null) return; // добавляется пустой элемент
            int index = GetIndex(data);
            int current = index; // запомнили индекс

            // если место уже занято
            if (table[index] != null)
            {
                // идём до конца таблицы или до первого пустого места
                while (current < table.Length && table[current] != null)
                    current++;

                // если таблица закончилась
                if (current == table.Length)
                {
                    // идём от начала таблицы до индекса, который запомнили
                    current = 0;
                    while (current < index && table[current] != null)
                        current++;

                    // места нет
                    if (current == index)
                    {
                        throw new Exception("Нет места в таблице");
                    }
                }
            }

            // место найдено
            table[current] = data;
            count++;
        }

        // Поиск элемента в таблице
        public int FindItem(T data)
        {
            int index = GetIndex(data);

            if (table[index] == null) return -1;

            if (table[index].Equals(data))
                return index;

            int current = index;

            while (current < table.Length)
            {
                if (table[current] != null && table[current].Equals(data))
                    return current;

                current++;
            }

            current = 0;

            while (current < index)
            {
                if (table[current] != null && table[current].Equals(data))
                    return current;
                current++;
            }

            return -1;
        }

        // Добавление элемента в коллекцию
        public void Add(T item)
        {
            if ((((double)Count) / Capacity) > fillRatio) // место в таблице закончилось
            {
                T[] temp = (T[])table.Clone();
                table = new T[temp.Length * 2];
                capacity *= 2;
                count = 0;
                foreach (T data in temp)
                    AddData(data);
            }

            AddData(item);
        }

        // Очистка коллекции
        public void Clear()
        {
            count = 0;
            table = new T[capacity];
        }

        // Копирование коллекции в массив
        public void CopyTo(T[] array, int index)
        {
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (array.Length - index < Count)
                throw new ArgumentException("Недостаточно места в целевом массиве.");

            int arrayIndex = index;

            foreach (T item in table)
            {
                if (item != null)
                {
                    array[arrayIndex] = item;
                    arrayIndex++;
                }
            }
        }

        // Удаление элемента из коллекции
        public bool Remove(T data)
        {
            int index = FindItem(data);
            if (index < 0) return false;
            count--;
            table[index] = default;
            return true;
        }

        // Итератор для коллекции
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Реализация итератора
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in table)
            {
                yield return item;
            }
        }
    }
}
