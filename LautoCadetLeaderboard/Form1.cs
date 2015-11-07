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
using Newtonsoft.Json;
using LotoCadetLeaderboard.JSCommands;

namespace LotoCadetLeaderboard
{
	public partial class Form1 : Form
	{
		private bool isFullscreen = false;
		private FormWindowState previousState;

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

		private void EnterFullScreenMode()
		{
			previousState = WindowState;
			WindowState = FormWindowState.Normal;
			FormBorderStyle = FormBorderStyle.None;
			WindowState = FormWindowState.Maximized;
			isFullscreen = true;
		}

		private void LeaveFullScreenMode()
		{
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			WindowState = previousState;
			isFullscreen = false;
		}

		private void ShowSource(string source)
		{
			new SourceCodeViewer(source).ShowDialog();
		}

		private void OnJavascriptMessage(object sender, JavascriptMessageEventArgs e)
		{
			Command command = JsonConvert.DeserializeObject<Command>(e.Message);
			switch (command.Type)
			{
				case "ShowSourceCode":
					ShowSource((string)command.Content);
					break;
			}
		}

		private void Awesomium_Windows_Forms_WebControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F11:
					if (isFullscreen)
						LeaveFullScreenMode();
					else
						EnterFullScreenMode();
					break;
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			WebApi.Stop();
		}
	}
}
