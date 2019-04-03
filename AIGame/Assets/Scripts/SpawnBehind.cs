using UnityEngine;

public class SpawnBehind : MonoBehaviour
{
  //  public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 0f;            // How long between each spawn.
    public Transform[] spawnPoints;              // An array of the spawn points this enemy can spawn from
    public bool friendSpawned = false;


    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        if (friendSpawned == false)
        {
            Invoke("Spawn", spawnTime);
            friendSpawned = true;
        }

    }


    void Spawn()
    {
        

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}