/* 
 *  ======= Antistasi Dev Deploy =======
 * 
 * Author:			Caleb S. Serafin
 * Date Created:	13-10-2019
 * Dynamically deploys any map template into mpmissions for testing,
 * 
 */
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static Antistasi_Dev_Deploy_Shared.GetFolderLib;
using static Antistasi_Dev_Deploy_Shared.ProgramValues;
using static Antistasi_Dev_Deploy_Shared.Registary;
using static Antistasi_Dev_Deploy.WindowPowerLib;

namespace Antistasi_Dev_Deploy {
	class Program {
		private static bool PBOFiles = BoolBin((int)FetchA3DD(Reg.ADD_PBOForce, 0));
		private static List<string> FilterList = new List<string>();
		private static bool FilterInvoked = BoolBin((int)FetchA3DD(Reg.ADD_ForceFilter, 0));
		private static string FilterArgs = string.Empty;

		static void Main(string[] args) {
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.ADD_LastPath, RunTimeValue.AppPath, RegistryValueKind.String);
			foreach (string arg in args) {
				switch (arg.Substring(0, 2).ToLower()) {
					case "/v":
						ShowMessage("Version: " + RunTimeValue.AppVersion);
						return;
					case "/p":
						PBOFiles = true;
						break;
					case "/f":
						FilterInvoked = true;
						FilterArgs = arg;
						break;
					case "/w":
						WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_HIDE);
						break;
					default:
						ShowMessage(
							Environment.NewLine,
							"/h                 Prints help list.",
							"/f                 Filter templates from ADD-Configurator.",
							"/f=\"Name,Name...\"  Pack these templates. Overrides Config.",
							"/p                 PBO files. Requires A3Tools:FileBank.",
							"/v                 Prints assembly version.",
							"/w                 Hide console Window.",
							"NOTE: the Configurator can override some of these settings.",
							"See https://github.com/CalebSerafin/Arma-3-Dev-Deploy for details."
						);
						return;
				}
			}
			if (PBOFiles && !HasFileBank) {
				ShowMessage("Arma 3 Tools: FileBank not installed on system.", "FileBank's Path was not found in system registry.");
				return;
			}
			if (FilterInvoked) {
				try {
					string FilterListString = (FilterArgs.Length > 3) ? FilterArgs.Substring(3).ToLower() : FetchA3DD(Reg.ADD_FilterList, string.Empty);
					FilterList = FilterListString.Split(',').ToList();
					if (string.IsNullOrEmpty(FilterList[0])) {
						ShowMessage("ADD-Config Filter is empty, please remove /f command.");
						return;
					}
				} catch (Exception) {
					ShowMessage("Error processing /f arguments.");
					throw;
				};
			}

			string OverrideSourceFolder = (string)FetchA3DD(Reg.ADD_OverrideSourceFolder, "C:\\");
			if (!OverrideSourceFolder.EndsWith("\\")) OverrideSourceFolder += "\\";

			string OverrideOutputFolder = (string)FetchA3DD(Reg.ADD_OverrideOutputFolder, "C:\\");
			if (!OverrideOutputFolder.EndsWith("\\")) OverrideOutputFolder += "\\";

			string SourceDirectory = BoolBin((int)FetchA3DD(Reg.ADD_OverrideSource, 0)) ? OverrideSourceFolder : RunTimeValue.AppFolder;
			SourceDirectory = FolderOps.FindRepository(SourceDirectory);
			if (string.IsNullOrEmpty(SourceDirectory)) {
				ShowMessage(
					"ERROR: Repository not found.",
					@"Ensure that '\A3-Antistasi' and '\Map-Templates' are present."
					);
				return;
			}
			string AntistasiCodePath = SourceDirectory + @"\A3-Antistasi";
			string MissionVersion = Mission.GetVersion(SourceDirectory);
			/*if there is an issue fetching Arma 3 profile name or if developing on a computer that 
			does not have Arma 3 Installed this allows it to still be able to package missions. 
			The name matches the out folder of a python tool in the Official Repository that does this as well.*/
			string PlayerName = FetchArma(Reg.Arma_PlayerName_Name, string.Empty);
			string OutputFolder = string.IsNullOrEmpty(PlayerName) ? SourceDirectory + @"\PackagedMissions" : Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Arma 3 - Other Profiles\" + PlayerName + @"\mpmissions\");
			if (BoolBin((int)FetchA3DD(Reg.ADD_OverrideOutput, 0))) OutputFolder = OverrideOutputFolder;

			bool TemplatePacked = false;
			string[] Templates_Directories = Directory.GetDirectories(SourceDirectory + @"\Map-Templates");

			Directory.CreateDirectory(OutputFolder);
			System.Threading.Tasks.Parallel.ForEach(Templates_Directories, TemplatePath => {
				string TemplateFolder = GetFolder(TemplatePath);

				if (FilterInvoked && !FilterList.Any(MapT => MapT.Equals(TemplateFolder, StringComparison.OrdinalIgnoreCase))) return;
				if (!File.Exists(TemplatePath + @"\mission.sqm")) return; // If no mission.sqm it is probably not a map template.
				string[] TemplateSplit = TemplateFolder.Split('.');
				if (TemplateSplit.Length < 2) return; // If split return at least two strings, it is probably a map template.

				string Name = string.Join(".", TemplateSplit.Take(TemplateSplit.Length - 1));
				string Map = TemplateSplit.Last();
				string Destination = OutputFolder + Name + MissionVersion + "." + Map;

				FolderOps.PackTemplate(AntistasiCodePath, TemplatePath, Destination, PBOFiles);
				TemplatePacked = true;
			});
			if (!TemplatePacked) {
				ShowMessage(
					"Filter matches no templates.",
					"Filter: ",
					string.Join(", ", FilterList),
					"Map Templates: ",
					Templates_Directories.Aggregate("", (string Templates, string Item) => Templates += GetFolder(Item) + Environment.NewLine)
				);
				return;
			}
			if (BoolBin((int)FetchA3DD(Reg.ADD_ForceOpenOutput, 0))) {
				Process.Start(OutputFolder + "\\");
			}
		}
	}
}