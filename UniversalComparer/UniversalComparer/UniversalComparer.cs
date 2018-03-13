using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniversalComparer
{
    public class UniversalComparer : IComparer
    {
        private readonly Comparer _comparer;

        public bool NullValuesSmallest { get; set; }
        public List<SortField> SortFields { get; }
        public string SortString { get; set; }

        public UniversalComparer(string sortString, bool nullValuesSmallest)
        {
            _comparer = Comparer.Default;
            NullValuesSmallest = nullValuesSmallest;
            SortString = sortString;
            SortFields = new List<SortField>();
            SortFields.AddRange(GetSortFields().Select(s => new SortField(s)));
        }

        /// <summary>
        /// IComparer.Compare method implementation 
        /// </summary>
        /// <param name="objectA"></param>
        /// <param name="objectB"></param>
        /// <returns>Comparison result</returns>
        public int Compare(object objectA, object objectB)
        {
            int compareResultCode = Constants.EqualCompareResultCode;

            foreach (var sortField in SortFields)
            {
                var comparableHelper = new CompareHelper();
                ComparablePair comparablePair = comparableHelper.GetComparableObjectsPair(objectA, objectB, sortField);

                compareResultCode = CompareNullable(comparablePair.ObjectA, comparablePair.ObjectB);
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
        /// <param name="objectA"></param>
        /// <param name="objectB"></param>
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
    }
}

