using UnityEngine;

public class FollowMouseX : MonoBehaviour
{
    [Range(0f, 1f)]
    public float fixedYPercentage = 0.5f; // A value between 0 (bottom of the screen) and 1 (top of the screen)
    public Vector3 offset; // Offset from the mouse position on the X-axis
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the object
        animator = GetComponent<Animator>();
        
        // Set the initial Y position relative to the screen size
        UpdateFixedYPosition();
    }

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Set the object's position, locking the Y value and adding the offset on X
        transform.position = new Vector3(mousePosition.x + offset.x, transform.position.y, transform.position.z);

        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickAnimation();
        }
    }

    void PlayClickAnimation()
    {
        if (animator)
        {
            animator.SetTrigger("PlayClick"); // Make sure you have a trigger parameter named "PlayClick" in the Animator
        }
    }

    void UpdateFixedYPosition()
    {
        // Get the desired Y position as a percentage of the screen height
        float screenHeight = Camera.main.orthographicSize * 2f; // Orthographic camera size in world units
        float worldYPosition = (fixedYPercentage - 0.5f) * screenHeight; // Offset by -0.5 to center

        // Set the Y position of the object based on this percentage
        transform.position = new Vector3(transform.position.x, worldYPosition, transform.position.z);
    }

    void OnValidate()
    {
        // Update the Y position when changing the percentage in the editor
        UpdateFixedYPosition();
    }
}