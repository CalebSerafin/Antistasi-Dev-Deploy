using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
	Very useful with tools such as https://marketplace.visualstudio.com/items?itemName=wk-j.save-and-run

*/

namespace Arma_3_Dev_Deploy
{
	class Program
	{
		class WindowPower
		{
			[DllImport("kernel32.dll")]
			public static extern IntPtr GetConsoleWindow();

			[DllImport("user32.dll")]
			public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

			public const int SW_HIDE = 0;
			public const int SW_SHOW = 5;


			//ShowWindow(WindowPowerHandle, SW_HIDE);
			//ShowWindow(WindowPowerHandle, SW_SHOW);
		}

		class Config
		{
			public string Dir { get; } = ".\\A3DD\\"; // A3DD\
			public string TSelected { get; } = "template.selected.cfg"; // template.selected.cfg
			public string TIgnoreFiles { get; } = "template.ignoreFiles.cfg"; // template.ignoreFiles.cfg
			public static int MTVersionI { get; } = 1; // template.ignoreFiles.cfg

			public string MTVersion { get; } = MTVersionI.ToString(); // template.ignoreFiles.cfg
		}

		static bool TemplateSelectedUpdate() //returns true for restart;
		{
			Config Config = new Config();
			string[] MapTemplatesRaw = File.ReadAllLines(Config.Dir + Config.TSelected);
			string TemplateCode, TemplateParam;
			string MapTemplateVersionString = "";
			foreach (string LineItem in MapTemplatesRaw)
			{
				if (LineItem == "") continue;
				if (LineItem[0] == ';') continue;
				TemplateCode = LineItem.Split(';')[0];
				TemplateParam = TemplateCode.Split('=')[0].ToLower();

				if (TemplateParam == "version")
				{
					MapTemplateVersionString = TemplateCode.Split('=')[1];
					if (MapTemplateVersionString != Config.MTVersion) break;
					return true;
				};
			};

			if (MapTemplateVersionString == "") goto VersionParamMissing;

			if (!Int32.TryParse(MapTemplateVersionString, out int MapTemplateVersion)) goto VersionParamMissing;

			if (MapTemplateVersion > Config.MTVersionI) goto VersionGreater;

			goto VersionParamMissing;

		VersionGreater:
			Console.WriteLine("Template.selected.cfg version " + MapTemplateVersion + " is newer than your installation of A3DD Version " + Config.MTVersion + ".");
			Console.WriteLine("Please download the new Arma 3 Dev Deploy from \"TODO:GITHUB LINK\"");
			Console.WriteLine("Press any key to exit");
			Console.ReadKey(); Console.WriteLine("\n");
			return false;

		VersionParamMissing:
			Console.WriteLine("The Version parameter is missing or malformed in template.selectd.cfg.");
			Console.WriteLine("Press R to reset template.selectd.cfg to factory default, or any other key to exit");
			if (Console.ReadKey().KeyChar == 'r')
			{
				Console.WriteLine("\n");
				CreateConfigFile(Config.TSelected);
				return true;
			}; 
			Console.WriteLine("\n");
			return false;
		}

		static void XCopy(string Source, string Destination, string Flags, string Args)
		{
			bool ArgFlag(string Flag)
			{
				return Args.ToLower().Contains(Flag.ToLower());
			}
			bool Quiet;
			Quiet = ArgFlag("/Q");
			Process process = new Process();

			try
			{
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.RedirectStandardInput = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = false;
				process.StartInfo.FileName = "XCopy";
				process.StartInfo.Arguments = "\"" + Source + "\" \"" + Destination + "\" " + Flags;
				//Console.WriteLine("Arguments: {" + process.StartInfo.Arguments + "}"); //For debugging purposes
				bool processStarted = process.Start();
				if (processStarted)
				{
					string outputReader = process.StandardOutput.ReadToEnd();
					string errorReader = process.StandardError.ReadToEnd();
					process.WaitForExit();

					//Display the result
					string displayText = "Output" + Environment.NewLine + "==============" + Environment.NewLine;
					displayText += outputReader;
					if (errorReader != "")
					{
						displayText += Environment.NewLine + "Error" + Environment.NewLine + "==============" +	Environment.NewLine;
						displayText += errorReader;
						if (errorReader == "Invalid number of parameters\r\n")	{displayText += process.StartInfo.Arguments;};
					};
					if (!Quiet)	Console.WriteLine(displayText);
				}
				else
				{
					if (!Quiet) Console.WriteLine("ERROR: Process has not started");
				}
			}
			catch (Exception XCopyError)
			{
				if (!Quiet) Console.WriteLine(XCopyError.Message);
			}
			finally
			{
				if (process.StandardOutput != null)
				{
					process.StandardOutput.Close();
				}
				if (process.StandardError != null)
				{
					process.StandardError.Close();
				}
				process.Close();
			}
		}

