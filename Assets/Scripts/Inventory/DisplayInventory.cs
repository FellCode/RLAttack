using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public int NUMBER_OF_COLUMNS;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int X_START;
    public int Y_START;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    void Start()
    {
        CreateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START, Y_START +(-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMNS)), 0f);
    }

    public void UpdateDisplay()
    {
        for(int i = 0;i < inventory.Container.Count; i++)
        {
            
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                UpdateItemAmount(i); 
            }
            else
            {
                createItemAtPosition(i);
            }
        }
    }

    private void createItemAtPosition(int slotPosition)
    {
        var obj = Instantiate(inventory.Container[slotPosition].item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(slotPosition);
        obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = inventory.Container[slotPosition].amount.ToString("n0");
        obj.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = inventory.Container[slotPosition].item.name;
        obj.GetComponent<OnMouseOverItem>().onHoverText = inventory.Container[slotPosition].item.description;
        itemsDisplayed.Add(inventory.Container[slotPosition],obj);
        
    }

    private void UpdateItemAmount(int slotPosition)
    {
        itemsDisplayed[inventory.Container[slotPosition]].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = inventory.Container[slotPosition].amount.ToString("n0");
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            createItemAtPosition(i);
        }
    }
}
