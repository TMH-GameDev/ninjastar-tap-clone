using UnityEngine;

public class ClickToDestroy : MonoBehaviour
{
    private Animator animator;
    private bool isBeingDestroyed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        if (!isBeingDestroyed)
        {
            isBeingDestroyed = true;
            // Trigger the destroy animation
            animator.SetTrigger("Destroy"); // Ensure you have a trigger named "Destroy" in your Animator
        }
    }

    // This method should be called at the end of the destroy animation
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}