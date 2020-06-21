using Microsoft.Win32;
using System;
using static Antistasi_Dev_Deploy_Shared.ProgramValues;

namespace Antistasi_Dev_Deploy_Configurator {
	class Registary {
		public static void EraseAll() {
			try {
				Registry.CurrentUser.DeleteSubKeyTree(Reg.RemoveHKeyCurrentUser(Reg.Key_A3DD_ADD));
			} catch (Exception exception) {
				switch (exception.GetType().Name) {
					case "ArgumentException": break;
					default: throw exception;
				}
			}
			try {
				Registry.CurrentUser.DeleteSubKey(Reg.RemoveHKeyCurrentUser(Reg.Key_A3DD)); ;
			} catch (Exception exception) {
				switch (exception.GetType().Name) {
					case "ArgumentException": break;
					default: throw exception;
				}
			}
			try {
				Registry.CurrentUser.DeleteSubKey(Reg.RemoveHKeyCurrentUser(Reg.Key_CalebSerafin));
			} catch (Exception exception) {
				switch (exception.GetType().Name) {
					case "ArgumentException": break;
					default: throw exception;
				}
			}
		}
	}
}
