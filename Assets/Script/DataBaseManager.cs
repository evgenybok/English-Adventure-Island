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

    private string userID;
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField emailInputField;
    public TMP_InputField nameInputField;
    public TMP_InputField ageInputField;
    private DatabaseReference dbReference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void CreateUser()
    {
        string newUserID = System.Guid.NewGuid().ToString();
        User newUser = new User(
            usernameInputField.text,
            passwordInputField.text,
            emailInputField.text, // Add email field
            nameInputField.text,  // Add name field
            int.Parse(ageInputField.text) // Parse age field as an integer
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
                    NotifyLoginStatus(true); // Notify login status with true
                    return;
                }
            }
        }

        Debug.Log("Login failed. Check your username and password.");
        NotifyLoginStatus(false); // Notify login status with false
    }

    private void NotifyLoginStatus(bool success)
    {
        OnLoginStatus?.Invoke(success); // Invoke the event with login status
    }
}
