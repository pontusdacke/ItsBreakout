using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ItsBreakout.Source
{
    public static class Settings
    {
        private static string settingsFilePath = @"gamedata.dat";

        public static void SaveSettings()
        {
            using (FileStream fs = new FileStream(settingsFilePath, FileMode.Create))
                using (BinaryWriter bw = new BinaryWriter(fs))
                    bw.Write(BreakoutGame.currentLevel);

        }

        public static void LoadSettings()
        {
            if (File.Exists(settingsFilePath))
                using (FileStream fs = new FileStream(settingsFilePath, FileMode.Open))
                    using (BinaryReader br = new BinaryReader(fs))
                    BreakoutGame.currentLevel = br.ReadInt32();
                
            else
                BreakoutGame.currentLevel = 1;
        }
    }
}
