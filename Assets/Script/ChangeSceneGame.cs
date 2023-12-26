using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneGame : MonoBehaviour
{
    public ToggleGroup categoryToggleGroup;
    public ToggleGroup difficultyToggleGroup;
    public int sceneIndexToLoad;

    public TextMeshProUGUI warningText;

    public AudioSource warningAudio;
    public AudioClip categoryWarningClip;
    public AudioClip difficultyWarningClip;

    private void Start()
    {
        categoryToggleGroup.SetAllTogglesOff();
        difficultyToggleGroup.SetAllTogglesOff();
        PlayerPrefs.DeleteKey("SelectedCategory");
        PlayerPrefs.DeleteKey("SelectedDifficulty");
        LoadSelectedToggles();
    }

    public void OnCategoryToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            Toggle selectedCategoryToggle = categoryToggleGroup.ActiveToggles().FirstOrDefault();
            if (selectedCategoryToggle != null)
            {
                PlayerPrefs.SetString("SelectedCategory", selectedCategoryToggle.name);
                Debug.Log("Selected Category: " + selectedCategoryToggle.name);
            }
        }
        else
        {
            PlayerPrefs.DeleteKey("SelectedCategory");
        }
    }

    public void OnDifficultyToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            Toggle selectedDifficultyToggle = difficultyToggleGroup.ActiveToggles().FirstOrDefault();
            if (selectedDifficultyToggle != null)
            {
                PlayerPrefs.SetString("SelectedDifficulty", selectedDifficultyToggle.name);
                Debug.Log("Selected Difficulty: " + selectedDifficultyToggle.name);
            }
        }
        else
        {
            PlayerPrefs.DeleteKey("SelectedDifficulty");
        }
    }

    private void LoadSelectedToggles()
    {
        if (PlayerPrefs.HasKey("SelectedCategory"))
        {
            string categoryName = PlayerPrefs.GetString("SelectedCategory");
            Toggle categoryToggle = categoryToggleGroup.GetComponentsInChildren<Toggle>().FirstOrDefault(toggle => toggle.name == categoryName);
            if (categoryToggle != null)
            {
                categoryToggle.isOn = true;
            }
        }

        if (PlayerPrefs.HasKey("SelectedDifficulty"))
        {
            string difficultyName = PlayerPrefs.GetString("SelectedDifficulty");
            Toggle difficultyToggle = difficultyToggleGroup.GetComponentsInChildren<Toggle>().FirstOrDefault(toggle => toggle.name == difficultyName);
            if (difficultyToggle != null)
            {
                difficultyToggle.isOn = true;
            }
        }
    }

    public void OnPlayButtonPressed()
    {
        bool allTogglesSelected = AreAllTogglesSelected();

        if (allTogglesSelected)
        {
            SceneManager.LoadScene(sceneIndexToLoad);
        }
        else
        {
            if (!IsCategorySelected())
            {
                ShowWarning("Please select a category!");
                PlayWarningAudio(categoryWarningClip);
            }

            if (!IsDifficultySelected())
            {
                ShowWarning("Please select a difficulty for the category!");
                PlayWarningAudio(difficultyWarningClip);
            }
        }
    }


    // Check if all toggles are selected
    bool AreAllTogglesSelected()
    {
        return IsCategorySelected() && IsDifficultySelected();
    }

    // Check if category and difficulty are selected
    bool IsCategorySelected()
    {
        return categoryToggleGroup.AnyTogglesOn();
    }

    bool IsDifficultySelected()
    {
        return difficultyToggleGroup.AnyTogglesOn();
    }

    void ShowWarning(string message)
    {
        if (warningText != null)
        {
            warningText.text = message;
            Invoke("ResetWarning", 3f); // Reset warning after 3 seconds
        }
    }

    void PlayWarningAudio(AudioClip clipToPlay)
    {
        if (warningAudio != null && clipToPlay != null)
        {
            warningAudio.clip = clipToPlay;
            warningAudio.Play();
        }
    }

    void ResetWarning()
    {
        if (warningText != null)
        {
            warningText.text = "";
        }
    }
}
