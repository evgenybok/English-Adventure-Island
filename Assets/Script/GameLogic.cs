using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameLogic : MonoBehaviour
{
    public GoogleCloudStreamingSpeechToText.StreamingRecognizer streamingRecognizer;
    public CategoryLogic categoryLogic;
    public ObjectTrigger objectTrigger;
    public AudioManager audioManager;
    private bool isObjectInSight = false;
    private bool detectedCorrectWord = false;
    private string lastDetectedWord = "";

    private void Start()
    {
        streamingRecognizer.onFinalResult.AddListener(OnFinalResult);
        string selectedCategory = PlayerPrefs.GetString("SelectedCategory");

        switch (selectedCategory)
        {
            case "Animals":
                categoryLogic.StartCategory(categoryLogic.animals);
                break;
            case "Colors":
                categoryLogic.StartCategory(categoryLogic.colors);
                break;
            case "Food":
                categoryLogic.StartCategory(categoryLogic.food);
                break;
            default:
                Debug.LogWarning("Selected category not recognized.");
                break;
        }
    }

    private void OnDestroy()
    {
        streamingRecognizer.onFinalResult.RemoveListener(OnFinalResult);
    }

    private void Update()
    {
        // Check if the object is in sight
        bool newObjectInSight = objectTrigger != null && objectTrigger.IsObjectInSight();

        // Start communication with API if the object is in sight and correct word wasn't detected yet
        if (newObjectInSight && !detectedCorrectWord)
        {
            if (!isObjectInSight || lastDetectedWord != categoryLogic.GetCurrentWord())
            {
                // Restart streaming if the object just came into sight or the word changed
                lastDetectedWord = categoryLogic.GetCurrentWord();
                // audioManager.PlayAnimalSound();

                streamingRecognizer.StartListening();
            }
        }
        else
        {
            // Stop communication with API if the object is out of sight or the word is detected correctly
            if (streamingRecognizer.IsListening())
            {
                streamingRecognizer.StopListening();
            }
        }

        isObjectInSight = newObjectInSight;
    }

    private void OnFinalResult(string finalResult)
    {
        if (finalResult != null)
        {
            string lowercasedResult = finalResult.ToLower();
            categoryLogic.CheckWord(lowercasedResult);

            // Check if the detected word is correct
            if (categoryLogic.IsCorrectWord(lowercasedResult))
            {
                detectedCorrectWord = true;

                // Restart streaming for the next word
                detectedCorrectWord = false;
            }
        }
    }


}
