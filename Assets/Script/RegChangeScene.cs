using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegChangeScene : MonoBehaviour
{
    public void MoveToscene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}