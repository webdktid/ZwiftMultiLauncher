using System;

namespace ZwiftLauncher
{
    public class FileTools
    {

        public static void CopyToVirtualUSer(string virtualuserdir)
        {
            string sourceDir = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\Zwift";

            //create and copy data.
            System.IO.Directory.CreateDirectory(virtualuserdir);

            System.IO.Directory.CreateDirectory(virtualuserdir + "\\Activities");
            System.IO.Directory.CreateDirectory(virtualuserdir + "\\AudioBroadcasts");
            System.IO.Directory.CreateDirectory(virtualuserdir + "\\cp");
            System.IO.Directory.CreateDirectory(virtualuserdir + "\\Logs");
            System.IO.Directory.CreateDirectory(virtualuserdir + "\\TEMP");
            System.IO.Directory.CreateDirectory(virtualuserdir + "\\Workouts");

            System.IO.File.Copy(sourceDir + "\\knowndevices.xml", virtualuserdir + "\\knowndevices.xml");
            System.IO.File.Copy(sourceDir + "\\ZL.ini", virtualuserdir + "\\ZL.ini");
            System.IO.File.Copy(sourceDir + "\\prefs.xml", virtualuserdir + "\\prefs.xml");
        } 
    }
}
