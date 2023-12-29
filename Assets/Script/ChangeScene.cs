using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private DataBaseManager dataBaseManager;
    public int sceneID;

    private void Start()
    {
        dataBaseManager = FindObjectOfType<DataBaseManager>();
        if (dataBaseManager != null)
        {
            DataBaseManager.OnLoginStatus += HandleLoginStatus;
        }
        else
        {
            Debug.LogError("DataBaseManager not found.");
        }
    }

    private void OnDestroy()
    {
        if (dataBaseManager != null)
        {
            DataBaseManager.OnLoginStatus -= HandleLoginStatus;
        }
    }

    private void HandleLoginStatus(bool success)
    {
        if (success)
        {
            SceneManager.LoadScene(sceneID); // Scene change if login is successful
        }
    }

    public void MoveToScene(int sceneID)
    {
        if (dataBaseManager != null)
        {
            dataBaseManager.CheckLogin();
        }
        else
        {
            Debug.LogError("DataBaseManager not found.");
        }
    }
}
