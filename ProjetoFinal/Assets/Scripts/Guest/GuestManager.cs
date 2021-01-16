using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GuestManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject guestPrefab;
    [SerializeField] private GameObject specialGuestPrefab;
    [SerializeField] private Zone spawnZone;
    [SerializeField] [Tooltip("X: Min | Y: Max")] private Vector2 spawnRate;
    private readonly List<GameObject> spawnedGuests = new List<GameObject>();
    
    [Header("Special Guests Settings")]
    [SerializeField] private bool canSpawnSpecial;
    [SerializeField] private int maxNumSpecial;
    private int nSpecialGuests;
    
    private void Start()
    {
        StartCoroutine(SpawnGuest());
    }
    
    private IEnumerator SpawnGuest()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnRate.x, spawnRate.y));
            
            int i = 0;
            if (canSpawnSpecial && nSpecialGuests < maxNumSpecial) i = Random.Range(0, 2);
            
            switch (i)
            {
                case 0:
                    GameObject normalGuest = Instantiate(guestPrefab, spawnZone.EntryWaypoint, Quaternion.identity);
                    spawnedGuests.Add(normalGuest);
                    break;
                case 1:
                    GameObject specialGuest = Instantiate(specialGuestPrefab, spawnZone.EntryWaypoint, Quaternion.identity);
                    spawnedGuests.Add(specialGuest);
                    nSpecialGuests++;
                    break;
            }
        }
    }
    
    public List<GuestStatus> GetAllGuests()
    {
        List<GuestStatus> guests = new List<GuestStatus>();
        
        foreach (var guest in spawnedGuests)
        {
            guests.Add(guest.GetComponent<GuestStatus>());
        }
        
        return guests;
    }
}