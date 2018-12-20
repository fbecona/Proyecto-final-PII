using UnityEngine; 
using NUnit.Framework; 
using System.Collections.Generic; 
using System; 
using Proyect; 
 
 
public class UserTests 
{ 
	[Test] 
	public void user_equality_with_identical_names_test() { 
		Level mainMenuLevel = new Level(Constants.Stages.MainMenu); 
		string username = "some user"; 
 
		User user1 = new User(username, mainMenuLevel); 
		User user2 = new User(username, mainMenuLevel); 
 
		Assert.AreEqual(user1, user2); 
	} 
 
	[Test] 
	public void user_equality_with_diferent_formatted_names_test() { 
		Level mainMenuLevel = new Level(Constants.Stages.MainMenu); 
		string username = "some user"; 
		string username2 = "Some UseR"; 
 
		User user1 = new User(username, mainMenuLevel); 
		User user2 = new User(username2, mainMenuLevel); 
 
		Assert.AreEqual(user1, user2); 
	} 
 
}