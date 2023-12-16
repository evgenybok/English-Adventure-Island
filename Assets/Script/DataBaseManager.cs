using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;

public class DataBaseManager : MonoBehaviour
{
    public delegate void LoginStatus(bool success); // Declare a delegate to handle login status
    public static event LoginStatus OnLoginStatus; // Declare an event to notify login status

    private DatabaseReference dbReference;

    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField emailInputField;
    public TMP_InputField nameInputField;
    public TMP_InputField ageInputField;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public async void CreateUser()
    {
        // Check if the username or email is already in use
        if (await IsUsernameOrEmailInUse(usernameInputField.text, emailInputField.text))
        {
            Debug.Log("Username or email is already in use. Please choose a different one.");
            return;
        }

        string newUserID = System.Guid.NewGuid().ToString();
        User newUser = new User(
            usernameInputField.text,
            passwordInputField.text,
            emailInputField.text,
            nameInputField.text,
            int.Parse(ageInputField.text)
        );

        string json = JsonUtility.ToJson(newUser);
        dbReference.Child("Users").Child(newUserID).SetRawJsonValueAsync(json);
        Debug.Log("User created with username: " + newUser.username);
    }

    public async void CheckLogin()
    {
        string inputUsername = usernameInputField.text;
        string inputPassword = passwordInputField.text;

        DataSnapshot snapshot = await dbReference.Child("Users").OrderByChild("username").EqualTo(inputUsername).GetValueAsync();

        if (snapshot != null && snapshot.Exists)
        {
            foreach (var childSnapshot in snapshot.Children)
            {
                string json = childSnapshot.GetRawJsonValue();
                User storedUser = JsonUtility.FromJson<User>(json);
                if (storedUser.password == inputPassword)
                {
                    Debug.Log("Login successful!");
                    NotifyLoginStatus(true);
                    return;
                }
            }
        }

        Debug.Log("Login failed. Check your username and password.");
        NotifyLoginStatus(false);
    }

    private async Task<bool> IsUsernameOrEmailInUse(string username, string email)
    {
        DataSnapshot usernameSnapshot = await dbReference.Child("Users").OrderByChild("username").EqualTo(username).GetValueAsync();
        DataSnapshot emailSnapshot = await dbReference.Child("Users").OrderByChild("email").EqualTo(email).GetValueAsync();

        return (usernameSnapshot != null && usernameSnapshot.Exists) || (emailSnapshot != null && emailSnapshot.Exists);
    }

    private void NotifyLoginStatus(bool success)
    {
        OnLoginStatus?.Invoke(success);
    }
}
