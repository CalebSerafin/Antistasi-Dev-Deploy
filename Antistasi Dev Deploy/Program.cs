using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using static Antistasi_Dev_Deploy.CompileTimeValues;
using static Antistasi_Dev_Deploy.CompileTimeValues.Reg;
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
			
			string CurrentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			string Reg_Value_Arma_PlayerName_Value = (string) Registry.GetValue(CompileTimeValue.Reg_Key_Arma, CompileTimeValue.Reg_Value_Arma_PlayerName_Name, string.Empty);
			string Dir_mpMissions = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Arma 3 - Other Profiles\" + Reg_Value_Arma_PlayerName_Value + @"\mpmissions\");
			string Dir_AntistasiRoot = CurrentDirectory + @"\A3-Antistasi";
			string Dir_AntistasiTemplates = CurrentDirectory + @"\Map-Templates";
			bool Reg_Value_ADD_OverrideOutput_Value;
			string Reg_Value_ADD_OverrideOutputFolder_Value;

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
			Reg_Value_ADD_OverrideOutputFolder_Value = (string)Registry.GetValue(Reg.Key_A3DD_ADD, Reg.Value_ADD_OverrideOutputFolder_Name, "C:\\");
			if (Reg_Value_ADD_OverrideOutputFolder_Value == null) Reg_Value_ADD_OverrideOutputFolder_Value = "C:\\";
			if (!Reg_Value_ADD_OverrideOutputFolder_Value.EndsWith("\\")) Reg_Value_ADD_OverrideOutputFolder_Value += "\\";

			if (!Directory.Exists("A3-Antistasi"))
			{
				Dir_AntistasiRoot = CurrentDirectory + @"\..\A3-Antistasi";
				Dir_AntistasiTemplates = CurrentDirectory + @"\..\Map-Templates";
			}
			if (Reg_Value_Arma_PlayerName_Value == string.Empty)
			{
				Dir_mpMissions = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Arma 3\mpmissions\");
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

			string IgnoreFiles_RoadsDB = "";
			foreach (MapTemplate item in AntistasiMapTemplates)
			{
				IgnoreFiles_RoadsDB += item.Dir + "\\\n";
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
			WriteLine(IgnoreFiles_RoadsDB);
#endif
			foreach (MapTemplate Item in AntistasiMapTemplates)
			{
				string Destination = Dir_mpMissions + Item.Dir;
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
			if (CompileTimeValue.Debug_OpenOutput)
			{
				WriteLine("Press any key to open " + GetFolder(Dir_mpMissions) + ".");
				ReadKey();
				Process.Start(Dir_mpMissions + "\\"); 
			}
		}
	}
}
