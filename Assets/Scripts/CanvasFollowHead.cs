using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CanvasFollowHead : MonoBehaviour
{
    public Transform head; // Ta caméra XR (souvent XR Origin > Camera Offset > Main Camera)
    public float distance = 2f;
    public float heightOffset = 0f;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (!head) return;

        // Position cible devant la tête
        Vector3 targetPos = head.position + head.forward * distance + Vector3.up * heightOffset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);

        // Toujours orienté vers la tête
        transform.LookAt(head);
        transform.Rotate(0, 180f, 0); // Pour qu’il soit bien face au joueur
    }
}
