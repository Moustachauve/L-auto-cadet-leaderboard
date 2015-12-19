using CefSharp;
using CefSharp.WinForms;
using LautoCadetAPI;
using System.IO;
using System.Windows.Forms;

namespace LotoCadetLeaderboard
{
	public partial class Form1 : Form
	{
		private delegate string StringInvoker();

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

		public string OpenFile()
		{
			if (InvokeRequired)
			{
				string path = (string)Invoke(new StringInvoker(delegate { return OpenFile(); }));
				return path;
			}
			else
			{
				OpenFileDialog openDialog = CreateOpenDialog();

				DialogResult result = openDialog.ShowDialog();

				if (result == DialogResult.OK)
					return openDialog.FileName;

				return "";
			}
		}

		public string SelectNewFile()
		{
			if (InvokeRequired)
			{
				string path = (string)Invoke(new StringInvoker(delegate { return SelectNewFile(); }));
				return path;
			}
			else
			{
				SaveFileDialog saveDialog = CreateSaveDialog();

				DialogResult result = saveDialog.ShowDialog();

				if (result == DialogResult.OK)
					return saveDialog.FileName;

				return "";
			}
		}

		private SaveFileDialog CreateSaveDialog()
		{
			SaveFileDialog saveDialog = new SaveFileDialog();

			saveDialog.InitialDirectory = Path.GetDirectoryName(Path.GetFullPath(WebApi.DEFAULT_FILE_PATH));
			saveDialog.RestoreDirectory = false;
			saveDialog.SupportMultiDottedExtensions = true;
			saveDialog.Filter = "Classement L'auto-cadet | *.cadet";
			saveDialog.DefaultExt = "cadet";

			saveDialog.OverwritePrompt = true;

			return saveDialog;
		}

		private OpenFileDialog CreateOpenDialog()
		{
			OpenFileDialog openDialog = new OpenFileDialog();

			openDialog.InitialDirectory = Path.GetDirectoryName(Path.GetFullPath(WebApi.DEFAULT_FILE_PATH));
			openDialog.RestoreDirectory = false;
			openDialog.SupportMultiDottedExtensions = true;
			openDialog.Filter = "Classement L'auto-cadet | *.cadet";
			openDialog.DefaultExt = "cadet";

			openDialog.CheckFileExists = true;
			openDialog.CheckPathExists = true;

			return openDialog;
		}
	}
}
