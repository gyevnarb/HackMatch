using System;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Threading;
using HackMatch;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace HackMatchServer
{
	class MainActivity
	{

		static Dictionary<string, User> users;

		static void Main(string[] args)
		{
			LoadUsers();
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
					case 0x05:
						Console.Out.WriteLine("<GetUsernames>");
						GetUsernames(ref input);
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
			if (users.ContainsKey(profile.Username))
			{
				input.WriteByte(0x00);
			}
			else
			{
				users[profile.Username] = profile;
				input.WriteByte(0x01);
				UpdateUsers();
			}
		}

		static void EditProfile(ref NetworkStream input)
		{
			DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));
			User profile = (User)json.ReadObject(input);
			if (users.ContainsKey(profile.Username))
			{
				users[profile.Username] = profile;
				input.WriteByte(0x01);
				UpdateUsers();
			}
			else
			{
				input.WriteByte(0x00);
			}
		}

		static void LoadProfile(ref NetworkStream input)
		{
			DataContractJsonSerializer str = new DataContractJsonSerializer(typeof(string));
			string username = (string)str.ReadObject(input);
			if (users.ContainsKey(username))
			{
				input.WriteByte(0x01);
				DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));
				json.WriteObject(input, users[username]);
			}
			else
			{
				input.WriteByte(0x00);
			}
		}

		//	TODO: Ensure usernames cannot contain null characters
		static void CalculateScore(ref NetworkStream input)
		{
			DataContractJsonSerializer str = new DataContractJsonSerializer(typeof(string));
			string user1 = (string)str.ReadObject(input);
			string user2 = (string)str.ReadObject(input);
			input.WriteByte(0x01);  //	Success
			DataContractJsonSerializer num = new DataContractJsonSerializer(typeof(Int32));
			num.WriteObject(input, (Int32)10);	//Return score of 10
		}

		static void GetUsernames(ref NetworkStream input)
		{
			DataContractJsonSerializer keyser = new DataContractJsonSerializer(users.Keys.GetType());
			input.WriteByte(0x01);
			keyser.WriteObject(input, users.Keys);
		}

		static void HandleInvalidInput(ref NetworkStream input)
		{
			input.Flush();
			input.WriteByte(0x00);
		}

		static void UpdateUsers()
		{
			DataContractJsonSerializer dbser = new DataContractJsonSerializer(users.GetType());
			FileStream dbout = new FileStream("users.json", FileMode.OpenOrCreate);
			dbser.WriteObject(dbout, users);
			dbout.Close();
		}

		static void LoadUsers()
		{
			try
			{
				DataContractJsonSerializer dbser = new DataContractJsonSerializer(users.GetType());
				FileStream dbout = new FileStream("users.json", FileMode.Open);
				users = (Dictionary<string, User>)dbser.ReadObject(dbout);
				dbout.Close();
			}
			catch (Exception ex)
			{
				users = new Dictionary<string, User>();
				Console.WriteLine("Failure loading users: " + ex.Data);
			}
		}
	}
}
