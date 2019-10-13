using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using static Antistasi_Dev_Deploy.CompileTimeValues;
using static Antistasi_Dev_Deploy.WindowPowerLib;
using static Antistasi_Dev_Deploy.XCopyLib;
using static Antistasi_Dev_Deploy.ConfigFiles;
using static Antistasi_Dev_Deploy.GetFolderLib;

namespace Antistasi_Dev_Deploy
{
	class Program
	{
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
			WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_SHOW);

			string Reg_Value_Arma_PlayerName_Value = (string) Registry.GetValue(CompileTimeValue.Reg_Key_Arma, CompileTimeValue.Reg_Value_Arma_PlayerName_Name, string.Empty);
			string Dir_mpMissions = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Arma 3 - Other Profiles\" + Reg_Value_Arma_PlayerName_Value + @"\mpmissions\");
			string Dir_AntistasiRoot = Environment.ExpandEnvironmentVariables(Environment.CurrentDirectory + @"\A3-Antistasi");
			string Dir_AntistasiTemplates = Environment.ExpandEnvironmentVariables(Environment.CurrentDirectory + @"\A3-Antistasi\Templates");

			if (!Directory.Exists("A3-Antistasi"))
			{
				Dir_AntistasiRoot = Environment.ExpandEnvironmentVariables(Environment.CurrentDirectory + @"\..\A3-Antistasi");
				Dir_AntistasiTemplates = Environment.ExpandEnvironmentVariables(Environment.CurrentDirectory + @"\..\A3-Antistasi\Templates");
			}
			if (Reg_Value_Arma_PlayerName_Value == string.Empty)
			{
				Dir_mpMissions = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\Arma 3\mpmissions\");
			}
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

			//*/////////////////////////////////DEBUGING
			//*
			foreach (MapTemplate item in AntistasiMapTemplates)
			{
				Console.WriteLine(item.Name + " on map " + item.Map);
			}
			Console.WriteLine(GetFolder(Environment.CurrentDirectory));
			Console.WriteLine(Dir_AntistasiTemplates);
			Console.WriteLine(Reg_Value_Arma_PlayerName_Value);
			Console.WriteLine(Dir_mpMissions);
			Console.WriteLine(IgnoreFiles_RoadsDB);
			//*/////////////////////////////////DEBUGING

			CreateConfigFile(CompileTimeValue.TIgnoreFiles, IgnoreFiles_RoadsDB);
			foreach (MapTemplate Item in AntistasiMapTemplates)
			{
				string Destination = Dir_mpMissions + Item.Dir;
				string TemplateFolder = Dir_AntistasiTemplates + @"\" + Item.Dir;
				string XCopyArgs = "/Q";

				//The following log option is broken, thanks XCOPY ♥.
				//XCopy(Source, mpMissions + Item.Map, "/c /s /d /i /y /exclude:" + Config.Dir + Config.TIgnoreFiles + " > " + Config.Dir + "Antistasi.xcopy.log");
				XCopy(Dir_AntistasiRoot, Destination, "/C /S /I /Y /Exclude:" + CompileTimeValue.TIgnoreFiles, XCopyArgs);
				XCopy(TemplateFolder, Destination, "/C /S /I /Y", XCopyArgs);
			};
			DeleteConfigFile(CompileTimeValue.TIgnoreFiles);

			Console.WriteLine("Press any key to open mpmissions.");
			Console.ReadKey();
			Process.Start(Dir_mpMissions);
		}
	}
}
