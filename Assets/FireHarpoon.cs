using UnityEngine;
using UnityEngine.InputSystem;

public class FireHarpoon : MonoBehaviour
{
    public InputActionReference RightTriggerController;
    public GameObject hook;
    public float moveSpeed = 5f; // Vitesse du déplacement

    void Start()
    {
        if (RightTriggerController == null || RightTriggerController.action == null)
        {
            Debug.LogError("RightTriggerController n'est pas assigné !");
        }
    }

    void Update()
    {
        if (RightTriggerController == null || RightTriggerController.action == null)
        {
            Debug.LogError("RightTriggerController n'est pas assigné !");
            return;
        }

        float triggerValue = RightTriggerController.action.ReadValue<float>();

        if (triggerValue > 0.5)
        {
            MoveHook();
        }
    }

    void MoveHook()
    {
        Debug.Log("Moving Hook");
        if (hook == null)
        {
            hook = GameObject.Find("Hook"); // Make sure the name matches exactly
            Debug.Log("Hook found!");
        }

        if (hook == null)
        {
            Debug.LogError("Hook not found! Assign it in the Inspector or check the name.");
        }
    }
}
