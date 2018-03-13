using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversalComparer
{
    class Program
    {
        private static UniversalComparer<Person> _universalComparer;

        private static void Main()
        {
            Console.WriteLine("UniversalComparer");
            _universalComparer = new UniversalComparer<Person>("LastName, FirstName, Born.Year desc", true);

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
                            _universalComparer = new UniversalComparer<Person>(sortCondition, nullValuesSmallest);
                            if (!_universalComparer.PropertiesSetSuccess)
                            {
                                OutputErrorSortStringInformation();
                            }
                            else
                            {
                                OutputSortedObjectsList();
                            }
                        }
                        break;
                    case "1":
                        {
                            if (!_universalComparer.PropertiesSetSuccess)
                            {
                                OutputErrorSortStringInformation();
                            }
                            else
                            {
                                OutputSortedObjectsList();
                            }
                        }
                        break;
                    case "2":
                        {
                            OutputObjects(TestDataHelper.GetObjectsList());
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
            Console.Write("Your choise: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Output message for error sort string
        /// </summary>
        private static void OutputErrorSortStringInformation()
        {
            Console.WriteLine($"Sort string has invalid format. Following fields don't exists for objects of type \"{nameof(Person)}\":");
            _universalComparer.ErrorSortFields.ForEach(Console.WriteLine);
        }

        /// <summary>
        /// Output objects list
        /// </summary>
        /// <param name="objects">Objects list</param>
        private static void OutputObjects(List<Person> objects)
        {
            var sortFields = new List<string>();
            sortFields.AddRange(_universalComparer.SortFields.Select(sf => sf.FieldName).ToList().Where(s => !sortFields.Contains(s)));
            sortFields.AddRange(TestDataHelper.GetSortFieldsList().Where(sf => !sortFields.Contains(sf)));

            var validFields = sortFields.Where(s =>
                !_universalComparer.ErrorSortFields.Contains(s) && !_universalComparer.ErrorCompareFields.Contains(s)).ToList();

            foreach (var field in validFields)
            {
                Console.Write($"|{field,Constants.OutputColumnWidth}");
            }
            Console.WriteLine();

            foreach (var obj in objects)
            {
                foreach (var field in validFields)
                {
                    object value = obj.GetObjectByProperties(field.Split(Constants.Point));

                    Console.Write($"|{TestDataHelper.GetOutputValueConsideringType(value),Constants.OutputColumnWidth}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Sort objects list and output result
        /// </summary>
        private static void OutputSortedObjectsList()
        {
            Console.WriteLine($"Comparer properties: \n - Sort rule: \"{_universalComparer.SortString}\"\n - NullValuesSmallest: {_universalComparer.NullValuesSmallest}");
            List<Person> objects = TestDataHelper.GetObjectsList();
            objects.Sort(_universalComparer);
            if (!_universalComparer.SortSuccess)
            {
                Console.WriteLine("Sort for the following objects fields failed. Fields are not comparable:");
                _universalComparer.ErrorCompareFields.ForEach(Console.WriteLine);
            }
            else
            {
                OutputObjects(objects);
            }
        }
    }
}