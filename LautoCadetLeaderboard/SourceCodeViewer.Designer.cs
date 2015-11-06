namespace LotoCadetLeaderboard
{
	partial class SourceCodeViewer
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
			this.txtSource = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// txtSource
			// 
			this.txtSource.BackColor = System.Drawing.SystemColors.Control;
			this.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSource.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSource.HideSelection = false;
			this.txtSource.Location = new System.Drawing.Point(8, 8);
			this.txtSource.Name = "txtSource";
			this.txtSource.ReadOnly = true;
			this.txtSource.Size = new System.Drawing.Size(1328, 612);
			this.txtSource.TabIndex = 0;
			this.txtSource.Text = "";
			this.txtSource.WordWrap = false;
			// 
			// SourceCodeViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1344, 628);
			this.Controls.Add(this.txtSource);
			this.Name = "SourceCodeViewer";
			this.Padding = new System.Windows.Forms.Padding(8);
			this.Text = "SourceCodeViewer";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox txtSource;
	}
}