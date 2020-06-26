using System;
using System.Runtime.InteropServices;
using static Antistasi_Dev_Deploy_Shared.ProgramValues;

namespace Antistasi_Dev_Deploy {
	class WindowPowerLib {
		public class WindowPower {
			[DllImport("kernel32.dll")]
			public static extern IntPtr GetConsoleWindow();

			[DllImport("user32.dll")]
			public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

			public const int SW_HIDE = 0;
			public const int SW_SHOW = 5;

			// WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_HIDE);
			// WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_SHOW);
		}
		public static bool YesToAll { get; set; } = false;
		public static bool WindowHidden { get; set; } = false;
		public static void ShowMessage(params string[] Message) {
			if (WindowHidden) WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_SHOW);
			Console.WriteLine(string.Join(Environment.NewLine,Message));
			if (!YesToAll) {
				Console.ReadKey();
				Console.WriteLine("");
			};
			if (WindowHidden) WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_HIDE);
		}
	}
}
