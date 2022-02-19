using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Test_nedra
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] array;
            using (StreamReader reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Test.txt"))) // треугольник храниться в файле в виде массива массивов
            {
                int sizes = reader.ReadLine().Split().Select(x => int.Parse(x)).FirstOrDefault(); // первое строка файла - число уровней в треугольнике
                array = new int[sizes][];
                for (int i = 0; i < sizes; i++)
                {
                    array[i] = reader.ReadLine().Split().Select(x => int.Parse(x)).ToArray(); // считываем поэтапно каждую строку файла/треугольника
                }
            }

            Console.WriteLine(MaxNumber(array));
        }

        static int MaxNumber(int[][] triangle)
        {
            var max_number = new List<List<int>>(); //создадим список списков, где будем хранить макисимальные значения сумм до каждого элемента треугольника
            max_number.Add(new List<int>() { triangle[0][0] }); // максимальное значение до вершины равно значению вершины

            for(int i = 1; i < triangle.GetUpperBound(0) + 1; i++)
            {
                max_number.Add(Enumerable.Range(0, i + 1).Select(i => 0).ToList()); //добавляем новый список для следующего уровня треугольника
                for (int j = 0; j <= i; j++)
                {
                    if( j % i != 0) // проверка на крайний элемент
                    {
                        max_number[i][j] = triangle[i][j];
                        max_number[i][j] += max_number[i - 1][j] > max_number[i - 1][j - 1] ? max_number[i - 1][j] : max_number[i - 1][j - 1]; //проверяем от какого прошлого элемента
                                                                                                                                               //сумма с текущим будет больше
                                                                                                                                               //(проверям сумму над текущим элементом и
                                                                                                                                               //и слева сверху)
                    }
                    else
                    {
                        max_number[i][j] = triangle[i][j] + max_number[i - 1][j - (j > 0 ? 1 : 0)]; //если элемент крайний -> проверяем с какой стороны 
                    }
                }
            }
            return (max_number.Last().Max()); // ответом будет максимальный элемент из последнего ряда
        }
    }
}
