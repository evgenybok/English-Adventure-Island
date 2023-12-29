using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeechRecognition : MonoBehaviour
{
    private AndroidJavaObject speechRecognizer;
    private bool isListening = false;
    public Text outputText; // Reference to the UI Text component to display recognized speech

    public object OpenAIWhisperASR { get; private set; }

    void Start()
    {
        Debug.Log("Start method called");
        StartCoroutine(InitializeSpeechRecognizerCoroutine());
    }

    System.Collections.IEnumerator InitializeSpeechRecognizerCoroutine()
    {
        Debug.Log("Initializing speech recognizer coroutine");

        // Yield for a few frames to avoid blocking the main thread
        int frameCount = 0;
        while (frameCount < 3)  // Adjust the number of frames as needed
        {
            yield return null;
            frameCount++;
        }

        InitializeSpeechRecognizer();
    }

    void OnDestroy()
    {
        // Release resources when the script is destroyed
        if (speechRecognizer != null)
        {
            speechRecognizer.Call("destroy");
        }
    }

    void Update()
    {
        // Check if speech recognition is active and handle it in the Update loop
        if (isListening)
        {
            // Handle continuous speech recognition logic here if needed
        }
    }

    void InitializeSpeechRecognizer()
    {
        Debug.Log("Initializing Whisper ASR");

        // Replace 'YOUR_API_KEY' with your actual OpenAI API key
        string apiKey = "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";
       // object p = OpenAIWhisperASR.Initialize(apiKey);

        Debug.Log("Whisper ASR initialized successfully");
    }

    public void StartSpeechRecognition()
    {
        // Start the speech recognition
        speechRecognizer.Call("startListening", null);
        isListening = true;
    }

    public void StopSpeechRecognition()
    {
        // Stop the speech recognition
        speechRecognizer.Call("stopListening");
        isListening = false;
    }

    private class SpeechRecognitionListener : AndroidJavaProxy
    {
        private SpeechRecognition speechRecognition;

        public SpeechRecognitionListener(SpeechRecognition speechRecognition) : base("android.speech.RecognitionListener")
        {
            this.speechRecognition = speechRecognition;
        }

        public void onResults(AndroidJavaObject results)
        {
            // Handle speech recognition results
            AndroidJavaObject matches = results.Call<AndroidJavaObject>("getStringArrayList", "results_recognition");
            if (matches.Call<int>("size") > 0)
            {
                string spokenText = matches.Call<string>("get", 0);
                Debug.Log("Speech Recognized: " + spokenText);

                // Update the UI Text with recognized speech
                speechRecognition.UpdateOutputText(spokenText);
            }
        }

        public static implicit operator AndroidJavaObject(SpeechRecognitionListener v)
        {
            throw new NotImplementedException();
        }

        // Implement other required methods of RecognitionListener if needed
        // For example: onReadyForSpeech, onEndOfSpeech, onError, etc.
    }

    // Update the UI Text with recognized speech
    private void UpdateOutputText(string spokenText)
    {
        if (outputText != null)
        {
            outputText.text = "Recognized Speech: " + spokenText;
        }
    }
}
