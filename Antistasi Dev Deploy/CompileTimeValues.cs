//This is a mess, I'm sorry.

namespace Antistasi_Dev_Deploy
{
	public class ProgramValues
	{
		public class CompileTimeValue
		{
#if DEBUG
			public const bool Debug_Log = true;
			public static bool Debug_OpenOutput { get; set; } = true;
			public const bool Debug_HideWindow = false;
#else
			public const bool Debug_Log = false;
			public static bool Debug_OpenOutput { get; set; } = false;
			public const bool Debug_HideWindow = true;
#endif
			public const int MTVersionI = 3;
			public const string MTVersionS = "3"; // template.ignoreFiles.cfg
		}
		public class RuntimeTimeValue
		{
			public static string MissionVersion { get; } = Mission.GetVersion();
		}
		public class Reg
		{
			public const string Key_Arma = @"HKEY_CURRENT_USER\Software\Bohemia Interactive\Arma 3";
			public const string Value_Arma_PlayerName_Name = @"Player Name";

			public const string Key_A3DD_ADD = @"HKEY_CURRENT_USER\Software\Caleb Serafin\Arma 3 Dev Deploy\Antistasi Dev Deploy";
			public const string Value_ADD_OverrideOutput_Name = @"Override Output";
			public const string Value_ADD_OverrideOutputFolder_Name = @"Override Output Folder";
			public const string Value_ADD_ForceOpenOutput_Name = @"Force Open Output";
		}
	}
}
