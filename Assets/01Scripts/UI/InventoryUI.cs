using System.Collections.Generic;
using GGM.EventSystem;
using GGM.Inventories;
using UnityEngine;

namespace GGM.UI
{
    //이 클래스는 프리젠터가 되는거고
    public class InventoryUI : MonoBehaviour
    {
        [field: SerializeField] public GameEventChannelSO InventoryEventChannel { get; private set; }
        public RectTransform RectTrm => transform as RectTransform;

        public List<InventoryItem> inventoryData; //이게 모델이 되는거야.
        
        public void Open()
        {
            
        }

        public void Close()
        {
            
        }
    }
}
