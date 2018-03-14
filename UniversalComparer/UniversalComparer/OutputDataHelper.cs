using System;
using System.Collections.Generic;

namespace UniversalComparer
{
    public class OutputDataHelper
    {
        /// <summary>
        /// Output data table
        /// </summary>
        /// <typeparam name="T">Type of objects to output</typeparam>
        /// <param name="objects">List of objects</param>
        /// <param name="fields">List of fields to output</param>
        public static void OutputDataTable<T>(List<T> objects, List<string> fields)
        {
            foreach (var field in fields)
            {
                Console.Write($"|{field,Constants.OutputColumnWidth}");
            }
            Console.WriteLine();

            foreach (var obj in objects)
            {
                foreach (var field in fields)
                {
                    object value = obj.GetObjectByProperties(field.Split(Constants.Point));

                    Console.Write($"|{TestDataHelper.GetOutputValueConsideringType(value),Constants.OutputColumnWidth}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Output error messages
        /// </summary>
        public static void OutputErrorSortStringInformation(Constants.ErrorType errorType, List<string> errorFields)
        {
            var message = errorType == Constants.ErrorType.PropertiesSetError
                ? $"Sort string has invalid format. Following fields don't exists for objects of type \"{nameof(Person)}\":"
                : "Sort for the following objects fields failed. Fields are not comparable:";
            Console.WriteLine(message);
            errorFields.ForEach(Console.WriteLine);
        }
    }
}