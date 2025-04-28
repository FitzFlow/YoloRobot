using UnityEngine;

public class Screen : MonoBehaviour
{
    public GameObject firstRecipe;    // First recipe screen
    public GameObject secondRecipe;   // Second recipe screen
    private Level1 level1Script;      // Reference to Level1 script

    void Start()
    {
        // Find the Level1 script in the scene
        level1Script = FindObjectOfType<Level1>();
        
        if (level1Script == null)
        {
            Debug.LogError("Level1 script not found!");
            return;
        }

        // Initially show first recipe and hide second recipe
        firstRecipe.SetActive(true);
        secondRecipe.SetActive(false);
    }

    void Update()
    {
        if (firstRecipe == null || secondRecipe == null || level1Script == null)
            return;

        // Check if Level 1 is complete
        if (level1Script.IsLevel1Complete())
        {
            // Hide first recipe and show second recipe
            firstRecipe.SetActive(false);
            secondRecipe.SetActive(true);
        }
        else
        {
            // Show first recipe and hide second recipe
            firstRecipe.SetActive(true);
            secondRecipe.SetActive(false);
        }
    }
}