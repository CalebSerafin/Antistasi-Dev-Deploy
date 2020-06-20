using static Antistasi_Dev_Deploy_Shared.ExternalExe;

namespace Antistasi_Dev_Deploy_Configurator {
	class ExternalExe {
		public static void AntistasiDevDeploy(string Path) {
			string Arguments = "";
			Exec(Path, Arguments, (x) => { });
		}
	}
}
