using System;

namespace NewsApp.Model
{
    public class LocalizedEnum<T> where T : Enum
    {
        public T Value { get; set; }
        public string DisplayName { get; set; }
    }
}