using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PhotoBatchRenamer.Library;
using PhotoBatchRenamer.Library.Types;

namespace PhotoBatchRenamer
{
    public partial class MainForm : Form
    {
        private readonly IDictionary<string, ValidationPropertyInfo> _validationPropertyInfoLookup;
        private readonly IDictionary<string, string> _validationPropertyHumanReadableDescriptions;
        private readonly Validator _validator;
        private readonly IDictionary<string, Control> _errorProviderControlLookup;
        private readonly IList<Control> _controlsToValidate;
        private readonly IList<string> _validationPropertyNames;

        private bool _hasDefaultFolderBeenSet;

        public MainForm()
        {
            InitializeComponent();

            // Initialize the validation property info lookup.
            _validationPropertyInfoLookup = CreateValidationPropertyInfoLookup();

            // Initialize the validation property human-readable description lookup.
            _validationPropertyHumanReadableDescriptions = CreateValidationPropertyHumanReadableDescriptions(_validationPropertyInfoLookup);

            // Initialize the parameter validator.
            _validator = new Validator(_validationPropertyHumanReadableDescriptions);

            // Initialize the error provider control lookup.
            _errorProviderControlLookup = CreateErrorProviderControlLookup(_validationPropertyInfoLookup);

            // Initialize the validation property name list.
            _validationPropertyNames = CreateValidationPropertyNames(_errorProviderControlLookup);

            // Initialize the list of controls to validate.
            _controlsToValidate = CreateListOfControlsToValidate(_errorProviderControlLookup);

            textBoxContainingFolder.Text = GetMyDocumentsFolderPath();
            comboBoxSourceFilenameSuffix.SelectedIndex = 0;
        }

        #region Private methods
        private IDictionary<string, ValidationPropertyInfo> CreateValidationPropertyInfoLookup()
        {
            IDictionary<string, ValidationPropertyInfo> result = new Dictionary<string, ValidationPropertyInfo>();

            Add(result, Validator.ContainingFolderPathName, "containing folder path", buttonContainingFolderBrowse);
            Add(result, Validator.SourceFilenamePrefixName, "source filename prefix", textBoxSourceFilenamePrefix);
            Add(result, Validator.SourceFilenameCounterStartValueName, "start value", textBoxSourceFilenameCounterStart);
            Add(result, Validator.SourceFilenameCounterFinishValueName, "finish value", textBoxSourceFilenameCounterFinish);
            Add(result, Validator.SourceFilenameExtensionName, "filename extension", comboBoxSourceFilenameSuffix);
            Add(result, Validator.DestinationFilenamePrefixName, "destination filename prefix", textBoxDestinationFilenamePrefix);
            Add(result, Validator.DestinationFilenameCounterInitialValueName, "destination filename counter initial value", textBoxDestinationFilenameCounterInitialValue);
            Add(result, Validator.DestinationFilenameSuffixName, "destination filename suffix", textBoxDestinationFilenameSuffix);

            return result;
        }

        private static void Add(
            IDictionary<string, ValidationPropertyInfo> validationPropertyInfoLookup,
            string propertyName,
            string propertyHumanReadableDescription,
            Control control)
        {
            validationPropertyInfoLookup.Add(
                propertyName,
                new ValidationPropertyInfo(propertyHumanReadableDescription, control));
        }

        private static IDictionary<string, string> CreateValidationPropertyHumanReadableDescriptions(
            IDictionary<string, ValidationPropertyInfo> validationPropertyInfoLookup)
        {
            IDictionary<string, string> result =
                validationPropertyInfoLookup
                .ToDictionary(
                    pil => pil.Key,
                    pil => pil.Value.PropertyHumanReadableDescription);

            return result;
        }

        private IDictionary<string, Control> CreateErrorProviderControlLookup(
            IDictionary<string, ValidationPropertyInfo> validationPropertyInfoLookup)
        {
            IDictionary<string, Control> result =
                validationPropertyInfoLookup
                .ToDictionary(
                    pil => pil.Key,
                    pil => pil.Value.Control);

            return result;
        }

        private static IList<string> CreateValidationPropertyNames(
            IDictionary<string, Control> errorProviderControlLookup)
        {
            IList<string> result =
                errorProviderControlLookup
                .Keys
                .ToList();

            return result;
        }

        private static IList<Control> CreateListOfControlsToValidate(
            IDictionary<string, Control> errorProviderControlLookup)
        {
            IList<Control> result =
                errorProviderControlLookup
                .Values
                .ToList();

            return result;
        }

        private void OnContainingFolderBrowseClick(object sender, EventArgs e)
        {
            NormalizeContainingFolderPath();

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            SetDefaultContainingFolderPathOnFirstUse(dialog);
            SetInitialContainingFolderPath(dialog);
            dialog.ShowNewFolderButton = false;
            DialogResult dialogResult = dialog.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                textBoxContainingFolder.Text = dialog.SelectedPath;
                _hasDefaultFolderBeenSet = true;

                ValidateBatchRenameArguments((string)((Control)sender).Tag);
            }
        }

        private static string GetMyDocumentsFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void NormalizeContainingFolderPath()
        {
            string path = textBoxContainingFolder.Text ?? string.Empty;
            path = path.Trim();

            textBoxContainingFolder.Text = path;
        }

        private void SetDefaultContainingFolderPathOnFirstUse(FolderBrowserDialog dialog)
        {
            if (!_hasDefaultFolderBeenSet)
            {
                //dialog.RootFolder = Environment.SpecialFolder.MyDocuments;
            }
        }

