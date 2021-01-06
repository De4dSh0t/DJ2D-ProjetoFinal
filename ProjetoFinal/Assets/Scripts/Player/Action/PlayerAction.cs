using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAction : MonoBehaviour
{
    [Header("Player Info Settings")]
    [SerializeField] protected PlayerInfo playerInfo;
    
    // Collider Settings
    protected Collider2D pCollider;
    
    protected GameObject GetClosestObject(ContactFilter2D filter)
    {
        // Save all the colliders that the entity is currently colliding with
        List<Collider2D> contacts = new List<Collider2D>();
        pCollider.OverlapCollider(filter, contacts);
        
        // Returns null if no collider has been detected
        if (contacts.Count <= 0) return null;
        
        // This is used as a reference to calculate the closest object
        int iClosest = 0;
        ColliderDistance2D closest = contacts[0].Distance(pCollider);
        
        // Calculates the closest object
        for (int i = 0; i < contacts.Count; i++)
        {
            ColliderDistance2D temp = contacts[i].Distance(pCollider);
            
            if (temp.distance < closest.distance)
            {
                closest = temp;
                iClosest = i;
            }
        }
        
        // Returns the closest garbage object
        return contacts[iClosest].gameObject;
    }
}