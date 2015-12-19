using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotoCadetLeaderboard
{
	public class JsClientUtils
	{
		Form1 form;

		public JsClientUtils(Form1 form)
		{
			this.form = form;
		}

		public void enterFullScreen()
		{
			form.EnterFullScreenMode();
		}

		public void leaveFullScreen()
		{
			form.LeaveFullScreenMode();
		}

		public void showDevTools()
		{
			form.ShowDevTools();
		}

		public string selectNewFile()
		{
			return form.SelectNewFile();
		}

		public string openFile()
		{
			return form.OpenFile();
		}
	}
}
