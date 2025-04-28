using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FireHarpoon : MonoBehaviour
{
    public Transform leftHandTransform;    // ←– add this so you can drag your left‐hand controller here
    public InputActionReference RightTriggerController;
    public GameObject hook;
    public float launchSpeed = 20f;
    public float maxDistance = 50f;
    public float cooldownDuration = 2f;

    private Vector3 initialHookPosition;
    private bool isFiring = false;
    private bool isOnCooldown = false;
    private Rigidbody hookRigidbody;
    private TrailRenderer trailRenderer;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool isHeld = false;

    void Start()
    {
        HookCollision hookCollision = hook.GetComponent<HookCollision>();
        if (hookCollision == null)
        {
            hookCollision = hook.AddComponent<HookCollision>();
        }
        hookCollision.fireHarpoonScript = this;

        if (RightTriggerController == null || RightTriggerController.action == null)
        {
            Debug.LogError("RightTriggerController n'est pas assigné !");
        }

        if (hook == null)
        {
            Debug.LogError("Hook n'est pas assigné !");
        }
        else
        {
            initialHookPosition = hook.transform.localPosition;

            trailRenderer = hook.GetComponent<TrailRenderer>();
            if (trailRenderer == null)
            {
                trailRenderer = hook.AddComponent<TrailRenderer>();
                ConfigureTrailRenderer(trailRenderer);
            }

            trailRenderer.enabled = false;
        }

        hookRigidbody = hook.GetComponent<Rigidbody>();
        if (hookRigidbody == null)
        {
            hookRigidbody = hook.AddComponent<Rigidbody>();
            hookRigidbody.useGravity = false;
        }
        hookRigidbody.isKinematic = true;

        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    void OnReleased(SelectExitEventArgs args)
    {
        isHeld = false;
    }

    void Update()
    {
        if (RightTriggerController == null || RightTriggerController.action == null)
        {
            Debug.LogError("RightTriggerController n'est pas assigné !");
            return;
        }

        float triggerValue = RightTriggerController.action.ReadValue<float>();

        if (isHeld && triggerValue > 0.5f && !isFiring && !isOnCooldown)
        {
            isFiring = true;

            Vector3 firingDirection = transform.right;

            hook.transform.parent = null;

            Rigidbody hookRb = hook.GetComponent<Rigidbody>();
            hookRb.isKinematic = false;
            hookRb.linearVelocity = firingDirection * launchSpeed;

            if (trailRenderer != null)
                trailRenderer.enabled = true;
        }

        if (isFiring)
        {
            MoveHookForward();
        }
    }

    void MoveHookForward()
    {
        if (hook != null && isFiring)
        {
            float distanceTraveled = Vector3.Distance(initialHookPosition, hook.transform.position);
            if (distanceTraveled >= maxDistance)
            {
                ResetHook();
                StartCooldown();
            }
        }
    }

    public void ResetHook()
    {
        Debug.Log("Resetting Hook");

        hook.transform.parent = transform;
        hook.transform.localPosition = initialHookPosition;
        hook.transform.localRotation = Quaternion.identity;

        Rigidbody hookRb = hook.GetComponent<Rigidbody>();
        hookRb.isKinematic = true;
        hookRb.linearVelocity = Vector3.zero;

        isFiring = false;

        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
            trailRenderer.Clear();
        }

        // Detach any Ingredient still stuck on the hook
        foreach (Transform child in hook.transform)
        {
            if (child.CompareTag("Ingredient"))
            {
                child.SetParent(null);
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = false;
            }
        }
    }

    // ←––– Add this new method
    public void AttachIngredient(GameObject ingredient)
    {
        Rigidbody ingRb = ingredient.GetComponent<Rigidbody>();
        if (ingRb != null)
            ingRb.isKinematic = true;

        // choose parent: left hand if assigned, otherwise hook
        Transform parent = leftHandTransform != null ? leftHandTransform : hook.transform;
        ingredient.transform.SetParent(parent);
        ingredient.transform.localPosition = Vector3.zero;
        ingredient.transform.localRotation = Quaternion.identity;
    }


    void StartCooldown()
    {
        Debug.Log("Starting Cooldown");
        isOnCooldown = true;
        Invoke(nameof(EndCooldown), cooldownDuration);
    }

    void EndCooldown()
    {
        Debug.Log("Cooldown Ended");
        isOnCooldown = false;
    }

    void ConfigureTrailRenderer(TrailRenderer trail)
    {
        trail.time = 0.5f;
        trail.startWidth = 0.1f;
        trail.endWidth = 0.05f;

        Material trailMaterial = new Material(Shader.Find("Sprites/Default"));
        trailMaterial.color = Color.white;

        trail.material = trailMaterial;
    }
}
