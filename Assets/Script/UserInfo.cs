using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public TextMeshProUGUI usernameText;

    void Start()
    {
        // Retrieve the username from PlayerPrefs
        string username = PlayerPrefs.GetString("Username", "DefaultUsername");

        usernameText.text = username;
    }
}
