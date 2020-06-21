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
using static Antistasi_Dev_Deploy_Shared.GetFolderLib;
using static Antistasi_Dev_Deploy.MapHandling;
using static Antistasi_Dev_Deploy.ProgramValues;
using static Antistasi_Dev_Deploy_Shared.ProgramValues;
using static Antistasi_Dev_Deploy_Shared.Registary;
using static Antistasi_Dev_Deploy.ExternalExe;
using static Antistasi_Dev_Deploy.WindowPowerLib;

namespace Antistasi_Dev_Deploy {
	class Program {
		//Needs to be Nullable as Registry calls may return null if key does not exist.
		private static bool BoolBin(int? Input) => !Input.HasValue ? false : Input > 0;

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
						ShowMessage("Version: " + CompileTimeValue.AppVersion);
						return;
					case "/h":
						ShowMessage(
							Environment.NewLine,
							"/v                 Prints assembly version.",
							"/h                 Prints help list.",
							"/p                 PBO files. Requires A3Tools:FileBank.",
							"/f                 Filter templates from ADD-Configurator.",
							"/f=\"Name,Name...\"  Pack these templates. Overrides Config.",
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
			if (PBOFiles || BoolBin((int)FetchA3DD(Reg.Value_ADD_PBOForce, 0))) {
				if (!HasFileBank) {
					ShowMessage("Arma 3 Tools: FileBank not installed on system.", "FileBank's Path was not found in system registry.");
					return;
				};
				PBOFiles = true;
			}
			if (BoolBin((int)FetchA3DD(Reg.Value_ADD_ForceFilter, 0))) {
				FilterInvoked = true;
			}
			if (FilterInvoked) {
				try {
					string FilterListString;
					if (FilterArgs.Length > 3) {
						FilterListString = FilterArgs.ToLower();
					} else {
						FilterListString = FetchA3DD(Reg.Value_ADD_FilterList, string.Empty);
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
			string PlayerName = FetchArma(Reg.Value_Arma_PlayerName_Name, @"empty");
			bool OverrideSource = BoolBin((int)FetchA3DD(Reg.Value_ADD_OverrideSource, 0));
			bool OverrideOutput = BoolBin((int)FetchA3DD(Reg.Value_ADD_OverrideOutput, 0));
			bool OpenOutput = BoolBin((int)FetchA3DD(Reg.Value_ADD_ForceOpenOutput, 0));
			Registry.SetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_LastPath, System.Reflection.Assembly.GetEntryAssembly().Location, RegistryValueKind.String);

			string OverrideSourceFolder = (string)FetchA3DD(Reg.Value_ADD_OverrideSourceFolder, "C:\\");
			if (!OverrideSourceFolder.EndsWith("\\")) OverrideSourceFolder += "\\";

			string OverrideOutputFolder = (string)FetchA3DD(Reg.Value_ADD_OverrideOutputFolder, "C:\\");
			if (!OverrideOutputFolder.EndsWith("\\")) OverrideOutputFolder += "\\";

			string CurrentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			string SourceDirectory = CurrentDirectory;
			if (OverrideSource) {
				CurrentDirectory = OverrideSourceFolder;
			}
			//The following handles whether the executable is placed inside a sub folder in the root git directory.
			if (!Directory.Exists(CurrentDirectory + @"\A3-Antistasi")) {
				SourceDirectory += @"\..";
			}
			string Dir_AntistasiRoot = CurrentDirectory + @"\A3-Antistasi";
			string Dir_AntistasiTemplates = CurrentDirectory + @"\Map-Templates";
			if (!Directory.Exists(Dir_AntistasiRoot)) { ShowMessage(@"ERROR: '\A3-Antistasi' not found."); return; };
			if (!Directory.Exists(Dir_AntistasiRoot)) { ShowMessage(@"ERROR: '\Map-Templates' not found."); return; };

			string Dir_mpMissions = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Arma 3 - Other Profiles\" + PlayerName + @"\mpmissions\");
			/*if there is an issue fetching Arma 3 profile name or if developing on a computer that 
			does not have Arma 3 Installed this allows it to still be able to package missions. 
			The name matches the out folder of a python tool in the Official Repository that does this as well.*/
			if (PlayerName == string.Empty || !Directory.Exists(Dir_mpMissions)) {
				Dir_mpMissions = CurrentDirectory + @"\PackagedMissions\";
			}
			if (OverrideOutput) Dir_mpMissions = OverrideOutputFolder;
			List<MapTemplate> AntistasiMapTemplates = new List<MapTemplate>();
			if (Directory.Exists(Dir_AntistasiTemplates)) {
				string[] Templates_Directories = Directory.GetDirectories(Dir_AntistasiTemplates);
				foreach (string item in Templates_Directories) {
					if (!File.Exists(item + @"\mission.sqm")) continue; // If no mission.sqm it is probably not a map template.
					string LastFolder = GetFolder(item);
					string[] TemplateData = LastFolder.Split('.');
					if (TemplateData.Length < 2) continue; // If split return at least two strings, it is probably a map template.
					AntistasiMapTemplates.Add(new MapTemplate(TemplateData));
				}
			}

			if (FilterInvoked) {
				AntistasiMapTemplates = AntistasiMapTemplates.Where(Item =>
					FilterList.Any(MapT => MapT.Equals(Item.Dir, StringComparison.OrdinalIgnoreCase))).ToList();
			};
#if DEBUG
			List<string> TemplateNamesDebug = new List<string>();
			foreach (MapTemplate item in AntistasiMapTemplates) {
				TemplateNamesDebug.Add(item.Name + " on map " + item.Map);
			}
			Console.WriteLine(string.Join(Environment.NewLine, TemplateNamesDebug.ToArray()));
			Console.WriteLine(string.Join(Environment.NewLine,
				"SourceDirectory", SourceDirectory,
				"CurrentDirectory", CurrentDirectory,
				"GetFolder(CurrentDirectory)", GetFolder(CurrentDirectory),
				"Dir_AntistasiTemplates", Dir_AntistasiTemplates,
				"PlayerName", PlayerName,
				"Dir_mpMissions", Dir_mpMissions,
				"RuntimeTimeValue.MissionVersion", RuntimeTimeValue.MissionVersion
			));
#endif
			Directory.CreateDirectory(Dir_mpMissions);
			foreach (MapTemplate Item in AntistasiMapTemplates) {
				string Destination = Dir_mpMissions + Item.Name + RuntimeTimeValue.MissionVersion + "." + Item.Map;
				string TemplateFolder = Dir_AntistasiTemplates + @"\" + Item.Dir;
#if DEBUG
				Console.WriteLine("Copying " + Item.Dir + " Base&Template assets...");
#endif
				XCopy(Dir_AntistasiRoot, Destination);
				XCopy(TemplateFolder, Destination);
				if (PBOFiles) {
					FileBank(Destination);
				};
			}
#if DEBUG
			ShowMessage("Press any key to open " + GetFolder(Dir_mpMissions) + ".");
			Process.Start(Dir_mpMissions + "\\");
#else
			if (OpenOutput) {
				Process.Start(Dir_mpMissions + "\\");
			}
#endif
		}
	}
}