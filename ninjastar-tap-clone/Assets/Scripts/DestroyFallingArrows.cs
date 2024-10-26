using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFallingArrows : MonoBehaviour
{
    // When the arrow hits a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the arrow has entered the specified trigger collider
        if (other.CompareTag("DestroyArrowTrigger")) // Make sure the trigger has this tag
        {
            // Destroy the current arrow
            Destroy(gameObject);
        }
    }
}
