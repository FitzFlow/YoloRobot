using UnityEngine;

public class PizzaInteraction : MonoBehaviour
{
    public PizzaMaker pizzaMaker; // Référence au script PizzaMaker
    public enum InteractionType { Roll, Sauce, Cheese }
    public InteractionType interactionType;

    private void OnTriggerEnter(Collider other)
    {
        switch (interactionType)
        {
            case InteractionType.Roll:
                if (other.CompareTag("RollingPin"))
                {
                    pizzaMaker.FlattenDough();
                }
                break;

            case InteractionType.Sauce:
                if (other.CompareTag("Sauce"))
                {
                    pizzaMaker.AddSauce();
                }
                break;

            case InteractionType.Cheese:
                if (other.CompareTag("Cheese"))
                {
                    pizzaMaker.AddCheese();
                }
                break;
        }
    }
}
