using System;
using System.Net.Sockets;
using System.Runtime.Serialization;

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
		/// Implements the CreateProfile function as described in IServerCommunicator
		/// </summary>
		public void IServerCommunicator.CreateProfile(User userdata)
		{
		
		}

	}
}