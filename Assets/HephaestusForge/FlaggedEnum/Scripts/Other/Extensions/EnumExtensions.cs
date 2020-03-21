using System;
using System.Collections.Generic;

namespace HephaestusForge.FlaggedEnum
{
    /// <summary>
    /// Extenstion methods for the Enum class, all enums can use these methods, but they are mainly targeted at Flags.
    /// </summary>
    public static class EnumExtensions
    {
        //A dictionary to store values and enumindexes of a specific type of enum.
        private static Dictionary<Type, Dictionary<Enum, int>> _storedEnumAndIndex = new Dictionary<Type, Dictionary<Enum, int>>();

        /// <summary>
        /// Check if the enum instance contains a value or values, by using bit operation, like so (instance.Contains(TEnum.Val1 | TEnum.Val2).
        /// This is just using the Enum.HasFlag method, which you could also do.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="source">The source of the extension method.</param>
        /// <param name="value">The value to check if is contained.</param>
        /// <returns>Whether the source contains the value.</returns>
        public static bool Contains<TEnum>(this TEnum source, TEnum value) where TEnum : Enum
        {
            return source.HasFlag(value);
        }

        /// <summary>
        /// Comparing the chosen enum with a value, you could just use == between the two.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="source">The source of the extension method.</param>
        /// <param name="value">The value to compare the source to.</param>
        /// <returns>Whether the source and value are equal.</returns>
        public static bool Is<TEnum>(this TEnum source, TEnum value) where TEnum : Enum
        {
            return source.CompareTo(value) == 0;
        }

        /// <summary>
        /// Adding a value to the enum Source, remember to get the return value, the source isnt modified.
        /// Should also use |= between the source and value instead, this will get the same effect without any casting.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="source">The source of the extension method.</param>
        /// <param name="value">The value to add to the source.</param>
        /// <returns>The value of source being modified with value.</returns>
        public static TEnum Add<TEnum>(this TEnum source, TEnum value) where TEnum : Enum
        {
            Type enumType = typeof(TEnum);

            var underlyingType = Enum.GetUnderlyingType(enumType);

            if (underlyingType == typeof(int))
            {
                int sourceVal = (int)Enum.ToObject(enumType, source);
                int valueVal = (int)Enum.ToObject(enumType, value);
                
                return (TEnum)Enum.ToObject(enumType, sourceVal | valueVal);
            }
            else if(underlyingType == typeof(long))
            {
                long sourceVal = (long)Enum.ToObject(enumType, source);
                long valueVal = (long)Enum.ToObject(enumType, value);

                return (TEnum)Enum.ToObject(enumType, sourceVal | valueVal);
            }
            else if (underlyingType == typeof(short))
            {
                short sourceVal = (short)Enum.ToObject(enumType, source);
                short valueVal = (short)Enum.ToObject(enumType, value);

                return (TEnum)Enum.ToObject(enumType, sourceVal | valueVal);
            }
            else if (underlyingType == typeof(byte))
            {
                byte sourceVal = (byte)Enum.ToObject(enumType, source);
                byte valueVal = (byte)Enum.ToObject(enumType, value);

                return (TEnum)Enum.ToObject(enumType, sourceVal | valueVal);
            }


#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.Log($"Couldnt find the right underlying type, the type is: {underlyingType}");
#endif


            return source;
        }

