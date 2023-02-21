using System.Collections.Generic;

namespace EnergySuite
{
	public static class EnergySuiteConfig
	{
		public static Dictionary<TimeValue, TimeBasedValue> StoredInfo = new Dictionary<TimeValue, TimeBasedValue>()
		{
			//Examples
			{TimeValue.Energy, new TimeBasedValue(TimeValue.Energy, 5, 0, 12)}
			
		};

		//Change this values once and never again
		public const string Password = "4dm1N2010";
		public const string PasswordSalt = "4dm1n20104dm1n2010";

		//Dont touch this
		public const string AmountPrefixKey = "Amount_";
		public const string LastTimeAddedPrefixKey = "LastTimeAdded_";
	}
}
