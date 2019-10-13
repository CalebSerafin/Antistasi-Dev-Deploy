using System;
using System.Collections.Generic;
using System.IO;
using static Antistasi_Dev_Deploy.CompileTimeValues;

namespace Antistasi_Dev_Deploy
{
	class ConfigFiles
	{
		public static void CreateConfigFile(string Name, string IgnoreFiles_RoadsDB = "")
		{
			Dictionary<string, string> NewFile = new Dictionary<string, string>
			{
				{
				CompileTimeValue.TIgnoreFiles,
@"description.ext\
mission.sqm\
mission_IFAOnly.sqm\
PIC.jpg\
ak.jpg\
" + IgnoreFiles_RoadsDB
				}
			};
		
			File.WriteAllText(Name, NewFile[Name]);
		}
		public static void DeleteConfigFile(string Name)
		{
			if (File.Exists(Name))
			{
				File.Delete(Name);
			}
		}
	}
}
