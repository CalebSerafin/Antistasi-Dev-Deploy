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
			this.lbl_PBOList = new System.Windows.Forms.Label();
			this.txt_PBOList = new System.Windows.Forms.TextBox();
			this.btn_PBOList_SelectPath = new System.Windows.Forms.Button();
			this.btn_PBOList_Help = new System.Windows.Forms.Button();
			this.chk_PBOList_Override = new System.Windows.Forms.CheckBox();
			this.btn_RunLastPath = new System.Windows.Forms.Button();
			this.chk_OverrideSource = new System.Windows.Forms.CheckBox();
			this.txt_OverrideSource = new System.Windows.Forms.TextBox();
			this.btn_OverrideSource_SelectPath = new System.Windows.Forms.Button();
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
			// lbl_PBOList
			// 
			resources.ApplyResources(this.lbl_PBOList, "lbl_PBOList");
			this.lbl_PBOList.Name = "lbl_PBOList";
			// 
			// txt_PBOList
			// 
			this.txt_PBOList.BackColor = System.Drawing.SystemColors.Window;
			this.helpProvider1.SetHelpString(this.txt_PBOList, resources.GetString("txt_PBOList.HelpString"));
			resources.ApplyResources(this.txt_PBOList, "txt_PBOList");
			this.txt_PBOList.Name = "txt_PBOList";
			this.helpProvider1.SetShowHelp(this.txt_PBOList, ((bool)(resources.GetObject("txt_PBOList.ShowHelp"))));
			// 
			// btn_PBOList_SelectPath
			// 
			this.helpProvider1.SetHelpString(this.btn_PBOList_SelectPath, resources.GetString("btn_PBOList_SelectPath.HelpString"));
			resources.ApplyResources(this.btn_PBOList_SelectPath, "btn_PBOList_SelectPath");
			this.btn_PBOList_SelectPath.Name = "btn_PBOList_SelectPath";
			this.helpProvider1.SetShowHelp(this.btn_PBOList_SelectPath, ((bool)(resources.GetObject("btn_PBOList_SelectPath.ShowHelp"))));
			this.btn_PBOList_SelectPath.UseVisualStyleBackColor = true;
			this.btn_PBOList_SelectPath.Click += new System.EventHandler(this.btn_PBOList_SelectPath_Click);
			// 
			// btn_PBOList_Help
			// 
			this.helpProvider1.SetHelpString(this.btn_PBOList_Help, resources.GetString("btn_PBOList_Help.HelpString"));
			resources.ApplyResources(this.btn_PBOList_Help, "btn_PBOList_Help");
			this.btn_PBOList_Help.Name = "btn_PBOList_Help";
			this.helpProvider1.SetShowHelp(this.btn_PBOList_Help, ((bool)(resources.GetObject("btn_PBOList_Help.ShowHelp"))));
			this.btn_PBOList_Help.UseVisualStyleBackColor = true;
			this.btn_PBOList_Help.Click += new System.EventHandler(this.btn_PBOList_Help_Click);
			// 
			// chk_PBOList_Override
			// 
			resources.ApplyResources(this.chk_PBOList_Override, "chk_PBOList_Override");
			this.helpProvider1.SetHelpString(this.chk_PBOList_Override, resources.GetString("chk_PBOList_Override.HelpString"));
			this.chk_PBOList_Override.Name = "chk_PBOList_Override";
			this.helpProvider1.SetShowHelp(this.chk_PBOList_Override, ((bool)(resources.GetObject("chk_PBOList_Override.ShowHelp"))));
			this.chk_PBOList_Override.UseVisualStyleBackColor = true;
			// 
			// btn_RunLastPath
			// 
			this.helpProvider1.SetHelpString(this.btn_RunLastPath, resources.GetString("btn_RunLastPath.HelpString"));
			resources.ApplyResources(this.btn_RunLastPath, "btn_RunLastPath");
			this.btn_RunLastPath.Name = "btn_RunLastPath";
			this.helpProvider1.SetShowHelp(this.btn_RunLastPath, ((bool)(resources.GetObject("btn_RunLastPath.ShowHelp"))));
			this.btn_RunLastPath.UseVisualStyleBackColor = true;
			this.btn_RunLastPath.Click += new System.EventHandler(this.btn_RunLastPath_Click);
			// 
			// chk_OverrideSource
			// 
			resources.ApplyResources(this.chk_OverrideSource, "chk_OverrideSource");
			this.helpProvider1.SetHelpString(this.chk_OverrideSource, resources.GetString("chk_OverrideSource.HelpString"));
			this.chk_OverrideSource.Name = "chk_OverrideSource";
			this.helpProvider1.SetShowHelp(this.chk_OverrideSource, ((bool)(resources.GetObject("chk_OverrideSource.ShowHelp"))));
			this.chk_OverrideSource.UseVisualStyleBackColor = true;
			this.chk_OverrideSource.CheckedChanged += new System.EventHandler(this.chk_OverrideSource_CheckedChanged);
			// 
			// txt_OverrideSource
			// 
			this.helpProvider1.SetHelpString(this.txt_OverrideSource, resources.GetString("txt_OverrideSource.HelpString"));
			resources.ApplyResources(this.txt_OverrideSource, "txt_OverrideSource");
			this.txt_OverrideSource.Name = "txt_OverrideSource";
			this.txt_OverrideSource.ReadOnly = true;
			this.helpProvider1.SetShowHelp(this.txt_OverrideSource, ((bool)(resources.GetObject("txt_OverrideSource.ShowHelp"))));
			// 
			// btn_OverrideSource_SelectPath
			// 
			resources.ApplyResources(this.btn_OverrideSource_SelectPath, "btn_OverrideSource_SelectPath");
			this.helpProvider1.SetHelpString(this.btn_OverrideSource_SelectPath, resources.GetString("btn_OverrideSource_SelectPath.HelpString"));
			this.btn_OverrideSource_SelectPath.Name = "btn_OverrideSource_SelectPath";
			this.helpProvider1.SetShowHelp(this.btn_OverrideSource_SelectPath, ((bool)(resources.GetObject("btn_OverrideSource_SelectPath.ShowHelp"))));
			this.btn_OverrideSource_SelectPath.UseVisualStyleBackColor = true;
			this.btn_OverrideSource_SelectPath.Click += new System.EventHandler(this.btn_OverrideSource_SelectPath_Click);
			// 
			// Menu
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btn_OverrideSource_SelectPath);
			this.Controls.Add(this.txt_OverrideSource);
			this.Controls.Add(this.chk_OverrideSource);
			this.Controls.Add(this.btn_RunLastPath);
			this.Controls.Add(this.chk_PBOList_Override);
			this.Controls.Add(this.btn_PBOList_Help);
			this.Controls.Add(this.btn_PBOList_SelectPath);
			this.Controls.Add(this.txt_PBOList);
			this.Controls.Add(this.lbl_PBOList);
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
		private System.Windows.Forms.Label lbl_PBOList;
		private System.Windows.Forms.TextBox txt_PBOList;
		private System.Windows.Forms.Button btn_PBOList_SelectPath;
		private System.Windows.Forms.Button btn_PBOList_Help;
		private System.Windows.Forms.CheckBox chk_PBOList_Override;
		private System.Windows.Forms.Button btn_RunLastPath;
		private System.Windows.Forms.CheckBox chk_OverrideSource;
		private System.Windows.Forms.TextBox txt_OverrideSource;
		private System.Windows.Forms.Button btn_OverrideSource_SelectPath;
	}
}

