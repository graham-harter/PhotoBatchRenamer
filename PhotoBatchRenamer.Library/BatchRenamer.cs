using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using PhotoBatchRenamer.Library.Interfaces;
using PhotoBatchRenamer.Library.Types;

namespace PhotoBatchRenamer.Library
{
    public sealed class BatchRenamer
    {
        private static readonly IDictionary<string, string> _propertyNameHumanReadableDescriptionLookup;
        private readonly IMessageOutputter _messageOutputter;

        #region Constructors
        static BatchRenamer()
        {
            // Set up the property name human-readable description lookup table.
            IDictionary<string, string> descriptions = new Dictionary<string, string>();

            descriptions.Add(Validator.ContainingFolderPathName, "containing folder path");
            descriptions.Add(Validator.SourceFilenamePrefixName, "source filename prefix");
            descriptions.Add(Validator.SourceFilenameCounterStartValueName, "source filename counter start value");
            descriptions.Add(Validator.SourceFilenameCounterFinishValueName, "source filename counter finish value");
            descriptions.Add(Validator.SourceFilenameExtensionName, "source filename extension");
            descriptions.Add(Validator.DestinationFilenamePrefixName, "destination filename prefix");
            descriptions.Add(Validator.DestinationFilenameCounterInitialValueName, "destination filename counter initial value");
            descriptions.Add(Validator.DestinationFilenameSuffixName, "destination filename suffix");

            _propertyNameHumanReadableDescriptionLookup =
                new ReadOnlyDictionary<string, string>(descriptions);
        }

        public BatchRenamer(
            IMessageOutputter messageOutputter)
        {
            // Validate argument(s).
            if (messageOutputter == null) throw new ArgumentNullException(nameof(messageOutputter));

            // Make these arguments available to the object.
            _messageOutputter = messageOutputter;
        }
        #endregion // #region Constructors

        #region Public methods
        public RenameFilesResult RenameFiles(
            BatchRenameArguments batchRenameArguments)
        {
            // Validate argument(s).
            if (batchRenameArguments == null) throw new ArgumentNullException(nameof(batchRenameArguments));

            Validator validator = new Validator(_propertyNameHumanReadableDescriptionLookup);
            var validationErrors = validator.Errors(batchRenameArguments);

            if (validationErrors.Count > 0)
            {
                throw new ArgumentException(
                    $"The supplied instance of {nameof(BatchRenameArguments)} is invalid for the following reason: {validationErrors[0].ErrorDescription}",
                    nameof(batchRenameArguments));
            }

            // Execute batch operation.
            var result = RenameFilesPrivate(batchRenameArguments);
            return result;
        }
        #endregion // #region Public methods

        #region Private methods
        private RenameFilesResult RenameFilesPrivate(
            BatchRenameArguments batchRenameArguments)
        {
            OutputBatchOperationHeaderInfo(batchRenameArguments);

            IList<FileInfo> selectedFiles = CollectListOfFilesToRename(batchRenameArguments);

            var result = RenameFiles(selectedFiles, batchRenameArguments);
            return result;
        }

        private void OutputBatchOperationHeaderInfo(
            BatchRenameArguments batchRenameArguments)
        {
            _messageOutputter.Output("=================================================");
            _messageOutputter.Output(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            _messageOutputter.Output("Batch rename arguments:");
            _messageOutputter.Output($"  {Validator.ContainingFolderPathName} = \"{batchRenameArguments.ContainingFolderPath}\"");
            _messageOutputter.Output($"  {Validator.SourceFilenamePrefixName} = \"{batchRenameArguments.SourceFilenamePrefix}\"");
            _messageOutputter.Output($"  {Validator.SourceFilenameCounterStartValueName} = {batchRenameArguments.SourceFilenameCounterStartValue}");
            _messageOutputter.Output($"  {Validator.SourceFilenameCounterFinishValueName} = {batchRenameArguments.SourceFilenameCounterFinishValue}");
            _messageOutputter.Output($"  {Validator.SourceFilenameExtensionName} = \"{batchRenameArguments.SourceFilenameExtension}\"");
            _messageOutputter.Output($"  {Validator.DestinationFilenamePrefixName} = \"{batchRenameArguments.DestinationFilenamePrefix}\"");
            _messageOutputter.Output($"  {Validator.DestinationFilenameCounterInitialValueName} = {batchRenameArguments.DestinationFilenameCounterInitialValue}");
            _messageOutputter.Output($"  {Validator.DestinationFilenameSuffixName} = \"{batchRenameArguments.DestinationFilenameSuffix}\"");
            _messageOutputter.Output("=================================================");
        }

        private IList<FileInfo> CollectListOfFilesToRename(
            BatchRenameArguments batchRenameArguments)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(batchRenameArguments.ContainingFolderPath);

            string searchPattern = CreateSourceFileSearchPattern(batchRenameArguments);

            IList<FileInfo> files =
                directoryInfo.EnumerateFiles(searchPattern)
                .ToList();

            IList<FileInfo> selectedFiles = SelectFilesMatchingPattern(
                files,
                batchRenameArguments);

            selectedFiles = OrderByName(selectedFiles);

            return selectedFiles;
        }

        private static string CreateSourceFileSearchPattern(
            BatchRenameArguments batchRenameArguments)
        {
            StringBuilder patternBuilder = new StringBuilder();
            patternBuilder.Append(batchRenameArguments.SourceFilenamePrefix);
            patternBuilder.Append("*");
            patternBuilder.Append(batchRenameArguments.SourceFilenameExtension);

            return patternBuilder.ToString();
        }

