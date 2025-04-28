using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class PizzaIngredientCatcher : MonoBehaviour
{
    [SerializeField] private float forcedYCorrection = -0.04f;
    private List<string> attachedIngredientNames = new List<string>();
    // public bool isGrilled { get; set; } = false;  // Add this line
    private Dictionary<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable, (UnityAction<SelectExitEventArgs>, UnityAction<SelectEnterEventArgs>)> interactableListeners =
        new Dictionary<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable, (UnityAction<SelectExitEventArgs>, UnityAction<SelectEnterEventArgs>)>();

    private float logTimer = 0f;
    private const float LOG_INTERVAL = 10.0f;

    public List<string> GetAttachedIngredientNames()
    {
        return new List<string>(attachedIngredientNames);
    }

    public bool HasIngredient(string ingredientName)
    {
        return attachedIngredientNames.Contains(ingredientName) || attachedIngredientNames.Contains(ingredientName + "(Clone)");
    }

    public bool HasAllIngredients(List<string> requiredIngredients)
    {
        if (requiredIngredients == null || requiredIngredients.Count == 0)
        {
            return true;
        }
        return requiredIngredients.All(req => HasIngredient(req));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.gameObject)
            return;

        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable == null)
            return;

        if (other.CompareTag("Ingredient"))
        {
            if (interactableListeners.ContainsKey(grabInteractable))
            {
                return;
            }

            UnityAction<SelectExitEventArgs> onSelectExited = null;
            onSelectExited = (args) =>
            {
                Collider pizzaCollider = GetComponent<Collider>();
                if (pizzaCollider != null && !pizzaCollider.bounds.Intersects(other.bounds))
                {
                     return;
                }

                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Destroy(rb);
                }

                other.transform.SetParent(this.transform, true);

                Vector3 localPos = other.transform.localPosition;
                localPos.y += forcedYCorrection;
                other.transform.localPosition = localPos;
                other.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                if (!attachedIngredientNames.Contains(other.gameObject.name))
                {
                    attachedIngredientNames.Add(other.gameObject.name);
                }
            };

            UnityAction<SelectEnterEventArgs> onSelectEntered = null;
            onSelectEntered = (args) =>
            {
                attachedIngredientNames.Remove(other.gameObject.name);

                if (other.transform.parent == this.transform)
                {
                     other.transform.SetParent(null, true);
                }

                if (other.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody newRb = other.gameObject.AddComponent<Rigidbody>();
                    newRb.mass = 1f;
                    newRb.useGravity = true;
                    newRb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                }
                 other.transform.rotation = Quaternion.identity;
            };

            grabInteractable.selectExited.AddListener(onSelectExited);
            grabInteractable.selectEntered.AddListener(onSelectEntered);

            interactableListeners.Add(grabInteractable, (onSelectExited, onSelectEntered));
        }
        else
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                rb.AddForce(pushDirection * 1.5f, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable != null && interactableListeners.ContainsKey(grabInteractable))
        {
            var listeners = interactableListeners[grabInteractable];
            grabInteractable.selectExited.RemoveListener(listeners.Item1);
            grabInteractable.selectEntered.RemoveListener(listeners.Item2);
            interactableListeners.Remove(grabInteractable);
        }
    }

    private void OnDestroy()
    {
        foreach (var kvp in interactableListeners)
        {
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable = kvp.Key;
            var listeners = kvp.Value;

            if (interactable != null)
            {
                interactable.selectExited.RemoveListener(listeners.Item1);
                interactable.selectEntered.RemoveListener(listeners.Item2);
            }
        }
        interactableListeners.Clear();
    }

    void Update()
    {
        logTimer += Time.deltaTime;
        if (logTimer >= LOG_INTERVAL)
        {
            logTimer -= LOG_INTERVAL;
            if (attachedIngredientNames.Count > 0)
            {
                Debug.Log($"Current attached ingredients: [{string.Join(", ", attachedIngredientNames)}]");
            }
            else
            {
                 Debug.Log("No ingredients currently attached.");
            }
        }
    }
}