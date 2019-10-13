using System;
using System.Diagnostics;

namespace Arma_3_Dev_Deploy
{
	class XCopyLib
	{
		public static void XCopy(string Source, string Destination, string Flags, string Args)
		{
			bool ArgFlag(string Flag)
			{
				return Args.ToLower().Contains(Flag.ToLower());
			}
			bool Quiet;
			Quiet = ArgFlag("/Q");
			Flags += ArgFlag("/R") ? "" : " /D";
			Process process = new Process();

			try
			{
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.RedirectStandardInput = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = false;
				process.StartInfo.FileName = "XCopy";
				process.StartInfo.Arguments = "\"" + Source + "\" \"" + Destination + "\" " + Flags;
				//Console.WriteLine("Arguments: {" + process.StartInfo.Arguments + "}"); //For debugging purposes
				bool processStarted = process.Start();
				if (processStarted)
				{
					string outputReader = process.StandardOutput.ReadToEnd();
					string errorReader = process.StandardError.ReadToEnd();
					process.WaitForExit();

					//Display the result
					string displayText = "Output" + Environment.NewLine + "==============" + Environment.NewLine;
					displayText += outputReader;
					if (errorReader != "")
					{
						displayText += Environment.NewLine + "Error" + Environment.NewLine + "==============" + Environment.NewLine;
						displayText += errorReader;
						if (errorReader == "Invalid number of parameters\r\n") { displayText += process.StartInfo.Arguments; };
					};
					if (!Quiet) Console.WriteLine(displayText);
				}
				else
				{
					if (!Quiet) Console.WriteLine("ERROR: Process has not started");
				}
			}
			catch (Exception XCopyError)
			{
				if (!Quiet) Console.WriteLine(XCopyError.Message);
			}
			finally
			{
				if (process.StandardOutput != null)
				{
					process.StandardOutput.Close();
				}
				if (process.StandardError != null)
				{
					process.StandardError.Close();
				}
				process.Close();
			}
		}
	}
}
