using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public Sprite openSprite;

    public void OpenChest()
    {
        spriteRenderer.sprite = openSprite;

        //Item in Inventar legen
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
