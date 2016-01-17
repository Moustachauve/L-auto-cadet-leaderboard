using LautoCadetAPI.DTO;
using LautoCadetAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DAL
{
	internal interface IService : IDisposable
	{

		#region Cadet

		Cadet CadetGetByID(int id);

		IEnumerable<Cadet> CadetGetAll();

		Cadet CadetAdd(CadetListItem cadetModel);

		Cadet CadetEdit(CadetListItem cadetEdit);

		void CadetDelete(int cadetID);

		#endregion

		#region Section

		Section SectionGetByID(int id);

		List<Section> SectionGetAll();

		Section SectionAdd(string name);

		Section SectionEdit(SectionListItem sectionModel);

		void SectionDelete(int sectionID);

		#endregion

		#region Grade

		Grade GradeGetByID(int gradeId);

		List<Grade> GradeGetAll();

		Grade GradeAdd(GradeListItem gradeModel);

		Grade GradeEdit(GradeListItem gradeModel);

		List<Grade> GradeEditOrder(GradeList gradeList);

		void GradeDelete(int gradeID);

		#endregion

		#region Leaderboard

		IEnumerable<Cadet> LeaderboardGetTopTenSeller();

		SectionLeaderboard LeaderboardGetSection();

		#endregion

		#region File

		Settings SettingsGet();

		void SettingsUpdate(Settings settings);

		void FileOpen(string path);

		void FileCreate(string path, string saveName = null);

		List<FichierRecent> FileGetRecentlyOpened();

		#endregion

		void Save();

	}
}
