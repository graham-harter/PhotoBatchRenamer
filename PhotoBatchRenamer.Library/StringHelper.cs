using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoBatchRenamer.Library
{
    internal static class StringHelper
    {
        public static string StringValueOrNull(string value)
        {
            if (value == null) return "[null]";

            return $"\"{value}\"";
        }

        public static string CollectionOrNull<T>(IReadOnlyCollection<T> collection)
        {
            if (collection == null) return "[null]";

            return CollectionOrNull((IList<T>)collection);
        }

        public static string CollectionOrNull<T>(ICollection<T> collection)
        {
            if (collection == null) return "[null]";

            Type collectionType = collection.GetType();
            Type elementType = typeof(T);

            StringBuilder sb = new StringBuilder();
            sb.Append(StripGenericParameterCount(collectionType.Name));
            sb.Append($"<{elementType.Name}>");

            sb.Append(" {");
            sb.Append($"Count = {collection.Count}");
            sb.Append("}");

            return sb.ToString();
        }

        #region Private helper methods
        private static string StripGenericParameterCount(string genericTypeName)
        {
            int genericParameterCountPosition = genericTypeName.IndexOf('`');

            if (genericParameterCountPosition < 0)
            {
                return genericTypeName;
            }

            return genericTypeName.Substring(0, genericParameterCountPosition);
        }
        #endregion // #region Private helper methods
    }
}
