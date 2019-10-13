using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antistasi_Dev_Deploy
{
	class GetFolderLib
	{
		public static string GetFolder(string Path)
		{
			string[] Folders = Path.Split('\\');
			return Folders[Folders.Count() - 1];
		}
	}
}