        /// <summary>
        /// Removing the value from the source, remember to get the return value, the source isnt modified.
        /// Should also use (& ~) between the source and value instead, this will get the same effect without any casting.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="source">The source of the extension method.</param>
        /// <param name="value">The value to remove from the source.</param>
        /// <returns>The value of source being modified with value.</returns>
        public static TEnum Remove<TEnum>(this TEnum source, TEnum value) where TEnum : Enum
        {
            Type enumType = typeof(TEnum);

            var underlyingType = Enum.GetUnderlyingType(enumType);

            if (underlyingType == typeof(int))
            {
                int sourceVal = (int)Enum.ToObject(enumType, source);
                int valueVal = (int)Enum.ToObject(enumType, value);

                return (TEnum)Enum.ToObject(enumType, sourceVal & ~valueVal);
            }
            else if (underlyingType == typeof(long))
            {
                long sourceVal = (long)Enum.ToObject(enumType, source);
                long valueVal = (long)Enum.ToObject(enumType, value);

                return (TEnum)Enum.ToObject(enumType, sourceVal & ~valueVal);
            }
            else if (underlyingType == typeof(short))
            {
                short sourceVal = (short)Enum.ToObject(enumType, source);
                short valueVal = (short)Enum.ToObject(enumType, value);

                return (TEnum)Enum.ToObject(enumType, sourceVal & ~valueVal);
            }
            else if (underlyingType == typeof(byte))
            {
                byte sourceVal = (byte)Enum.ToObject(enumType, source);
                byte valueVal = (byte)Enum.ToObject(enumType, value);

                return (TEnum)Enum.ToObject(enumType, sourceVal & ~valueVal);
            }


#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.Log($"Couldnt find the right underlying type, the type is: {underlyingType}");
#endif


            return source;
        }

        /// <summary>
        /// Getting all the values contained in the source.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="source">The source of the extension method.</param>
        /// <returns>All the contained values of the source.</returns>
        public static IEnumerable<TEnum> ContainedValues<TEnum>(this TEnum source) where TEnum : Enum
        {
            Type enumType = typeof(TEnum);

            if (!_storedEnumAndIndex.ContainsKey(enumType))
            {
                StoreInDictionary(enumType);
            }

            foreach (TEnum value in _storedEnumAndIndex[enumType].Keys)
            {
                if (source.HasFlag(value))
                {
                    yield return value;
                }
            }
        }

        /// <summary>
        /// Get the index of the enum value, found by reverse bitshifting with one.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="source">The source of the extension method.</param>
        /// <returns>The different enum indexes of the source.</returns>
        public static int GetEnumValueIndex<TEnum>(this TEnum source) where TEnum : Enum
        {
            int sourceVal = (int)Enum.ToObject(typeof(TEnum), source);

            int i = 0;

            while (sourceVal != 0)
            {
                i++;
                sourceVal = sourceVal >> 1;


#if UNITY_EDITOR || DEVELOPMENT_BUILD
                if(sourceVal < 0)
                {
                    UnityEngine.Debug.LogError("Only use a single value of the enum and use bitshifting with 1 bit at a time.");
                    break;
                }
#endif
            }

            return i - 1;
        }

        /// <summary>
        /// Loop through the values contained in the TEnum and if the source contains the values, the action will be executed.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="source">The source of the extension method.</param>
        /// <param name="action">The action to be executed when the source contains a value.</param>
        public static void ForEachContained<TEnum>(this TEnum source, Action<TEnum> action) where TEnum : Enum
        {
            Type enumType = typeof(TEnum);

            if (!_storedEnumAndIndex.ContainsKey(enumType))
            {
                StoreInDictionary(enumType);
            }

            foreach (TEnum value in _storedEnumAndIndex[enumType].Keys)
            {
                if (source.HasFlag(value))
                {
                    action.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Loop through the values in the TEnum and if the source contains the values, the action will be executed.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="source">The source of the extension method.</param>
        /// <param name="action">The action to be executed when the source contains a value.</param>
        public static void ForEach<TEnum>(this TEnum source, Action<TEnum> action) where TEnum : Enum
        {
            Type enumType = typeof(TEnum);

            if (!_storedEnumAndIndex.ContainsKey(enumType))
            {
                StoreInDictionary(enumType);
            }

            foreach (TEnum value in _storedEnumAndIndex[enumType].Keys)
            {
                action.Invoke(value);
            }
        }

        /// <summary>
        /// Initialize the dictionary at an enumtype, getting the Enum values and indexes for the enum value.
        /// </summary>
        /// <param name="enumType"></param>
        private static void StoreInDictionary(Type enumType)
        {
            _storedEnumAndIndex.Add(enumType, new Dictionary<Enum, int>());

            var values = Enum.GetValues(enumType);

            foreach (Enum value in values)
            {
                _storedEnumAndIndex[enumType].Add(value, Array.IndexOf(values, value));
            }
        }
    }
}