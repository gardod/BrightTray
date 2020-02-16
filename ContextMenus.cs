using System;
using System.Windows.Forms;
using BrightTray.Properties;
using System.Drawing;

namespace BrightTray
{
	class ContextMenus
	{
		/// <summary>
		/// Creates this instance.
		/// </summary>
		/// <returns>ContextMenu</returns>
		public ContextMenu Create()
		{
			// Add the default menu options.
			ContextMenu menu = new ContextMenu();
			MenuItem item;

			// Separator.
            //item = new MenuItem();
            //item.Text = "-";
            //menu.MenuItems.Add(item);

			// Exit.
			item = new MenuItem();
			item.Text = "Exit";
			item.Click += new System.EventHandler(Exit_Click);
			menu.MenuItems.Add(item);

			return menu;
		}

		/// <summary>
		/// Processes exit.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void Exit_Click(object sender, EventArgs e)
		{
			// Quit without further ado.
			Application.Exit();
		}
	}
}