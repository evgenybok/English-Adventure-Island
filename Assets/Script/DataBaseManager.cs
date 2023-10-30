using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;

public class DataBaseManager : MonoBehaviour
{

    private string userID;
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    private DatabaseReference dbReference;
    

    // Start is called before the first frame update
    void Start()
    {
        //userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateUser()
    {
        string newUserID = System.Guid.NewGuid().ToString();
        User newUser = new User(usernameInputField.text, passwordInputField.text);
        string json = JsonUtility.ToJson(newUser);
        dbReference.Child("Users").Child(newUserID).SetRawJsonValueAsync(json);
        Debug.Log(usernameInputField.text);
        Debug.Log(passwordInputField.text);
    }
}
