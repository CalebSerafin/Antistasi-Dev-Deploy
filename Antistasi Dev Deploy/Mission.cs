using Antistasi_Dev_Deploy_Shared;
using System;
using System.IO;
using System.Xml;
using static Antistasi_Dev_Deploy.WindowPowerLib;

namespace Antistasi_Dev_Deploy {
	class Mission {
		public static string GetVersion() {
			string SourceDirectory = FolderOps.FindRepository(ProgramValues.RunTimeValue.AppFolder);
			string Path_StringTable = SourceDirectory + @"\A3-Antistasi\Stringtable.xml";
			string MissionVersion = "-2-X";

			if (File.Exists(Path_StringTable)) {
				string StringTableRaw = File.ReadAllText(Path_StringTable);
				XmlDocument StringTable = new XmlDocument();
				try {
					StringTable.LoadXml(StringTableRaw);
					XmlNamespaceManager XMLNamespace = new XmlNamespaceManager(StringTable.NameTable);
					XmlNodeList StringTable_Node = StringTable.SelectNodes("/Project/Package/Container/Key", XMLNamespace);
					foreach (XmlNode xNode in StringTable_Node) {
						if (xNode.Attributes[0].Value == "STR_antistasi_credits_generic_version_text") {
							MissionVersion = "-" + xNode.InnerText.Replace('.', '-');
						}
					}
				} catch (System.Xml.XmlException e) {
					ShowMessage(
						"Malformed Stringtable.xml! Please check the last commit made to Stringtable.xml.",
						e.Message,
						"Press any key to continue packing with version 2-X..."
					);
				};
			}
			return MissionVersion;
		}
	}
}
