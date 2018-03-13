namespace UniversalComparer
{
    public class CompareHelper
    {
        private ComparablePair _comparablePair;

        /// <summary>
        /// Get the pair of objects to compare
        /// </summary>
        /// <param name="objectA">First object to compare</param>
        /// <param name="objectB">Another object to compare</param>
        /// <param name="sortField">Sort field with sort type and properties</param>
        /// <returns>Pair of comparable objects</returns>
        public ComparablePair GetComparableObjectsPair(object objectA, object objectB, SortField sortField)
        {
            _comparablePair = sortField.SortType == Constants.SortType.Asc ?
                new ComparablePair(objectA, objectB) : new ComparablePair(objectB, objectA);

            SetComparableObjectsPair(sortField.SortProperties);
            return _comparablePair;
        }

        /// <summary>
        /// Set the pair of objects to compare
        /// </summary>
        /// <param name="properties"></param>
        private void SetComparableObjectsPair(string[] properties)
        {
            var objectA = _comparablePair.ObjectA.GetObjectByProperties(properties);
            var objectB = _comparablePair.ObjectB.GetObjectByProperties(properties);
            _comparablePair = new ComparablePair(objectA, objectB);
        }
    }
}