        private static IList<FileInfo> SelectFilesMatchingPattern(
            IList<FileInfo> allFiles,
            BatchRenameArguments batchRenameArguments)
        {
            var sourceFileFilterValues = new SourceFileFilterValues(batchRenameArguments);

            IList<FileInfo> selectedFiles = new List<FileInfo>();
            foreach (var fileInfo in allFiles)
            {
                if (FileMatchesSpecification(fileInfo, sourceFileFilterValues))
                {
                    selectedFiles.Add(fileInfo);
                }
            }

            return selectedFiles;
        }

        private static bool FileMatchesSpecification(
            FileInfo fileInfo,
            SourceFileFilterValues filterValues)
        {
            string filename = fileInfo.Name;
            string filenameWithoutPrefix = filename.Remove(0, filterValues.PrefixLength);
            string numericComponent = filenameWithoutPrefix.Substring(0, filterValues.NumericComponentLength);

            int numericValue = int.Parse(numericComponent, CultureInfo.InvariantCulture);

            return
                (numericValue >= filterValues.StartValueInt) &&
                (numericValue <= filterValues.FinishValueInt);
        }

        private static IList<FileInfo> OrderByName(IList<FileInfo> files)
        {
            IList<FileInfo> result = files
                .OrderBy(f => f.Name)
                .ToList();

            return result;
        }

        private RenameFilesResult RenameFiles(
            IList<FileInfo> selectedFiles,
            BatchRenameArguments batchRenameArguments)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(batchRenameArguments.ContainingFolderPath);

            // Pull out some info. from the arguments object.
            var prefix = batchRenameArguments.DestinationFilenamePrefix;
            var suffix = batchRenameArguments.DestinationFilenameSuffix;
            string targetExtension = batchRenameArguments.SourceFilenameExtension.ToLowerInvariant();

            int incrementingNumberLength = batchRenameArguments.DestinationFilenameCounterInitialValue.Length;
            int nextNumberToAllocate = int.Parse(batchRenameArguments.DestinationFilenameCounterInitialValue);

            IList<FileRename> fileRenames = new List<FileRename>(selectedFiles.Count);

            for (int i = 0; i < selectedFiles.Count; i++)
            {
                FileInfo file = selectedFiles[i];

                var sourceFilename = file.Name;
                var destinationFilename = ComposeDestinationFilename(
                    prefix,
                    nextNumberToAllocate,
                    incrementingNumberLength,
                    suffix,
                    targetExtension);

                var destinationFullPath = Path.Combine(directoryInfo.FullName, destinationFilename);

                File.Move(file.FullName, destinationFullPath);

                fileRenames.Add(new FileRename(sourceFilename, destinationFilename));

                _messageOutputter.Output($"Renamed \"{sourceFilename}\" to \"{destinationFilename}\"");

                nextNumberToAllocate++;
            }

            int numberOfFilesRenamed = fileRenames.Count;

            _messageOutputter.Output($"{numberOfFilesRenamed} files renamed.");

            return new RenameFilesResult(numberOfFilesRenamed, fileRenames);
        }

        private static string ComposeDestinationFilename(
            string prefix,
            int numberToAllocate,
            int incrementingNumberLength,
            string suffix,
            string extension)
        {
            string zeroPaddedAllocatedNumber = ZeroPad(numberToAllocate, incrementingNumberLength);
            return string.Concat(prefix, zeroPaddedAllocatedNumber, suffix, extension);
        }

        private static string ZeroPad(int value, int length)
        {
            string valueAsString = value.ToString(CultureInfo.InvariantCulture);
            int numberOfZeroesToPrepend = length - valueAsString.Length;

            if (numberOfZeroesToPrepend <= 0)
            {
                return valueAsString;
            }

            return string.Concat(new string('0', numberOfZeroesToPrepend), valueAsString);
        }
        #endregion // #region Private methods

        #region Private class SourceFileFilterValues
        private sealed class SourceFileFilterValues
        {
            public SourceFileFilterValues(
                BatchRenameArguments batchRenameArguments)
                : this(
                      batchRenameArguments.SourceFilenamePrefix.Length,
                      batchRenameArguments.SourceFilenameCounterStartValue,
                      batchRenameArguments.SourceFilenameCounterFinishValue)
            {
            }

            public SourceFileFilterValues(
                int prefixLength,
                string startValueString,
                string finishValueString)
            {
                // Make these arguments available to the object.
                PrefixLength = prefixLength;
                StartValueString = startValueString;
                FinishValueString = finishValueString;

                // Make the int conversion values available to the object.
                StartValueInt = int.Parse(startValueString, CultureInfo.InvariantCulture);
                FinishValueInt = int.Parse(finishValueString, CultureInfo.InvariantCulture);

                // Make other derived values available to the object.
                NumericComponentLength = StartValueString.Length;
            }

            public int PrefixLength { get; private set; }

            public string StartValueString { get; private set; }

            public int StartValueInt { get; private set; }

            public string FinishValueString { get; private set; }

            public int FinishValueInt { get; private set; }

            public int NumericComponentLength { get; private set; }

            #region ToString() override
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetType().Name);
                sb.Append(" {");

                sb.Append($"PrefixLength = {PrefixLength}");

                sb.Append(", ");
                sb.Append($"StartValueString = \"{StartValueString}\"");

                sb.Append(", ");
                sb.Append($"StartValueInt = {StartValueInt}");

                sb.Append(", ");
                sb.Append($"FinishValueString = \"{FinishValueString}\"");

                sb.Append(", ");
                sb.Append($"FinishValueInt = {FinishValueInt}");

                sb.Append(", ");
                sb.Append($"NumericComponentLength = {NumericComponentLength}");

                sb.Append("}");
                return sb.ToString();
            }
            #endregion // #region ToString() override
        }
        #endregion // #region Private class SourceFileFilterValues
    }
}
