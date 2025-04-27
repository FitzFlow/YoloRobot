using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PizzaIngredientCatcher : MonoBehaviour
{
    [SerializeField] private float forcedYCorrection = -0.04f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.gameObject)
            return;

        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grabInteractable == null)
            return;

        if (other.CompareTag("Ingredient"))
        {
            grabInteractable.selectExited.AddListener((SelectExitEventArgs args) =>
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    Destroy(rb); // Supprimer le Rigidbody pour arrêter la physique
                }

                Vector3 worldPos = other.transform.position;
                other.transform.SetParent(this.transform, true);

                worldPos.y += forcedYCorrection;
                other.transform.position = worldPos;

                other.transform.localRotation = Quaternion.Euler(90f, 0f, 0f); // A plat

                // 🔥 Correction clé : Freeze la position et rotation
                other.transform.localPosition = new Vector3(other.transform.localPosition.x, other.transform.localPosition.y, other.transform.localPosition.z);
                other.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            });

            grabInteractable.selectEntered.AddListener((SelectEnterEventArgs args) =>
            {
                // Lorsqu'on reprend, recréer un Rigidbody propre
                if (other.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody newRb = other.gameObject.AddComponent<Rigidbody>();
                    newRb.mass = 1f;
                    newRb.linearDamping = 0f;
                    newRb.angularDamping = 0.05f;
                    newRb.useGravity = true;
                    newRb.isKinematic = false;
                    newRb.collisionDetectionMode = CollisionDetectionMode.Discrete;
                }

                other.transform.SetParent(null, true);

                // Rotation normale debout
                other.transform.rotation = Quaternion.identity;
            });
        }
        else
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                rb.AddForce(pushDirection * 3f, ForceMode.Impulse);
            }
        }
    }
}
