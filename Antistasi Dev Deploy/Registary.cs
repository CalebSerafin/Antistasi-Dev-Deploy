using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using static Antistasi_Dev_Deploy.ProgramValues;

namespace Antistasi_Dev_Deploy
{
	class Registary
	{
		public static dynamic Fetch(string Key, string Name, dynamic Defualt)
		{
			//When a registry tree doesn't exist, registary.GetValue instead throws an error rather than returning null.
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
					//If there is an error other than the registary tree not existing it should be thrown.
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
	}
}
