using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
[CreateAssetMenu(fileName = "Inventory SO", menuName = "ScriptableObject/InventorySO")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<ItemStack> _itemsStacks ; 

    public void AddItem(ItemSO item,int count)
    {
        if (count <= 0)
            return;

        for (int i = 0; i < _itemsStacks.Count; i++)
        {
            if (_itemsStacks[i].Item.ItemName == item.ItemName)
            {
                _itemsStacks[i].Quantity += count;
                return; 
            }
        }
        _itemsStacks.Add(new ItemStack(item, count));
    }

    public ItemStack GetItemStack(ItemSO itemSO)
    {
        foreach (ItemStack itemStack in _itemsStacks)
        {
            if (itemStack.Item.ItemName == itemSO.ItemName)
                return itemStack; 
        }

        return null;
    }
    public List<ItemStack> ItemStack => _itemsStacks;  

    
}

/*Contain Information about each item and its quantity*/
[Serializable]
public class ItemStack
{
    public ItemSO Item;
    public int Quantity = 0;

    public ItemStack(ItemSO item, int quantity = 1 )
    {
        Item = item ;
        Quantity = quantity;
    }

    public ItemStack(ItemStack itemStack)
    {
        Item = itemStack.Item             ;
        Quantity = itemStack.Quantity     ;
    }

    


}