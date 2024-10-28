using UnityEngine;
using System.Collections;

public class SpawnArrows : MonoBehaviour
{
    public GameObject incomingArrow;  // The arrow prefab
    public Transform[] animals;       // Array of positions for arrow spawns (animal objects)

    private float spawnDelay = 2.0f;  // Initial delay between spawns
    private float minSpawnDelay = 0.75f; // Minimum allowed spawn delay
    private int rounds = 1;  // Tracks the current round, starting with 1
    private float roundDelay = 3.5f; // Delay between rounds
    private int arrowsToSpawn = 5; // Number of arrows to spawn in the current round
    private bool usePattern = false;  // Toggle between random and pattern-based spawning
    private int patternIndex = 0;  // Used to track the sequential pattern
    private int currentPattern = 0; // Tracks the active pattern type

    void Start()
    {
        StartCoroutine(SpawnMoreArrows());
    }

    private IEnumerator SpawnMoreArrows()
    {
        while (true)
        {
            for (int i = 0; i < arrowsToSpawn; i++)
            {
                // Switch between pattern-based and random-based spawns
                if (usePattern)
                {
                    Transform chosenAnimal = null;

                    switch (currentPattern)
                    {
                        case 0:
                            // Pattern 1-6: Sequentially from positions 1-6
                            chosenAnimal = animals[patternIndex];
                            patternIndex = (patternIndex + 1) % animals.Length;

                            if (patternIndex == 0)
                                usePattern = false;
                            break;

                        case 1:
                            // Pattern 6-1: Sequentially from positions 6 to 1
                            chosenAnimal = animals[animals.Length - 1 - patternIndex];
                            patternIndex = (patternIndex + 1) % animals.Length;

                            if (patternIndex == 0)
                                usePattern = false;
                            break;

                        case 2:
                            // Pattern: Same target twice
                            chosenAnimal = animals[patternIndex];
                            for (int j = 0; j < 2; j++)
                            {
                                Instantiate(incomingArrow, chosenAnimal.position, Quaternion.identity);
                                yield return new WaitForSeconds(spawnDelay); // Wait before spawning again
                            }
                            patternIndex = (patternIndex + 1) % animals.Length;
                            if (patternIndex == 0)
                                usePattern = false;
                            break;

                        case 3:
                            // Pattern: Same target three times
                            chosenAnimal = animals[patternIndex];
                            for (int j = 0; j < 3; j++)
                            {
                                Instantiate(incomingArrow, chosenAnimal.position, Quaternion.identity);
                                yield return new WaitForSeconds(spawnDelay);
                            }
                            patternIndex = (patternIndex + 1) % animals.Length;
                            if (patternIndex == 0)
                                usePattern = false;
                            break;

                        case 4:
                            // Pattern: Adjacent animals 2 times (e.g., 1 & 2, 2 & 3, 3 & 4)
                            int adjacentIndex1 = patternIndex % (animals.Length - 1);
                            int adjacentIndex2 = adjacentIndex1 + 1;

                            // First, spawn an arrow at the first adjacent animal.
                            Instantiate(incomingArrow, animals[adjacentIndex1].position, Quaternion.identity);
                            yield return new WaitForSeconds(spawnDelay);

                            // Then, spawn an arrow at the second adjacent animal.
                            Instantiate(incomingArrow, animals[adjacentIndex2].position, Quaternion.identity);
                            yield return new WaitForSeconds(spawnDelay);

                            patternIndex++;

                            // If all pairs have been used, reset and exit the pattern.
                            if (patternIndex >= animals.Length - 1)
                            {
                                patternIndex = 0;
                                usePattern = false;
                            }
                            break;
                    }

                    if (!chosenAnimal || currentPattern >= 2) continue;
                    Vector2 spawnPosition = chosenAnimal.position;
                    Instantiate(incomingArrow, spawnPosition, Quaternion.identity);
                    // Ensure delay between each arrow
                }
                else
                {
                    // Random-based: Spawn arrows at random positions
                    int randomIndex = Random.Range(0, animals.Length);
                    Transform chosenAnimal = animals[randomIndex];
                    Vector2 spawnPosition = chosenAnimal.position;
                    Instantiate(incomingArrow, spawnPosition, Quaternion.identity);

                    // After some random spawns, switch to pattern-based spawns
                    if (Random.Range(0, 10) > 7)
                    {
                        usePattern = true;
                        currentPattern = Random.Range(0, 6);
                        patternIndex = 0;
                    }
                }

                yield return new WaitForSeconds(spawnDelay); // Ensure delay between each arrow
            }

            // 1-second delay before starting the next round of arrows
            yield return new WaitForSeconds(roundDelay);

            // Increase the round and update the arrows to spawn
            rounds++;
            arrowsToSpawn = rounds * 5; // 5 arrows for round 1, 10 arrows for round 2, etc.

            // Decrease the spawn delay per round but do not go below the minimum
            spawnDelay = Mathf.Max(minSpawnDelay, spawnDelay - 0.05f);
        }
    }
}

