using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFallingArrows : MonoBehaviour
{
    public GameObject arrowBroken;
    
    // When the arrow hits a trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the arrow has entered the specified trigger collider
        if (collision.CompareTag("DestroyArrowTrigger")) // Make sure the trigger has this tag
        {
            // Destroy the current arrow
            Destroy(gameObject);
        }
    }
    
    void OnMouseDown()
    {
        // Instantiate a broken arrow at the current arrow's position
        Instantiate(arrowBroken, transform.position, Quaternion.identity);
        
        // Destroy the current arrow
        Destroy(gameObject);
    }
    
}
