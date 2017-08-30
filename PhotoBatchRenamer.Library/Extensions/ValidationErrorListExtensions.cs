using System;
using System.Collections.Generic;

namespace PhotoBatchRenamer.Library.Extensions
{
    /// <summary>
    /// Extension methods for an <see cref="IList{ValidationError}" />.
    /// </summary>
    internal static class ValidationErrorListExtensions
    {
        public static void Add(
            this IList<ValidationError> errors,
            string propertyName,
            string errorFormatString,
            string propertyHumanReadableDescription,
            string comparisonPropertyName = null,
            string comparisonPropertyHumanReadableDescription = null)
        {
            // Validate arguments.
            if (errors == null) throw new ArgumentNullException(nameof(errors));
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException(nameof(propertyName));
            if (string.IsNullOrWhiteSpace(errorFormatString)) throw new ArgumentNullException(nameof(errorFormatString));
            if (string.IsNullOrWhiteSpace(propertyHumanReadableDescription)) throw new ArgumentNullException(nameof(propertyHumanReadableDescription));

            bool comparisonPropertyNameSpecified = !string.IsNullOrWhiteSpace(comparisonPropertyName);
            bool comparisonPropertyHumanReadableDescriptionSpecified =
                !string.IsNullOrWhiteSpace(comparisonPropertyHumanReadableDescription);

            if (comparisonPropertyNameSpecified != comparisonPropertyHumanReadableDescriptionSpecified)
            {
                string message = $"If the {nameof(comparisonPropertyName)} is specified then the {nameof(comparisonPropertyHumanReadableDescription)} must be specified, and vice versa.";
                string parameterName;
                if (!comparisonPropertyNameSpecified) parameterName = nameof(comparisonPropertyName);
                else parameterName = nameof(comparisonPropertyHumanReadableDescription);
                throw new ArgumentNullException(parameterName, message);
            }

            // Add the ValidationError with these arguments to the list.
            errors.Add(new ValidationError(propertyName, errorFormatString, propertyHumanReadableDescription, comparisonPropertyName, comparisonPropertyHumanReadableDescription));
        }
    }
}
