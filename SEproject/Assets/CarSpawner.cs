using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform player;      
    public float spawnRadius = 10f;  
    public float spawnInterval = 3f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCar), 0f, spawnInterval);
    }

    void SpawnCar()
    {
        if (carPrefabs.Length == 0) return;

        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        Vector3 randomPosition = player.position + Random.insideUnitSphere * spawnRadius;
        randomPosition.y = player.position.y;

        GameObject newCar = Instantiate(carPrefab, randomPosition, Quaternion.identity);

        // AssignRandomColor(newCar);
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
