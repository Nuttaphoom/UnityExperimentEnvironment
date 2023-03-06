using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITypeSort : MonoBehaviour
{
    [SerializeField] ItemTypeEventChannel _changeDisplayItemType;
    public void SetItemInventoryType_Misc()
    {
        _changeDisplayItemType.RaiseEvent(ItemInventoryType.Miscellaneous); 
    }

    public void SetItemInventoryType_Consumable()
    {
        _changeDisplayItemType.RaiseEvent(ItemInventoryType.Consumable);
    }

}
