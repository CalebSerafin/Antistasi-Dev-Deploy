using System.Collections.Generic;

namespace Antistasi_Dev_Deploy {
	public class ProgramValues {
		public class RuntimeTimeValue {
			public static string MissionVersion { get; } = Mission.GetVersion();
		}
	}
}
