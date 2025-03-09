using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Array to hold different car prefabs
    public Transform spawnPoint; // The location where cars will spawn
    public float spawnInterval = 3f; // Time between spawns

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCar), 0f, spawnInterval); // Spawn cars periodically
    }

    void SpawnCar()
    {
        if (carPrefabs.Length == 0) return;

        // Choose a random car prefab
        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        // Instantiate the car at the spawn point
        GameObject newCar = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

        // Assign a random color to the car
        AssignRandomColor(newCar);
    }

    void AssignRandomColor(GameObject car)
    {
        Renderer[] renderers = car.GetComponentsInChildren<Renderer>(); // Get all renderers

        Color randomColor = new Color(Random.value, Random.value, Random.value); // Generate a random color

        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = randomColor; // Apply color to all renderers
        }
    }
}
