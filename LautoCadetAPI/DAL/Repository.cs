using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPI.DAL
{
	internal class Repository<T> : IRepository<T> where T : new()
	{
		private T dataObject;
		private string path;
		private FileStream fileStream;

		public string Path { get { return path; } }

		public Repository(string filePath)
		{
			ChangePath(filePath);
		}

		public Repository(string filePath, bool doOverride)
		{
			ChangePath(filePath, doOverride);
		}

		public T GetData()
		{
			fileStream.Position = 0;
			StreamReader reader = new StreamReader(fileStream);
			dataObject = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());

			return dataObject;
		}

		public bool IsFileContentValid()
		{
			bool isValid = false;
			try
			{
				T data = GetData();
				isValid = data != null;
			}
			catch
			{
				isValid = false;
			}
			return isValid;
		}

		public void Save()
		{
			fileStream.Position = 0;
			fileStream.SetLength(0);
			StreamWriter writer = new StreamWriter(fileStream);
			writer.Write(JsonConvert.SerializeObject(dataObject, Formatting.Indented));
			writer.Flush();
		}

		public void Reset()
		{
			dataObject = new T();
			Save();
		}

		public void ChangePath(string newPath)
		{
			ChangePath(newPath, false);
		}

		public void ChangePath(string newPath, bool doOverride)
		{
			path = System.IO.Path.GetFullPath(newPath);

			if(fileStream != null)
			{
				fileStream.Close();
				fileStream.Dispose();
				fileStream = null;
			}

			if (File.Exists(path))
			{
				fileStream = File.Open(path, FileMode.OpenOrCreate);
				if (!doOverride && !IsFileContentValid())
				{
					this.Dispose();
					throw new FileFormatException();
				}
				else if(doOverride && dataObject == null)
				{
					dataObject = new T();
				}

				Save();
			}
			else
			{
				string saveDirectory = System.IO.Path.GetDirectoryName(path);
				if (!Directory.Exists(saveDirectory))
					Directory.CreateDirectory(saveDirectory);

				if (dataObject == null)
					dataObject = new T();

				fileStream = File.Open(path, FileMode.OpenOrCreate);
				Save();
			}
		}

		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					fileStream.Close();
					fileStream.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
