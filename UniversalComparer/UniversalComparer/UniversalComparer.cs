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
        public List<string> ErrorPropertiesSetFields { get; set; }
        public List<string> ErrorSortFields { get; set; }

        public UniversalComparer(string sortString, bool nullValuesSmallest)
        {
            _comparer = Comparer.Default;
            ErrorSortFields = new List<string>();
            SetComparerProperties(sortString, nullValuesSmallest);
        }

        /// <summary>
        /// Check if objects are comparable
        /// </summary>
        /// <param name="comparablePair">The pair of comparable objects</param>
        /// <param name="sortField">Sort field</param>
        /// <returns>Check result</returns>
        private bool CheckObjectsAreComparable(ComparablePair comparablePair, string sortField)
        {
            if (!(comparablePair.ObjectA.IsComparable() && comparablePair.ObjectB.IsComparable()))
            {
                ErrorSortFields.AddUnique(sortField);
                SortSuccess = false;
            }
            return SortSuccess;
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

                if (CheckObjectsAreComparable(comparablePair, sortField.FieldName))
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

        /// <summary>
        /// Set comparer properties
        /// </summary>
        /// <param name="sortString">Sort string</param>
        /// <param name="nullValuesSmallest">Null values smallest condition</param>
        private void SetComparerProperties(string sortString, bool nullValuesSmallest)
        {
            NullValuesSmallest = nullValuesSmallest;
            SortString = sortString;
            SortFields = new List<SortField>();
            SortFields.AddRange(GetSortFields().Select(s => new SortField(s)));
            var comparableObjectsType = typeof(T);
            ErrorPropertiesSetFields = new List<string>();
            ErrorPropertiesSetFields.AddRange(SortFields.Where(s => !comparableObjectsType.CheckPropertiesExist(s.SortProperties)).Select(s => s.FieldName));
            PropertiesSetSuccess = ErrorPropertiesSetFields.Count == 0;
            SortSuccess = true;
        }
    }
}