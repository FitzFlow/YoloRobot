using UnityEngine;

public class HookCollision : MonoBehaviour
{
    public FireHarpoon fireHarpoonScript;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Harpon a touch� : " + collision.gameObject.name);

        // Appeler la m�thode ResetHook du script FireHarpoon
        if (fireHarpoonScript != null)
        {
            fireHarpoonScript.ResetHook();
        }
    }
}
