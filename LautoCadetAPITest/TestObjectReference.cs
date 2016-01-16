using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPITest
{
	[JsonObject(IsReference = true)]
	[Serializable]
	public class TestObjectReference
	{
		public TestObjectReference Reference { get; set; }
		public int Value { get; set; }
	}
}
