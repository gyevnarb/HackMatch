using System;
using System.Net.Sockets;
using System.Threading;

namespace HackMatchServer
{
	class MainActivity
	{
		static void Main(string[] args)
		{
			TcpListener listener = TcpListener.Create(6969);
			listener.Start();
			do
			{
				TcpClient client = listener.AcceptTcpClient();
				Thread job = new Thread(() => HandleClient(client));
				job.Start();

			} while (true);	//	No condition to stop.
		}

		static void HandleClient(TcpClient client)
		{
			NetworkStream input = client.GetStream();
			byte[] buffer = new byte[4096];
			Array.Clear(buffer, 0, buffer.Length);
			input.Read(buffer, 0, buffer.Length);
			switch (buffer[0])
			{
				case 0x01:
					input.WriteByte(0x01);
					Console.Out.WriteLine("<CreateProfile>");
					break;
				case 0x02:
					input.WriteByte(0x01);
					Console.Out.WriteLine("<EditProfile>");
					break;
				case 0x03:
					input.WriteByte(0x01);
					Console.Out.WriteLine("<LoadProfile>");
					break;
				case 0x04:
					Int32 result = 99;
					input.Write(BitConverter.GetBytes(result), 0, sizeof(Int32));
					Console.Out.WriteLine("<CalculateScore>");
					break;
				default:
					input.WriteByte(0x00);
					Console.Out.WriteLine("<Whoops>");
					break;
			}
			client.Close();
		}
	}
}
