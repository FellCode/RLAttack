using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName ="Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    public ItemDatabaseObject database;
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(ItemObject item, int amount)
    {
        foreach (var inventorySlot in container.Where(inventorySlot => inventorySlot.item == item))
        {
            inventorySlot.AddAmount(amount);
            return;
        }

        container.Add(new InventorySlot(database.GetId[item],item, amount));
    }

    public void Safe()
    {
        var saveData = JsonUtility.ToJson(this, true);
        var bf = new BinaryFormatter();
        var file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        
        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Save Stuff");
    }

    
    public void Load()
    {
        if (!File.Exists(string.Concat(Application.persistentDataPath, savePath))) return;
        
        var bf = new BinaryFormatter();
        var file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
        JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
        file.Close();
        Debug.Log("Load Stuff");
    }

    public void OnAfterDeserialize()
    {
        foreach (var inventorySlot in container)
            inventorySlot.item = database.GetItem[inventorySlot.id];
    }

    public void OnBeforeSerialize()
    {
        
    }
}

[System.Serializable]
public class InventorySlot
{
    public int id;
    public ItemObject item;
    public int amount;
    public InventorySlot(int id,ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
        this.id = id;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
