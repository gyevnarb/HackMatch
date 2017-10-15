using System;
using System.Net.Sockets;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace HackMatch
{
	/// <summary>
	/// Provides a connection to a HackMatch server.
	/// </summary>
	public class ServerConnection : IServerCommunicator
	{

		public string Server { get; set; }
		public Int32 Port { get; set; }

		/// <summary>
		/// Creates a new connection to the given server on the given port.
		/// </summary>
		public ServerConnection(string server, Int32 port)
		{
			Server = server;
			Port = port;
		}

		/// <summary>
		/// Implements the CreateProfile function as described in IServerCommunicator.
		/// </summary>
		void IServerCommunicator.CreateProfile(User userdata)
		{
			Console.Out.WriteLine("<CreateProfile>");
			TcpClient connection = new TcpClient(Server, Port);
			NetworkStream create = connection.GetStream();
			create.WriteByte(0x01);
			BinaryFormatter form = new BinaryFormatter();
			form.Serialize(create, userdata);
			int flag = create.ReadByte();
			create.Close();
			connection.Close();
			if (flag != 0x01)
			{
				throw new Exception("Operation failed.");
			}
		}

		/// <summary>
		/// Implements the EditProfile function as described in IServerCommunicator.
		/// </summary>
		void IServerCommunicator.EditProfile(User userdata)
		{
			Console.Out.WriteLine("<EditProfile>");
			TcpClient connection = new TcpClient(Server, Port);
			//	Need serialization before writing this part.
			NetworkStream edit = connection.GetStream();
			edit.WriteByte(0x02);
			BinaryFormatter form = new BinaryFormatter();
			form.Serialize(edit, userdata);
			int flag = edit.ReadByte();
			edit.Close();
			connection.Close();
			if (flag != 0x01)
			{
				throw new Exception("Operation failed.");
			}
		}

		/// <summary>
		/// Implements the LoadProfile function as described in IServerCommunicator.
		/// </summary>
		User IServerCommunicator.LoadProfile(string userid)
		{
			Console.Out.WriteLine("<LoadProfile>");
			TcpClient connection = new TcpClient(Server, Port);
			NetworkStream load = connection.GetStream();
			load.WriteByte(0x03);
			BinaryFormatter form = new BinaryFormatter();
			form.Serialize(load, userid);
			int flag = load.ReadByte();
			if (flag != 0x01)
			{
				throw new Exception("Operation failed.");
			}
			User profile = (User)form.Deserialize(load);
			load.Close();
			connection.Close();
			return profile;
		}

		/// <summary>
		/// Implements the CalculateScore function as described in IServerCommunicator.
		/// </summary>
		Int32 IServerCommunicator.CalculateScore(string userid1, string userid2)
		{
			Console.Out.WriteLine("<CalculateScore>");
			TcpClient connection = new TcpClient(Server, Port);
			NetworkStream score = connection.GetStream();
			score.WriteByte(0x04);
			BinaryFormatter form = new BinaryFormatter();
			form.Serialize(score, userid1);
			form.Serialize(score, userid2);
			int flag = score.ReadByte();
			if (flag != 0x01)
			{
				throw new Exception("Operation failed.");
			}
			Int32 result = (Int32)form.Deserialize(score);
			score.Close();
			connection.Close();
			return result;
		}

		Dictionary<string, User>.KeyCollection IServerCommunicator.GetUsernames()
		{
			TcpClient connection = new TcpClient(Server, Port);
			NetworkStream names = connection.GetStream();
			names.WriteByte(0x05);
			int flag = names.ReadByte();
			if (flag != 0x01)
			{
				throw new Exception("Operation failed.");
			}
			BinaryFormatter form = new BinaryFormatter();
			Dictionary<string, User>.KeyCollection usernames = (Dictionary<string, User>.KeyCollection)form.Deserialize(names);
			names.Close();
			connection.Close();
			return usernames;
		}
	}
}