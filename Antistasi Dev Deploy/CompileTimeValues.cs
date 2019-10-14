namespace Antistasi_Dev_Deploy
{
	public class CompileTimeValues
	{
		public class CompileTimeValue
		{
#if DEBUG
			public const bool Debug_Log = true;
			public const bool Debug_OpenOutput = true;
			public const bool Debug_HideWindow = false;
#else
			public const bool Debug_Log = false;
			public const bool Debug_OpenOutput = false;
			public const bool Debug_HideWindow = true;
#endif
			public const string Dir = ".\\A3DD\\"; // A3DD\
			public const int MTVersionI = 2;
			public const string MTVersionS = "2"; // template.ignoreFiles.cfg

			public const string Reg_Key_Arma = @"HKEY_CURRENT_USER\Software\Bohemia Interactive\Arma 3";
			public const string Reg_Value_Arma_PlayerName_Name = @"Player Name";

			public const string TSelected = "Template.Selected.cfg"; // template.selected.cfg
			public const string TIgnoreFiles = "Template.IgnoreFiles.cfg"; // template.ignoreFiles.cfg

			public const string VSCodeSettings = ".vscode\\settings.json"; // settings.json
		}
		public class Reg
		{
			public const string Key_Arma = @"HKEY_CURRENT_USER\Software\Bohemia Interactive\Arma 3";
			public const string Value_Arma_PlayerName_Name = @"Player Name";

			public const string Key_A3DD_ADD = @"HKEY_CURRENT_USER\Software\Caleb Serafin\Arma 3 Dev Deploy\Antistasi Dev Deploy";
			public const string Value_ADD_OverrideOutput_Name = @"Override Output";
			public const string Value_ADD_OverrideOutputFolder_Name = @"Override Output Folder";
		}
	}
}