		class MapTemplate
		{
			public string Name { get; set; }
			public string Map { get; set; }

			public MapTemplate(string nameX_mapX)
			{
				string[] data = nameX_mapX.Split('.');
				Name = data[0];
				Map = data[1];
			}

			public MapTemplate(string nameX, string mapX)
			{
				Name = nameX;
				Map = mapX;
			}
		};

		static void CreateConfigFile(string Name)
		{
			Config Config = new Config();
			Dictionary<string, string> NewFile = new Dictionary<string, string>
			{
				{
					Config.TSelected,
					@"; The following controls which mission templates are copied:
; To run this A3DD silently, run the exe with param /Q

Version=" + Config.MTVersion + @";
A3Profile=FrostsBite;							Default Arma Profile is not supported.
MissionType=A3Antistasi;					Only Type available, will expand on in the future...
Template=A3-AA-BLUFORTemplate.Altis;	Replace as you wish.
Template=A3-AATemplate.Altis;
Template=A3-ArmiaKrajowaTemplate.chernarus_summer;
Template=A3-WotPTemplate.Tanoa;

ManagedMode=false;							Change to true to disable all user interaction (If called from another application)
"
				},
				{
					Config.TIgnoreFiles,
					@"description.ext\
mission.sqm\
mission_IFAOnly.sqm\
PIC.jpg\

roadsDBAltis.sqf\
roadsDBcherna.sqf\
roadsDB.sqf\

ak.jpg\
"
				}
			};

			File.WriteAllText(Config.Dir + Name, NewFile[Name]);
		}

		static void Main(string[] args) //one day...
		{
			bool ArgFlag(string Flag)
			{
				string ArgsMerged = "";
				foreach (string arg in args)
				{
					ArgsMerged += arg + " ";
				}
				return ArgsMerged.ToLower().Contains(Flag.ToLower());
			}

			if (ArgFlag("/Q")) { WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_HIDE); };
			Config Config = new Config();
			bool Quiet = ArgFlag("/Q");
		Restart:

			bool FactoryDefaultConfig = false;
			if (!Directory.Exists(Config.Dir))
			{
				Console.WriteLine("It appears this is the first time running Arma 3 Dev Deploy in this dictionary.");
				Console.WriteLine("Press any key to setup the configuration files");
				Console.ReadKey(); Console.WriteLine("\n");
				Directory.CreateDirectory(Config.Dir);
				CreateConfigFile(Config.TSelected);
				CreateConfigFile(Config.TIgnoreFiles);
				FactoryDefaultConfig = true;
			};
			if (!File.Exists(Config.Dir + Config.TSelected))
			{
				Console.WriteLine("Whoops: " + Config.TSelected + " is missing.");
				Console.WriteLine("Press any key to setup " + Config.TSelected);
				Console.ReadKey(); Console.WriteLine("\n");
				CreateConfigFile(Config.TSelected);
				FactoryDefaultConfig = true;
			};
			if (!File.Exists(Config.Dir + Config.TIgnoreFiles))
			{
				Console.WriteLine("Whoops: " + Config.TIgnoreFiles + " is missing.");
				Console.WriteLine("Press any key to setup " + Config.TIgnoreFiles);
				Console.ReadKey(); Console.WriteLine("\n");
				CreateConfigFile(Config.TIgnoreFiles);
				Console.WriteLine("");
				FactoryDefaultConfig = true;
			};
			if (FactoryDefaultConfig)
			{
				Console.WriteLine("Factory default config file(s) have been setup inside the folder " + Config.Dir + ".");
				Console.WriteLine("You should exit and change them to suit your needs.");
				Console.WriteLine("Press any key to exit and reveal " + Config.Dir + " in explorer");
				Console.ReadKey(); Console.WriteLine("\n");
				Process.Start(Config.Dir);
				return;
			};

