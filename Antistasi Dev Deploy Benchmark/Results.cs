using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Antistasi_Dev_Deploy_Shared.Registary;

namespace Antistasi_Dev_Deploy_Benchmark {
	class Results {
		public static string GetVersion(string FileName) {
			return FileVersionInfo.GetVersionInfo(FileName).FileVersion;
		}
		public static string GetCPU() {
			return Fetch(@"Computer\HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\0", @"ProcessorNameString", "CPU NOT FOUND");
		}
		public static double GetOutMB() {
			return 0;
		}
		public static double GetOutCount() {
			return 1;
		}
		public static List<string> CompileResults() {
			return new List<string>();
		}
	}
}
