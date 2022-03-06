using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public int numberOfColumns;
    public int ySpaceBetweenItems;
    public int xStart;
    public int yStart;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    private void Start()
    {
        CreateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(xStart, yStart +(-ySpaceBetweenItems * (i / numberOfColumns)), 0f);
    }

    private void UpdateDisplay()
    {
        for(int i = 0;i < inventory.container.Count; i++)
        {
            
            if (itemsDisplayed.ContainsKey(inventory.container[i]))
            {
                UpdateItemAmount(i); 
            }
            else
            {
                CreateItemAtPosition(i);
            }
        }
    }

    private void CreateItemAtPosition(int slotPosition)
    {
        var obj = Instantiate(inventory.container[slotPosition].item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(slotPosition);
        obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = inventory.container[slotPosition].amount.ToString("n0");
        obj.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = inventory.container[slotPosition].item.name;
        obj.GetComponent<OnMouseOverItem>().onHoverText = inventory.container[slotPosition].item.description;
        itemsDisplayed.Add(inventory.container[slotPosition],obj);
        
    }

    private void UpdateItemAmount(int slotPosition)
    {
        itemsDisplayed[inventory.container[slotPosition]].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = inventory.container[slotPosition].amount.ToString("n0");
    }

    private void CreateDisplay()
    {
        for (var i = 0; i < inventory.container.Count; i++)
        {
            CreateItemAtPosition(i);
        }
    }
}
