namespace Antistasi_Dev_Deploy_Configurator
{
	public class CompileTimeValues
	{
		public class CompileTimeValue
		{
			public const int MTVersionI = 3;
			public const string MTVersionS = "3"; // template.ignoreFiles.cfg
		}
		public class Reg
		{
			public static string RemoveCU(string Input)
			{
				return Input.Remove(0, 18);
			}

			public const string Key_Arma = @"HKEY_CURRENT_USER\Software\Bohemia Interactive\Arma 3";
			public const string Value_Arma_PlayerName_Name = @"Player Name";

			public const string Key_CalebSerafin = @"HKEY_CURRENT_USER\Software\Caleb Serafin";
			public const string Key_A3DD = @"HKEY_CURRENT_USER\Software\Caleb Serafin\Arma 3 Dev Deploy";
			public const string Key_A3DD_ADD = @"HKEY_CURRENT_USER\Software\Caleb Serafin\Arma 3 Dev Deploy\Antistasi Dev Deploy";
			public const string Value_ADD_OverrideOutput_Name = @"Override Output";
			public const string Value_ADD_OverrideOutputFolder_Name = @"Override Output Folder";
			public const string Value_ADD_ForceOpenOutput_Name = @"Force Open Output";
		}
	}
}
