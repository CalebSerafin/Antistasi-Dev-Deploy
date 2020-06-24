using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Antistasi_Dev_Deploy_Shared.GetFolderLib;

namespace Antistasi_Dev_Deploy {
	static class FolderOps {
		private static bool CheckIfRepository(string Folder) {
			return Directory.Exists(Folder + @"\A3-Antistasi") && Directory.Exists(Folder + @"\Map-Templates");
		}
		private static string UpFolder(string Folder) {
			return Path.GetFullPath(Folder + @"\..");
		}
		private static bool IsRoot(string Folder) {
			return Path.GetPathRoot(Folder) == Folder;
		}
		public static string FindRepository(string Folder) {
			Folder = Folder.TrimEnd('\\');
			if (CheckIfRepository(Folder + @"\A3-Antistasi")) return Folder + @"\A3-Antistasi";
			for (int i = 0; i < 5 && !IsRoot(Folder); i++) {
				if (CheckIfRepository(Folder)) return Folder;
				Folder = UpFolder(Folder);
			}
			return "";
		}
		public static void PackTemplate(string GenericCode, string TemplateFolder, string Destination, bool PBO) {
			ExternalExe.XCopy(GenericCode, Destination);
			ExternalExe.XCopy(TemplateFolder, Destination);
			if (PBO) {
				ExternalExe.FileBank(Destination);
			};
		}
	}
}
