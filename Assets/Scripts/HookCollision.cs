using UnityEngine;

public class HookCollision : MonoBehaviour
{
    public FireHarpoon fireHarpoonScript;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Harpon a touché : " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Ingredient"))
        {
            fireHarpoonScript?.AttachIngredient(collision.gameObject);
        }
        else
        {
            fireHarpoonScript?.ResetHook();
        }
    }
}