        private void SetInitialContainingFolderPath(FolderBrowserDialog dialog)
        {
            // If there is a valid path in the text box, use that.
            string textBoxPath = textBoxContainingFolder.Text ?? string.Empty;
            if (!string.IsNullOrEmpty(textBoxPath)
                && Directory.Exists(textBoxPath))
            {
                dialog.SelectedPath = textBoxPath;
            }
        }

        private void OnLeaveBatchRenameParameterControl(object sender, EventArgs e)
        {
            // Pull out the property name filter from the control.
            Control control = sender as Control;
            string propertyNameFilter = (control != null ? (string)control.Tag : null) ?? string.Empty; 

            ValidateBatchRenameArguments(propertyNameFilter);
        }

        private void OnRenamePhotosClicked(object sender, EventArgs e)
        {
            // Last chance check for errors!
            ValidateBatchRenameArguments();

            var batchRenameArguments = CreateBatchRenameArguments();
            var errors = GetBatchRenameArgumentsErrors(batchRenameArguments);
            if (errors.Count > 0)
            {
                MessageBox.Show(this,
                    "There are some errors on the form. Please correct these before proceeding.",
                    "Errors found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var messageOutputter = new FileMessageOutputter("BatchRenamer.txt");

            RenameFilesResult renameFilesResult = null;
            try
            {
                var batchRenamer = new BatchRenamer(messageOutputter);
                renameFilesResult = batchRenamer.RenameFiles(batchRenameArguments);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"An error occurred when attempting to rename files. The error was: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // Log the exception somewhere!
                messageOutputter.Output("An error occurred:");
                messageOutputter.Output($"  {ex.Message}");
                messageOutputter.Output($"  {ex.StackTrace}");

                return;
            }

            MessageBox.Show(this,
                $"Completed!\n\n{renameFilesResult.NumberOfFilesRenamed} files were renamed.",
                "Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ValidateBatchRenameArguments(string propertyNameFilter = null)
        {
            var errors = GetBatchRenameArgumentsErrors(CreateBatchRenameArguments());

            var errorsOnePerPropertyName =
                errors
                .GroupBy(er => er.PropertyName)
                .Select(erg => erg.First())
                .ToList();

            var errorsOnePerPropertyNameFiltered =
                errorsOnePerPropertyName
                .Where(er =>
                    string.IsNullOrEmpty(propertyNameFilter)
                    || er.PropertyName == propertyNameFilter)
                .ToList();

            var errorsOnePerControlFiltered =
                errorsOnePerPropertyNameFiltered
                .Select(er => new
                {
                    Control = _errorProviderControlLookup[er.PropertyName],
                    ErrorDescription = er.ErrorHumanReadableDescription
                })
                .ToList();

            IList<Control> controlsToValidate;
            if (string.IsNullOrEmpty(propertyNameFilter))
            {
                controlsToValidate = _controlsToValidate;
            }
            else
            {
                controlsToValidate = new List<Control>(new Control[]
                {
                    _errorProviderControlLookup[propertyNameFilter]
                });
            }

            foreach (var control in controlsToValidate)
            {
                var error =
                    errorsOnePerControlFiltered
                    .FirstOrDefault(er => er.Control == control);

                if (error == null)
                {
                    errorProvider.SetError(control, string.Empty);
                }
                else
                {
                    errorProvider.SetError(control, error.ErrorDescription);
                }
            }

            buttonRenamePhotos.Enabled = (errors.Count == 0);
        }

        private IList<ValidationError> GetBatchRenameArgumentsErrors(
            BatchRenameArguments batchRenameArguments)
        {
            var errors = _validator.Errors(batchRenameArguments);

            return errors;
        }

        private BatchRenameArguments CreateBatchRenameArguments()
        {
            var result = new BatchRenameArguments(
                textBoxContainingFolder.Text ?? string.Empty,
                textBoxSourceFilenamePrefix.Text ?? string.Empty,
                textBoxSourceFilenameCounterStart.Text ?? string.Empty,
                textBoxSourceFilenameCounterFinish.Text ?? string.Empty,
                comboBoxSourceFilenameSuffix.Text ?? string.Empty,
                textBoxDestinationFilenamePrefix.Text ?? string.Empty,
                textBoxDestinationFilenameCounterInitialValue.Text ?? string.Empty,
                textBoxDestinationFilenameSuffix.Text ?? string.Empty);

            return result;
        }
        #endregion // #region Private methods

        #region Private nested class ValidationPropertyInfo
        private sealed class ValidationPropertyInfo
        {
            public ValidationPropertyInfo(
                string propertyHumanReadableDescription,
                Control control)
            {
                // Validate arguments.
                if (string.IsNullOrWhiteSpace(propertyHumanReadableDescription))
                {
                    throw new ArgumentNullException(nameof(propertyHumanReadableDescription));
                }
                if (control == null) throw new ArgumentNullException(nameof(control));

                // Make these arguments available to the object.
                PropertyHumanReadableDescription = propertyHumanReadableDescription;
                Control = control;
            }

            public string PropertyHumanReadableDescription { get; private set; }

            public Control Control { get; private set; }

            #region ToString() override
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetType().Name);
                sb.Append(" {");

                sb.Append($"PropertyHumanReadableDescription = \"{PropertyHumanReadableDescription}\"");

                sb.Append(", ");
                sb.Append($"Control = \"{Control}\"");

                sb.Append("}");
                return sb.ToString();
            }
            #endregion // #region ToString() override
        }
        #endregion // #region Private nested class ValidationPropertyInfo
    }
}
