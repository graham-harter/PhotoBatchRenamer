using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoBatchRenamer.Library
{
    public sealed class BatchRenameArguments
    {
        public BatchRenameArguments(
            string containingFolderPath,
            string sourceFilenamePrefix,
            string sourceFilenameCounterStartValue,
            string sourceFilenameCounterFinishValue,
            string sourceFilenameExtension,
            string destinationFilenamePrefix,
            string destinationFilenameCounterInitialValue,
            string destinationFilenameSuffix
            )
        {
            // Validate arguments.
            ValidateIsNotNull(containingFolderPath, nameof(containingFolderPath));
            ValidateIsNotNull(sourceFilenamePrefix, nameof(sourceFilenamePrefix));
            ValidateIsNotNull(sourceFilenameCounterStartValue, nameof(sourceFilenameCounterStartValue));
            ValidateIsNotNull(sourceFilenameCounterFinishValue, nameof(sourceFilenameCounterFinishValue));
            ValidateIsNotNull(sourceFilenameExtension, nameof(sourceFilenameExtension));
            ValidateIsNotNull(destinationFilenamePrefix, nameof(destinationFilenamePrefix));
            ValidateIsNotNull(destinationFilenameCounterInitialValue, nameof(destinationFilenameCounterInitialValue));
            ValidateIsNotNull(destinationFilenameSuffix, nameof(destinationFilenameSuffix));

            // Make these arguments available to the object.
            ContainingFolderPath = containingFolderPath.Trim();
            SourceFilenamePrefix = sourceFilenamePrefix.TrimStart();
            SourceFilenameCounterStartValue = sourceFilenameCounterStartValue.Trim();
            SourceFilenameCounterFinishValue = sourceFilenameCounterFinishValue.Trim();
            SourceFilenameExtension = sourceFilenameExtension.Trim();
            DestinationFilenamePrefix = destinationFilenamePrefix.TrimStart();
            DestinationFilenameCounterInitialValue = destinationFilenameCounterInitialValue.Trim();
            DestinationFilenameSuffix = destinationFilenameSuffix.TrimEnd();
        }

        #region Public properties
        public string ContainingFolderPath { get; private set; }

        public string SourceFilenamePrefix { get; private set; }

        public string SourceFilenameCounterStartValue { get; private set; }

        public string SourceFilenameCounterFinishValue { get; private set; }

        public string SourceFilenameExtension { get; private set; }

        public string DestinationFilenamePrefix { get; private set; }

        public string DestinationFilenameCounterInitialValue { get; private set; }

        public string DestinationFilenameSuffix { get; private set; }
        #endregion // #region Public properties

        #region Private methods
        private void ValidateIsNotNull(string value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
        #endregion // #region Private methods

        #region ToString() override
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetType().Name);
            sb.Append(" {");

            sb.Append($"ContainingFolderPath = {StringHelper.StringValueOrNull(ContainingFolderPath)}");

            sb.Append(", ");
            sb.Append($"SourceFilenamePrefix = {StringHelper.StringValueOrNull(SourceFilenamePrefix)}");

            sb.Append(", ");
            sb.Append($"SourceFilenameCounterStartValue = {StringHelper.StringValueOrNull(SourceFilenameCounterStartValue)}");

            sb.Append(", ");
            sb.Append($"SourceFilenameCounterFinishValue = {StringHelper.StringValueOrNull(SourceFilenameCounterFinishValue)}");

            sb.Append(", ");
            sb.Append($"SourceFilenameExtension = {StringHelper.StringValueOrNull(SourceFilenameExtension)}");

            sb.Append(", ");
            sb.Append($"DestinationFilenamePrefix = {StringHelper.StringValueOrNull(DestinationFilenamePrefix)}");

            sb.Append(", ");
            sb.Append($"DestinationFilenameCounterInitialValue = {StringHelper.StringValueOrNull(DestinationFilenameCounterInitialValue)}");

            sb.Append(", ");
            sb.Append($"DestinationFilenameSuffix = {StringHelper.StringValueOrNull(DestinationFilenameSuffix)}");

            sb.Append("}");
            return sb.ToString();
        }
        #endregion // #region ToString() override
    }
}
