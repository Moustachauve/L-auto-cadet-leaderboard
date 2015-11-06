using Awesomium.Core;
using LautoCadetAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotoCadetLeaderboard
{
	public partial class Form1 : Form
	{
		private bool isFullscreen = false;

		public Form1()
		{
			InitializeComponent();

			// Start OWIN host 
			WebApi.Start();

			webControl1.Source = new Uri(WebApi.API_URL);
		}

		private void Awesomium_Windows_Forms_WebControl_TitleChanged(object sender, Awesomium.Core.TitleChangedEventArgs e)
		{
			this.Text = e.Title;
		}

		public void EnterFullScreenMode()
		{
			WindowState = FormWindowState.Normal;
			FormBorderStyle = FormBorderStyle.None;
			WindowState = FormWindowState.Maximized;
			isFullscreen = true;
		}

		public void LeaveFullScreenMode()
		{
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			WindowState = FormWindowState.Normal;
			isFullscreen = false;
		}

		private void Form1_MouseClick(object sender, MouseEventArgs e)
		{
			MessageBox.Show("TEST CLISSE");
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			MessageBox.Show("TEST CLISSE");
			return true;
		}

		private void Awesomium_Windows_Forms_WebControl_JavascriptMessage(object sender, JavascriptMessageEventArgs e)
		{
			switch (e.Message)
			{
				case "ToggleFullscreen":
					if (isFullscreen)
						LeaveFullScreenMode();
					else
						EnterFullScreenMode();

					e.Result = null;
					break;
			}
		}
	}
}
