using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniversalComparer
{
    class Program
    {
        private static UniversalComparer _universalComparer;

        /// <summary>
        /// Get objects ArrayList filled with test values
        /// </summary>
        /// <returns>Objects ArrayList</returns>
        private static ArrayList GetObjectsArrayList()
        {
            var people = new List<Person>
            {
                new Person {FirstName = "Ivan",     LastName = "Alekseev", Born = new DateTime(1980, 5, 3)},
                new Person {FirstName = "Petr",     LastName = "Alekseev", Born = new DateTime(1990, 10, 15)},
                new Person {FirstName = "Nikolay",     LastName = "Alekseev", Born = new DateTime(1970, 5, 3)},
                new Person {FirstName = "Alexander",LastName = "Ivanov", Born = new DateTime(1111, 11, 11)},
                new Person {FirstName = "Alexander",LastName = "Petrov", Born = new DateTime(1111, 11, 11)},
                new Person {FirstName = "Dmitry",   LastName = "Popov", Born = new DateTime(1111, 11, 11)},
                new Person {FirstName = "Vladimir", LastName = "Smirnov", Born = new DateTime(2222, 12, 12)},
                new Person {FirstName = "Petr",     LastName = "Ivanov", Born = new DateTime(1122, 12,11)},
                new Person {FirstName = "Ivan",     LastName = "Petrov", Born = new DateTime(2211, 11, 22)},
                new Person {FirstName = "Nikolay",  LastName = "Abramov", Born = new DateTime(2010, 3, 8)},
                new Person {FirstName = "Denis",    LastName = "Davidov", Born = new DateTime(1960, 4, 5)},
                new Person {FirstName = "Ilya",     LastName = "Nesterov", Born = new DateTime(1994, 4, 10)},
                new Person {FirstName = "Igor",     LastName = "Yakovlev", Born = new DateTime(1950, 5, 11)},
                new Person {FirstName = "Sergey",   LastName = "Davidov", Born = new DateTime(1111, 1, 5)},
                new Person {FirstName = "Andrey",   LastName = "Belov", Born = new DateTime(2000, 3, 30)},
                new Person {                        LastName = "Borisov", Born = DateTime.MaxValue},
                new Person {FirstName ="",          LastName = "Borisov"},
                new Person {FirstName ="",          LastName = "Belov", Born = DateTime.MaxValue},
                new Person {FirstName ="Andrey",                        Born = DateTime.MaxValue},
                new Person {FirstName ="Nikolay",   LastName = "", Born = DateTime.MaxValue},
                new Person {         LastName = "Tarasov"},
                new Person {FirstName ="",          Born = DateTime.MaxValue},
                new Person(),
                new Person {FirstName = "Ivan",   LastName = "Alekseev", Born = new DateTime(1970, 5, 2)},
                new Person {FirstName = "Ivan",   LastName = "Alekseev", Born = new DateTime(1960, 3, 4)},
                new Person {FirstName = "Andrey",     LastName = "Ivanov", Born = new DateTime(1122, 12,11)},
                new Person {FirstName = "Dmitry",     LastName = "Ivanov", Born = new DateTime(1122, 12,11)},
            };

            people[0].Chief = people[6];
            people[1].Chief = people[10];
            people[2].Chief = people[8];
            people[3].Chief = people[0];
            people[4].Chief = people[1];
            people[5].Chief = people[22];
            people[6].Chief = people[5];
            people[7].Chief = people[4];
            people[9].Chief = people[21];
            people[10].Chief = people[8];
            people[11].Chief = people[5];
            people[12].Chief = people[2];
            people[13].Chief = people[20];
            people[14].Chief = people[8];
            people[16].Chief = people[11];
            people[18].Chief = people[12];
            people[21].Chief = people[5];
            people[25].Chief = people[23];
            people[26].Chief = people[23];

            var objectsArrayList = new ArrayList();
            objectsArrayList.AddRange(people);
            return objectsArrayList;
        }

        /// <summary>
        /// Get base object fields for output
        /// </summary>
        /// <returns>Base objects fields</returns>
        private static List<string> GetSortFieldsList()
        {
            return new List<string>
            {
                "FirstName",
                "LastName",
                "Born"
            };
        }

        private static void Main()
        {
            Console.WriteLine("UniversalComparer");
            _universalComparer = new UniversalComparer("LastName, FirstName, Born.Year desc", true);

            string code = Menu();
            while (code != "q")
            {
                switch (code)
                {
                    case "0":
                        {
                            Console.WriteLine("Set sort rule:");
                            var sortCondition = Console.ReadLine();
                            Console.WriteLine("Set NullValueIsSmallest condition (enter t for true, any for false):");
                            var nullValuesSmallest = Console.ReadLine() == "t";
                            _universalComparer = new UniversalComparer(sortCondition, nullValuesSmallest);
                            Console.WriteLine($"Comparer properties: \n - Sort rule: \"{_universalComparer.SortString}\"\n - NullValuesSmallest: {_universalComparer.NullValuesSmallest}");
                            ArrayList objects = GetObjectsArrayList();
                            objects.Sort(_universalComparer);
                            OutputObjects(objects);
                        }
                        break;
                    case "1":
                        {
                            Console.WriteLine($"Comparer properties: \n - Sort rule: \"{_universalComparer.SortString}\"\n - NullValuesSmallest: {_universalComparer.NullValuesSmallest}");
                            ArrayList objects = GetObjectsArrayList();
                            objects.Sort(_universalComparer);
                            OutputObjects(objects);
                        }
                        break;
                    case "2":
                        {
                            OutputObjects(GetObjectsArrayList());
                        }
                        break;
                    case "q":
                        break;
                }
                code = Menu();
            }
        }

        /// <summary>
        /// Main menu
        /// </summary>
        /// <returns>User input string</returns>
        private static string Menu()
        {
            Console.WriteLine("Enter\n " +
                              "- 0 to set comparer properties and view sorted list\n " +
                              "- 1 to view sorted list\n " +
                              "- 2 to view unsorted list\n " +
                              "- q to quit");
            return Console.ReadLine();
        }

        /// <summary>
        /// Output objects ArrayList
        /// </summary>
        /// <param name="objects">Objects ArrayList</param>
        private static void OutputObjects(ArrayList objects)
        {
            var sortFields = new List<string>();
            sortFields.AddRange(_universalComparer.SortFields.Select(sf => sf.FieldName).ToList().Where(s => !sortFields.Contains(s)));
            sortFields.AddRange(GetSortFieldsList().Where(sf => !sortFields.Contains(sf)));

            foreach (var field in sortFields)
            {
                Console.Write($"|{field,Constants.OutputColumnWidth}");
            }
            Console.WriteLine();

            foreach (var obj in objects)

            {
                foreach (var field in sortFields)
                {
                    object value = obj.GetObjectByProperties(field.Split(Constants.Point));
                    var outputString = value?.ToString() ?? "NULL";
                    Console.Write($"|{outputString,Constants.OutputColumnWidth}");
                }
                Console.WriteLine();
            }
        }
    }
}
