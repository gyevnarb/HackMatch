using System;
using System.Net.Sockets;

public class DatabaseConnection : IDatabaseCommunicator
{

	private TcpClient connection;

	public DatabaseConnection(String server, Int32 port)
	{
		connection = new TcpClient(server, port);
	}

	public void IDatabaseCommunicator.CreateProfile(User userdata)
	{

	}
	
}