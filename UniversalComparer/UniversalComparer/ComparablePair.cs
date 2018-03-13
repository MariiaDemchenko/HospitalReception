namespace UniversalComparer
{
    public class ComparablePair
    {
        public object ObjectA { get; }
        public object ObjectB { get; }

        public ComparablePair(object objectA, object objectB)
        {
            ObjectA = objectA;
            ObjectB = objectB;
        }
    }
}