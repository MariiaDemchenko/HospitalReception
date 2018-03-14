using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversalComparer
{
    public class Program
    {
        private static UniversalComparer<Person> _universalComparer;

        /// <summary>
        /// Get valid fields to outuput
        /// </summary>
        /// <returns>Valid fields list</returns>
        private static List<string> GetValidFields()
        {
            var sortFields = new List<string>();
            sortFields.AddRange(_universalComparer.SortFields.Select(sf => sf.FieldName));
            sortFields.AddRangeUnique(TestDataHelper.GetSortFieldsList());
            return sortFields.Except(_universalComparer.ErrorPropertiesSetFields.Union(_universalComparer.ErrorSortFields)).ToList();
        }

        /// <summary>
        /// Check if properties set result is success and output error message if it is not
        /// </summary>
        /// <returns>Check result</returns>
        private static bool IsPropertiesSetSuccess()
        {
            if (!_universalComparer.PropertiesSetSuccess)
            {
                OutputDataHelper.OutputErrorSortStringInformation(Constants.ErrorType.PropertiesSetError, _universalComparer.ErrorPropertiesSetFields);
            }
            return _universalComparer.PropertiesSetSuccess;
        }

        /// <summary>
        /// Check if sort result is success and output error message if it is not
        /// </summary>
        /// <returns>Check result</returns>
        private static bool IsSortSuccess()
        {
            if (!_universalComparer.SortSuccess)
            {
                OutputDataHelper.OutputErrorSortStringInformation(Constants.ErrorType.SortError,
                    _universalComparer.ErrorSortFields);
            }
            return _universalComparer.SortSuccess;
        }

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
                            SetComparerProperties();
                            OutputSortedObjectsList();
                        }
                        break;
                    case "1":
                        {
                            OutputSortedObjectsList();
                        }
                        break;
                    case "2":
                        {
                            OutputDataHelper.OutputDataTable(TestDataHelper.GetObjectsList(), GetValidFields());
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
        /// Sort objects list and output result
        /// </summary>
        private static void OutputSortedObjectsList()
        {
            if (!IsPropertiesSetSuccess())
            {
                return;
            }
            List<Person> objects = SortObjects();
            if (IsSortSuccess())
            {
                OutputDataHelper.OutputDataTable(objects, GetValidFields());
            }
        }

        /// <summary>
        /// SetComparerProperties
        /// </summary>
        private static void SetComparerProperties()
        {
            Console.WriteLine("Set sort rule:");
            var sortCondition = Console.ReadLine();
            Console.WriteLine("Set NullValueIsSmallest condition (enter t for true, any for false):");
            var nullValuesSmallest = Console.ReadLine() == "t";
            _universalComparer = new UniversalComparer<Person>(sortCondition, nullValuesSmallest);
        }

        /// <summary>
        /// Sort objects
        /// </summary>
        /// <returns>List of sorted objects</returns>
        private static List<Person> SortObjects()
        {
            Console.WriteLine($"Comparer properties: \n - Sort rule: \"{_universalComparer.SortString}\"\n - NullValuesSmallest: {_universalComparer.NullValuesSmallest}");
            List<Person> objects = TestDataHelper.GetObjectsList();
            objects.Sort(_universalComparer);
            return objects;
        }
    }
}