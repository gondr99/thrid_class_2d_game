using System;
using System.Collections.Generic;
using GGM.EventSystem;
using GGM.Inventories;
using GGM.Items;
using GGM.Players;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GGM.UI
{
    //이 클래스는 프리젠터가 되는거고
    public class InventoryUI : MonoBehaviour
    {
        public UnityEvent<ItemDataSO> OnItemSelected;
        [field: SerializeField] public GameEventChannelSO InventoryEventChannel { get; private set; }
        [SerializeField] private PlayerInputSO _playerInput;
        [SerializeField] private ItemSelectionUI _itemSelectionUI;
        [SerializeField] private int _columnCount = 5;

        [SerializeField] private RectTransform _selectionRect;
        public RectTransform RectTrm => transform as RectTransform;
        
        public List<InventoryItem> inventoryData; //이게 모델이 되는거야.

        [SerializeField] protected Transform _slotParent;
        protected ItemSlotUI[] _itemSlots;
        
        private ItemSlotUI _selectedItem;
        private int _selectedItemIndex;

        private void Awake()
        {
            _itemSlots = _slotParent.GetComponentsInChildren<ItemSlotUI>(); //모든 슬롯을 가져온다.
        }

        public void Open()
        {
            InventoryEventChannel.AddListener<InventoryDataList>(HandleDataRefresh);
            InventoryEventChannel.RaiseEvent(InventoryEvents.RequestInventoryData);

            _playerInput.UINavigationKeyEvent += HandleUINavigation;
            SelectItem(0);
        }
        
        public void Close()
        {
            InventoryEventChannel.RemoveListener<InventoryDataList>(HandleDataRefresh);
            _playerInput.UINavigationKeyEvent -= HandleUINavigation;
        }

        private void HandleUINavigation(Vector2 movement)
        {
            int nextIndex = GetNextSelection(movement);
            if(nextIndex != _selectedItemIndex)
                SelectItem(nextIndex);
        }

        private int GetNextSelection(Vector2 movement)
        {
            movement.y *= -1; //y축은 인덱스로 가면 방향이 반대니까.
            Vector2Int currentPosition = new Vector2Int(
                                        _selectedItemIndex % _columnCount, _selectedItemIndex / _columnCount);
            
            int totalRows = Mathf.CeilToInt((float) _itemSlots.Length / _columnCount);

            if (Mathf.Abs(movement.x) > 0)
            {
                currentPosition.x += (int)Mathf.Sign(movement.x);
            }else if (Mathf.Abs(movement.y) > 0)
            {
                currentPosition.y += (int)Mathf.Sign(movement.y);
            }
            
            if (currentPosition.x >= _columnCount || currentPosition.x < 0 
                                                  || currentPosition.y < 0 ||  currentPosition.y >= totalRows)
                return _selectedItemIndex;
            
            return currentPosition.x + currentPosition.y * _columnCount;
        }
        

        private void HandleDataRefresh(InventoryDataList evt)
        {
            inventoryData = evt.items;
            UpdateSlotUI();
        }
        
        private void SelectItem(int slotIndex)
        {
            _selectedItem = _itemSlots[slotIndex];
            _selectedItemIndex = slotIndex;
            MoveSelectionUI();
        }

        private void MoveSelectionUI()
        {
            Vector2 anchorPoint = _selectionRect.InverseTransformPoint(_selectedItem.RectTrm.position);
            _itemSelectionUI.MoveAnchorPosition(anchorPoint, true);
            OnItemSelected?.Invoke(_selectedItem.item?.data);
        }


        private void UpdateSlotUI()
        {
            for (int i = 0; i < _itemSlots.Length; i++)
            {
                _itemSlots[i].CleanUpSlot();
            }

            for (int i = 0; i < inventoryData.Count; i++)
            {
                _itemSlots[i].UpdateSlot(inventoryData[i]);
            }
        }
    }
}
