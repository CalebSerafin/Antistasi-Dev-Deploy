using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using static Antistasi_Dev_Deploy.ProgramValues;
using static Antistasi_Dev_Deploy.ProgramValues.Reg;
using static Antistasi_Dev_Deploy.WindowPowerLib;
using static Antistasi_Dev_Deploy.XCopyLib;
using static Antistasi_Dev_Deploy.GetFolderLib;
using static Antistasi_Dev_Deploy.CustomIO;

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
		class MapTemplate
		{
			public string Name { get; set; }
			public string Map { get; set; }
			public string Dir { get; set; }

			public MapTemplate(string nameX_mapX)
			{
				string[] data = nameX_mapX.Split('.');
				Name = data[0];
				Map = data[1];
				Dir = Name + "." + Map;
			}
			public MapTemplate(string nameX, string mapX)
			{
				Name = nameX;
				Map = mapX;
				Dir = Name + "." + Map;
			}
			public MapTemplate(string[] nameX_mapX)
			{
				Name = nameX_mapX[0];
				Map = nameX_mapX[1];
				Dir = Name + "." + Map;
			}
		};


		static void Main(string[] args)
		{
			if (CompileTimeValue.Debug_HideWindow)
			{
				WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_HIDE);
			}


			bool Reg_Value_ADD_ForceOpenOutput_Value;
			bool Reg_Value_ADD_OverrideOutput_Value;
			string Reg_Value_Arma_PlayerName_Value;
			//////////Fetch Values from Registry
			try
			{
				Reg_Value_Arma_PlayerName_Value = (string)Registry.GetValue(Reg.Key_Arma, Reg.Value_Arma_PlayerName_Name, string.Empty);
			}
			catch (Exception e)
			{
				switch (e.GetType().Name)
				{
					case "NullReferenceException": Reg_Value_Arma_PlayerName_Value = string.Empty; break;
					default: throw e;
				}
			}
			try
			{
				Reg_Value_ADD_OverrideOutput_Value = BoolBin((int)Registry.GetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_OverrideOutput_Name, 0));
			}
			catch (Exception e)
			{
				switch (e.GetType().Name)
				{
					case "NullReferenceException": Reg_Value_ADD_OverrideOutput_Value = false; break;
					default: throw e;
				}
			}
			try
			{
				Reg_Value_ADD_ForceOpenOutput_Value = BoolBin((int)Registry.GetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_ForceOpenOutput_Name, 0));
			}
			catch (Exception e)
			{
				switch (e.GetType().Name)
				{
					case "NullReferenceException": Reg_Value_ADD_ForceOpenOutput_Value = false; break;
					default: throw e;
				}
			}
			//////////Fetch Values from Registry

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
			if (Reg_Value_ADD_ForceOpenOutput_Value) CompileTimeValue.Debug_OpenOutput = true;
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
			if (CompileTimeValue.Debug_Log)
			{
				WriteLine("Press any key to open " + GetFolder(Dir_mpMissions) + ".");
				ReadKey(); ;
			}
			if (CompileTimeValue.Debug_Log || CompileTimeValue.Debug_OpenOutput)
			{
				Process.Start(Dir_mpMissions + "\\"); 
			}
		}
	}
}
