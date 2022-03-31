using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterCollisionManager : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacterController playerCharacterController;
    internal readonly List<Collider2D> Colliders = new List<Collider2D>();
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Colliders.Contains(other) || !other.CompareTag("Interactable")) return;
        
        Colliders.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Colliders.Remove(other);
    }
}
