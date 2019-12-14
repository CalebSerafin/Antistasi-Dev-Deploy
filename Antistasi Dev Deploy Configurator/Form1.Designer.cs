namespace Antistasi_Dev_Deploy_Configurator
{
	partial class Menu
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
			this.chk_OverrideOutput = new System.Windows.Forms.CheckBox();
			this.txt_OverrideOutput = new System.Windows.Forms.TextBox();
			this.btn_OverrideOutput_SelectPath = new System.Windows.Forms.Button();
			this.btn_Apply = new System.Windows.Forms.Button();
			this.btn_EraseRegistry = new System.Windows.Forms.Button();
			this.chk_ForceOpenOutput = new System.Windows.Forms.CheckBox();
			this.lbl_JustConfig1 = new System.Windows.Forms.Label();
			this.lbl_JustConfig2 = new System.Windows.Forms.Label();
			this.helpProvider1 = new System.Windows.Forms.HelpProvider();
			this.SuspendLayout();
			// 
			// chk_OverrideOutput
			// 
			resources.ApplyResources(this.chk_OverrideOutput, "chk_OverrideOutput");
			this.helpProvider1.SetHelpString(this.chk_OverrideOutput, resources.GetString("chk_OverrideOutput.HelpString"));
			this.chk_OverrideOutput.Name = "chk_OverrideOutput";
			this.helpProvider1.SetShowHelp(this.chk_OverrideOutput, ((bool)(resources.GetObject("chk_OverrideOutput.ShowHelp"))));
			this.chk_OverrideOutput.UseVisualStyleBackColor = true;
			this.chk_OverrideOutput.CheckStateChanged += new System.EventHandler(this.Chk_OverrideOutput_CheckStateChanged);
			// 
			// txt_OverrideOutput
			// 
			this.helpProvider1.SetHelpString(this.txt_OverrideOutput, resources.GetString("txt_OverrideOutput.HelpString"));
			resources.ApplyResources(this.txt_OverrideOutput, "txt_OverrideOutput");
			this.txt_OverrideOutput.Name = "txt_OverrideOutput";
			this.txt_OverrideOutput.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txt_OverrideOutput, ((bool)(resources.GetObject("txt_OverrideOutput.ShowHelp"))));
			// 
			// btn_OverrideOutput_SelectPath
			// 
			resources.ApplyResources(this.btn_OverrideOutput_SelectPath, "btn_OverrideOutput_SelectPath");
			this.helpProvider1.SetHelpString(this.btn_OverrideOutput_SelectPath, resources.GetString("btn_OverrideOutput_SelectPath.HelpString"));
			this.btn_OverrideOutput_SelectPath.Name = "btn_OverrideOutput_SelectPath";
			this.helpProvider1.SetShowHelp(this.btn_OverrideOutput_SelectPath, ((bool)(resources.GetObject("btn_OverrideOutput_SelectPath.ShowHelp"))));
			this.btn_OverrideOutput_SelectPath.UseVisualStyleBackColor = true;
			this.btn_OverrideOutput_SelectPath.Click += new System.EventHandler(this.Btn_OverrideOutput_SelectPath_Click);
			// 
			// btn_Apply
			// 
			this.helpProvider1.SetHelpString(this.btn_Apply, resources.GetString("btn_Apply.HelpString"));
			resources.ApplyResources(this.btn_Apply, "btn_Apply");
			this.btn_Apply.Name = "btn_Apply";
			this.helpProvider1.SetShowHelp(this.btn_Apply, ((bool)(resources.GetObject("btn_Apply.ShowHelp"))));
			this.btn_Apply.UseVisualStyleBackColor = true;
			this.btn_Apply.Click += new System.EventHandler(this.Btn_Apply_Click);
			// 
			// btn_EraseRegistry
			// 
			this.btn_EraseRegistry.BackColor = System.Drawing.SystemColors.Control;
			this.btn_EraseRegistry.ForeColor = System.Drawing.Color.Red;
			this.helpProvider1.SetHelpString(this.btn_EraseRegistry, resources.GetString("btn_EraseRegistry.HelpString"));
			resources.ApplyResources(this.btn_EraseRegistry, "btn_EraseRegistry");
			this.btn_EraseRegistry.Name = "btn_EraseRegistry";
			this.helpProvider1.SetShowHelp(this.btn_EraseRegistry, ((bool)(resources.GetObject("btn_EraseRegistry.ShowHelp"))));
			this.btn_EraseRegistry.UseVisualStyleBackColor = true;
			this.btn_EraseRegistry.Click += new System.EventHandler(this.Btn_EraseRegistry_Click);
			// 
			// chk_ForceOpenOutput
			// 
			resources.ApplyResources(this.chk_ForceOpenOutput, "chk_ForceOpenOutput");
			this.helpProvider1.SetHelpString(this.chk_ForceOpenOutput, resources.GetString("chk_ForceOpenOutput.HelpString"));
			this.chk_ForceOpenOutput.Name = "chk_ForceOpenOutput";
			this.helpProvider1.SetShowHelp(this.chk_ForceOpenOutput, ((bool)(resources.GetObject("chk_ForceOpenOutput.ShowHelp"))));
			this.chk_ForceOpenOutput.UseVisualStyleBackColor = true;
			// 
			// lbl_JustConfig1
			// 
			resources.ApplyResources(this.lbl_JustConfig1, "lbl_JustConfig1");
			this.lbl_JustConfig1.BackColor = System.Drawing.Color.Transparent;
			this.lbl_JustConfig1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.lbl_JustConfig1.Name = "lbl_JustConfig1";
			// 
			// lbl_JustConfig2
			// 
			resources.ApplyResources(this.lbl_JustConfig2, "lbl_JustConfig2");
			this.lbl_JustConfig2.BackColor = System.Drawing.Color.Transparent;
			this.lbl_JustConfig2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.lbl_JustConfig2.Name = "lbl_JustConfig2";
			// 
			// Menu
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lbl_JustConfig2);
			this.Controls.Add(this.lbl_JustConfig1);
			this.Controls.Add(this.chk_ForceOpenOutput);
			this.Controls.Add(this.btn_EraseRegistry);
			this.Controls.Add(this.btn_Apply);
			this.Controls.Add(this.btn_OverrideOutput_SelectPath);
			this.Controls.Add(this.txt_OverrideOutput);
			this.Controls.Add(this.chk_OverrideOutput);
			this.HelpButton = true;
			this.helpProvider1.SetHelpString(this, resources.GetString("$this.HelpString"));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Menu";
			this.helpProvider1.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chk_OverrideOutput;
		private System.Windows.Forms.TextBox txt_OverrideOutput;
		private System.Windows.Forms.Button btn_OverrideOutput_SelectPath;
		private System.Windows.Forms.Button btn_Apply;
		private System.Windows.Forms.Button btn_EraseRegistry;
		private System.Windows.Forms.CheckBox chk_ForceOpenOutput;
		private System.Windows.Forms.Label lbl_JustConfig1;
		private System.Windows.Forms.Label lbl_JustConfig2;
		private System.Windows.Forms.HelpProvider helpProvider1;
	}
}

