using TMPro;
using UnityEngine;

public class CategoryManager : MonoBehaviour
{
    public TextMeshProUGUI categoryText;   // Text to display the category
    public string[] categoryNames = { "Animals", "Colors", "Food" }; // Names of categories

    void Start()
    {
        if (PlayerPrefs.HasKey("SelectedDifficulty") && PlayerPrefs.HasKey("SelectedCategory"))
        {
            string selectedDifficulty = PlayerPrefs.GetString("SelectedDifficulty");
            string selectedCategory = PlayerPrefs.GetString("SelectedCategory");

            // Check if the selected category exists in the array
            int index = System.Array.IndexOf(categoryNames, selectedCategory);
            if (index != -1)
            {
                // Text to display
                string formattedText = selectedCategory + " (" + selectedDifficulty + ")";
                categoryText.text = formattedText;
            }
            else
            {
                Debug.LogWarning("Invalid category!");
            }
        }
        else
        {
            Debug.LogWarning("Category or difficulty not selected!");
        }
    }
}
