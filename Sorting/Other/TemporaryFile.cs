using System.IO;
using System.Windows;

namespace Sorting
{
    public sealed class TemporaryFile : IDisposable
    {
        public TemporaryFile() :
          this(Path.GetTempPath())
        { }

        public TemporaryFile(string directory)
        {
            Create(Path.Combine(directory, Path.GetRandomFileName() + ".txt"));
        }

        ~TemporaryFile()
        {
            Delete();
        }

        public void Dispose()
        {
            Delete();
            GC.SuppressFinalize(this);
        }

        public string FilePath { get; private set; }

        private void Create(string path)
        {
            FilePath = path;

            TemporaryFileCleaner.TemporaryFiles.Add(path);
            using (File.Create(FilePath)) { };
        }

        private void Delete()
        {
            if (FilePath == null) return;
            File.Delete(FilePath);
            FilePath = null;
        }
    }

    public class TemporaryFileCleaner
    {
        public static List<string> TemporaryFiles = new List<string>();

        public static void Clear()
        {
            foreach (var file in TemporaryFiles)
            {
                File.Delete(file);
            }
        }
    }
}
