using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using Proyect;

public class UserControllerTests 
{
    
    # region Variables
    // keep tack of added users through the test
    private List<string> addedUsers;  
  
    /// for later checking that users when test suite ran, all users
    /// are the same  
    private List<string> initialUsers = new List<string>();
    
    UserController userController; 
    # endregion


    # region Tests Tools
    [OneTimeSetUp]
    public void SetUp() {
        userController = new UserController();
        initialUsers.AddRange(userController.GetCurrentUserNames());
        addedUsers = new List<string>();
    }

    [OneTimeTearDown]
    public void TearDown() {
        cleanUpAddedUsers();
        Assert.AreEqual(userController.GetCurrentUserNames(), initialUsers);
    }

    void cleanUpAddedUsers() {
        foreach(string user in addedUsers) {
            userController.DeleteUser(user);
        }
    }

    /// add user thorugh userController and keep track of them
    void test_add_user(string userName) {
        this.addedUsers.Add(userName);
        this.userController.AddUser(userName);
    }

    # endregion


    # region Tests
    [Test]
    public void new_user_is_added_test() {
        string newUserName = "Newusername1";
        test_add_user(newUserName);
        Assert.Contains(newUserName, userController.GetCurrentUserNames());
    }

    [TestCase("single_name", "Single_name")]
    [TestCase("double name", "Double Name")]
    [TestCase("spaced  name    ", "Spaced Name")]
    public void name_is_added_in_correct_format_test(string newUser, string expected) {
        test_add_user(newUser);
        Assert.Contains(expected, userController.GetCurrentUserNames());
    }

    [TestCase("Newusername2")]
    [TestCase("Newusername")]
    [TestCase("random  user name with    spaces")]
    public void user_is_removed_test(string newUserName) {
        userController.AddUser(newUserName);
        userController.DeleteUser(newUserName);
        Assert.IsFalse(userController.GetCurrentUserNames().Contains(newUserName), 
            String.Format("El usuario {0} no deberia existir", newUserName));
    }

    [Test]
    public void add_existing_user_throws_exception_test() {
        string userName = "some user";
        test_add_user(userName);
        Assert.Throws<UserAlreadyExistsError>(
            code: delegate { userController.AddUser(userName); }, 
            message: String.Format("El usuario {0} ya existe", userName)
        );
    }

    [Test]
    public void can_find_users_by_name() {
        string userName = "this user should be found";
        test_add_user(userName);
        Level expectedLevel = new Level(Constants.Stages.MainMenu);
        User expectedMatch = new User(userName, expectedLevel);
        User actualMatch = userController.SearchUserByName(userName);
        Assert.AreEqual(expectedMatch, actualMatch);
    }

    # endregion

}