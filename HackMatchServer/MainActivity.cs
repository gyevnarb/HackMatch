using System;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Threading;
using HackMatch;
using System.IO;
using System.Text;

namespace HackMatchServer
{
	class MainActivity
	{

		static SqlConnection connection;

		static void Main(string[] args)
		{
			const string connString = "Data Source=hackup-massive-garbage.c1dtjcewzlpj.eu-west-2.rds.amazonaws.com,1433;User Id=Murto;";
			connection = new SqlConnection(connString);
			connection.Open();
			TcpListener listener = TcpListener.Create(6969);
			listener.Start();
			Console.Out.WriteLine("Listening for incoming connections...");
			do
			{
				TcpClient client = listener.AcceptTcpClient();
				Console.Out.WriteLine("Client accepted, starting job in background...");
				Thread job = new Thread(() => HandleClient(client));
				job.Start();
			} while (true);	//	No condition to stop.
		}

		static void HandleClient(TcpClient client)
		{
			Console.Out.WriteLine("Job Started.");
			NetworkStream input = client.GetStream();
			int flag = input.ReadByte();
			try
			{
				switch (flag)
				{
					case 0x01:
						input.WriteByte(0x01);
						Console.Out.WriteLine("Creating profile...");
						CreateProfile(ref input);
						break;
					case 0x02:
						input.WriteByte(0x01);
						Console.Out.WriteLine("<EditProfile>");
						EditProfile(ref input);
						break;
					case 0x03:
						input.WriteByte(0x01);
						Console.Out.WriteLine("<LoadProfile>");
						LoadProfile(ref input);
						break;
					case 0x04:
						Int32 result = 99;
						input.Write(BitConverter.GetBytes(result), 0, sizeof(Int32));
						Console.Out.WriteLine("<CalculateScore>");
						CalculateScore(ref input);
						break;
					default:
						input.WriteByte(0x00);
						Console.Out.WriteLine("<Whoops>");
						HandleInvalidInput(ref input);
						break;
				}
			}
			catch (Exception ex)
			{
				input.Flush();
				HandleInvalidInput(ref input);
			}
			input.Close();
			client.Close();
			Console.Out.WriteLine("Job completed.");
		}

		//	TODO: Sanitize input


		static void CreateProfile(ref NetworkStream input)
		{
			DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));
			User profile = (User)json.ReadObject(input);
			string username = profile.UserName;
			MemoryStream transaction = new MemoryStream(Encoding.ASCII.GetBytes("INSERT INTO Profiles VALUES (\"" + username + "\",\""));
			json.WriteObject(transaction, profile);
			string str = transaction.ToString() + "\");";

			//	Temporary:
			input.WriteByte(0x01);
		}

		static void EditProfile(ref NetworkStream input)
		{
			DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));
			User profile = (User)json.ReadObject(input);
			string username = profile.UserName;
			MemoryStream transaction = new MemoryStream(Encoding.ASCII.GetBytes("INSERT INTO Profiles VALUES (\"" + username + "\",\""));
			json.WriteObject(transaction, profile);
			string str = transaction.ToString() + "\");";
			//	TODO: Run transaction

			//	Temporary:
			input.WriteByte(0x01);
		}

		static void LoadProfile(ref NetworkStream input)
		{
			DataContractJsonSerializer str = new DataContractJsonSerializer(typeof(string));
			string username = (string)str.ReadObject(input);
			string transaction = "SELECT Username, Information WHERE Username = \"" + username + "\";";
			//	Get Json somehow and shove it in "input"
			//json.WriteData(input);
			//	TODO: Run transaction

			//	Temporary:
			User sinan = new User
			{
				UserName = "Dat Boi",
				FirstNames = "Sinan",
				LastNames = "Vanli",
				Bio = "Dank"
			};
			sinan.Technologies.Add("Java", ExperienceLevel.Experienced);
			sinan.Technologies.Add("Haskell", ExperienceLevel.Learner);
			sinan.Technologies.Add("MIPS", ExperienceLevel.Beginner);
			sinan.SpokenLanguages.Add("English");
			sinan.SpokenLanguages.Add("Turkish");
			DataContractJsonSerializer json = new DataContractJsonSerializer(sinan.GetType());
			input.WriteByte(0x01);
			json.WriteObject(input, sinan);
		}

		//	TODO: Ensure usernames cannot contain null characters
		static void CalculateScore(ref NetworkStream input)
		{
			DataContractJsonSerializer str = new DataContractJsonSerializer(typeof(string));
			string user1 = (string)str.ReadObject(input);
			string user2 = (string)str.ReadObject(input);
			string transaction = "SELECT Username, Information WHERE Username = \"" + user1 + "\" OR \"" + user2 + "\";";
			//	TODO: Run transaction

			//	Temporary:
			input.WriteByte(0x01);  //	Success
			DataContractJsonSerializer num = new DataContractJsonSerializer(typeof(Int32));
			num.WriteObject(input, (Int32)10);	//Return score of 10
		}

		static void HandleInvalidInput(ref NetworkStream input)
		{
			input.Flush();
			input.WriteByte(0x00);
		}
	}
}
