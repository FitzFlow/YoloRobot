using UnityEngine;

public class PizzaController : MonoBehaviour
{
    public enum PizzaState
    {
        DoughBall,
        DoughFlat,
        DoughWithSauce,
        DoughWithSauceCheese
    }

    public PizzaState currentState = PizzaState.DoughBall;
    public GameObject doughFlatPrefab;
    public GameObject doughWithSaucePrefab;
    public GameObject doughWithSauceCheesePrefab;

    private bool isTransforming = false;

    public void HandleInteraction(string toolTag)
    {
        if (isTransforming) return;

        switch (currentState)
        {
            case PizzaState.DoughBall:
                if (toolTag == "RollingPin" && doughFlatPrefab != null)
                    TransformTo(doughFlatPrefab, PizzaState.DoughFlat);
                break;

            case PizzaState.DoughFlat:
                if (toolTag == "Sauce" && doughWithSaucePrefab != null)
                    TransformTo(doughWithSaucePrefab, PizzaState.DoughWithSauce);
                break;

            case PizzaState.DoughWithSauce:
                if (toolTag == "Cheese" && doughWithSauceCheesePrefab != null)
                    TransformTo(doughWithSauceCheesePrefab, PizzaState.DoughWithSauceCheese);
                break;

            case PizzaState.DoughWithSauceCheese:
                // Dernier état : ne rien faire
                break;
        }
    }

    private void TransformTo(GameObject nextPrefab, PizzaState nextState)
    {
        isTransforming = true;

        GameObject newStage = Instantiate(nextPrefab, transform.position, Quaternion.identity);

        // Choisir la bonne scale selon l'étape
        if (nextState == PizzaState.DoughFlat)
            newStage.transform.localScale = new Vector3(30f, 30f, 30f); // Grand (aplati)
        else
            newStage.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); // Petit (sauce, fromage)

        // Fixer rotation (à plat)
        newStage.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

        Destroy(gameObject);
    }


}
