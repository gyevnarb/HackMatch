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

		private TcpClient connection;

		/// <summary>
		/// Creates a new connection to the given server on the given port.
		/// </summary>
		public ServerConnection(string server, Int32 port)
		{
			connection = new TcpClient(server, port);
		}

		/// <summary>
		/// Implements the CreateProfile function as described in IServerCommunicator.
		/// </summary>
		void IServerCommunicator.CreateProfile(User userdata)
		{
			//	Need serialization before writing this part.
			byte[] data = Encoding.UTF8.GetBytes("CREATE ");
			NetworkStream create = connection.GetStream();
			create.Write(data, 0, data.Length);
		}

		/// <summary>
		/// Implements the EditProfile function as described in IServerCommunicator.
		/// </summary>
		void IServerCommunicator.EditProfile(User userdata)
		{
			//	Need serialization before writing this part.
			byte[] data = Encoding.UTF8.GetBytes("EDIT ");
			NetworkStream edit = connection.GetStream();
			edit.Write(data, 0, data.Length);
		}

		/// <summary>
		/// Implements the LoadProfile function as described in IServerCommunicator.
		/// </summary>
		User IServerCommunicator.LoadProfile(string userid)
		{
			byte[] data = Encoding.UTF8.GetBytes("LOAD " + userid);
			NetworkStream load = connection.GetStream();
			load.Write(data, 0, data.Length);

			//	Deserialize from load stream here.
			//	return profile;
			return null;	//	Placeholder
		}

		/// <summary>
		/// Implements the CalculateScore function as described in IServerCommunicator.
		/// </summary>
		int IServerCommunicator.CalculateScore(string userid1, string userid2)
		{
			byte[] data = Encoding.UTF8.GetBytes("SCORE " + userid1 + ' ' + userid2);
			NetworkStream score = connection.GetStream();
			score.Write(data, 0, data.Length);
			byte[] result = new byte[8];
			Array.Clear(result, 0, 8);
			score.Read(result, 0, 8);
			
		}
	}
}