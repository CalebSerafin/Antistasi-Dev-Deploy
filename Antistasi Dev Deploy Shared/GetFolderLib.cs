using System.Linq;

namespace Antistasi_Dev_Deploy_Shared {
	public class GetFolderLib {
		public static string GetFolder(string Path) {
			Path = Path.TrimEnd(new char[] { '\\' });
			string[] Folders = Path.Split('\\');
			return Folders[Folders.Count() - 1];
		}
	}
}
