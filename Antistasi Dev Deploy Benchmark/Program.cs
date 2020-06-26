using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static Antistasi_Dev_Deploy_Benchmark.UserInput;

namespace Antistasi_Dev_Deploy_Benchmark {
	class Program {
		[STAThread]
		static void Main(string[] args) {

			string LastPath = GetPath();
			string HelpText = PingADDHelpText(LastPath);
		StartAgain:
			Console.Clear();
			string Args = GetArgs(HelpText);

			Process ADD = new Process();
			ADD.StartInfo.FileName = LastPath;
			ADD.StartInfo.Arguments = Args;
			Stopwatch ADDTimer = new Stopwatch();

			ADDTimer.Start();
			ADD.Start();
			ADD.WaitForExit();
			ADDTimer.Stop();

			Console.WriteLine(string.Join(Environment.NewLine,
				"Operation Done!",
				"Total time (D:H:S:MS) " + ADDTimer.Elapsed.ToString(),
				"Press \"Q\" to quit."
			));
			if (Console.ReadKey().KeyChar.ToString().ToLower() == "q") return;
			goto StartAgain;
		}
	}
}
