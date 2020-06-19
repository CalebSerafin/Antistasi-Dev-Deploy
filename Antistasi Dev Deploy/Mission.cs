﻿using System;
using System.IO;
using System.Xml;
using static Antistasi_Dev_Deploy.WindowPowerLib;

namespace Antistasi_Dev_Deploy {
	class Mission {
		public static string GetVersion() {
			string CurrentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			string Dir_AntistasiRoot = CurrentDirectory + @"\A3-Antistasi\";
			if (!Directory.Exists(CurrentDirectory + @"\A3-Antistasi")) {
				Dir_AntistasiRoot = CurrentDirectory + @"\..\A3-Antistasi\";
			}
			string Dir_StringTable = Dir_AntistasiRoot + "Stringtable.xml";

			string MissionVersion = "-2-X";

			if (File.Exists(Dir_AntistasiRoot + "Stringtable.xml")) {
				string StringTableRaw = File.ReadAllText(Dir_StringTable);
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
					WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_SHOW);
					Console.WriteLine("ERROR: Malformed stringtable.xml! Please check the last commit made to stringtable.xml.");
					Console.WriteLine("Packing missions with default version 2-X.");
					Console.WriteLine("Details:\n" + e.Message);
					Console.WriteLine("Press any key to continue packing...");
					Console.ReadKey();
				};
			}
			return MissionVersion;
		}
	}
}
