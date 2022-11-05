using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "New Generic Item",menuName = "Items/Generic Item")]
    public class GenericItem : ItemBase, iPickupable
    {
   
    
        public void Pickup(Inventory stats)
        {
            throw new System.NotImplementedException();
        }
    }
}
