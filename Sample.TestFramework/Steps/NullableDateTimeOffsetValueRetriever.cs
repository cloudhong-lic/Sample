using System;
using System.Collections.Generic;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace Sample.TestFramework.Steps
{
	/// <summary>
	/// 在这里重写NullableDateTimeOffsetValueRetriever
	/// 因为SpecFlow提供的NullableDateTimeOffsetValueRetriever不好用
	/// 即使数据不是null, 也会强制将其设为null, 原因未知
	/// </summary>
	public class NullableDateTimeOffsetValueRetriever : IValueRetriever
	{

		private readonly Func<string, DateTimeOffset> dateTimeOffsetValueRetriever = v => new DateTimeOffsetValueRetriever().GetValue(v);

		public NullableDateTimeOffsetValueRetriever(Func<string, DateTimeOffset> dateTimeOffsetValueRetriever = null)
		{
			if (dateTimeOffsetValueRetriever != null)
				this.dateTimeOffsetValueRetriever = dateTimeOffsetValueRetriever;
		}

		public DateTimeOffset? GetValue(string value)
		{
			if (string.IsNullOrEmpty(value)) return null;
			return dateTimeOffsetValueRetriever(value);
		}

		public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
		{
			return GetValue(keyValuePair.Value);
		}

		public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
		{
			return propertyType == typeof(DateTimeOffset?);
		}
	}
}