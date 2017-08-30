using System;
using System.Text;

namespace PhotoBatchRenamer.Library.Types
{
    /// <summary>
    /// Holds information about a single file rename.
    /// </summary>
    public sealed class FileRename
    {
        public FileRename(
            string fromName,
            string toName)
        {
            // Validate arguments.
            if (string.IsNullOrWhiteSpace(fromName)) throw new ArgumentNullException(nameof(fromName));
            if (string.IsNullOrWhiteSpace(toName)) throw new ArgumentNullException(nameof(toName));

            // Make these arguments available to the object.
            FromName = fromName;
            ToName = toName;
        }

        public string FromName { get; private set; }

        public string ToName { get; private set; }

        #region ToString() override
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetType().Name);
            sb.Append(" {");

            sb.Append($"FromName = \"{FromName}\"");

            sb.Append(", ");
            sb.Append($"ToName = \"{ToName}\"");

            sb.Append("}");
            return sb.ToString();
        }
        #endregion // #region ToString() override
    }
}
