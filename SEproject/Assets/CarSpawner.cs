using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform player;      
    public float minSpawnDistance = 5f;
    public float maxSpawnDistance = 10f;
    public float spawnInterval = 3f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCar), 0f, spawnInterval);
    }

    void SpawnCar()
    {
        if (carPrefabs.Length == 0) return;

        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = 0;

        Vector3 randomPosition = player.position + randomDirection * spawnDistance;

        GameObject newCar = Instantiate(carPrefab, randomPosition, Quaternion.identity);
    }

    void AssignRandomColor(GameObject car)
    {
        Renderer[] renderers = car.GetComponentsInChildren<Renderer>();

        Color randomColor = new Color(Random.value, Random.value, Random.value);

        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = randomColor;
        }
    }
}
