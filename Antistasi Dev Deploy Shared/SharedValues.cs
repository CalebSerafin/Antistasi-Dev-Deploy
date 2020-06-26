using System.IO;
using System.Reflection;

namespace Antistasi_Dev_Deploy_Shared {
	public class ProgramValues {
		public class RunTimeValue {
			public static string AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString() + " (Pre-release v4b5)";
			public static string AppPath = Assembly.GetEntryAssembly().Location;
			public static string AppFolder = Path.GetDirectoryName(AppPath);

		}
		public class CompileTimeValue {
		}
		public class Reg {
			public const string Key_BI = @"HKEY_CURRENT_USER\Software\Bohemia Interactive";
			public const string Key_Arma = Key_BI + @"\Arma 3";
			public const string Arma_PlayerName_Name = @"Player Name";
			public const string Key_FileBank = Key_BI + @"\FileBank";
			public const string FileBank_Path = @"path";
			public const string FileBank_EXE = @"EXE";

			public const string Key_CalebSerafin = @"HKEY_CURRENT_USER\Software\Caleb Serafin";
			public const string Key_A3DD = Key_CalebSerafin + @"\Arma 3 Dev Deploy";
			public const string Key_A3DD_ADD = Key_A3DD + @"\Antistasi Dev Deploy";
			public const string ADD_LastPath = @"ADD Last Path";

			public const string ADD_OverrideOutput = @"Override Output";
			public const string ADD_OverrideOutputFolder = @"Override Output Folder";
			public const string ADD_OverrideSource = @"Override Source";
			public const string ADD_OverrideSourceFolder = @"Override Source Folder";
			public const string ADD_ForceFilter = @"Force Map-Templates Filter";
			public const string ADD_FilterList = @"Map-Templates Filter List";
			public const string ADD_ForceOpenOutput = @"Force Open Output";
			public const string ADD_PBOForce = @"Force PBO Files";
#if ADDC
			public static string RemoveHKeyCurrentUser(string Input) {
				return Input.Remove(0, 18);
			}
#endif
		}
	}
}