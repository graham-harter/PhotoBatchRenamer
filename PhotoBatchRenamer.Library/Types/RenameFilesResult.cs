using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PhotoBatchRenamer.Library.Types
{
    /// <summary>
    /// Object holding the results of a batch file rename.
    /// </summary>
    public sealed class RenameFilesResult
    {
        public RenameFilesResult(
            int numberOfFilesRenamed,
            IList<FileRename> filesRenamed)
        {
            // Validate arguments.
            if (filesRenamed == null) throw new ArgumentNullException(nameof(filesRenamed));

            // Make these arguments available to the object.
            NumberOfFilesRenamed = numberOfFilesRenamed;
            FilesRenamed = new ReadOnlyCollection<FileRename>(filesRenamed);
        }

        public int NumberOfFilesRenamed { get; private set; }

        public IReadOnlyCollection<FileRename> FilesRenamed { get; private set; }

        #region ToString() override
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetType().Name);
            sb.Append(" {");

            sb.Append($"NumberOfFilesRenamed = {NumberOfFilesRenamed}");

            sb.Append(", ");
            sb.Append($"FilesRenamed = {StringHelper.CollectionOrNull(FilesRenamed)}");

            sb.Append("}");
            return sb.ToString();
        }
        #endregion // #region ToString() override
    }
}
