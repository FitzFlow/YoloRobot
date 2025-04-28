using UnityEngine;
  // Assure-toi d'utiliser ce namespace

public class Level1 : MonoBehaviour
{
    // Nom de l'élément à vérifier
    public string targetName = "DoughWithSauceCheese"; // Nom de la pizza
    private bool isLevel1Complete = false; // Si le niveau est terminé

    void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'objet entrant en collision avec la boîte est le bon
        if (!isLevel1Complete && (collision.gameObject.name == targetName || collision.gameObject.name == targetName + "(Clone)"))
        {
            isLevel1Complete = true;
            Debug.Log("Niveau 1 terminé !"); // Message de débogage
            Debug.Log("Has tag: " + collision.gameObject.tag); // Message de débogage
        }
        
        else
        {
            Debug.Log($"Collision avec un objet non valide : {collision.gameObject.name}");
        }
    }
}
