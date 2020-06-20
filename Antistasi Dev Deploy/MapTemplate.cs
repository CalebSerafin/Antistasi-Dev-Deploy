using System.Linq;

namespace Antistasi_Dev_Deploy {
	class MapHandling {
		public class MapTemplate {
			public string Name { get; set; }
			public string Map { get; set; }
			public string Dir { get; set; }

			public MapTemplate(string nameX_mapX) {
				string[] data = nameX_mapX.Split('.');
				Name = data[0];
				Map = data[1];
				Dir = Name + "." + Map;
			}
			public MapTemplate(string nameX, string mapX) {
				Name = nameX;
				Map = mapX;
				Dir = Name + "." + Map;
			}
			public MapTemplate(string[] nameX_mapX) {
				Name = string.Join(".",nameX_mapX.Take(nameX_mapX.Length-1));
				Map = nameX_mapX.Last();
				Dir = Name + "." + Map;
			}

			public void Dispose() {
				Name = null;
				Map = null;
				Dir = null;
			}
		}
	}
}