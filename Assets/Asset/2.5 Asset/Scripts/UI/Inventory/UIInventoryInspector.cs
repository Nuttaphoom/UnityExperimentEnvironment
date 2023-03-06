using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class UIInventoryInspector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemNameTMP;
    [SerializeField] private TextMeshProUGUI _itemDescriptionTMP;
    [SerializeField] private UIInventoryManager _uiInventoryManager;

    private ItemInventoryType _currentDisplayType ; 

    [Header("Listen to")]
    [SerializeField] ItemTypeEventChannel _changeDisplayItemType;

    private void OnEnable()
    {
        _uiInventoryManager.HighlightSlotEvent += ActiveInspector;
        _changeDisplayItemType._raiseEvent += ChangeDisplayType;
    }

    private void OnDisable()
    {
        _uiInventoryManager.HighlightSlotEvent -= ActiveInspector;
        _changeDisplayItemType._raiseEvent -= ChangeDisplayType;
    }

    private void ChangeDisplayType(ItemInventoryType type)
    {
        _currentDisplayType = type; 
        if (type == ItemInventoryType.Consumable)
        {

        }else if (type == ItemInventoryType.Miscellaneous)
        {

        }

        return;
    }

    public void ActiveInspector(ItemSO _itemSO)
    {
        if (_currentDisplayType == ItemInventoryType.Consumable || _currentDisplayType == ItemInventoryType.Miscellaneous)
        {
            _itemNameTMP.text = _itemSO.ItemName;
            _itemDescriptionTMP.text = _itemSO.ItemDescription;
        }
    }
}
