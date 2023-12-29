using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] animalAudioClips;
    public AudioClip[] colorsAudioClips;
    public AudioClip[] foodAudioClips;

    public AudioClip[] animalSoundAudioClips;

    bool audioPlayed = false;

    public ObjectTrigger objectTrigger;
    public CategoryLogic categoryLogic;


    void Start()
    {
        StartCoroutine(PlayAudioClipAfterDelay());
    }

    IEnumerator PlayAudioClipAfterDelay()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        while (true)
        {
            if (objectTrigger != null && objectTrigger.IsObjectInSight() && audioSource != null && !audioPlayed)
            {
                string selectedCategory = PlayerPrefs.GetString("SelectedCategory");

                AudioClip[] selectedClips = null;

                
                switch (selectedCategory)
                {
                    case "Animals":
                        selectedClips = animalAudioClips;
                        break;
                    case "Colors":
                        selectedClips = colorsAudioClips;
                        break;
                    case "Food":
                        selectedClips = foodAudioClips;
                        break;
                    default:
                        Debug.LogWarning("Selected category not recognized.");
                        break;
                }

                if (selectedClips != null && selectedClips.Length > 0)
                {
                    audioSource.clip = selectedClips[0];
                    audioSource.Play();
                    audioPlayed = true;
                    yield return new WaitForSeconds(3f);
                    audioSource.clip = selectedClips[1];
                    audioSource.Play();
                    audioPlayed = true;

                }
            }

            yield return null;
        }
    }

    public void PlayAnimalSound()
    {

          //  audioSource.clip = animalSoundAudioClips[categoryLogic.currentWordIndex];
          //  audioSource.Play();

    }
}