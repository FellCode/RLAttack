using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public ItemObject Item;
    public int ItemAmount;

    public void OpenChest(GameObject Player)
    {
        spriteRenderer.sprite = openSprite;
        Player.GetComponent<PlayerCharacterController>().Inventory.AddItem(Item, ItemAmount);
    }
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
}
