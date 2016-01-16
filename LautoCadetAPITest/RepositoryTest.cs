using LautoCadetAPI.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LautoCadetAPITest
{
	[TestClass]
	public class RepositoryTest
	{
		public const string TEST_DIRECTORY = @"testRepo\";

		[TestInitialize]
		public void Init()
		{
			Directory.CreateDirectory(TEST_DIRECTORY);
		}

		[TestCleanup]
		public void Cleanup()
		{
			Directory.Delete(TEST_DIRECTORY, true);
		}

		[TestMethod, TestCategory("Repository")]
		public void TestConstructor()
		{
			string path = TEST_DIRECTORY + "testConstructor.txt";
			using (Repository<TestObject> manager = new Repository<TestObject>(path))
			{
				Assert.IsTrue(File.Exists(path));
			}
		}

		[TestMethod, TestCategory("Repository")]
		public void TestConstructorInexistantFolder()
		{
			string path = TEST_DIRECTORY + @"newFolder\testConstructorInexistantFolder.txt";
			using (Repository<TestObject> manager = new Repository<TestObject>(path))
			{
				Assert.IsTrue(File.Exists(path));
			}
		}

		[TestMethod, TestCategory("Repository")]
		public void TestSaving()
		{
			string path = TEST_DIRECTORY + "testSaving.txt";
			string name = "Test";
			int number = 42;

			Repository<TestObject> manager = null;

			using (manager = new Repository<TestObject>(path))
			{
				TestObject data = manager.GetData();
				data.Name = name;
				data.Number = number;
				manager.Save();

				TestObject loadData1 = manager.GetData();
				Assert.AreEqual(name, loadData1.Name);
				Assert.AreEqual(number, loadData1.Number);
			}
			using (manager = new Repository<TestObject>(path))
			{
				TestObject loadData2 = manager.GetData();
				Assert.AreEqual(name, loadData2.Name);
				Assert.AreEqual(number, loadData2.Number);
			}
		}

		[TestMethod, TestCategory("Repository")]
		public void TestChangePath()
		{
			string name = "Test before";
			int number = 42;
			string newpath = TEST_DIRECTORY + "testChangePathNew.txt";

			using (Repository<TestObject> manager = new Repository<TestObject>(TEST_DIRECTORY + "testChangePath.txt"))
			{
				TestObject data = manager.GetData();
				data.Name = name;
				data.Number = number;
				manager.Save();

				manager.ChangePath(newpath);

				Assert.IsTrue(File.Exists(newpath));
				Assert.AreEqual(Path.GetFullPath(newpath), manager.Path);
				TestObject loadData = manager.GetData();
				Assert.AreEqual(name, loadData.Name);
				Assert.AreEqual(number, loadData.Number);
			}
			using (Repository<TestObject> manager = new Repository<TestObject>(newpath))
			{
				TestObject loadData = manager.GetData();
				Assert.AreEqual(name, loadData.Name);
				Assert.AreEqual(number, loadData.Number);
			}
		}

		[TestMethod, TestCategory("Repository")]
		[ExpectedException(typeof(FileFormatException))]
		public void TestInvalidFileThrows()
		{
			string path = TEST_DIRECTORY + "testInvalidFileThrows.txt";

			using (StreamWriter writer = new StreamWriter(path))
			{
				writer.Write("}!Invalid file!{");
			}

			using (Repository<TestObject> manager = new Repository<TestObject>(path))
			{
				Assert.Fail("FileFormatException expected");
			}
		}

		[TestMethod, TestCategory("Repository")]
		public void TestInvalidFileUntouched()
		{
			string path = TEST_DIRECTORY + "testInvalidFileUntouched.txt";
			string fileContent = "}!Invalid file!{";
			using (StreamWriter writer = new StreamWriter(path))
			{
				writer.Write(fileContent);
			}

			try
			{
				using (Repository<TestObject> manager = new Repository<TestObject>(path))
				{
					Assert.Fail("FileFormatException expected");
				}
			}
			catch (FileFormatException) { }

			using (StreamReader reader = new StreamReader(path))
			{
				Assert.AreEqual(fileContent, reader.ReadToEnd());
			}
		}

		[TestMethod, TestCategory("Repository")]
		public void TestInvalidFileOverride()
		{
			string path = TEST_DIRECTORY + "testInvalidFileOverride.txt";
			string fileContent = "}!Invalid file!{";
			using (StreamWriter writer = new StreamWriter(path))
			{
				writer.Write(fileContent);
			}

			using (Repository<TestObject> manager = new Repository<TestObject>(path, true)) {
				TestObject loadData = manager.GetData();
				loadData.Name = "Testing one two";
				loadData.Number = 42;
				manager.Save();
			}

			using (StreamReader reader = new StreamReader(path))
			{
				Assert.AreNotEqual(fileContent, reader.ReadToEnd());
			}
		}

		[TestMethod, TestCategory("Repository")]
		public void TestReset()
		{
			using (Repository<TestObject> manager = new Repository<TestObject>(TEST_DIRECTORY + "testReset.txt"))
			{
				string name = "Test before";
				int number = 42;
				TestObject data = manager.GetData();
				data.Name = name;
				data.Number = number;
				manager.Save();

				manager.Reset();
				TestObject loadData = manager.GetData();
				Assert.AreNotEqual(loadData.Name, name);
				Assert.AreNotEqual(loadData.Number, number);
			}
		}

		[TestMethod, TestCategory("Repository")]
		public void TestReference()
		{
			using (Repository<TestObjectReference> manager = new Repository<TestObjectReference>(TEST_DIRECTORY + "testReference.txt"))
			{
				int value1 = 1;
				int value2 = 2;

				TestObjectReference data1 = manager.GetData();
				TestObjectReference data2 = new TestObjectReference();

				data1.Reference = data2;
				data1.Value = value1;

				data2.Reference = data1;
				data2.Value = value2;

				manager.Save();
				TestObjectReference result = manager.GetData();

				Assert.AreEqual(value1, result.Value);
				Assert.AreEqual(value2, result.Reference.Value);
				Assert.AreEqual(value1, result.Reference.Reference.Value);
			}
		}
	}
}
