using System.Reflection;

namespace Antistasi_Dev_Deploy_Shared {
	public class ProgramValues {
		public class CompileTimeValue {
			public static string AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if ADD
#if DEBUG
			public const bool Debug_Log = true;
			public static bool Debug_OpenOutput { get; set; } = true;
			public const bool Debug_HideWindow = false;
#else
			public const bool Debug_Log = false;
			public static bool Debug_OpenOutput { get; set; } = false;
			public const bool Debug_HideWindow = true;
#endif
			public const string MTVersion = "4"; // template.ignoreFiles.cfg
#endif
		}
		public class Reg {
			public const string Key_BI = @"HKEY_CURRENT_USER\Software\Bohemia Interactive";
			public const string Key_Arma = Key_BI + @"\Arma 3";
			public const string Value_Arma_PlayerName_Name = @"Player Name";
			public const string Key_FileBank = Key_BI + @"\FileBank";
			public const string Value_FileBank_Path = @"path";
			public const string Value_FileBank_EXE = @"EXE";

			public const string Key_CalebSerafin = @"HKEY_CURRENT_USER\Software\Caleb Serafin";
			public const string Key_A3DD = Key_CalebSerafin + @"\Arma 3 Dev Deploy";
			public const string Key_A3DD_ADD = Key_A3DD + @"\Antistasi Dev Deploy";
			public const string Value_ADD_LastPath = @"ADD Last Path";

			public const string Value_ADD_OverrideOutput = @"Override Output";
			public const string Value_ADD_OverrideOutputFolder = @"Override Output Folder";
			public const string Value_ADD_OverrideSource = @"Override Source";
			public const string Value_ADD_OverrideSourceFolder = @"Override Source Folder";
			public const string Value_ADD_ForceFilter = @"Force Map-Templates Filter";
			public const string Value_ADD_FilterList = @"Map-Templates Filter List";
			public const string Value_ADD_ForceOpenOutput = @"Force Open Output";
			public const string Value_ADD_PBOForce = @"Force PBO Files";
#if ADDC
			public static string RemoveCurrentUser(string Input) {
				return Input.Remove(0, 18);
			}
#endif
		}
	}
}