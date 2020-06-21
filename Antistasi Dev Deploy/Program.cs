/* 
 *  ======= Antistasi Dev Deploy =======
 * 
 * Author:			Caleb S. Serafin
 * Version:			3.1
 * Date Created:	13-10-2019
 * Date Modified:	20-06-2020
 * Memory Usage:	20 MB
 * Dynamically deploys any map template into mpmissions for testing,
 * 
 */
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using static Antistasi_Dev_Deploy_Shared.GetFolderLib;
using static Antistasi_Dev_Deploy.MapHandling;
using static Antistasi_Dev_Deploy_Shared.ProgramValues;
using static Antistasi_Dev_Deploy_Shared.Registary;
using static Antistasi_Dev_Deploy.ExternalExe;
using static Antistasi_Dev_Deploy.WindowPowerLib;

namespace Antistasi_Dev_Deploy {
	class Program {
		//Needs to be Nullable as Registry calls may return null if key does not exist.
		private static bool BoolBin(int? Input) => Input.HasValue && Input > 0;
		private static string MissionVersion { get; } = Mission.GetVersion();

		private static bool PBOFiles = false;
		private static List<string> FilterList = new List<string>();
		private static bool FilterInvoked = false;
		private static string FilterArgs = string.Empty;

		static void Main(string[] args) {
#if !DEBUG
			WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_HIDE);
#endif
			foreach (string arg in args) {
				switch (arg.Substring(0, 2).ToLower()) {
					case "/v":
						ShowMessage("Version: " + RunTimeValue.AppVersion);
						return;
					case "/h":
						ShowMessage(
							Environment.NewLine,
							"/v                 Prints assembly version.",
							"/h                 Prints help list.",
							"/p                 PBO files. Requires A3Tools:FileBank.",
							"/f                 Filter templates from ADD-Configurator.",
							"/f=\"Name,Name...\"  Pack these templates. Overrides Config.",
							"NOTE: the Configurator can override some of these settings.",
							"See https://github.com/CalebSerafin/Arma-3-Dev-Deploy for details."
						);
						return;
					case "/p":
						PBOFiles = true;
						break;
					case "/f":
						FilterInvoked = true;
						FilterArgs = arg;
						break;
				}
			}
			if (PBOFiles || BoolBin((int)FetchA3DD(Reg.ADD_PBOForce, 0))) {
				if (!HasFileBank) {
					ShowMessage("Arma 3 Tools: FileBank not installed on system.", "FileBank's Path was not found in system registry.");
					return;
				};
				PBOFiles = true;
			}
			if (BoolBin((int)FetchA3DD(Reg.ADD_ForceFilter, 0))) {
				FilterInvoked = true;
			}
			if (FilterInvoked) {
				try {
					string FilterListString;
					if (FilterArgs.Length > 3) {
						FilterListString = FilterArgs.ToLower();
					} else {
						FilterListString = FetchA3DD(Reg.ADD_FilterList, string.Empty);
					}
					FilterList = FilterListString.Split(',').ToList();
					if (FilterList.Count() == 1 && string.IsNullOrEmpty(FilterList[0])) {
						FilterInvoked = false;
						ShowMessage("ADD-Config Filter is empty, please remove /f command.");
						return;
					}
				} catch (Exception) {
					ShowMessage("Error processing /f arguments.");
					throw;
				};
			}
			string PlayerName = FetchArma(Reg.Arma_PlayerName_Name, @"empty");
			bool OverrideSource = BoolBin((int)FetchA3DD(Reg.ADD_OverrideSource, 0));
			bool OverrideOutput = BoolBin((int)FetchA3DD(Reg.ADD_OverrideOutput, 0));
			bool OpenOutput = BoolBin((int)FetchA3DD(Reg.ADD_ForceOpenOutput, 0));
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.ADD_LastPath, Assembly.GetEntryAssembly().Location, RegistryValueKind.String);

			string OverrideSourceFolder = (string)FetchA3DD(Reg.ADD_OverrideSourceFolder, "C:\\");
			if (!OverrideSourceFolder.EndsWith("\\")) OverrideSourceFolder += "\\";

			string OverrideOutputFolder = (string)FetchA3DD(Reg.ADD_OverrideOutputFolder, "C:\\");
			if (!OverrideOutputFolder.EndsWith("\\")) OverrideOutputFolder += "\\";

			string SourceDirectory = OverrideSource ? OverrideSourceFolder : RunTimeValue.AppFolder;
			SourceDirectory = FolderOps.FindRepository(SourceDirectory);
			if (string.IsNullOrEmpty(SourceDirectory)) {
				ShowMessage(
					"ERROR: Repository not found.",
					@"Ensure that '\A3-Antistasi' and '\Map-Templates' are present."
					);
				return;
			}
			string Dir_AntistasiCode = SourceDirectory + @"\A3-Antistasi";
			string Dir_AntistasiTemplates = SourceDirectory + @"\Map-Templates";
			/*if there is an issue fetching Arma 3 profile name or if developing on a computer that 
			does not have Arma 3 Installed this allows it to still be able to package missions. 
			The name matches the out folder of a python tool in the Official Repository that does this as well.*/
			string OutputFolder = string.IsNullOrEmpty(PlayerName) ? SourceDirectory + @"\PackagedMissions" : Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Arma 3 - Other Profiles\" + PlayerName + @"\mpmissions\");
			if (OverrideOutput) OutputFolder = OverrideOutputFolder;

			List<MapTemplate> AntistasiMapTemplates = new List<MapTemplate>();
			string[] Templates_Directories = Directory.GetDirectories(Dir_AntistasiTemplates);
			foreach (string Item in Templates_Directories) {
				if (FilterInvoked && !FilterList.Any(MapT => MapT.Equals(Item, StringComparison.OrdinalIgnoreCase))) continue;
				if (!File.Exists(Item + @"\mission.sqm")) continue; // If no mission.sqm it is probably not a map template.
				string[] TemplateData = GetFolder(Item).Split('.');
				if (TemplateData.Length < 2) continue; // If split return at least two strings, it is probably a map template.
				AntistasiMapTemplates.Add(new MapTemplate(TemplateData));
			}
			Directory.CreateDirectory(OutputFolder);
			System.Threading.Tasks.Parallel.ForEach(AntistasiMapTemplates, (Template) => {
				string Destination = OutputFolder + Template.Name + MissionVersion + "." + Template.Map;
				string TemplateFolder = Dir_AntistasiTemplates + @"\" + Template.Dir;
#if DEBUG
				Console.WriteLine("Copying " + Template.Dir + " Base&Template assets...");
#endif
				FolderOps.PackTemplate(Dir_AntistasiCode, TemplateFolder, Destination, PBOFiles);
			});
#if DEBUG
			ShowMessage("Press any key to open " + GetFolder(OutputFolder) + ".");
			Process.Start(OutputFolder + "\\");
#else
			if (OpenOutput) {
				Process.Start(OutputFolder + "\\");
			}
#endif
		}
	}
}