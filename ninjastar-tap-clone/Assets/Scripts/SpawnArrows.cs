using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrows : MonoBehaviour
{
    public GameObject incomingArrow;  // The arrow prefab
    public Transform[] animals;       // Array of positions for arrow spawns (animal objects)

    private float spawnDelay = 2.0f;  // Initial delay between spawns
    private float minSpawnDelay = 0.2f; // Minimum allowed spawn delay
    private float delayReductionRate = 0.05f; // The amount to reduce the delay each time
    private bool usePattern = false;  // Toggle between random and pattern-based spawning

    void Start()
    {
        StartCoroutine(SpawnMoreArrows());
    }

    private IEnumerator SpawnMoreArrows()
    {
        int patternIndex = 0;  // Used to track the sequential pattern

        while (true)
        {
            // Switch between pattern-based and random-based spawns
            if (usePattern)
            {
                // Pattern-based: Spawn arrows sequentially from positions 1-6
                Transform chosenAnimal = animals[patternIndex];
                Vector2 spawnPosition = chosenAnimal.position;
                Instantiate(incomingArrow, spawnPosition, Quaternion.identity);

                // Move to the next pattern index
                patternIndex = (patternIndex + 1) % animals.Length;

                // Once the pattern is complete (1-6), switch back to random
                if (patternIndex == 0)
                {
                    usePattern = false;
                }
            }
            else
            {
                // Random-based: Spawn arrows at random positions
                int randomIndex = Random.Range(0, animals.Length);
                Transform chosenAnimal = animals[randomIndex];
                Vector2 spawnPosition = chosenAnimal.position;
                Instantiate(incomingArrow, spawnPosition, Quaternion.identity);

                // After some random spawns, switch to pattern-based spawns
                if (Random.Range(0, 10) > 7)  // Adjust frequency of pattern vs random
                {
                    usePattern = true;
                }
            }

            // Reduce the spawn delay over time but never go below the minimum
            spawnDelay = Mathf.Max(minSpawnDelay, spawnDelay - delayReductionRate);

            // Wait for the reduced delay before spawning the next arrow
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
