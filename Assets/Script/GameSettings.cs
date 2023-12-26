using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public GameObject menuPanel;
    public Button openMenuButton;
    public Button option1Button;
    public Button option2Button;

    private bool isMenuOpen = false;

    void Start()
    {
        menuPanel.SetActive(false);

        openMenuButton.onClick.AddListener(ToggleMenu);
        option1Button.onClick.AddListener(ChangeToScene1);
        option2Button.onClick.AddListener(ChangeToScene2);
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);
    }

    void ChangeToScene1()
    {
        SceneManager.LoadScene("MainMenuScene"); 
    }

    void ChangeToScene2()
    {
        SceneManager.LoadScene("CategoryScene");
    }
}
