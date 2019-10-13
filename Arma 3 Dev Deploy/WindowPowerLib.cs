using System;
using System.Runtime.InteropServices;

namespace Arma_3_Dev_Deploy
{
	class WindowPowerLib
	{
		public class WindowPower
		{
			[DllImport("kernel32.dll")]
			public static extern IntPtr GetConsoleWindow();

			[DllImport("user32.dll")]
			public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

			public const int SW_HIDE = 0;
			public const int SW_SHOW = 5;


			//ShowWindow(WindowPowerHandle, SW_HIDE);
			//ShowWindow(WindowPowerHandle, SW_SHOW);
		}
	}
}
