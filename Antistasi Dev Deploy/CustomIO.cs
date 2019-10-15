using System;
using static Antistasi_Dev_Deploy.ProgramValues;
using static Antistasi_Dev_Deploy.WindowPowerLib;

namespace Antistasi_Dev_Deploy
{
	class CustomIO
	{
		public static void WriteLine(string ConsoleText)
		{
			if (CompileTimeValue.Debug_Log)
			{
				Console.WriteLine(ConsoleText);
			}
		}
		public static char ReadKey()
		{
			if (CompileTimeValue.Debug_HideWindow)
			{
				WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_SHOW);
			}
			char KeyPressed = Console.ReadKey().KeyChar;
			if (CompileTimeValue.Debug_HideWindow)
			{
				WindowPower.ShowWindow(WindowPower.GetConsoleWindow(), WindowPower.SW_HIDE);
			}
			return KeyPressed;
		}
	}
}
