using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGM.Inventories
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] protected Image _image;
        [SerializeField] protected TextMeshProUGUI _itemText;

        public InventoryItem item;
        public RectTransform RectTrm => transform as RectTransform;

        public void UpdateSlot(InventoryItem newItem)
        {
            item = newItem;
            _image.color = Color.white;
            if (item != null)
            {
                _image.sprite = item.data.icon;

                if (item.stackSize > 1)
                    _itemText.text = item.stackSize.ToString();
                else
                    _itemText.text = string.Empty;
            }
        }

        public void CleanUpSlot()
        {
            item = null;
            _image.sprite = null;
            _image.color = Color.clear;
            _itemText.text = string.Empty;
        }
    }
}
