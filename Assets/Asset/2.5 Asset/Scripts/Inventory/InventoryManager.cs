using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader; 
    [SerializeField] private InventorySO _inventorySO ;

    

    [Header("Listen to")]
    [SerializeField]
    private ItemEventChannelSO _pickUpEventChannel ;
 

    /*These events will be being listened by UIInventory Manager, and input reader*/
    public UnityAction<InventorySO> UpdateInventoryUIEvent;
    public UnityAction OpenInventoryEvent;
    public UnityAction CloseInventoryEvent; 

    private bool _isInventoryOpen = false ; 
    

    private void OnEnable()
    {
        _pickUpEventChannel._raiseEvent += OnPickUpItem;
        _inputReader.TabPressEvent +=  ToggleInventory ;
        _inputReader.TabPressedMenuEvent += ToggleInventory ;  
        OpenInventoryEvent += _inputReader.SetMenuActive;
        CloseInventoryEvent += _inputReader.SetGameplayActive;
     }

    private void OnDisable()
    {
        _pickUpEventChannel._raiseEvent -= OnPickUpItem;
        _inputReader.TabPressEvent -= ToggleInventory;
        _inputReader.TabPressedMenuEvent -= ToggleInventory;
        OpenInventoryEvent -= _inputReader.SetMenuActive; 
        CloseInventoryEvent -= _inputReader.SetGameplayActive;


    }


    //Event Listener 
    private void OnPickUpItem(GameObject retriever, ItemSO _itemSO)
    {
        _inventorySO.AddItem(_itemSO, 1);
    }

    


    private void ToggleInventory()
    {
        if (_isInventoryOpen)
        {
            CloseInventory(); 
            
        } else
        {
            OpenInventory(); 
        }

        _isInventoryOpen = !_isInventoryOpen; 
    }
    private void OpenInventory()
    {        
        //Use Consumable type in here because we want to display
        //Consumable when we first open inventory menu
        UpdateInventoryUIEvent?.Invoke(_inventorySO );
        OpenInventoryEvent?.Invoke(); 
    }

    private void CloseInventory()
    {
        CloseInventoryEvent?.Invoke(); 
    }


     public InventorySO inventorySO => _inventorySO; 
 }
