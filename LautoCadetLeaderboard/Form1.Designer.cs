namespace LotoCadetLeaderboard
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.webControl1 = new Awesomium.Windows.Forms.WebControl(this.components);
			this.SuspendLayout();
			// 
			// webControl1
			// 
			this.webControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webControl1.Location = new System.Drawing.Point(0, 0);
			this.webControl1.Size = new System.Drawing.Size(780, 493);
			this.webControl1.TabIndex = 0;
			this.webControl1.JavascriptMessage += new Awesomium.Core.JavascriptMessageEventHandler(this.Awesomium_Windows_Forms_WebControl_JavascriptMessage);
			this.webControl1.TitleChanged += new Awesomium.Core.TitleChangedEventHandler(this.Awesomium_Windows_Forms_WebControl_TitleChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(780, 493);
			this.Controls.Add(this.webControl1);
			this.DoubleBuffered = true;
			this.MinimumSize = new System.Drawing.Size(545, 380);
			this.Name = "Form1";
			this.Text = "L\'auto Cadets";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

		}

		#endregion

		private Awesomium.Windows.Forms.WebControl webControl1;
	}
}