			string[] MapTemplatesRaw;
			try
			{
				MapTemplatesRaw = File.ReadAllLines(Config.Dir + Config.TSelected);
			}
			catch (Exception FileReadError)
			{
				throw FileReadError;
			};
			//===========================================
			List<MapTemplate> MapTemplates = new List<MapTemplate>();
			string MapTemplateVersion = "0";
			string arma3ProfileName = "";
			string MissionType = "";
			bool ManagedMode = false;
			string TemplateCode, TemplateParam;

			foreach (string LineItem in MapTemplatesRaw)
			{
				if (LineItem == "") continue;
				if (LineItem[0] == ';') continue;
				TemplateCode = LineItem.Split(';')[0];
				TemplateParam = TemplateCode.Split('=')[0].ToLower();

				switch (TemplateParam)
				{
					case "a3profile":
						arma3ProfileName = TemplateCode.Split('=')[1];
						break;
					case "missiontype":
						MissionType = TemplateCode.Split('=')[1];
						break;
					case "template":
						MapTemplates.Add(new MapTemplate(TemplateCode.Split('=')[1]));
						break;
					case "version":
						MapTemplateVersion = TemplateCode.Split('=')[1];
						break;
					case "ManagedMode":
						ManagedMode = TemplateCode.Split('=')[1].ToLower() == "false";
						break;
				};
			}
			if (MapTemplateVersion != Config.MTVersion)
			{
				if (ManagedMode) return;
				if (TemplateSelectedUpdate()) goto Restart; else return;
			};
			if (MissionType != "A3Antistasi") goto MissionTypeNotSupported;
			if (arma3ProfileName == "") goto A3ProfileNameInvalid;

			//xcopy ".\A3-Antistasi" "C:\\Users\\User\\Documents\\Arma 3 - Other Profiles\\FrostsBite\\mpmissions\\A3-Antistasi.Altis" /i /e
			string Source = Environment.ExpandEnvironmentVariables(@".\A3-Antistasi");
			string mpMissions = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Arma 3 - Other Profiles\" + arma3ProfileName + @"\mpmissions\");
			string Destination, TemplateFolder;
			string XCopyArgs = Quiet ? "/Q" : "";

			foreach (MapTemplate Item in MapTemplates)
			{
				Destination = mpMissions + Item.Name + "." + Item.Map;
				TemplateFolder = Source + "\\Templates\\" + Item.Name + "." + Item.Map;

				//The following log option is broken, thanks XCOPY ♥.
				//XCopy(Source, mpMissions + Item.Map, "/c /s /d /i /y /exclude:" + Config.Dir + Config.TIgnoreFiles + " > " + Config.Dir + "Antistasi.xcopy.log");
				XCopy(Source, Destination, "/c /s /d /i /y /exclude:" + Config.Dir + Config.TIgnoreFiles, XCopyArgs);
				XCopy(TemplateFolder, Destination, "/c /s /d /i /y", XCopyArgs);//"/Q");

			};

			if (ManagedMode || Quiet) return;
			Console.Write("\nPress any key to exit...");
			Console.ReadKey(); Console.WriteLine("\n");
			return;

		A3ProfileNameInvalid:
			if (ManagedMode) return;
			Console.WriteLine("A3Profile Name is missing");
			Console.WriteLine("Example: \"A3Profile=FrostsBite;\"");
			Console.WriteLine("Press any key to exit...");
			Console.ReadKey(); Console.WriteLine("\n");
			return;

		MissionTypeNotSupported:
			if (ManagedMode) return;
			Console.WriteLine("Sorry, MissionType " + MissionType + " is not supported.");
			Console.WriteLine("The following MissionTypes are supported:");
			Console.WriteLine("A3Antistasi");
			Console.WriteLine("");
			Console.WriteLine("Press any key to exit...");
			Console.ReadKey(); Console.WriteLine("\n");
			return;
		}
	}
}
