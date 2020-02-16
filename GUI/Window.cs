using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightTray.Models.Display;
using BrightTray.Win32;

namespace BrightTray
{
    public partial class Window : Form
    {
        private readonly MonitorCollection _monitorCollection = new MonitorCollection();

        public Window()
        {
            InitializeComponent();

            // read all monitors
            var @delegate = new NativeMethods.MonitorEnumDelegate(MonitorEnum);
            NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, @delegate, IntPtr.Zero);

            // set initial values of controls
            // assuming all monitors have identical Brightness min and max!!!
            foreach (Monitor monitor in _monitorCollection)
            {
                if (monitor.Brightness.Supported)
                {
                    trackBar.Minimum = (int) monitor.Brightness.Min;
                    trackBar.Maximum = (int) monitor.Brightness.Max;
                    trackBar.Value = (int) monitor.Brightness.Current;
                    label.Text = string.Format("{0} %", monitor.Brightness.Current);
                }
            }

            foreach (var screen in Screen.AllScreens) {
            }

        }

        ~Window()
        {
            // release monitors
            foreach (Monitor monitor in _monitorCollection)
            {
                NativeMethods.DestroyPhysicalMonitor(monitor.HPhysicalMonitor);
            }
        }

        private bool MonitorEnum(IntPtr hMonitor, IntPtr hdcMonitor, ref Rectangle lprcMonitor, IntPtr dwData)
        {
            _monitorCollection.Add(hMonitor);
            return true;
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            // change brightness
            uint newValue = (uint) trackBar.Value;
            foreach (Monitor monitor in _monitorCollection)
            {
                if (monitor.Brightness.Supported)
                {
                    monitor.Brightness.Current = newValue;
                    NativeMethods.SetMonitorBrightness(monitor.HPhysicalMonitor, newValue);
                }
            }
            label.Text = string.Format("{0} %", newValue);
        }
        
        private void Window_Activated(object sender, EventArgs e)
        {
            // position above the mouse 
            int X = Control.MousePosition.X - Width / 2;
            int Y = 10;
            if (Control.MousePosition.Y > Screen.PrimaryScreen.WorkingArea.Height / 2)
            {
                Y = Screen.PrimaryScreen.WorkingArea.Height - Height - 10;
            }
            SetDesktopLocation(X, Y);
        }

        private void Window_Deactivate(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
