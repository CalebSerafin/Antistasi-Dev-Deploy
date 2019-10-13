using System;
using System.Collections.Generic;
using System.IO;
using static Arma_3_Dev_Deploy.CompileTimeValues;

namespace Arma_3_Dev_Deploy
{
	class ConfigFiles
	{
		public static void CreateConfigFile(string Name)
		{
			Dictionary<string, string> NewFile = new Dictionary<string, string>
			{
				{
					CompileTimeValue.TSelected,
					@"; The following controls which mission templates are copied:
; To run A3DD Quietly, run the exe with param: /Q
; By default only new modified files are recopied into the mpmissions dictionary.
; To force all files to be Replaced, add the param: /R
; To output ""Job Done"" to Notify when all files have been copied, add the param: /N

Version=" + CompileTimeValue.MTVersionS + @";
A3Profile=FrostsBite;					Default Arma Profile is not supported.
MissionType=A3Antistasi;				Only A3Antistasi is available, will expand on in the future...
Template=A3-AA-BLUFORTemplate.Altis;	Replace as you wish.
Template=A3-AATemplate.Altis;
Template=A3-ArmiaKrajowaTemplate.chernarus_summer;
Template=A3-WotPTemplate.Tanoa;

ManagedMode=false;						Change to true to disable all user interaction (If called from another application)
"
				},
				{
					CompileTimeValue.TIgnoreFiles,
					@"description.ext\
mission.sqm\
mission_IFAOnly.sqm\
PIC.jpg\

roadsDBAltis.sqf\
roadsDBcherna.sqf\
roadsDB.sqf\

ak.jpg\
"
				},
				{
					CompileTimeValue.VSCodeSettings,
"{"+
"	\"saveAndRun\": {"+
"		\"commands\": ["+
"		{"+
"			\"match\": \".sqf\","+
"			\"cmd\": \".\\\\\\\"Arma 3 Dev Deploy.exe\\\" /Q /N\","+
"			\"useShortcut\": false,"+
"			\"silent\": true"+
"		},"+
"		{"+
"			\"match\": \".sqm\","+
"			\"cmd\": \".\\\\\\\"Arma 3 Dev Deploy.exe\\\" /Q /N\","+
"			\"useShortcut\": false,"+
"			\"silent\": true"+
"		},"+
"		{"+
"			\"match\": \".ext\","+
"			\"cmd\": \".\\\\\\\"Arma 3 Dev Deploy.exe\\\" /Q /N\","+
"			\"useShortcut\": false,"+
"			\"silent\": true"+
"		},"+
"		{"+
"			\"match\": \".hpp\","+
"			\"cmd\": \".\\\\\\\"Arma 3 Dev Deploy.exe\\\" /Q /N\","+
"			\"useShortcut\": false,"+
"			\"silent\": true"+
"		}]"+
"	}"+
"}"
			}
		};
			File.WriteAllText(CompileTimeValue.Dir + Name, NewFile[Name]);
		}
		public static bool TemplateSelectedUpdate() //returns true for restart;
		{
			string[] MapTemplatesRaw = File.ReadAllLines(CompileTimeValue.Dir + CompileTimeValue.TSelected);
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
					if (MapTemplateVersionString != CompileTimeValue.MTVersionS) break;
					return true;
				};
			};

			if (MapTemplateVersionString == "") goto VersionParamMissing;

			if (!Int32.TryParse(MapTemplateVersionString, out int MapTemplateVersion)) goto VersionParamMissing;

			if (MapTemplateVersion > CompileTimeValue.MTVersionI) goto VersionGreater;

			goto VersionParamMissing;

		VersionGreater:
			Console.WriteLine("Template.selected.cfg version " + MapTemplateVersion + " is newer than your installation of A3DD Version " + CompileTimeValue.MTVersionS + ".");
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
				CreateConfigFile(CompileTimeValue.TSelected);
				return true;
			};
			Console.WriteLine("\n");
			return false;
		}
	}
}
