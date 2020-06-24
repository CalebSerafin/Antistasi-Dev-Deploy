using System.IO;
using System.Xml;
using static Antistasi_Dev_Deploy.WindowPowerLib;

namespace Antistasi_Dev_Deploy {
	class Mission {
		public static string GetVersion(string SourceDirectory) {
			string Path_Stringtable = SourceDirectory + @"\A3-Antistasi\Stringtable.xml";
			string MissionVersion = "-2-X";
			const string PackWith2X = "Press any key to pack with version 2-X...";

			if (File.Exists(Path_Stringtable)) {
				string StringtableRaw = File.ReadAllText(Path_Stringtable);
				XmlDocument Stringtable = new XmlDocument();
				try {
					Stringtable.LoadXml(StringtableRaw);
					XmlNamespaceManager XMLNamespace = new XmlNamespaceManager(Stringtable.NameTable);
					XmlNodeList Stringtable_Node = Stringtable.SelectNodes("/Project/Package/Container/Key", XMLNamespace);
					foreach (XmlNode xNode in Stringtable_Node) {
						if (xNode.Attributes[0].Value == "STR_antistasi_credits_generic_version_text") {
							MissionVersion = "-" + xNode.InnerText.Replace('.', '-');
						}
					}
				} catch (System.Xml.XmlException e) {
					ShowMessage(
						"Malformed Stringtable! Check the last commit made to Stringtable.xml.",
						e.Message,
						PackWith2X
					);
				};
			} else {
				ShowMessage("Stringtable not found!", PackWith2X);
			}
			return MissionVersion;
		}
	}
}
