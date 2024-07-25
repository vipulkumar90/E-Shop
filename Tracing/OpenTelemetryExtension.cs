using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Instrumentation.AspNetCore;

namespace Tracing
{
	public static class OpenTelemetryExtension
	{
		public static IServiceCollection AddOpenTelemetryTracing(this IServiceCollection services, IConfiguration configuration)
		{
			
			Action<ResourceBuilder> configureResource = r => r.AddService(
			serviceName: configuration.GetValue("OpenTelemetry:ServiceName", defaultValue: "unknown")!,
			serviceVersion: configuration.GetValue("OpenTelemetry:ServiceVersion", defaultValue: "unknown")!,
			serviceInstanceId: Environment.MachineName);

			services.AddOpenTelemetry()
				.ConfigureResource(configureResource)
				.WithTracing(builder =>
				{
					builder
						.AddHttpClientInstrumentation()
						.AddAspNetCoreInstrumentation(o =>
						{
							// to trace only api requests
							o.Filter = (context) => !string.IsNullOrEmpty(context.Request.Path.Value) && context.Request.Path.Value.Contains("Api", StringComparison.InvariantCulture);

							// example: only collect telemetry about HTTP GET requests
							// return httpContext.Request.Method.Equals("GET");

							// enrich activity with http request and response
							o.EnrichWithHttpRequest = (activity, httpRequest) => { activity.SetTag("requestProtocol", httpRequest.Protocol); };
							o.EnrichWithHttpResponse = (activity, httpResponse) => { activity.SetTag("responseLength", httpResponse.ContentLength); };

							// automatically sets Activity Status to Error if an unhandled exception is thrown
							o.RecordException = true;
							o.EnrichWithException = (activity, exception) =>
							{
								activity.SetTag("exceptionType", exception.GetType().ToString());
								activity.SetTag("stackTrace", exception.StackTrace);
							};
						});

					services.Configure<AspNetCoreTraceInstrumentationOptions>(configuration.GetSection("AspNetCoreInstrumentation"));
					builder.AddOtlpExporter(otlpOptions =>
					{
						// Use IConfiguration directly for Otlp exporter endpoint option.
						otlpOptions.Endpoint = new Uri(configuration.GetValue("Otlp:Endpoint", defaultValue: "http://otel:4317")!);
					});
				});
			return services;
		}
	}
}

