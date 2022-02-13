using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour,IInteractable
{

    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public ItemObject Item;
    public int ItemAmount;

    public void Interact()
    {
        OpenChest();
    }

    public void OpenChest()
    {
        spriteRenderer.sprite = openSprite;
        PlayerCharacterController.AddItemToPlayerInventoryStatic(Item, ItemAmount);
    }
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
}
