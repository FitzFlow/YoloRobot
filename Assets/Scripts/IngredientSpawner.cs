using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject ingredientPrefab;  
    public Transform spawnPoint;          

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnBoxClicked);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnBoxClicked);
    }

    private void OnBoxClicked(SelectEnterEventArgs args)
    {
        if (ingredientPrefab != null && spawnPoint != null)
        {
            Instantiate(ingredientPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
