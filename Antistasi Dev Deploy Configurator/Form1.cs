using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows.Forms;
using static Antistasi_Dev_Deploy_Shared.ProgramValues;
using static Antistasi_Dev_Deploy_Shared.Registary;
using static Antistasi_Dev_Deploy_Shared.GetFolderLib;
using static Antistasi_Dev_Deploy_Configurator.Registary;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace Antistasi_Dev_Deploy_Configurator {
	public partial class Menu : Form {
		static bool BoolBin(int? Input) {
			if (!Input.HasValue) return false;
			return Input > 0;
		}
		static int BoolBin(bool? Input) {
			if (!Input.HasValue) return 0;
			return (bool)Input ? 1 : 0;
		}
		private static string LastPath = FetchA3DD(Reg.ADD_LastPath, "");
		public Menu() {
			InitializeComponent();

			chk_OverrideSource.Checked = BoolBin((int)FetchA3DD(Reg.ADD_OverrideSource, (int)0));
			txt_OverrideSource.Text = FetchA3DD(Reg.ADD_OverrideSourceFolder, "C:\\");

			chk_OverrideOutput.Checked = BoolBin((int)FetchA3DD(Reg.ADD_OverrideOutput, (int)0));
			txt_OverrideOutput.Text = FetchA3DD(Reg.ADD_OverrideOutputFolder, "C:\\");

			chk_ForceFilter.Checked = BoolBin((int)FetchA3DD(Reg.ADD_ForceFilter, (int)0));
			txt_FilterList.Text = FetchA3DD(Reg.ADD_FilterList, "");

			chk_ForceOpenOutput.Checked = BoolBin((int)FetchA3DD(Reg.ADD_ForceOpenOutput, (int)0));
			chk_ForcePBO.Checked = BoolBin((int)FetchA3DD(Reg.ADD_PBOForce, (int)0));

			txt_OverrideOutput.ReadOnly = !chk_OverrideOutput.Checked;
			btn_OverrideOutput_SelectPath.Enabled = chk_OverrideOutput.Checked;

			this.helpProvider1.SetShowHelp(this, true);
			this.helpProvider1.SetHelpString(this, "Antistasi Dev Deploy Configurator Version: " + RunTimeValue.AppVersion);
		}

		private void btn_OverrideSource_SelectPath_Click(object sender, EventArgs e) {
			CommonOpenFileDialog SelecteSourceDialog = new CommonOpenFileDialog {
				InitialDirectory = txt_OverrideSource.Text,
				IsFolderPicker = true
			};
			if (SelecteSourceDialog.ShowDialog() == CommonFileDialogResult.Ok) {
				txt_OverrideSource.Text = SelecteSourceDialog.FileName + "\\";
			}
			SelecteSourceDialog.Dispose();
		}

		private void Btn_OverrideOutput_SelectPath_Click(object sender, EventArgs e) {
			CommonOpenFileDialog SelectOutputDialog = new CommonOpenFileDialog {
				InitialDirectory = txt_OverrideOutput.Text,
				IsFolderPicker = true
			};
			if (SelectOutputDialog.ShowDialog() == CommonFileDialogResult.Ok) {
				txt_OverrideOutput.Text = SelectOutputDialog.FileName + "\\";
			}
			SelectOutputDialog.Dispose();
		}

		private void btn_PBOList_SelectPath_Click(object sender, EventArgs e) {
			CommonOpenFileDialog SelectPBOListDialog = new CommonOpenFileDialog {
				InitialDirectory = Path.GetDirectoryName(LastPath),
				IsFolderPicker = true,
				Multiselect = true
			};
			if (SelectPBOListDialog.ShowDialog() == CommonFileDialogResult.Ok) {
				List<string> FileNames = SelectPBOListDialog.FileNames.ToList();
				for (int i = 0; i < FileNames.Count; i++) {
					FileNames[i] = GetFolder(FileNames[i]);
				};
				txt_FilterList.Text = string.Join(",",FileNames);
			}
			SelectPBOListDialog.Dispose();
		}

		private void btn_PBOList_Help_Click(object sender, EventArgs e) {
			MessageBox.Show("Input Map-Templates (comma separated) to filter output files. Alternatively you can use the file picker to select Map-Template folder(s) (Not connected to source path)");
		}

		private void chk_OverrideSource_CheckedChanged(object sender, EventArgs e) {
			txt_OverrideSource.ReadOnly = !chk_OverrideSource.Checked;
			btn_OverrideSource_SelectPath.Enabled = chk_OverrideSource.Checked;
		}

		private void Chk_OverrideOutput_CheckStateChanged(object sender, EventArgs e) {
			txt_OverrideOutput.ReadOnly = !chk_OverrideOutput.Checked;
			btn_OverrideOutput_SelectPath.Enabled = chk_OverrideOutput.Checked;
		}

		private void SaveSettings() {
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.ADD_OverrideSource, BoolBin(chk_OverrideSource.Checked), RegistryValueKind.DWord);
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.ADD_OverrideSourceFolder, txt_OverrideSource.Text, RegistryValueKind.String);

			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.ADD_OverrideOutput, BoolBin(chk_OverrideOutput.Checked), RegistryValueKind.DWord);
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.ADD_OverrideOutputFolder, txt_OverrideOutput.Text, RegistryValueKind.String);

			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.ADD_ForceFilter, BoolBin(chk_ForceFilter.Checked), RegistryValueKind.DWord);
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.ADD_FilterList, txt_FilterList.Text, RegistryValueKind.String);

			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.ADD_ForceOpenOutput, BoolBin(chk_ForceOpenOutput.Checked), RegistryValueKind.DWord);
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.ADD_PBOForce, BoolBin(chk_ForcePBO.Checked), RegistryValueKind.DWord);
		}

		private void Btn_Apply_Click(object sender, EventArgs e) {
			SaveSettings();
			MessageBox.Show("Configuration Applied!");
		}

		private void Btn_EraseRegistry_Click(object sender, EventArgs e) {
			DialogResult ResetConfirmation = MessageBox.Show("Are you sure you want to erase the configurations?",
					  "Erase Configurations", MessageBoxButtons.YesNo);
			if (ResetConfirmation == DialogResult.Yes) {
				EraseAll();
				MessageBox.Show("Configurations Erased!");
			}
		}

		private void btn_RunLastPath_Click(object sender, EventArgs e) {
			SaveSettings();
			if (File.Exists(LastPath)) {
				Process ADD = new Process();
				ADD.StartInfo.FileName = LastPath;
				ADD.Start();
				ADD.WaitForExit();
			} else {
				MessageBox.Show("You need to run Antistasi Dev Deploy at-least once for it to save its path.");
			};
		}
	}
}
