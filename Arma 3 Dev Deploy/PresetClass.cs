using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Arma_3_Dev_Deploy.CompileTimeValues;

namespace Arma_3_Dev_Deploy
{
	class ConfigOveridesClass
	{
		public string TemplateSelected { get; set; }
		public string TemplateIgnoredFiles { get; set; }
		public ConfigOveridesClass()
		{
			TemplateSelected = "";
			TemplateIgnoredFiles = "";
		}
	}
	class PresetClass
	{
		public static string Name;
		ConfigOveridesClass ConfigOverides;
		public PresetClass(string NameX)
		{
			Name = "null";
			ConfigOverides = new ConfigOveridesClass();
		}
		public PresetClass(string NameX, ConfigOveridesClass ConfigOveridesX)
		{ 
			Name = "null";
			ConfigOverides = ConfigOveridesX;
		}
	}
}
