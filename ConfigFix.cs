using System.Collections.Generic;
using System.Linq;

namespace ZwiftLauncher
{
    public class ConfigFix
    {
        private readonly string _file;
        private readonly List<string> _lines;

        public ConfigFix(string file)
        {
            _file = file;

            //xml files are not real xml files, read them as text

            _lines = System.IO.File.ReadAllLines(file).ToList();
        }

        public void Save()
        {

            System.IO.File.WriteAllLines(_file,_lines);
        }

        public void DisableFullScreen()
        {
            for (int i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].Contains("<FULLSCREEN>"))
                    _lines[i] = "<FULLSCREEN>0</FULLSCREEN>";
            }

        }

        public void UpdateResolution(string resolution)
        {

            for (int i = 0; i < _lines.Count; i++)
            {
                if (_lines[i].Contains("<USER_RESOLUTION_PREF>"))
                    _lines[i] = $"<USER_RESOLUTION_PREF>{resolution}</USER_RESOLUTION_PREF>";
            }
        }

    }
}
