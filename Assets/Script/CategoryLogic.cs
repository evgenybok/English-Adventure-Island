using UnityEngine;
using TMPro;
using System.Collections;

public class CategoryLogic : MonoBehaviour
{
    public string[] animals = { "cat", "dog", "bird", "elephant", "spider", "monkey", "fish", "pig", "fox" };
    public string[] colors = { "red", "yellow", "pink", "brown", "purple", "green", "blue", "white", "orange" };
    public string[] food = { "pizza", "burger", "sushi", "coffee", "smoothie", "ice cream", "pancake", "juice", "sandwich" };

    public Sprite[] animalImages;
    public Sprite[] colorsImages;
    public Sprite[] foodImages;

    public TextMeshProUGUI wordDisplay;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI triesLeftText;

    public SpriteRenderer objectImageRenderer;
    public Transform objectToApproach;

    public string[] currentCategory;
    public Sprite[] currentImages;

    public int currentWordIndex = 0;
    public int wrongWordCount = 0;
    public int score = 0;

    public AudioClip[] correctWordAudioClips;
    public AudioClip[] animalsSoundAudioClips;
    public AudioSource audioSource;

    private int maxWrongAttempts = 4;

    public void StartCategory(string[] category)
    {
        wordDisplay.text = "";
        triesLeftText.text = "Tries left: " + maxWrongAttempts;

        currentCategory = category;
        if (category == animals)
        {
            currentImages = animalImages;
        }
        else if (category == colors)
        {
            currentImages = colorsImages;
        }
        else if (category == food)
        {
            currentImages = foodImages;
        }

        UpdateUI();
    }

    public void CheckWord(string word)
    {
        if (word == currentCategory[currentWordIndex])
        {
            Debug.Log("Correct word!");
            score += 10;
            UpdateScoreUI();
            wordDisplay.text = currentCategory[currentWordIndex];
            PlayRandomCorrectWordAudio();
            StartCoroutine(DelayedMoveToNextWord(5f)); // Move to the next word after 5 seconds
        }
        else
        {
            wrongWordCount++;
            wordDisplay.text = "Wrong!";
            Debug.Log("Incorrect word! Remaining tries: " + (maxWrongAttempts - wrongWordCount));

            if (wrongWordCount >= maxWrongAttempts || currentWordIndex >= currentCategory.Length - 1)
            {
                wordDisplay.text = currentCategory[currentWordIndex];
                StartCoroutine(DelayedMoveToNextWord(5f));
            }
            else
            {
                triesLeftText.text = "Tries left: " + (maxWrongAttempts - wrongWordCount);
            }
        }
    }

    private IEnumerator DelayedMoveToNextWord(float delay)
    {
        yield return new WaitForSeconds(delay);

        MoveToNextWord();
    }

    private void MoveToNextWord()
    {
        wrongWordCount = 0;
        currentWordIndex++;

        if (currentWordIndex >= currentCategory.Length)
        {
            Debug.Log("Game Over! All words completed.");
            triesLeftText.text = "Good Job! You Scored " + score + " Points";
            wordDisplay.text = "";
            if (objectImageRenderer != null)
            {
                objectImageRenderer.sprite = null;
                wordDisplay.text = "";
            }
            return;
        }

        if (objectImageRenderer != null)
        {
            objectImageRenderer.sprite = null;
            wordDisplay.text = "";
        }

        if (objectToApproach != null)
        {
            objectToApproach.Translate(Vector3.right * 1500f);
        }
        triesLeftText.text = "Tries left: " + maxWrongAttempts; // Reset tries left
        UpdateUI();
    }

    private void UpdateUI()
    {
        objectImageRenderer.sprite = currentImages[currentWordIndex];
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void PlayRandomCorrectWordAudio()
    {
        if (correctWordAudioClips.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, correctWordAudioClips.Length);
            audioSource.clip = correctWordAudioClips[randomIndex];
            audioSource.Play();
        }
    }

    public bool IsCorrectWord(string word)
    {
        if (currentCategory != null && currentWordIndex < currentCategory.Length)
        {
            return word == currentCategory[currentWordIndex];
        }
        return false;
    }

    public string GetCurrentWord()
    {
        if (currentWordIndex >= 0 && currentWordIndex < currentCategory.Length)
        {
            return currentCategory[currentWordIndex];
        }
        else
        {
            return string.Empty;
        }
    }
}
