using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChestController : MonoBehaviour, IInteractable
{

    private SpriteRenderer _spriteRenderer;
    public Sprite openSprite;
    public ItemObject item;
    public int itemAmount;
    private ShowNewItem _floatingText;

    public void Interact()
    {
        OpenChest();
    }

    private void OpenChest()
    {
        _spriteRenderer.sprite = openSprite;
        _floatingText.ShowFloatingText();
        PlayerCharacterController.AddItemToPlayerInventoryStatic(item, itemAmount);
    }

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _floatingText = gameObject.GetComponentInChildren<ShowNewItem>();
    }
}
