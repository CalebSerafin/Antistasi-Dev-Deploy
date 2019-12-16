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
using static Antistasi_Dev_Deploy.CustomIO;
using static Antistasi_Dev_Deploy.MapHandling;

namespace Antistasi_Dev_Deploy
{
	class Program
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
		static void Main(string[] args)
		{
			if (CompileTimeValue.Debug_HideWindow)
			{
				WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_HIDE);
			}
			foreach (string arg in args)
			{
				switch (arg.Substring(0, 2).ToLower())
				{
					case "/v":
						Console.WriteLine("Version: " + CompileTimeValue.MTVersionS);
						ReadKey();
						return;
					case "/h":
						Console.WriteLine("/v	Version		Prints current version.");
						Console.WriteLine("/h	Help		Prints switch list.");
						ReadKey();
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

			if (!Directory.Exists(CurrentDirectory + @"\A3-Antistasi"))
			{
				Dir_AntistasiRoot = CurrentDirectory + @"\..\A3-Antistasi";
				Dir_AntistasiTemplates = CurrentDirectory + @"\..\Map-Templates";
			}
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
					if (TemplateData.Length == 2)
					{
						AntistasiMapTemplates.Add(new MapTemplate(TemplateData));
					}
				}
			}
#if DEBUG
			foreach (MapTemplate item in AntistasiMapTemplates)
			{
				WriteLine(item.Name + " on map " + item.Map);
			}
			WriteLine(CurrentDirectory);
			WriteLine(GetFolder(CurrentDirectory));
			WriteLine(Dir_AntistasiTemplates);
			WriteLine(Reg_Value_Arma_PlayerName_Value);
			WriteLine(Dir_mpMissions);
			WriteLine(RuntimeTimeValue.MissionVersion);
#endif
			Directory.CreateDirectory(Dir_mpMissions);
			foreach (MapTemplate Item in AntistasiMapTemplates)
			{
				string Destination = Dir_mpMissions + Item.Name + RuntimeTimeValue.MissionVersion + "." + Item.Map;
				string TemplateFolder = Dir_AntistasiTemplates + @"\" + Item.Dir;
				string XCopyArgs = "/Q";

				//The following log option is broken, thanks XCOPY ♥.
				//XCopy(Source, mpMissions + Item.Map, "/c /s /d /i /y /exclude:" + Config.Dir + Config.TIgnoreFiles + " > " + Config.Dir + "Antistasi.xcopy.log");
				WriteLine("Copying " + Item.Dir + " base assets...");
				//XCopy(Dir_AntistasiRoot, Destination, "/C /S /I /Y /Exclude:" + CompileTimeValue.TIgnoreFiles, XCopyArgs);
				XCopy(Dir_AntistasiRoot, Destination, "/C /S /I /Y", XCopyArgs);
				WriteLine("Copying " + Item.Dir + " Template assets...");
				XCopy(TemplateFolder, Destination, "/C /S /I /Y", XCopyArgs);
			}
#if DEBUG
			
			WriteLine("Press any key to open " + GetFolder(Dir_mpMissions) + ".");
			ReadKey();
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
