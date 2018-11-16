using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZwiftLauncher
{
    public class Resizer
    {
        private readonly List<Process> _processList;
        static readonly IntPtr HwndTop = new IntPtr(0);


        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        public Resizer(List<Process> processList)
        {
            _processList = processList;
        }

        private void ReSize(int number, int left, int top, int width, int height)
        {
            if (_processList?[number] == null)
                return;

            if (_processList[number].HasExited)
                return;


            SetWindowPos(_processList[number].MainWindowHandle,
                HwndTop,
                left,
                top,
                width,
                height,
                0);
        }


        public void ReSize()
        {
            int numberOfZwifts = Convert.ToInt32(ConfigSettings.Number);


            var screen = Screen.PrimaryScreen.Bounds;

            if (numberOfZwifts == 2)
            {
                int with = screen.Width / 2;
                ReSize(0, 0, 0, with, screen.Height);
                ReSize(1, with, 0, with, screen.Height);
            }

            if (numberOfZwifts == 3)
            {
                int with = screen.Width / 3;
                ReSize(0, 0, 0, with, screen.Height);
                ReSize(1, with, 0, with, screen.Height);
                ReSize(2, with * 2, 0, with, screen.Height);
            }

            if (numberOfZwifts == 4)
            {
                int with = screen.Width / 2;
                int hight = screen.Height / 2;

                ReSize(0, 0, 0, with, hight);
                ReSize(1, with, 0, with, hight);
                ReSize(2, 0, hight, with, hight);
                ReSize(3, with, hight, with, hight);
            }
        }

    }
}
