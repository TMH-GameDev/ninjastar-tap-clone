using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab; // Assign the arrow prefab for downward arrow

    // When the arrow hits a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the arrow has entered the specified trigger collider
        if (other.CompareTag("ArrowTrigger")) // Make sure the trigger has this tag
        {
            Vector3 destroyedPosition = transform.position;

            // Destroy the current arrow
            Destroy(gameObject);

            // Spawn a new arrow falling down at the same position
            SpawnFallingArrow(destroyedPosition);
        }
    }

    private void SpawnFallingArrow(Vector3 spawnPosition)
    {
        // Instantiate a new arrow at the destroyed arrow's position
        Instantiate(arrowPrefab, spawnPosition, Quaternion.identity);

    }
}
