using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracing
{
	public class OpenTelemetryParameters
	{
		public string ServiceName { get; set; }
		public string ServiceVersion { get; set; }
		public string ActivitySourceName { get; set; }
	}
}
