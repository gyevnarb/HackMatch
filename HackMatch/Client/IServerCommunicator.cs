using System;
namespace HackMatch
{
	/// <summary>
	/// Provides an abstraction for classes able to communicate with a HackMatch server.
	/// </summary>
	public interface IServerCommunicator
	{
		/// <summary>
		/// Requests a new profile, "userdata", is added to the database managed by the server.
		/// </summary>
		public void CreateProfile(User userdata);

		/// <summary>
		/// Requests a profile, "userdata", should take on the given changes in the database managed by the server.
		/// </summary>
		public void EditProfile(User userdata);

		/// <summary>
		///	Requests the profile with the corresponding userid.
		/// </summary>
		public User LoadProfile(string userid);

		/// <summary>
		/// Requests the match score between the given users.
		/// </summary>
		public int CalculateScore(string userid1, string userid2);
	}
}