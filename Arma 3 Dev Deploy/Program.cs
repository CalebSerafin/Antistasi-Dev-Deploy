using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static Arma_3_Dev_Deploy.CompileTimeValues;
using static Arma_3_Dev_Deploy.ConfigFiles;
using static Arma_3_Dev_Deploy.XCopyLib;
using static Arma_3_Dev_Deploy.WindowPowerLib;

/*
	Very useful with tools such as https://marketplace.visualstudio.com/items?itemName=wk-j.save-and-run

*/

namespace Arma_3_Dev_Deploy
{
	class Program
	{
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
		
		static void Main(string[] args)
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

			if (ArgFlag("/Q")) {WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_HIDE); };
			bool Quiet = ArgFlag("/Q");
			bool Replace = ArgFlag("/R");
			bool Notify = ArgFlag("/N");
		Restart:

			bool FactoryDefaultConfig = false;
			if (!Directory.Exists(CompileTimeValue.Dir))
			{
				Console.WriteLine("It appears this is the first time running Arma 3 Dev Deploy in this dictionary.");
				Console.WriteLine("Press any key to setup the configuration files");
				Console.ReadKey(); Console.WriteLine("\n");
				Directory.CreateDirectory(CompileTimeValue.Dir);
				CreateConfigFile(CompileTimeValue.TSelected);
				CreateConfigFile(CompileTimeValue.TIgnoreFiles);
				FactoryDefaultConfig = true;
			};
			if (!File.Exists(CompileTimeValue.Dir + CompileTimeValue.TSelected))
			{
				Console.WriteLine("Whoops: " + CompileTimeValue.TSelected + " is missing.");
				Console.WriteLine("Press any key to setup " + CompileTimeValue.TSelected);
				Console.ReadKey(); Console.WriteLine("\n");
				CreateConfigFile(CompileTimeValue.TSelected);
				FactoryDefaultConfig = true;
			};
			if (!File.Exists(CompileTimeValue.Dir + CompileTimeValue.TIgnoreFiles))
			{
				Console.WriteLine("Whoops: " + CompileTimeValue.TIgnoreFiles + " is missing.");
				Console.WriteLine("Press any key to setup " + CompileTimeValue.TIgnoreFiles);
				Console.ReadKey(); Console.WriteLine("\n");
				CreateConfigFile(CompileTimeValue.TIgnoreFiles);
				Console.WriteLine("");
				FactoryDefaultConfig = true;
			};
			if (FactoryDefaultConfig)
			{
				Console.WriteLine("Factory default config file(s) have been setup inside the folder " + CompileTimeValue.Dir + ".");
				Console.WriteLine("You should exit and change them to suit your needs.");
				Console.WriteLine("Press any key to exit and reveal " + CompileTimeValue.Dir + " in explorer");
				Console.ReadKey(); Console.WriteLine("\n");
				Process.Start(CompileTimeValue.Dir);
				return;
			};

			string[] MapTemplatesRaw;
			try
			{
				MapTemplatesRaw = File.ReadAllLines(CompileTimeValue.Dir + CompileTimeValue.TSelected);
			}
			catch (Exception FileReadError)
			{
				throw FileReadError;
			};
			
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
			if (MapTemplateVersion != CompileTimeValue.MTVersionS)
			{
				if (ManagedMode) return;
				if (TemplateSelectedUpdate()) goto Restart; else return;
			};
			if (MissionType != "A3Antistasi") goto MissionTypeNotSupported;
			if (arma3ProfileName == "") goto A3ProfileNameInvalid;

			string Source = Environment.ExpandEnvironmentVariables(@".\A3-Antistasi");
			string mpMissions = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Arma 3 - Other Profiles\" + arma3ProfileName + @"\mpmissions\");
			string Destination, TemplateFolder;
			string XCopyArgs = " ";
			XCopyArgs += Quiet ? "/Q" : "";
			XCopyArgs += Replace ? "/R" : "";

			foreach (MapTemplate Item in MapTemplates)
			{
				Destination = mpMissions + Item.Name + "." + Item.Map;
				TemplateFolder = Source + "\\Templates\\" + Item.Name + "." + Item.Map;

				//The following log option is broken, thanks XCOPY ♥.
				//XCopy(Source, mpMissions + Item.Map, "/c /s /d /i /y /exclude:" + Config.Dir + Config.TIgnoreFiles + " > " + Config.Dir + "Antistasi.xcopy.log");
				XCopy(Source, Destination, "/C /S /I /Y /Exclude:" + CompileTimeValue.Dir + CompileTimeValue.TIgnoreFiles, XCopyArgs);
				XCopy(TemplateFolder, Destination, "/C /S /I /Y", XCopyArgs);

			};

			if (Notify) Console.Write("Job Done");
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
