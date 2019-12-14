using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using static Antistasi_Dev_Deploy_Configurator.CompileTimeValues;

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
			try
			{
				chk_OverrideOutput.Checked = BoolBin((int)Registry.GetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_OverrideOutput_Name, 0));
			}
			catch (Exception e)
			{
				switch (e.GetType().Name)
				{
					case "NullReferenceException": chk_OverrideOutput.Checked = false; break;
					default: throw e;
				}
			}
			try
			{
				chk_ForceOpenOutput.Checked = BoolBin((int)Registry.GetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_ForceOpenOutput_Name, 0));
			}
			catch (Exception e)
			{
				switch (e.GetType().Name)
				{
					case "NullReferenceException": chk_ForceOpenOutput.Checked = false; break;
					default: throw e;
				}
			}
			txt_OverrideOutput.Text = (string) Registry.GetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_OverrideOutputFolder_Name, "C:\\");
			if (txt_OverrideOutput.Text == string.Empty) txt_OverrideOutput.Text = "C:\\";

			txt_OverrideOutput.ReadOnly = !chk_OverrideOutput.Checked;
			btn_OverrideOutput_SelectPath.Enabled = chk_OverrideOutput.Checked;


			this.helpProvider1.SetShowHelp(this, true);
			this.helpProvider1.SetHelpString(this, "Antistasi Dev Deploy Configurator Version: " + CompileTimeValue.MTVersionS);
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
				try
				{
					Registry.CurrentUser.DeleteSubKeyTree(Reg.RemoveCU(Reg.Key_A3DD_ADD));
				}
				catch (Exception exception)
				{
					switch (exception.GetType().Name)
					{
						case "ArgumentException": chk_OverrideOutput.Checked = false; break;
						default: throw exception;
					}
				}
				try
				{
					Registry.CurrentUser.DeleteSubKey(Reg.RemoveCU(Reg.Key_A3DD)); ;
				}
				catch (Exception exception)
				{
					switch (exception.GetType().Name)
					{
						case "ArgumentException": chk_OverrideOutput.Checked = false; break;
						default: throw exception;
					}
				}
				try
				{
					Registry.CurrentUser.DeleteSubKey(Reg.RemoveCU(Reg.Key_CalebSerafin));
				}
				catch (Exception exception)
				{
					switch (exception.GetType().Name)
					{
						case "ArgumentException": chk_OverrideOutput.Checked = false; break;
						default: throw exception;
					}
				}
				MessageBox.Show("Configurations Erased!");
			}
		}
	}
}
