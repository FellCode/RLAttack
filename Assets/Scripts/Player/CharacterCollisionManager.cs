using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterCollisionManager : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacterController playerCharacterController;
    internal readonly List<Collider2D> _colliders = new List<Collider2D>();
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_colliders.Contains(other) || !other.CompareTag("Interactable")) return;
        
        _colliders.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        _colliders.Remove(other);
    }
}
