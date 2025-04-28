using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class MissingIngredientsDisplay : MonoBehaviour
{
    public GameObject level1Detector;
    public GameObject level2Detector;
    public Level1 level1Script;
    public Level2 level2Script;
    public TextMeshProUGUI missingIngredientsText;

    private PizzaIngredientCatcher pizzaCatcher;

    private void Start()
    {
        pizzaCatcher = FindObjectOfType<PizzaIngredientCatcher>();

        if (!pizzaCatcher)
        {
            Debug.LogError("PizzaIngredientCatcher introuvable !");
        }
    }

    private void Update()
    {
        if (!level1Detector || !level2Detector || !level1Script || !level2Script || !missingIngredientsText || !pizzaCatcher)
            return;

        // 🔥 Cas Niveau 1
        if (level1Detector.activeInHierarchy)
        {
            if (level1Script.IsLevel1Complete())
            {
                missingIngredientsText.text = "🍕 Pizza Complète pour le Niveau 1 !";
                return;
            }

            List<string> missingIngredients = level1Script.requiredIngredients
                .Where(req => !pizzaCatcher.HasIngredient(req))
                .ToList();

            if (missingIngredients.Count > 0)
            {
                missingIngredientsText.text = "Ingrédients manquants :\n" + string.Join("\n", missingIngredients);
            }
            else
            {
                missingIngredientsText.text = "Il ne manque aucun ingrédients !";
            }
            return;
        }

        // 🔥 Cas Niveau 2
        if (level2Detector.activeInHierarchy)
        {
            if (level2Script.IsLevel2Complete())
            {
                missingIngredientsText.text = "🍕 Pizza Complète pour le Niveau 2 !";
                return;
            }

            List<string> missingIngredients = level2Script.requiredIngredients
                .Where(req => !pizzaCatcher.HasIngredient(req))
                .ToList();

            if (missingIngredients.Count > 0)
            {
                missingIngredientsText.text = "Ingrédients manquants :\n" + string.Join("\n", missingIngredients);
            }
            else
            {
                missingIngredientsText.text = "Il ne manque aucun ingrédients !";
            }
            return;
        }

        // 🔥 Cas aucun niveau actif
        missingIngredientsText.text = "";
    }
}
