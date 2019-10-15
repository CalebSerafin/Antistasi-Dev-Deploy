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
			this.SuspendLayout();
			// 
			// chk_OverrideOutput
			// 
			resources.ApplyResources(this.chk_OverrideOutput, "chk_OverrideOutput");
			this.chk_OverrideOutput.Name = "chk_OverrideOutput";
			this.chk_OverrideOutput.UseVisualStyleBackColor = true;
			this.chk_OverrideOutput.CheckStateChanged += new System.EventHandler(this.Chk_OverrideOutput_CheckStateChanged);
			// 
			// txt_OverrideOutput
			// 
			resources.ApplyResources(this.txt_OverrideOutput, "txt_OverrideOutput");
			this.txt_OverrideOutput.Name = "txt_OverrideOutput";
			this.txt_OverrideOutput.ReadOnly = true;
			// 
			// btn_OverrideOutput_SelectPath
			// 
			resources.ApplyResources(this.btn_OverrideOutput_SelectPath, "btn_OverrideOutput_SelectPath");
			this.btn_OverrideOutput_SelectPath.Name = "btn_OverrideOutput_SelectPath";
			this.btn_OverrideOutput_SelectPath.UseVisualStyleBackColor = true;
			this.btn_OverrideOutput_SelectPath.Click += new System.EventHandler(this.Btn_OverrideOutput_SelectPath_Click);
			// 
			// btn_Apply
			// 
			resources.ApplyResources(this.btn_Apply, "btn_Apply");
			this.btn_Apply.Name = "btn_Apply";
			this.btn_Apply.UseVisualStyleBackColor = true;
			this.btn_Apply.Click += new System.EventHandler(this.Btn_Apply_Click);
			// 
			// btn_EraseRegistry
			// 
			this.btn_EraseRegistry.BackColor = System.Drawing.SystemColors.Control;
			this.btn_EraseRegistry.ForeColor = System.Drawing.Color.Red;
			resources.ApplyResources(this.btn_EraseRegistry, "btn_EraseRegistry");
			this.btn_EraseRegistry.Name = "btn_EraseRegistry";
			this.btn_EraseRegistry.UseVisualStyleBackColor = true;
			this.btn_EraseRegistry.Click += new System.EventHandler(this.Btn_EraseRegistry_Click);
			// 
			// chk_ForceOpenOutput
			// 
			resources.ApplyResources(this.chk_ForceOpenOutput, "chk_ForceOpenOutput");
			this.chk_ForceOpenOutput.Name = "chk_ForceOpenOutput";
			this.chk_ForceOpenOutput.UseVisualStyleBackColor = true;
			// 
			// Menu
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chk_ForceOpenOutput);
			this.Controls.Add(this.btn_EraseRegistry);
			this.Controls.Add(this.btn_Apply);
			this.Controls.Add(this.btn_OverrideOutput_SelectPath);
			this.Controls.Add(this.txt_OverrideOutput);
			this.Controls.Add(this.chk_OverrideOutput);
			this.Name = "Menu";
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
	}
}

