using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using PhotoBatchRenamer.Library.Extensions;

namespace PhotoBatchRenamer.Library
{
    public sealed class Validator
    {
        public const string ContainingFolderPathName = nameof(BatchRenameArguments.ContainingFolderPath);
        public const string SourceFilenamePrefixName = nameof(BatchRenameArguments.SourceFilenamePrefix);
        public const string SourceFilenameCounterStartValueName = nameof(BatchRenameArguments.SourceFilenameCounterStartValue);
        public const string SourceFilenameCounterFinishValueName = nameof(BatchRenameArguments.SourceFilenameCounterFinishValue);
        public const string SourceFilenameExtensionName = nameof(BatchRenameArguments.SourceFilenameExtension);
        public const string DestinationFilenamePrefixName = nameof(BatchRenameArguments.DestinationFilenamePrefix);
        public const string DestinationFilenameCounterInitialValueName = nameof(BatchRenameArguments.DestinationFilenameCounterInitialValue);
        public const string DestinationFilenameSuffixName = nameof(BatchRenameArguments.DestinationFilenameSuffix);

        private const NumberStyles FileCounterNumberStyle = NumberStyles.None;

        private readonly IDictionary<string, string> _propertyNameHumanReadableDescriptionLookup;

        #region Constructor(s)
        public Validator(
            IDictionary<string, string> propertyNameHumanReadableDescriptionLookup)
        {
            // Validate argument(s).
            if (propertyNameHumanReadableDescriptionLookup == null)
            {
                throw new ArgumentNullException(nameof(propertyNameHumanReadableDescriptionLookup));
            }

            // Make these arguments available to the object.
            _propertyNameHumanReadableDescriptionLookup = propertyNameHumanReadableDescriptionLookup;
        }
        #endregion // #region Constructor(s)

        #region Public methods
        public bool IsValid(BatchRenameArguments args)
        {
            // Validate argument.
            if (args == null) throw new ArgumentNullException(nameof(args));

            return ErrorsPrivate(args).Count == 0;
        }

        public IList<ValidationError> Errors(BatchRenameArguments args)
        {
            // Validate argument.
            if (args == null) throw new ArgumentNullException(nameof(args));

            return ErrorsPrivate(args);
        }
        #endregion // #region Public methods

        #region Private methods
        private IList<ValidationError> ErrorsPrivate(BatchRenameArguments args)
        {
            // Declare result variable.
            IList<ValidationError> errors = new List<ValidationError>();

            // Validate ContainingFolderPath.
            if (string.IsNullOrWhiteSpace(args.ContainingFolderPath))
            {
                errors.Add(ContainingFolderPathName,
                    "The {0} must be specified.",
                    _propertyNameHumanReadableDescriptionLookup[ContainingFolderPathName]);
            }
            else if (!Directory.Exists(args.ContainingFolderPath))
            {
                errors.Add(ContainingFolderPathName,
                    "The {0} must be a valid folder path.",
                    _propertyNameHumanReadableDescriptionLookup[ContainingFolderPathName]);
            }

            // Validate SourceFilenamePrefix.
            if (string.IsNullOrWhiteSpace(args.SourceFilenamePrefix))
            {
                errors.Add(SourceFilenamePrefixName,
                    "The {0} must be specified.",
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenamePrefixName]);
            }

            // Validate SourceFilenameCounterStartValue, SourceFilenameCounterFinishValue.
            ValidateSourceFilenameCounters(errors, args);

