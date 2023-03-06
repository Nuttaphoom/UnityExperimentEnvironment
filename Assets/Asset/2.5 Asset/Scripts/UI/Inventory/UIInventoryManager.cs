using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

/*This class responsible for manimulate visual of an inventory slot.*/
/*Any logic of remove, add, select, use should be put in Inventory Manager itself*/
public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private List<UIInventorySlot> _slots;
    [SerializeField] private GameObject _inventoryRenderer;

    [Header("Listen to")]
    [SerializeField] ItemTypeEventChannel _changeDisplayItemType;


    /*parameters to determine whether each item should be displayed in the inventory.*/
    private int _nextIndex = 0 ;
    private ItemInventoryType  _currentDisplayType  ;
    private InventorySO _currentInventorySO; 

    /*Listen by UIInventoryInspector, */
    /*Boardcast by UIInventorySlot*/
    public UnityAction<ItemSO> HighlightSlotEvent ; 


    private void OnEnable()
    {
        InventoryManager im = FindObjectOfType<InventoryManager>();
        im.UpdateInventoryUIEvent += FillInventoryUI ;
        im.OpenInventoryEvent += OpenInventory;
        im.CloseInventoryEvent += CloseInventory ;

        _changeDisplayItemType._raiseEvent +=  ChangeDisplayType ; 

        _currentDisplayType = ItemInventoryType.Consumable ; 

    }

    private void OnDisable()
    {
        InventoryManager im = FindObjectOfType<InventoryManager>();
        im.UpdateInventoryUIEvent -= FillInventoryUI ;
        im.OpenInventoryEvent -= OpenInventory;
        im.CloseInventoryEvent -= CloseInventory;

        _changeDisplayItemType._raiseEvent -= ChangeDisplayType;

    }
    public void FillInventoryUI(InventorySO _inventorySO = null )
    {
        if (_inventorySO == null)
            if (_currentInventorySO == null)
                return;
            else
            _inventorySO = _currentInventorySO;

        _currentInventorySO = _inventorySO; 
        ClearSlot();
        _nextIndex = 0;
        foreach (ItemStack itemStack in _inventorySO.ItemStack)
        {
            AddItem(itemStack);
        }
    }

 

    private void AddItem(ItemStack _itemStack)
    {
        if (_itemStack.Item.ItemType.GetItemType() != _currentDisplayType )
            return; 

        for (int i = 0 ;  i < _nextIndex; i++)
        {
            if (_slots[i].GetCurrentItem.ItemName == _itemStack.Item.ItemName)
            {
                _slots[i].SetActiveSlot(_itemStack) ;
                return; 
            }
        }

        _slots[_nextIndex].SetActiveSlot(_itemStack);
        _nextIndex++;
    }
    private void ClearSlot()
    {
        for (int i = 0; i < _nextIndex; i++)
        {
            _slots[i].SetInActiveSlot();
        }
    }
    //Event listeners 
 
    private void ChangeDisplayType(ItemInventoryType type)
    {
        _currentDisplayType = type ;
        FillInventoryUI();
    }
    private void OpenInventory()
    {
        _inventoryRenderer.SetActive(true); 
    }

    private void CloseInventory()
    {
        _inventoryRenderer.SetActive(false);
    }

    //Event Boardcasts
    public void OnHighlightSlotEvent(ItemSO itemso)
    {
        HighlightSlotEvent?.Invoke(itemso); 
    }


}
