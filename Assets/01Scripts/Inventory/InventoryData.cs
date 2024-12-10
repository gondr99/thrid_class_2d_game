using System.Collections.Generic;
using System.Linq;
using GGM.Items;
using UnityEngine;

namespace GGM.Inventories
{
    public abstract class InventoryData : MonoBehaviour
    {
        public List<InventoryItem> inventory;

        //하나만 가져올 때
        public virtual InventoryItem GetItem(ItemDataSO itemData) 
            => inventory.FirstOrDefault(inventoryItem => inventoryItem.data == itemData);
        
        //해당 타입의 아이템을 다 가져올 때
        public virtual IEnumerable<InventoryItem> GetItems(ItemDataSO itemData)
            => inventory.Where(inventoryItem => inventoryItem.data == itemData);

        public abstract void AddItem(ItemDataSO itemData, int count = 1);
        public abstract void RemoveItem(ItemDataSO itemData, int count);
        public abstract bool CanAddItem(ItemDataSO itemData);
        public abstract bool CanRemoveItem(ItemDataSO itemData, int count);
    }
}
