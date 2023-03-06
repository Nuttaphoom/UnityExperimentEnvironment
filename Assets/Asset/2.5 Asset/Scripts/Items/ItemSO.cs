using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObject/Items/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Header("UI Inventory Variable")]
    [SerializeField] private string _itemName = " " ;
    [TextArea]
    [SerializeField] private string _itemDescription = " ";
    [SerializeField] private Sprite _previewImage;

    [Header("Item Type SO : to determine what type of the item is.")]
    [SerializeField] private ItemTypeSO _itemType ;

    #region Getter 
    public string ItemName => _itemName;
    public string ItemDescription => _itemDescription; 

    public ItemTypeSO ItemType => _itemType;
    public Sprite PreviewImage => _previewImage;

    

    #endregion  

}

