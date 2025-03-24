using UnityEngine;

public class HookCollision : MonoBehaviour
{
    public FireHarpoon fireHarpoonScript;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Harpon a touché : " + collision.gameObject.name);

        // Appeler la méthode ResetHook du script FireHarpoon
        if (fireHarpoonScript != null)
        {
            fireHarpoonScript.ResetHook();
        }
    }
}
