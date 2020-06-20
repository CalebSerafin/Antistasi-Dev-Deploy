using System;
using static Antistasi_Dev_Deploy.WindowPowerLib;
using static Antistasi_Dev_Deploy_Shared.Registary;
using static Antistasi_Dev_Deploy_Shared.ExternalExe;

namespace Antistasi_Dev_Deploy {
	class ExternalExe {
		public static void XCopy(string Source, string Destination, string Flags) {
			string FileName = "XCopy";
			string Arguments = "\"" + Source + "\" \"" + Destination + "\" " + Flags;
			Exec(FileName, Arguments, (x) => ShowMessage(x));
		}
		public static void FileBank(string Target, string Flags = "") {
			string FileName = FileBankPath(); // https://community.bistudio.com/wiki/FileBank
			string Arguments = Flags + '\"' + Target + '\"';
			Exec(FileName, Arguments, (x) => ShowMessage(x));
		}
	}
}
