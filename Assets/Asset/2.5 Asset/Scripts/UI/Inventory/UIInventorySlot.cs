using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

/*This class responsible for visual of an inventory slot.*/
/*Any logic of remove, add, select, use should be put in Inventory Manager itself*/
public class UIInventorySlot : MonoBehaviour
{
    [Header("Required Reference ")]
    [SerializeField] private UIInventoryManager _uiInventoryManager;
    [SerializeField] private Image _previewRenderer;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private Image _backgroundRenderer ;

    [Header("Visual when highlighted")]
    [SerializeField] private Image _hoverHightlighted;
    [SerializeField] private float _hightlightedIntensity = 40.0f; 

    [Header("Defaul Visual When This Slot is Empty")]
    [SerializeField] private Sprite _defaultPreviewSprite;


    private bool _isActive; 
    private ItemSO _currentItem; 
    private Color _itemColor;



    public void SetActiveSlot(ItemStack itemStack)
    {
        _currentItem = itemStack.Item;
        _previewRenderer.sprite = itemStack.Item.PreviewImage;
        _quantityText.text = itemStack.Quantity.ToString();
        _backgroundRenderer.color = _currentItem.ItemType.TypeColor;
        _isActive = true; 
    }

    public void SetInActiveSlot()
    {
        _previewRenderer.sprite = _defaultPreviewSprite ;
        _quantityText.text = "0";
        _backgroundRenderer.color = Color.black;
        _isActive = false; 

    }

    public void UpdateSlot(ItemStack itemStack)
    {
        if (itemStack.Item.ItemName == _currentItem.ItemName)
            SetActiveSlot(itemStack);
    }

    public void HighlightTihsSlot()
    {
        if (!_isActive)
            return; 
        _hoverHightlighted.color = new Color(_hoverHightlighted.color.r, _hoverHightlighted.color.g, _hoverHightlighted.color.b, _hightlightedIntensity);
        _uiInventoryManager.OnHighlightSlotEvent(GetCurrentItem);
    }

    public void UnHightlightThisSlot()
    {
        _hoverHightlighted.color = new Color(_hoverHightlighted.color.r, _hoverHightlighted.color.g, _hoverHightlighted.color.b, 0);
    }


    public ItemSO GetCurrentItem => (_currentItem); 

}
