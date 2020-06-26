using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;
using static Antistasi_Dev_Deploy_Shared.Registary;
using static Antistasi_Dev_Deploy_Shared.ProgramValues;
using static Antistasi_Dev_Deploy_Shared.ExternalExe;
using System.IO;

namespace Antistasi_Dev_Deploy_Benchmark {
	class UserInput {
		public static string GetPath() {
			string LastPath = FetchA3DD(Reg.ADD_LastPath, "");
			string KeyPressed;
		CheckValid:
			if (File.Exists(LastPath)) {
				Console.WriteLine(string.Join(Environment.NewLine,
					"ADD was last executed at " + LastPath,
					"Is this correct? (Y)es (N)o"
				));
				KeyPressed = Console.ReadKey().KeyChar.ToString().ToLower();
				Console.WriteLine("");
				if (KeyPressed == "y") return LastPath;
			};

			Console.WriteLine("Please select your installation of \"Antistasi Dev Deploy.exe\".");
			CommonOpenFileDialog SelecteADDDialog = new CommonOpenFileDialog {
				InitialDirectory = Directory.GetParent(LastPath).FullName
			};
			if (SelecteADDDialog.ShowDialog() == CommonFileDialogResult.Ok) {
				LastPath = SelecteADDDialog.FileName;
			}
			SelecteADDDialog.Dispose();
			goto CheckValid;
		}
		public static string PingADDHelpText(string FileName) {
			string Arguments = "/y /h";
			string HelpText = "";
			HelpText += Exec(FileName, Arguments, (x) => HelpText += x);
			return HelpText;
		}
		public static string GetArgs(string HelpText) {
			Console.WriteLine(string.Join(Environment.NewLine,
				"Possible Args:",
				HelpText,
				"Enter any args you wish to run ADD with."
			));
			return Console.ReadLine();
		}
	}
}
