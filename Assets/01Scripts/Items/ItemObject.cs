using System;
using UnityEngine;

namespace GGM.Items
{
    public class ItemObject : MonoBehaviour, IPickable
    {
        private Rigidbody2D _rbCompo;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ItemDataSO _itemData;

        private void OnValidate()
        {
            if (_itemData == null) return;
            if (_spriteRenderer == null) return;

            _spriteRenderer.sprite = _itemData.icon; //이거 Warning나오긴 함.
            gameObject.name = $"Item_[{_itemData.itemName}]";
        }

        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
        }

        public void SetUpItem(ItemDataSO itemData, Vector2 velocity)
        {
            _itemData = itemData;
            _rbCompo.linearVelocity = velocity;
            _spriteRenderer.sprite = itemData.icon;
        }

        public void PickUp()
        {
            Destroy(gameObject);
        }
    }
}
