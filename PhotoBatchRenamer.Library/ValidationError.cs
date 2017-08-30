using System;
using System.Text;

namespace PhotoBatchRenamer.Library
{
    /// <summary>
    /// Class which holds details of a single validation error.
    /// </summary>
    public sealed class ValidationError
    {
        public ValidationError(
            string propertyName,
            string errorFormatString,
            string propertyHumanReadableDescription,
            string comparisonPropertyName = null,
            string comparisonPropertyHumanReadableDescription = null)
        {
            // Validate arguments.
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

            // Make these arguments available to the object.
            PropertyName = propertyName.Trim();
            ErrorFormatString = errorFormatString.Trim();
            PropertyHumanReadableDescription = propertyHumanReadableDescription.Trim();
            ComparisonPropertyName = (comparisonPropertyName ?? string.Empty).Trim();
            ComparisonPropertyHumanReadableDescription = (comparisonPropertyHumanReadableDescription ?? string.Empty).Trim();
        }

        public string PropertyName { get; private set; }

        public string ErrorFormatString { get; private set; }

        public string PropertyHumanReadableDescription { get; private set; }

        public string ComparisonPropertyName { get; private set; }

        public string ComparisonPropertyHumanReadableDescription { get; private set; }

        public string ErrorDescription
        {
            get { return string.Format(ErrorFormatString, PropertyName, ComparisonPropertyName); }
        }

        public string ErrorHumanReadableDescription
        {
            get { return string.Format(ErrorFormatString, PropertyHumanReadableDescription, ComparisonPropertyHumanReadableDescription); }
        }

        #region ToString() override
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetType().Name);
            sb.Append(" {");

            sb.Append($"PropertyName = \"{PropertyName}\"");

            sb.Append(", ");
            sb.Append($"ErrorHumanReadableDescription = \"{ErrorHumanReadableDescription}\"");

            sb.Append("}");
            return sb.ToString();
        }
        #endregion // #region ToString() override
    }
}
