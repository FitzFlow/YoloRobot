using UnityEngine;
using System.Collections.Generic;

public class Level1 : MonoBehaviour
{
    public List<string> requiredIngredients = new List<string>(); // Pour MissingIngredientsDisplay
    private string targetName = "DoughWithSauceCheese"; 
    private bool isLevel1Complete = false;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision détectée 1");

        if (!isLevel1Complete && (collision.gameObject.name == targetName || collision.gameObject.name == targetName + "(Clone)"))
        {
            isLevel1Complete = true;
            Debug.Log("Niveau 1 terminé !");

            // 🔥 IMPORTANT : Demander au LevelManager de passer au niveau suivant !
            FindObjectOfType<LevelManager>().NextLevel();
        }
        else
        {
            Debug.Log($"Collision avec un objet non valide : {collision.gameObject.name}");
        }
    }

    public bool IsLevel1Complete()
    {
        return isLevel1Complete;
    }
}