            // Validate SourceFilenameExtension.
            if (string.IsNullOrWhiteSpace(args.SourceFilenameExtension))
            {
                errors.Add(SourceFilenameExtensionName,
                    "The {0} must be specified.",
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenameExtensionName]);
            }
            else if (!args.SourceFilenameExtension.StartsWith("."))
            {
                errors.Add(SourceFilenameExtensionName,
                    "The {0} must begin with a period (.) character.",
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenameExtensionName]);
            }

            // Validate DestinationFilenamePrefix, DestinationFilenameSuffix.
            ValidateDestinationFilenameParts(errors, args);

            // Validate DestinationFilenameCounterInitialValue.
            ValidateDestinationFilenameCounterInitialValue(errors, args);

            // Return all errors found.
            return errors;
        }

        private void ValidateSourceFilenameCounters(IList<ValidationError> errors, BatchRenameArguments args)
        {
            bool startValueValidated = false;
            bool finishValueValidated = false;

            int startValueInt = -1;
            int finishValueInt = -1;

            // Validate SourceFilenameCounterStartValue in isolation.
            if (string.IsNullOrWhiteSpace(args.SourceFilenameCounterStartValue))
            {
                errors.Add(SourceFilenameCounterStartValueName,
                    "The {0} must be specified.",
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenameCounterStartValueName]);
                startValueValidated = true;
            }
            else if (!TryParseAsFileCounter(args.SourceFilenameCounterStartValue, out startValueInt))
            {
                errors.Add(SourceFilenameCounterStartValueName,
                    "The {0} must be a non-negative integer value.",
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenameCounterStartValueName]);
                startValueValidated = true;
            }

            // Validate SourceFilenameCounterFinishValue in isolation.
            if (string.IsNullOrWhiteSpace(args.SourceFilenameCounterFinishValue))
            {
                errors.Add(SourceFilenameCounterFinishValueName,
                    "The {0} must be specified.",
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenameCounterFinishValueName]);
                finishValueValidated = true;
            }
            else if (!TryParseAsFileCounter(args.SourceFilenameCounterFinishValue, out finishValueInt))
            {
                errors.Add(SourceFilenameCounterFinishValueName,
                    "The {0} must be a non-negative integer value.",
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenameCounterFinishValueName]);
                finishValueValidated = true;
            }

            // Can return now if either value has been validated (produced errors) in isolation.
            if (startValueValidated || finishValueValidated) return;

            // Validate both values together.
            if (args.SourceFilenameCounterStartValue.Length != args.SourceFilenameCounterFinishValue.Length)
            {
                errors.Add(SourceFilenameCounterFinishValueName,
                    "The {1} and {0} must be the same length.",
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenameCounterFinishValueName],
                    SourceFilenameCounterStartValueName,
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenameCounterStartValueName]);
            }
            else if (startValueInt > finishValueInt)
            {
                errors.Add(SourceFilenameCounterFinishValueName,
                    "The {1} must be less than or equal to the {0}.",
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenameCounterFinishValueName],
                    SourceFilenameCounterStartValueName,
                    _propertyNameHumanReadableDescriptionLookup[SourceFilenameCounterStartValueName]);
            }
        }

        private void ValidateDestinationFilenameParts(IList<ValidationError> errors, BatchRenameArguments args)
        {
            var prefixLength = args.DestinationFilenamePrefix.Length;
            var suffixLength = args.DestinationFilenameSuffix.Length;

            if (prefixLength + suffixLength == 0)
            {
                errors.Add(DestinationFilenameSuffixName,
                    "Either a {1}, or a {0}, or both, must be specified.",
                    _propertyNameHumanReadableDescriptionLookup[DestinationFilenameSuffixName],
                    DestinationFilenamePrefixName,
                    _propertyNameHumanReadableDescriptionLookup[DestinationFilenamePrefixName]);
            }
        }

        private void ValidateDestinationFilenameCounterInitialValue(IList<ValidationError> errors, BatchRenameArguments args)
        {
            int valueInt;

            if (string.IsNullOrWhiteSpace(args.DestinationFilenameCounterInitialValue))
            {
                errors.Add(DestinationFilenameCounterInitialValueName,
                    "The {0} must be specified.",
                    _propertyNameHumanReadableDescriptionLookup[DestinationFilenameCounterInitialValueName]);
            }
            else if (!TryParseAsFileCounter(args.DestinationFilenameCounterInitialValue, out valueInt))
            {
                errors.Add(DestinationFilenameCounterInitialValueName,
                    "The {0} must be a non-negative integer value.",
                    _propertyNameHumanReadableDescriptionLookup[DestinationFilenameCounterInitialValueName]);
            }
        }

        private static bool TryParseAsFileCounter(string value, out int result)
        {
            return int.TryParse(
                value,
                FileCounterNumberStyle,
                CultureInfo.InvariantCulture,
                out result);
        }
        #endregion // #region Private methods
    }
}
