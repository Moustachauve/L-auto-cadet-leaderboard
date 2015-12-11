﻿using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LautoCadetAPI.DAL
{
	public class Service
	{
		#region Singleton

		private static Service instance;
		public static Service Instance
		{
			get
			{
				if (instance == null)
					instance = new Service();
				return instance;
			}
		}

		private Service()
		{ }

		#endregion

		private Repository repo = new Repository();
		private EscadronConfiguration escadron;

		public void Save()
		{
			repo.Save();
		}

		public void Reload()
		{
			repo.Load();
			escadron = repo.EscadronConfiguration;
		}

		public IEnumerable<Section> GetAllSections()
		{
			return repo.GetAllSections();
		}

		public Section AddSection(string name)
		{
			Reload();
			Section section = new Section();
			section.Nom = name;
			section.SectionID = escadron.GetNextSectionID();

			escadron.Sections.Add(section);
			Save();

			return section;
		}

		public IEnumerable<Cadet> GetAllCadets()
		{
			return repo.GetAllCadets();
		}

		public IEnumerable<Cadet> GetTopTen()
		{
			Reload();
			return repo.GetAllCadets().OrderByDescending(c => c.NbBilletsVendu).Take(10);
		}

		public Cadet AddCadet(Cadet cadet)
		{
			Reload();
			Section section = escadron.Sections.FirstOrDefault();

            cadet.CadetID = escadron.GetNextCadetID();

			section.Cadets.Add(cadet);
            Save();
			return cadet;
        }

		public bool DeleteCadet(int cadetID)
		{
			return repo.DeleteCadet(cadetID);
		}
	}
}
