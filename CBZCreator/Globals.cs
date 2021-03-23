namespace CBZCreator
{
    public static class Globals
    {
        public static InputArgs InputArgs { get; set; }

        public static bool VerboseLogging { get; set; }

        public static string[] AddDebugArguments(string[] args)
        {
            // ISBNs FOR TESTING
            // Trinity Seven 15.5 - 1975382951
            // Three Years Apart 1 - 9784757558120
            // Blade Dance of Elementars 1 - 9784040668048
            // Tsuki 50 Man Moratte Mo Ikigai No Nai Tonari No Oneesan Ni 30 Man De Yatowarete "Okaeri" Tte Iu Oshigoto Ga Tanoshi 1 - 9784865547726

            return new string[]
            {
                @"D:\Downloads\workspace\test",                                 // Default arg1, InputFolderPath
                @"D:\Downloads\workspace\output.cbz",                           // Default arg2, OutputFilePath
                "-ISBN", "9784865547726",
                "-Verbose",
                //"-VolumeCoverPath", @"D:\Downloads\workspace\cover.jpg",
                //"-GoogleSearchAPIKey", "MYAPIKEY"
            };
        }
    }
}
