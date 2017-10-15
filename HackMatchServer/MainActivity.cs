using System;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using HackMatch;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;

namespace HackMatchServer
{
	class MainActivity
	{

		static Dictionary<string, User> users;

		static void Main(string[] args)
		{
			LoadUsers();
			TcpListener listener = TcpListener.Create(Constants.PORT);
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
						Console.Out.WriteLine("Editing profile...");
						EditProfile(ref input);
						break;
					case 0x03:
						input.WriteByte(0x01);
						Console.Out.WriteLine("Loading profile...");
						LoadProfile(ref input);
						break;
					case 0x04:
						Int32 result = 99;
						input.Write(BitConverter.GetBytes(result), 0, sizeof(Int32));
						Console.Out.WriteLine("Calculating score...");
						CalculateScore(ref input);
						break;
					case 0x05:
						Console.Out.WriteLine("Getting usernames...");
						GetUsernames(ref input);
						break;
					default:
						input.WriteByte(0x00);
						Console.Out.WriteLine("Unexpected failure!");
						HandleInvalidInput(ref input);
						break;
				}
			}
			catch (Exception ex)
			{
				Console.Out.WriteLine("Unexpected error: " + ex);
				input.Flush();
				HandleInvalidInput(ref input);
			}
			input.Close();
			client.Close();
			Console.Out.WriteLine("Job completed.");
		}

		//	TODO: Sanitize input
		/*
		private static byte[] ReadBlock(ref NetworkStream input)
		{
			byte[] rawLen = new byte[4];
			input.Read(rawLen, 0, 4);
			int dataLen = BitConverter.ToInt32(rawLen, 0);
			byte[] block = new byte[dataLen];
			input.Read(block, 0, dataLen);
			return block;
		}
		
		private static void WriteBlock<T>(ref NetworkStream output, ref T value)
		{
			byte[] data = value.
			byte[] rawLen = BitConverter.GetBytes((Int32)Marshal.SizeOf(typeof(T));

		}
		*/
		static void CreateProfile(ref NetworkStream input)
		{
			BinaryFormatter form = new BinaryFormatter();
			User profile = (User)form.Deserialize(input);
			Console.Out.WriteLine(profile.ToString());
			if (users.ContainsKey(profile.Username))
			{
				input.WriteByte(0x00);
				Console.Out.WriteLine("Profile was not created.");
			}
			else
			{
				users[profile.Username] = profile;
				input.WriteByte(0x01);
				UpdateUsers();
				Console.Out.WriteLine("Profile was created.");
			}
		}

		static void EditProfile(ref NetworkStream input)
		{
			BinaryFormatter form = new BinaryFormatter();
			User profile = (User)form.Deserialize(input);
			if (users.ContainsKey(profile.Username))
			{
				users[profile.Username] = profile;
				input.WriteByte(0x01);
				UpdateUsers();
				Console.Out.WriteLine("Profile was edited.");
			}
			else
			{
				input.WriteByte(0x00);
				Console.Out.WriteLine("Profile was not edited.");
			}
		}

		static void LoadProfile(ref NetworkStream input)
		{
			BinaryFormatter form = new BinaryFormatter();
			string username = (string)form.Deserialize(input);
			if (users.ContainsKey(username))
			{
				input.WriteByte(0x01);
				form.Serialize(input, users[username]);
				Console.Out.WriteLine("Profile was loaded.");
			}
			else
			{
				input.WriteByte(0x00);
				Console.Out.WriteLine("Profile was not loaded.");
			}
		}

		//	TODO: Ensure usernames cannot contain null characters
		static void CalculateScore(ref NetworkStream input)
		{
			BinaryFormatter form = new BinaryFormatter();
			string user1 = (string)form.Deserialize(input);
			string user2 = (string)form.Deserialize(input);
			if (users.ContainsKey(user1) && users.ContainsKey(user2))
			{
				input.WriteByte(0x01);  //	Success
				form.Serialize(input, (Int32)10);  //Return score of 10
				Console.Out.WriteLine("Score was calculated.");
			}
			else
			{
				input.WriteByte(0x00);
				Console.Out.WriteLine("Score was not calculated.");
			}

		}

		static void GetUsernames(ref NetworkStream input)
		{
			BinaryFormatter form = new BinaryFormatter();
			input.WriteByte(0x01);
			form.Serialize(input, users.Keys);
			Console.Out.WriteLine("Usernames were returned.");
		}

		static void HandleInvalidInput(ref NetworkStream input)
		{
			input.Flush();
			input.WriteByte(0x00);
		}

		static void UpdateUsers()
		{
			BinaryFormatter form = new BinaryFormatter();
			FileStream dbout = new FileStream("users.json", FileMode.OpenOrCreate);
			form.Serialize(dbout, users);
			dbout.Close();
			Console.Out.WriteLine("Users updated.");
		}

		static void LoadUsers()
		{
			try
			{
				BinaryFormatter form = new BinaryFormatter();
				FileStream dbout = new FileStream("users.json", FileMode.Open);
				users = (Dictionary<string, User>)form.Deserialize(dbout);
				dbout.Close();
				Console.Out.WriteLine("Users loaded.");
			}
			catch (Exception ex)
			{
				users = new Dictionary<string, User>();
				Console.WriteLine("Failure loading users: " + ex.Data);
			}
		}
	}
}
