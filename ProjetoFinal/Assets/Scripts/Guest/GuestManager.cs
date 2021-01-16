using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuestManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject guestPrefab;
    [SerializeField] private Zone spawnZone;
    [SerializeField] [Tooltip("X: Min | Y: Max")] private Vector2 spawnRate;
    private readonly List<GameObject> spawnedGuests = new List<GameObject>();
    
    private void Start()
    {
        StartCoroutine(SpawnGuest());
    }
    
    private IEnumerator SpawnGuest()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnRate.x, spawnRate.y));
            GameObject newGuest = Instantiate(guestPrefab, spawnZone.EntryWaypoint, Quaternion.identity);
            spawnedGuests.Add(newGuest);
        }
    }
}