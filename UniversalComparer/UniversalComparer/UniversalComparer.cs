using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniversalComparer
{
    public class UniversalComparer<T> : IComparer<T>
    {
        private readonly Comparer _comparer;

        public bool NullValuesSmallest { get; set; }
        public List<SortField> SortFields { get; set; }
        public string SortString { get; set; }
        public bool PropertiesSetSuccess { get; set; }
        public bool SortSuccess { get; set; }
        public List<string> ErrorSortFields { get; set; }
        public List<string> ErrorCompareFields { get; set; }

        public UniversalComparer(string sortString, bool nullValuesSmallest)
        {
            _comparer = Comparer.Default;
            ErrorCompareFields = new List<string>();
            SetComparerProperties(sortString, nullValuesSmallest);
        }

        /// <summary>
        /// IComparer.Compare method implementation 
        /// </summary>
        /// <param name="objectA">The first object to compare</param>
        /// <param name="objectB">The second object to compare</param>
        /// <returns>Comparison result</returns>
        public int Compare(T objectA, T objectB)
        {
            int compareResultCode = Constants.EqualCompareResultCode;

            foreach (var sortField in SortFields)
            {
                var comparableHelper = new CompareHelper();
                ComparablePair comparablePair = comparableHelper.GetComparableObjectsPair(objectA, objectB, sortField);

                if (!(comparablePair.ObjectA.IsComparable() && comparablePair.ObjectB.IsComparable()))
                {
                    if (!ErrorCompareFields.Contains(sortField.FieldName))
                    {
                        ErrorCompareFields.Add(sortField.FieldName);
                        SortSuccess = false;
                    }
                }
                else
                {
                    compareResultCode = CompareNullable(comparablePair.ObjectA, comparablePair.ObjectB);
                }

                if (compareResultCode != Constants.EqualCompareResultCode)
                {
                    return compareResultCode;
                }
            }
            return compareResultCode;
        }

        /// <summary>
        /// Compare method for nullable objects, considering NullValuesSmallest condition
        /// </summary>
        /// <param name="objectA">The first object to compare</param>
        /// <param name="objectB">The second object to compare</param>
        /// <returns>Comparison result</returns>
        private int CompareNullable(object objectA, object objectB)
        {
            var res = _comparer.Compare(objectA, objectB);

            if ((objectA == null || objectB == null) &&
                res != Constants.EqualCompareResultCode &&
                !NullValuesSmallest)
            {
                return -res;
            }
            return res;
        }

        /// <summary>
        /// Get sort fields from the sort string, splitted with ','
        /// </summary>
        /// <returns>Sort fields</returns>
        public IEnumerable<string> GetSortFields()
        {
            return SortString.Split(Constants.Comma).Select(p => p.Trim(Constants.WhiteSpace));
        }

        private void SetComparerProperties(string sortString, bool nullValuesSmallest)
        {
            NullValuesSmallest = nullValuesSmallest;
            SortString = sortString;
            SortFields = new List<SortField>();
            SortFields.AddRange(GetSortFields().Select(s => new SortField(s)));
            var comparableObjectsType = typeof(T);
            ErrorSortFields = new List<string>();
            ErrorSortFields.AddRange(SortFields.Where(s => !comparableObjectsType.CheckPropertiesExist(s.SortProperties)).Select(s => s.FieldName));
            PropertiesSetSuccess = ErrorSortFields.Count == 0;
            SortSuccess = true;
        }
    }
}