using System;

public interface IDatabaseCommunicator
{
	void CreateProfile(User userdata);
	void EditProfile(User userdata);

	User LoadProfile(String userid);
	int CalculateScore(String userid1, String userid2);
}
