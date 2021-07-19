using System;
using System.Collections.Generic;
using System.Text;

namespace Match_
{
    public static class Main
    {
        private static readonly object entriesLock = new object();

        public static int partSize = 10000;
        
        public static string searchFile = "";
        public static string sourceFile = "";

        public static string lastResult = "";

        public static string targetText = "";
        public static List<string> tokens = new List<string>();
        
        public static List<Entry> publicEntries = new List<Entry>();

        public static void PullEntries(List<Entry> entries)
        {
            lock (entriesLock)
            {
                publicEntries.AddRange(entries);
            }
        }
    }
}
