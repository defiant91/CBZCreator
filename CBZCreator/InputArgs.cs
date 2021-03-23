using PowerArgs;

namespace CBZCreator
{
    [TabCompletion]
    public class InputArgs
    {
        [HelpHook, ArgShortcut("-?"), ArgDescription("Shows this help")]
        public bool Help { get; set; }

        public bool Verbose { get; set; }

        [ArgDescription("The Google API key which is used to fetch volume covers."), ArgShortcut("-apikey"), ArgRequired, StickyArg]
        public string GoogleSearchAPIKey { get; set; }

        [ArgDescription("The ISBN of the volume to fetch the cover image.")]
        public string ISBN { get; set; }

        [ArgDescription("The path to the volume cover image."), ArgExistingFile]
        public string VolumeCoverPath { get; set; }

        [ArgRequired(PromptIfMissing = true), ArgDescription("The path to the folder to make a cbz."), ArgPosition(0), ArgExistingDirectory]
        public string InputFolderPath { get; set; }

        [ArgRequired(PromptIfMissing = true), ArgDescription("The path to place the cbz file."), ArgPosition(1)]
        public string OutputFilePath { get; set; }
    }
}
