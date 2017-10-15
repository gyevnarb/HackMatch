using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace HackMatchServer.Database
{
	class DatabaseController<T>
	{
		public string Path { get; set; }

		public DatabaseController(string filepath)
		{
			Path = filepath;
		}

		public void Update(T data)
		{
			DataContractJsonSerializer dbser = new DataContractJsonSerializer(typeof(T));
			FileStream dbout = new FileStream(Path, FileMode.OpenOrCreate);
			dbser.WriteObject(dbout, data);
			dbout.Close();
			Console.Out.WriteLine("Users updated.");
		}

		public T Load()
		{
			DataContractJsonSerializer dbser = new DataContractJsonSerializer(typeof(T));
			FileStream dbout = new FileStream("users.json", FileMode.Open);
			T data = (T)dbser.ReadObject(dbout);
			dbout.Close();
			return data;
		}

	}
}
