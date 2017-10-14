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
			TcpClient connection = new TcpClient(Server, Port);
			byte[] data = Encoding.UTF8.GetBytes("CREATE ");
			NetworkStream create = connection.GetStream();
			create.Write(data, 0, data.Length);
			DataContractJsonSerializer json = new DataContractJsonSerializer(userdata.GetType());
			json.WriteObject(create, userdata);
			connection.Close();
		}

		/// <summary>
		/// Implements the EditProfile function as described in IServerCommunicator.
		/// </summary>
		void IServerCommunicator.EditProfile(User userdata)
		{
			TcpClient connection = new TcpClient(Server, Port);
			//	Need serialization before writing this part.
			byte[] data = Encoding.UTF8.GetBytes("EDIT ");
			NetworkStream edit = connection.GetStream();
			edit.Write(data, 0, data.Length);
			DataContractJsonSerializer json = new DataContractJsonSerializer(userdata.GetType());
			json.WriteObject(edit, userdata);
			connection.Close();
		}

		/// <summary>
		/// Implements the LoadProfile function as described in IServerCommunicator.
		/// </summary>
		User IServerCommunicator.LoadProfile(string userid)
		{
			TcpClient connection = new TcpClient(Server, Port);
			byte[] data = Encoding.UTF8.GetBytes("LOAD " + userid);
			NetworkStream load = connection.GetStream();
			load.Write(data, 0, data.Length);
			DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));
			connection.Close();
			return (User)json.ReadObject(load);
		}

		/// <summary>
		/// Implements the CalculateScore function as described in IServerCommunicator.
		/// </summary>
		Int32 IServerCommunicator.CalculateScore(string userid1, string userid2)
		{
			TcpClient connection = new TcpClient(Server, Port);
			byte[] data = Encoding.UTF8.GetBytes("SCORE " + userid1 + ' ' + userid2);
			NetworkStream score = connection.GetStream();
			score.Write(data, 0, data.Length);
			byte[] result = new byte[4];
			score.Read(result, 0, 4);
			connection.Close();
			return BitConverter.ToInt32(data, 0);
		}
	}
}