using System;
using System.Collections.Generic;
using System.Linq;
using GGM.Entities;
using GGM.EventSystem;
using GGM.Inventories;
using GGM.Items;
using UnityEngine;

namespace GGM.Players
{
    public class PlayerInventoryData : InventoryData, IEntityComponent, IAfterInitable
    {
        [field: SerializeField] public GameEventChannelSO InventoryEventChannel { get; private set; }

        private Player _player;
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            inventory = new List<InventoryItem>(); //인벤토리 생성
        }
        
        public void AfterInit()
        {
            InventoryEventChannel.AddListener<RequestInventoryData>(HandleRequestInventoryData);
        }

        private void OnDestroy()
        {
            InventoryEventChannel.RemoveListener<RequestInventoryData>(HandleRequestInventoryData);
        }

        private void HandleRequestInventoryData(RequestInventoryData evt)
        {
            UpdateInventoryUI();
        }

        private void UpdateInventoryUI()
        {
            
        }

        public override void AddItem(ItemDataSO itemData, int count = 1)
        {
            IEnumerable<InventoryItem> items = GetItems(itemData); // 지정한 아이템에 해당하는 아이템을 모두 가져온다.

            InventoryItem canAddItem = items.FirstOrDefault(item => item.IsFullStack == false);
            if (canAddItem == null)
            {
                CreateNewInventoryItem(itemData, count);
            }
            else
            {
                int remain = canAddItem.AddStack(count);
                if (remain > 0)
                    CreateNewInventoryItem(itemData, remain); 
                //만약 한번에 스택을 초과하는 수치만큼 먹는게 가능하다면 while루프 돌려야한다.
            }
            UpdateInventoryUI();
        }

        private void CreateNewInventoryItem(ItemDataSO itemData, int count)
        {
            InventoryItem inventoryItem = new InventoryItem(itemData, count);
            inventory.Add(inventoryItem);
        }

        public override void RemoveItem(ItemDataSO itemData, int count)
        {
            GetItem(itemData).RemoveStack(count);
            UpdateInventoryUI();
        }

        public override bool CanAddItem(ItemDataSO itemData)
        {
            return true; //일단 true 리턴
        }

        public override bool CanRemoveItem(ItemDataSO itemData, int count)
        {
            return true; //일단 true 리턴
        }
        
    }
}
