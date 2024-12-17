using UnityEngine;

namespace GGM.Items
{
    public interface IPickable
    {
        public void PickUp(Collider2D picker);
    }
}
