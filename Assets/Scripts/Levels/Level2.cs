using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Level2 : MonoBehaviour
{
    public List<string> requiredIngredients = new List<string> { "pepperoni" };
    // Use name check instead of tag
    private string requiredPizzaName = "DoughWithSauceCheese"; // Set this to the exact name of the pizza base GameObject

    private bool isLevel2Complete = false;

    void OnCollisionEnter(Collision collision)
    {
        if (isLevel2Complete) // Check if this level (Level 2) is already complete
        {
            return;
        }

        if (collision.gameObject.name == requiredPizzaName || collision.gameObject.name == requiredPizzaName + "(Clone)")
        {
            // Now that we've identified the correct pizza by name, try to get its ingredient catcher
            PizzaIngredientCatcher pizzaCatcher = collision.gameObject.GetComponent<PizzaIngredientCatcher>();
            Debug.Log("Collision détéctée 2");

            if (pizzaCatcher != null)
            {
                // Check if the correctly named pizza also has all required ingredients
                if (pizzaCatcher.HasAllIngredients(requiredIngredients))
                {
                    isLevel2Complete = true;
                    Debug.Log($"Level 2 Complete! Pizza '{collision.gameObject.name}' has name '{requiredPizzaName}' and ingredients: [{string.Join(", ", requiredIngredients)}]");
                    // Add level completion effects here
                }
                else
                {
                    List<string> currentIngredients = pizzaCatcher.GetAttachedIngredientNames();
                    Debug.Log($"Pizza '{collision.gameObject.name}' has name '{requiredPizzaName}' but ingredients not complete for Level 2. Has: [{string.Join(", ", currentIngredients)}]. Needs: [{string.Join(", ", requiredIngredients)}]");
                }
            }
            else
            {
                // Log warning if the correctly named object is missing the catcher script
                Debug.LogWarning($"Collided object '{collision.gameObject.name}' has the required name but is missing the PizzaIngredientCatcher script!");
            }
        }
        else
        {
            Debug.Log($"Level 2 collision ignored: Object '{collision.gameObject.name}' does not have the required name '{requiredPizzaName}'.");
        }
    }

    public void ResetLevel()
    {
        isLevel2Complete = false;
    }

    public bool IsLevel2Complete()
    {
        return isLevel2Complete;
    }
}