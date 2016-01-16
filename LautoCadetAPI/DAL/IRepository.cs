using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DAL
{
	internal interface IRepository<T> : IDisposable
	{
		/// <summary>
		/// Get the current path from which the object is loaded or saved
		/// </summary>
		string Path { get; }

		/// <summary>
		/// Load the object from the file
		/// </summary>
		T GetData();

		/// <summary>
		/// Saves any changes made to the object
		/// </summary>
		void Save();

		/// <summary>
		/// Set the path to a new location and saves the object at this location
		/// </summary>
		/// <param name="newPath">Location of the new save file</param>
		void ChangePath(string newPath);

		/// <summary>
		/// Delete the current object and create a new empty one
		/// </summary>
		void Reset();

	}
}
