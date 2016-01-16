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
	public class TestObject
	{
		public string Name { get; set; }

		public int Number { get; set; }
	}
}
