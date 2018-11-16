using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ZwiftMultiLauncher
{
    public partial class FormMain : Form
    {
        
        private readonly Timer _timer = new Timer();
        private static readonly List<Process> ProcessList = new List<Process>();
        private readonly bool _ready;
        private readonly DateTime _starteDateTime;

    
        public FormMain()
        {
            InitializeComponent();
            _timer.Interval = 5000;
            _timer.Enabled = true;
            _timer.Tick += _timer_Tick;
            _timer.Start();
            _starteDateTime = DateTime.Now;

            if (!PreCheck())
            {
                AddWarningMessage(@"zwift not ready, please update configuration", @"config not ready");
            }
            else
            {
                _ready = true;
                WindowState = FormWindowState.Minimized;
            }


        }

        void AddWarningMessage(string message, string caption)
        {
            labelError.Text = labelError.Text + message + @"\r\n";
            MessageBox.Show(message, caption,MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = DateTime.Now - _starteDateTime;

            double remaining = ConfigSettings.AutoClose - ts.TotalSeconds;
            labelSec.Text = remaining.ToString("0");

            if(remaining<=0)
                Close();

            if (!_ready)
                return;

            if(ConfigSettings.Resize)
            {
                Resizer resizer = new Resizer(ProcessList);
                resizer.ReSize();
            }
        }

 
        private bool PreCheck()
        {
            string exefile = ConfigSettings.ZwiftLocation + "\\ZwiftApp.exe";

            if (!System.IO.File.Exists(exefile))
            {
                AddWarningMessage(@"ZwiftApp File is missing " + exefile, @"Error file missing");
                return false;
            }

           
            for (int i = 1; i < ConfigSettings.Number + 1; i++)
            {
                string newDir = ConfigSettings.UserFolder + "\\zwift" + i.ToString("0#") + "\\Documents\\Zwift";

                if (!System.IO.Directory.Exists(newDir))
                {
                    DialogResult result = MessageBox.Show(newDir + @" is missing, create it?", @"Directory missing",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        try
                        {
                            FileTools.CopyToVirtualUSer(newDir);
                            //change perf.xml

                            ConfigFix configFix = new ConfigFix(newDir + "\\prefs.xml");
                            configFix.DisableFullScreen();
                            configFix.UpdateResolution(ConfigSettings.Resolution);
                        }
                        catch (Exception exception)
                        {
                            AddWarningMessage(@"Error " + exception.Message, @"Error updating files");
                            return false;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
            }



            return true;

        }

      

      
        private void FormMain_Load(object sender, EventArgs e)
        {
            StartZwift();
        }

        private void StartZwift()
        {
            
            for (int i = 1; i < ConfigSettings.Number + 1; i++)
            {
                string profileDir = ConfigSettings.UserFolder + "\\zwift" + i.ToString("0#");
                string progLocation = ConfigSettings.ZwiftLocation;


                UpdateResolution(profileDir);


                var startInfo = new ProcessStartInfo();
                startInfo.EnvironmentVariables["USERPROFILE"] = profileDir;
                startInfo.UseShellExecute = false;
                startInfo.FileName = progLocation + "\\ZwiftApp.exe";
                startInfo.WorkingDirectory = progLocation;
                Process process = Process.Start(startInfo);

                if (process!=null)
                    ProcessList.Add(process);

                System.Threading.Thread.Sleep(5000);

            }
        }

        private void UpdateResolution(string profileDir)
        {
            string configfile = profileDir + "\\Documents\\Zwift\\prefs.xml";

            ConfigFix configFix = new ConfigFix(configfile);
            configFix.DisableFullScreen();
            configFix.UpdateResolution(ConfigSettings.Resolution);
        }
    }
}