using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit; // Ajout de l'espace de noms pour XR Interaction Toolkit


public class FireHarpoon : MonoBehaviour
{
    public InputActionReference RightTriggerController; // Input action for the trigger
    public GameObject hook; // Reference to the hook GameObject
    public float launchSpeed = 20f; // Speed at which the hook moves
    public float maxDistance = 50f; // Maximum distance the hook can travel
    public float cooldownDuration = 2f; // Cooldown time between shots

    private Vector3 initialHookPosition; // Initial position of the hook
    private bool isFiring = false; // Whether the hook is currently being fired
    private bool isOnCooldown = false; // Whether firing is on cooldown
    private Vector3 firingDirection;
    private Rigidbody hookRigidbody;

    private TrailRenderer trailRenderer; // Trail effect for the hook

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
            // Save the initial position of the hook for resetting later
            initialHookPosition = hook.transform.localPosition;

            // Get or add a TrailRenderer component to the hook
            trailRenderer = hook.GetComponent<TrailRenderer>();
            if (trailRenderer == null)
            {
                trailRenderer = hook.AddComponent<TrailRenderer>();
                ConfigureTrailRenderer(trailRenderer);
            }

            // Ensure trail starts disabled
            trailRenderer.enabled = false;
        }
        hookRigidbody = hook.GetComponent<Rigidbody>();
        if (hookRigidbody == null)
        {
            hookRigidbody = hook.AddComponent<Rigidbody>();
            hookRigidbody.useGravity = false; // Désactiver la gravité si nécessaire
        }
        hookRigidbody.isKinematic = true; // Le rendre cinématique par défaut
    }

    void Update()
    {
        if (RightTriggerController == null || RightTriggerController.action == null)
        {
            Debug.LogError("RightTriggerController n'est pas assigné !");
            return;
        }

        float triggerValue = RightTriggerController.action.ReadValue<float>();

        // Fire the hook when the trigger is pressed beyond a threshold and cooldown is not active
        if (triggerValue > 0.5f && !isFiring && !isOnCooldown)
        {
            isFiring = true;

            // Stocker la direction initiale du tir
            Vector3 firingDirection = transform.right;

            // Détacher le harpon du parent
            hook.transform.parent = null;

            // Configurer le Rigidbody pour le mouvement indépendant
            Rigidbody hookRb = hook.GetComponent<Rigidbody>();
            hookRb.isKinematic = false;
            hookRb.linearVelocity = firingDirection * launchSpeed;

            // Activer le trail
            if (trailRenderer != null)
                trailRenderer.enabled = true;
        }

        // Move the hook forward if it's firing
        if (isFiring)
        {
            MoveHookForward();
        }
    }

    void MoveHookForward()
    {
        if (hook != null && isFiring)
        {
            // Move the hook along its local X-axis direction
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

        // Rattacher le harpon au pistolet
        hook.transform.parent = transform;

        // Réinitialiser la position
        hook.transform.localPosition = initialHookPosition;
        hook.transform.localRotation = Quaternion.identity;

        // Réinitialiser la physique
        Rigidbody hookRb = hook.GetComponent<Rigidbody>();
        hookRb.isKinematic = true;
        hookRb.linearVelocity = Vector3.zero;

        isFiring = false;

        // Désactiver le trail
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
            trailRenderer.Clear();
        }
    }

    void StartCooldown()
    {
        Debug.Log("Starting Cooldown");

        isOnCooldown = true;

        // Start cooldown timer
        Invoke(nameof(EndCooldown), cooldownDuration);
    }

    void EndCooldown()
    {
        Debug.Log("Cooldown Ended");

        isOnCooldown = false;
    }

    void ConfigureTrailRenderer(TrailRenderer trail)
    {
        trail.time = 0.5f; // Duration of the trail effect
        trail.startWidth = 0.1f; // Width at the start of the trail
        trail.endWidth = 0.05f; // Width at the end of the trail

        // Set a simple material for better visibility (you can replace this with your own material)
        Material trailMaterial = new Material(Shader.Find("Sprites/Default"));
        trailMaterial.color = Color.white;

        trail.material = trailMaterial;
    }
}
