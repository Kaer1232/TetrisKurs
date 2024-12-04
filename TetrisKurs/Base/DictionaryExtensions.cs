namespace TetrisKurs.Base 
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, TValue defaultValue = default(TValue))
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));

            TValue result;
            return  self.TryGetValue(key, out result)
                ?   result
                :   defaultValue;
        }
    }
}