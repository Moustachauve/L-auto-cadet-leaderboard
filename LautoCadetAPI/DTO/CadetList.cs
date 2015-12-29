using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DTO
{
	public class CadetList : List<CadetListItem>
	{

		public CadetList() : base()
		{ }

		public CadetList(IEnumerable<Cadet> cadetList) : base()
		{
			foreach (Cadet cadet in cadetList)
			{
				Add(new CadetListItem(cadet));
			}
		}
	}
}
