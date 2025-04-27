using UnityEngine;

public class PizzaInteraction : MonoBehaviour
{
    private PizzaController pizzaController;

    private void Awake()
    {
        pizzaController = GetComponentInParent<PizzaController>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger touché par : {other.gameObject.name} avec tag {other.tag}"); // teste

        if (pizzaController == null) return;

        if (other.CompareTag("RollingPin") || other.CompareTag("Sauce") || other.CompareTag("Cheese"))
        {
            pizzaController.HandleInteraction(other.tag);
        }
    }
}
