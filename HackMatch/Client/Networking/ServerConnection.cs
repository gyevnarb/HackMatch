using System;
using System.Net.Sockets;
using System.Text;
using System.Runtime.Serialization.Json;

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
			DataContractJsonSerializer json = new DataContractJsonSerializer(userdata.GetType());
			json.WriteObject(create, userdata);
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
			DataContractJsonSerializer json = new DataContractJsonSerializer(userdata.GetType());
			json.WriteObject(edit, userdata);
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
			DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));
			int flag = load.ReadByte();
			if (flag != 0x01)
			{
				throw new Exception("Operation failed.");
			}
			User profile = (User)json.ReadObject(load);
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
			DataContractJsonSerializer usernames = new DataContractJsonSerializer(typeof(string));
			usernames.WriteObject(score, userid1);
			usernames.WriteObject(score, userid2);
			int flag = score.ReadByte();
			if (flag != 0x01)
			{
				throw new Exception("Operation failed.");
			}
			DataContractJsonSerializer scoring = new DataContractJsonSerializer(typeof(Int32));
			Int32 result = (Int32)scoring.ReadObject(score);
			score.Close();
			connection.Close();
			return result;
		}
	}
}