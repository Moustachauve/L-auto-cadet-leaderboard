using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotoCadetLeaderboard
{
	public class JsDialog : IJsDialogHandler
	{
		public void OnDialogClosed(IWebBrowser browserControl, IBrowser browser)
		{
			
		}

		public bool OnJSBeforeUnload(IWebBrowser browserControl, IBrowser browser, string message, bool isReload, IJsDialogCallback callback)
		{
			return false;
		}

		public bool OnJSDialog(IWebBrowser browserControl, IBrowser browser, string originUrl, string acceptLang, CefJsDialogType dialogType, string messageText, string defaultPromptText, IJsDialogCallback callback, ref bool suppressMessage)
		{
			switch (dialogType)
			{
				case CefJsDialogType.Alert:
					MessageBox.Show(messageText, "L'auto-cadet", MessageBoxButtons.OK);
					callback.Continue(true);
					return true;
				case CefJsDialogType.Confirm:
					var result = MessageBox.Show(messageText, "L'auto-cadet", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
					callback.Continue(result == DialogResult.Yes);
					return true;
			}

			return false;
		}

		public void OnResetDialogState(IWebBrowser browserControl, IBrowser browser)
		{

		}
	}
}
