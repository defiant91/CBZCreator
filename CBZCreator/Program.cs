using PowerArgs;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System;
using System.Diagnostics;
using System.IO;

namespace CBZCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Add some test data if we are debugging
                if (Debugger.IsAttached && args.Length < 1)
                {
                    args = Globals.AddDebugArguments(args);
                }

                Globals.InputArgs = Args.Parse<InputArgs>(args);
                if (Globals.InputArgs == null) { return; } // If help argument, returns null. Exit.
                Globals.VerboseLogging = Globals.InputArgs.Verbose;

                if (!string.IsNullOrWhiteSpace(Globals.InputArgs.ISBN))
                {
                    var coverSavedLocation = CoverFetcher.DownloadCover(Globals.InputArgs.ISBN, Globals.InputArgs.InputFolderPath, Globals.InputArgs.GoogleSearchAPIKey);
                }
                else if (!string.IsNullOrWhiteSpace(Globals.InputArgs.VolumeCoverPath))
                {
                    File.Copy(Globals.InputArgs.VolumeCoverPath, $"{Globals.InputArgs.InputFolderPath}\\0000_cover{Path.GetExtension(Globals.InputArgs.VolumeCoverPath)}");
                }

                if (!CreateZip())
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred! - {ex.Message}");
            }
        }

        static bool CreateZip()
        {
            if (File.Exists(Globals.InputArgs.OutputFilePath))
            {
                Console.WriteLine($"File '{Globals.InputArgs.OutputFilePath}' already exists, do you want to overwrite it? (Y/N)");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) || input.ToUpper() != "Y")
                {
                    return false;
                }
            }

            Console.WriteLine("Zipping up...");
            using (var archive = ZipArchive.Create())
            {
                archive.AddAllFromDirectory(Globals.InputArgs.InputFolderPath);

                archive.SaveTo(Globals.InputArgs.OutputFilePath, CompressionType.Deflate);
            }

            Console.WriteLine($"'{Globals.InputArgs.OutputFilePath}' created successfully!");
            return true;
        }
    }
}
