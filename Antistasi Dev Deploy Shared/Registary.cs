using Microsoft.Win32;
using System;
using static Antistasi_Dev_Deploy_Shared.ProgramValues;

namespace Antistasi_Dev_Deploy_Shared {
	class Registary {
		public static bool BoolBin(int? Input) => Input.HasValue && Input > 0;
		public static dynamic Fetch(string Key, string Name, dynamic Defualt) {
			//When a registry tree doesn't exist, registary.GetValue instead throws an error rather than returning null.
			try {
				return Registry.GetValue(Key, Name, Defualt) ?? Defualt;
			} catch (Exception e) {
				switch (e.GetType().Name) {
					case "NullReferenceException": return Defualt;
					default: throw;
						//If there is an error other than the registry tree not existing it should be thrown.
				}
			}
		}
		public static dynamic FetchA3DD(string Name, dynamic Defualt) {
			return Fetch(Reg.Key_A3DD_ADD, Name, Defualt);
		}
		public static dynamic FetchArma(string Name, dynamic Defualt) {
			return Fetch(Reg.Key_Arma, Name, Defualt);
		}
		public static string FileBankPath () {
			string Path = Fetch(Reg.Key_FileBank, Reg.FileBank_Path, string.Empty);
			string ExeName = Fetch(Reg.Key_FileBank, Reg.FileBank_EXE, string.Empty);
			if (string.IsNullOrEmpty(Path) || string.IsNullOrEmpty(ExeName)) {
				return "";
			};
			return Path + @"\" + ExeName;
		}
		public static bool HasFileBank = !string.IsNullOrEmpty(FileBankPath());
	}
}
