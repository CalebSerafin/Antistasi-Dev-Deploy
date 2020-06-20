using System;
using System.Diagnostics;

namespace Antistasi_Dev_Deploy_Shared {
	class ExternalExe {
		public static string Exec(string FileName, string Args, Action<string> ShowMessage) {
			Process process = new Process();
			string displayText = string.Empty;
			try {
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.RedirectStandardInput = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = false;
				process.StartInfo.FileName = FileName;
				process.StartInfo.Arguments = Args;
				bool processStarted = process.Start();
				if (processStarted) {
					string outputReader = process.StandardOutput.ReadToEnd();
					string errorReader = process.StandardError.ReadToEnd();
					process.WaitForExit();
					displayText += outputReader;
					displayText += errorReader;
				} else {
					ShowMessage("ERROR: " + FileName + " failed to start.");
				}
			} catch (Exception ExecError) {
				ShowMessage(ExecError.Message);
			} finally {
				if (process.StandardOutput != null) {
					process.StandardOutput.Close();
				}
				if (process.StandardError != null) {
					process.StandardError.Close();
				}
				process.Close();
			}
			return displayText;
		}
	}
}
