
using MJW.Player;
using UnityEngine;

namespace MJW.Items
{
    public class ItemBase : MonoBehaviour
    {
        [SerializeField] private ItemType _type;

        public bool IsFree;
        public ItemType Type => _type;

        private void Awake()
        {
            IsFree = true;
        }

        public virtual void Interact()
        {

        }

        public virtual void Pick(Transform parent)
        {
            IsFree = false;
        }

        public virtual void Drop()
        {
            IsFree = true;
        }
    }
}