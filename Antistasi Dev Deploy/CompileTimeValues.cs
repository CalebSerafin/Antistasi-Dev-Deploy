namespace Antistasi_Dev_Deploy
{
	public class CompileTimeValues
	{
		public class CompileTimeValue
		{
			public static string Dir { get; } = ".\\A3DD\\"; // A3DD\
			public static int MTVersionI { get; } = 2;
			public static string MTVersionS { get; } = MTVersionI.ToString(); // template.ignoreFiles.cfg

			public static string Reg_Key_Arma { get; } = @"HKEY_CURRENT_USER\Software\Bohemia Interactive\Arma 3";
			public static string Reg_Value_Arma_PlayerName_Name { get; } = @"Player Name";

			public static string TSelected { get; } = "Template.Selected.cfg"; // template.selected.cfg
			public static string TIgnoreFiles { get; } = "Template.IgnoreFiles.cfg"; // template.ignoreFiles.cfg

			public static string VSCodeSettings { get; } = ".vscode\\settings.json"; // settings.json
		}
	}
}
