using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using static Antistasi_Dev_Deploy.Registary;
using static Antistasi_Dev_Deploy.ProgramValues;
using static Antistasi_Dev_Deploy.WindowPowerLib;
using static Antistasi_Dev_Deploy.XCopyLib;
using static Antistasi_Dev_Deploy.GetFolderLib;
using static Antistasi_Dev_Deploy.MapHandling;

namespace Antistasi_Dev_Deploy
{
	class Program
	{
		//Needs to be Nullable as Registry calls may return null if key does not exist.
		private static bool BoolBin(int? Input) => !Input.HasValue ? false : Input > 0;
		//Exposed so that a pressKey wait at the end of the program does not hold up system resources.
		static void Main(string[] args)
		{
#if !DEBUG
			WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_HIDE);
#endif
			foreach (string arg in args)
			{
				switch (arg.Substring(0, 2).ToLower())
				{
					case "/v":
						Console.WriteLine("Version: " + CompileTimeValue.MTVersionS);
						Console.ReadKey();
						return;
					case "/h":
						Console.WriteLine("/v	Version		Prints current version.");
						Console.WriteLine("/h	Help		Prints switch list.");
						Console.ReadKey();
						return;
				}
			}

			string Reg_Value_Arma_PlayerName_Value = FetchArma(Reg.Value_Arma_PlayerName_Name, string.Empty);
			Console.WriteLine("yes");
			bool Reg_Value_ADD_OverrideOutput_Value = BoolBin((int)FetchA3DD(Reg.Value_ADD_OverrideOutput_Name, 0));
			Console.WriteLine("no");
			bool Reg_Value_ADD_ForceOpenOutput_Value = BoolBin((int)FetchA3DD(Reg.Value_ADD_ForceOpenOutput_Name, 0));

			string CurrentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			string Dir_AntistasiRoot = CurrentDirectory + @"\A3-Antistasi";
			string Dir_AntistasiTemplates = CurrentDirectory + @"\Map-Templates";
			string Reg_Value_ADD_OverrideOutputFolder_Value;
			string Dir_mpMissions = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Arma 3 - Other Profiles\" + Reg_Value_Arma_PlayerName_Value + @"\mpmissions\");
			
			Reg_Value_ADD_OverrideOutputFolder_Value = (string)Registry.GetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_OverrideOutputFolder_Name, "C:\\");
			if (Reg_Value_ADD_OverrideOutputFolder_Value == null) Reg_Value_ADD_OverrideOutputFolder_Value = "C:\\";
			if (!Reg_Value_ADD_OverrideOutputFolder_Value.EndsWith("\\")) Reg_Value_ADD_OverrideOutputFolder_Value += "\\";
			//The following handles whether the executable is placed inside a sub folder in the root git directory.
			if (!Directory.Exists(CurrentDirectory + @"\A3-Antistasi"))
			{
				Dir_AntistasiRoot = CurrentDirectory + @"\..\A3-Antistasi";
				Dir_AntistasiTemplates = CurrentDirectory + @"\..\Map-Templates";
			}
			/*if there is an issue fetching Arma 3 profile name or if developing on a computer that 
			does not have Arma 3 Installed this allows it to still be able to package missions. 
			The name matches the outfolder of a python tool in the Offical Repository that does this aswell.*/
			if (Reg_Value_Arma_PlayerName_Value == string.Empty || !Directory.Exists(Dir_mpMissions))
			{
				Dir_mpMissions = CurrentDirectory + @"\PackagedMissions\";
			}
			if (Reg_Value_ADD_OverrideOutput_Value) Dir_mpMissions = Reg_Value_ADD_OverrideOutputFolder_Value;
			List<MapTemplate> AntistasiMapTemplates = new List<MapTemplate>();
			if (Directory.Exists(Dir_AntistasiTemplates))
			{
				string[] Templates_Directories = Directory.GetDirectories(Dir_AntistasiTemplates);
				foreach (string item in Templates_Directories)
				{
					string LastFolder = GetFolder(item);
					string[] TemplateData = LastFolder.Split('.');
					//If spliting by '.' does not produce two string it is probably not a map template.
					if (TemplateData.Length != 2) continue;
					AntistasiMapTemplates.Add(new MapTemplate(TemplateData));
				}
			}
#if DEBUG
			foreach (MapTemplate item in AntistasiMapTemplates)
			{
				Console.WriteLine(item.Name + " on map " + item.Map);
			}
			Console.WriteLine(CurrentDirectory);
			Console.WriteLine(GetFolder(CurrentDirectory));
			Console.WriteLine(Dir_AntistasiTemplates);
			Console.WriteLine(Reg_Value_Arma_PlayerName_Value);
			Console.WriteLine(Dir_mpMissions);
			Console.WriteLine(RuntimeTimeValue.MissionVersion);
#endif
			Directory.CreateDirectory(Dir_mpMissions);
			foreach (MapTemplate Item in AntistasiMapTemplates)
			{
				string Destination = Dir_mpMissions + Item.Name + RuntimeTimeValue.MissionVersion + "." + Item.Map;
				string TemplateFolder = Dir_AntistasiTemplates + @"\" + Item.Dir;
				string XCopyArgs = "/Q";

				//The following log option is broken, thanks XCOPY ♥.
				//XCopy(Source, mpMissions + Item.Map, "/c /s /d /i /y /exclude:" + Config.Dir + Config.TIgnoreFiles + " > " + Config.Dir + "Antistasi.xcopy.log");
				Console.WriteLine("Copying " + Item.Dir + " base assets...");
				//XCopy(Dir_AntistasiRoot, Destination, "/C /S /I /Y /Exclude:" + CompileTimeValue.TIgnoreFiles, XCopyArgs);
				XCopy(Dir_AntistasiRoot, Destination, "/C /S /I /Y", XCopyArgs);
				Console.WriteLine("Copying " + Item.Dir + " Template assets...");
				XCopy(TemplateFolder, Destination, "/C /S /I /Y", XCopyArgs);
			}
#if DEBUG

			Console.WriteLine("Press any key to open " + GetFolder(Dir_mpMissions) + ".");
			Console.ReadKey();
			Process.Start(Dir_mpMissions + "\\");
#else
			if (Reg_Value_ADD_ForceOpenOutput_Value)
			{
				Process.Start(Dir_mpMissions + "\\");
			}
#endif
		}
	}
}