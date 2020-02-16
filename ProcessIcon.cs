using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BrightTray.Properties;

namespace BrightTray
{
    class ProcessIcon : IDisposable
    {
        NotifyIcon ni;
        Window controls;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessIcon"/> class.
        /// </summary>
        public ProcessIcon()
        {
            ni = new NotifyIcon();
            controls = new Window();
        }

        /// <summary>
        /// Displays the icon in the system tray.
        /// </summary>
        public void Display()
        {
            // Put the icon in the system tray and allow it react to mouse clicks.			
            ni.MouseClick += new MouseEventHandler(ni_MouseClick);
            ni.Icon = Resources.BrightTray;
            ni.Text = "BrightTray";
            ni.Visible = true;

            // Attach a context menu.
            ni.ContextMenu = new ContextMenus().Create();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            // When the application closes, this will remove the icon from the system tray immediately.
            ni.Dispose();
            controls.Dispose();
        }

        /// <summary>
        /// Handles the MouseClick event of the ni control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
		void ni_MouseClick(object sender, MouseEventArgs e)
        {
            // Handle mouse button clicks.
            if (e.Button == MouseButtons.Left)
            {
                controls.Show();
                SetForegroundWindow(controls.Handle);
            }
        }

        [DllImport("user32")]
        public static extern int SetForegroundWindow(IntPtr hwnd);
    }
}
