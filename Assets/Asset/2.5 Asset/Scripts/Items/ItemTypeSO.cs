using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTypeSO", menuName = "ScriptableObject/Item/ItemTypesSO")]
public class ItemTypeSO : ScriptableObject
{
 	[SerializeField] private ItemInventoryActionType itemActionType ;
	[SerializeField] private ItemInventoryType itemType ;
	[SerializeField] private Color _typeColor ;


	public ItemInventoryType GetItemType()
    {
		return itemType ;
    }

	public ItemInventoryActionType GetItemActionType()
	{
		return itemActionType ;
	}

	public Color TypeColor => _typeColor; 

}

public enum ItemInventoryType
{
	Consumable,
	Miscellaneous,

}

public enum ItemInventoryActionType
{
	Use,
	DoNothing
}
