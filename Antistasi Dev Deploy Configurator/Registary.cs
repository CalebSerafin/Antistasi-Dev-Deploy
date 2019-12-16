using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using static Antistasi_Dev_Deploy_Configurator.CompileTimeValues;

namespace Antistasi_Dev_Deploy_Configurator
{
	class Registary
	{
		public static dynamic Fetch(string Key, string Name, dynamic Defualt)
		{
			try
			{
				dynamic RetrievedValue = Registry.GetValue(Key, Name, Defualt);
				return RetrievedValue is null ? Defualt : RetrievedValue;
			}
			catch (Exception e)
			{
				switch (e.GetType().Name)
				{
					case "NullReferenceException": return Defualt;
					default: throw e;
				}
			}
		}
		public static dynamic FetchA3DD(string Name, dynamic Defualt)
		{
			return Fetch(Reg.Key_A3DD_ADD, Name, Defualt);
		}
		public static dynamic FetchArma(string Name, dynamic Defualt)
		{
			return Fetch(Reg.Key_Arma, Name, Defualt);
		}

		public static void EraseAll()
		{
			try
			{
				Registry.CurrentUser.DeleteSubKeyTree(Reg.RemoveCU(Reg.Key_A3DD_ADD));
			}
			catch (Exception exception)
			{
				switch (exception.GetType().Name)
				{
					case "ArgumentException": break;
					default: throw exception;
				}
			}
			try
			{
				Registry.CurrentUser.DeleteSubKey(Reg.RemoveCU(Reg.Key_A3DD)); ;
			}
			catch (Exception exception)
			{
				switch (exception.GetType().Name)
				{
					case "ArgumentException": break;
					default: throw exception;
				}
			}
			try
			{
				Registry.CurrentUser.DeleteSubKey(Reg.RemoveCU(Reg.Key_CalebSerafin));
			}
			catch (Exception exception)
			{
				switch (exception.GetType().Name)
				{
					case "ArgumentException": break;
					default: throw exception;
				}
			}
		}
	}
}
