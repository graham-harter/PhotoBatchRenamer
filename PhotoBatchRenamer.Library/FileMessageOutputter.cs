using System;
using System.IO;
using PhotoBatchRenamer.Library.Interfaces;

namespace PhotoBatchRenamer.Library
{
    public sealed class FileMessageOutputter : IMessageOutputter
    {
        private readonly object _lockObject = new object();
        private readonly string _outputFilename;

        public FileMessageOutputter(string outputFilename)
        {
            // Validate argument(s).
            if (string.IsNullOrWhiteSpace(outputFilename)) throw new ArgumentNullException(nameof(outputFilename));

            // Make these arguments available to the object.
            _outputFilename = outputFilename;
        }

        public void Output(string message)
        {
            // Validate argument(s).
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(nameof(message));

            // Output message to file.
            try
            {
                lock (_lockObject)
                {
                    using (StreamWriter sw = new StreamWriter(_outputFilename, true))
                    {
                        sw.WriteLine(message);
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Swallow exception writing file.
            }
        }
    }
}
