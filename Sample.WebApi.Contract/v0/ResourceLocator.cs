using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Sample.WebApi.Contract.v0
{
	public static class ResourceLocator
	{
		public const string ServiceName = "Sample";
		public const string Version = "v0";

		public static class Animals
		{
			public static string ResourceName = $"{ServiceName}.Animals";
			public static string BaseUri = $"{Version}/Animals";

			public static string Get(int animalKey)
			{
				//var location = $"{ServiceName}/{BaseUri}/{animalKey}";
				//TODO: 目前还不知道如何将ServiceName作为路径, 这个和运行IISExpress以及applicationhost.config有关
				var location = $"{BaseUri}/{animalKey}";
				return location.ToLowerInvariant();
			}
		}

		/// <summary>
		/// 处理日期类型参数
		/// 也许这个应该放到某个Library中去
		/// TODO: 增加一些和日期有关的WebAPI进去
		/// </summary>
		private static class UriHelper
		{
			public static string GetUriDateParameters(DateTimeOffset? start, DateTimeOffset? end, string uri, List<string> extraParameters = null)
			{
				var parameters = extraParameters ?? new List<string>();
				var isStartNull = start == null;
				if (!isStartNull)
					parameters.Add($"start={WebUtility.UrlEncode(start.Value.ToString("o"))}");
				if (end != null)
					parameters.Add($"end={WebUtility.UrlEncode(end.Value.ToString("o"))}");

				if (parameters.Any())
					return uri.ToLowerInvariant() + "?" + string.Join("&", parameters);
				else
					return uri.ToLowerInvariant();
			}
		}
	}
}