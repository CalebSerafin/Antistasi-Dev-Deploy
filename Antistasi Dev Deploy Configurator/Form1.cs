using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows.Forms;
using static Antistasi_Dev_Deploy_Configurator.CompileTimeValues;
using static Antistasi_Dev_Deploy_Configurator.Registary;

namespace Antistasi_Dev_Deploy_Configurator
{
	public partial class Menu : Form
	{
		static bool BoolBin(int? Input)
		{
			if (!Input.HasValue) return false;
			return Input > 0;
		}
		static int BoolBin(bool? Input)
		{
			if (!Input.HasValue) return 0;
			return (bool)Input ? 1 : 0;
		}
		public Menu()
		{
			InitializeComponent();

			chk_OverrideOutput.Checked = BoolBin((int)FetchA3DD(Reg.Value_ADD_OverrideOutput_Name, (int)0));
			chk_ForceOpenOutput.Checked = BoolBin((int)FetchA3DD(Reg.Value_ADD_ForceOpenOutput_Name, (int)0));
			txt_OverrideOutput.Text = FetchA3DD(Reg.Value_ADD_OverrideOutputFolder_Name, "C:\\");

			txt_OverrideOutput.ReadOnly = !chk_OverrideOutput.Checked;
			btn_OverrideOutput_SelectPath.Enabled = chk_OverrideOutput.Checked;

			this.helpProvider1.SetShowHelp(this, true);
			this.helpProvider1.SetHelpString(this, "Antistasi Dev Deploy Configurator Version: " + CompileTimeValue.AppVersion);
		}

		private void Btn_OverrideOutput_SelectPath_Click(object sender, EventArgs e)
		{
			CommonOpenFileDialog SelectOutputDialog = new CommonOpenFileDialog
			{
				InitialDirectory = txt_OverrideOutput.Text,
				IsFolderPicker = true
			};
			if (SelectOutputDialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				txt_OverrideOutput.Text = SelectOutputDialog.FileName + "\\";
			}
			SelectOutputDialog.Dispose();
		}

		private void Chk_OverrideOutput_CheckStateChanged(object sender, EventArgs e)
		{
			txt_OverrideOutput.ReadOnly = !chk_OverrideOutput.Checked;
			btn_OverrideOutput_SelectPath.Enabled = chk_OverrideOutput.Checked;
		}

		private void Btn_Apply_Click(object sender, EventArgs e)
		{
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_OverrideOutput_Name, BoolBin(chk_OverrideOutput.Checked), RegistryValueKind.DWord);
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_OverrideOutputFolder_Name, txt_OverrideOutput.Text, RegistryValueKind.String);
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_ForceOpenOutput_Name, BoolBin(chk_ForceOpenOutput.Checked), RegistryValueKind.DWord);
			MessageBox.Show("Configuration Applied!");
		}

		private void Btn_EraseRegistry_Click(object sender, EventArgs e)
		{
			DialogResult ResetConfirmation = MessageBox.Show("Are you sure you want to erase the configurations?",
					  "Erase Configurations", MessageBoxButtons.YesNo);
			if (ResetConfirmation == DialogResult.Yes)
			{
				EraseAll();
				MessageBox.Show("Configurations Erased!");
			}
		}
	}
}
