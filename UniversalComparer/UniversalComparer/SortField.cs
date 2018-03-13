namespace UniversalComparer
{
    public class SortField
    {
        public string FieldName { get; set; }
        public string[] SortProperties { get; set; }
        public Constants.SortType SortType { get; set; }

        public SortField(string field)
        {
            SetFieldName(field);
            SetSortType(field);
            SetSortProperties();
        }

        /// <summary>
        /// Check if field contains asc/desc modifier
        /// </summary>
        /// <param name="field">Sort field with/without asс/desc modifiers</param>
        /// <returns>Check result</returns>
        private bool IsSortOrderSet(string field)
        {
            return field.EndsWith(Constants.Asc) || field.EndsWith(Constants.Desc);
        }

        /// <summary>
        /// Set sort field without asc/desc modifiers
        /// </summary>
        /// <param name="field">Sort field with/without asc/desc modifiers</param>
        private void SetFieldName(string field)
        {
            if (!IsSortOrderSet(field))
            {
                FieldName = field;
            }
            else
            {
                var symbolsToRemove = field.EndsWith(Constants.Asc) ? 4 : 5;
                FieldName = field.Remove(field.LastIndexOf(Constants.WhiteSpace), symbolsToRemove);
            }
        }

        /// <summary>
        /// Set sort properties from the sort field string splitted with '.'
        /// </summary>
        private void SetSortProperties()
        {
            SortProperties = FieldName.Split(Constants.Point);
        }

        /// <summary>
        /// Set ascending/descending sort type
        /// </summary>
        /// <param name="field">Sort field with/without asс/desc modifiers</param>
        private void SetSortType(string field)
        {
            SortType = !IsSortOrderSet(field) || field.EndsWith(Constants.Asc)
                ? Constants.SortType.Asc
                : Constants.SortType.Desc;
        }
    }
}