using System;

public interface IDatabaseCommunicator
{
	bool CreateProfile(User userdata);
	bool EditProfile(User userdata);

	User LoadProfile(String userid);
	int CalculateScore(String userid1, String userid2);
}
