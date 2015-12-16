using CefSharp;
using CefSharp.WinForms;
using LautoCadetAPI;
using System.Windows.Forms;

namespace LotoCadetLeaderboard
{
	public partial class Form1 : Form
	{
		private bool isFullscreen = false;
		private FormWindowState previousState;
		private ChromiumWebBrowser browser;

		public Form1()
		{
			InitializeComponent();

			// Start OWIN host 
			WebApi.Start();
			browser = new ChromiumWebBrowser(WebApi.API_URL);
			browser.Dock = DockStyle.Fill;
			browser.TitleChanged += browser_TitleChanged;
			browser.KeyDown += browser_KeyDown;
			browser.RegisterJsObject("clientUtils", new JsClientUtils(this));
			Controls.Add(this.browser);
		}

		public void EnterFullScreenMode()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => { EnterFullScreenMode(); }));
			}
			else
			{
				previousState = WindowState;
				WindowState = FormWindowState.Normal;
				FormBorderStyle = FormBorderStyle.None;
				WindowState = FormWindowState.Maximized;
				isFullscreen = true;
			}
		}

		public void LeaveFullScreenMode()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => { LeaveFullScreenMode(); }));
			}
			else
			{
				FormBorderStyle = FormBorderStyle.Sizable;
				WindowState = previousState;
				isFullscreen = false;
			}
		}

		public void ShowDevTools()
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => { ShowDevTools(); }));
			}
			else
			{
				browser.ShowDevTools();
			}
		}

		private void ShowSource(string source)
		{
			new SourceCodeViewer(source).ShowDialog();
		}

		private void browser_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F11:
					if (isFullscreen)
						LeaveFullScreenMode();
					else
						EnterFullScreenMode();
					break;
				case Keys.F12:
					browser.ShowDevTools();
					break;
			}
		}

		private void browser_TitleChanged(object sender, TitleChangedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => { browser_TitleChanged(sender, e); }));
			}
			else
			{
				this.Text = e.Title;
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			WebApi.Stop();
		}
	}
}
