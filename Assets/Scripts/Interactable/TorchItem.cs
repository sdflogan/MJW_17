
using MJW.Player;
using UnityEngine;

namespace MJW.Items
{
    public class TorchItem : ItemBase
    {
        [Header("Config")]
        [SerializeField] private float _force = 20f;
        [SerializeField] private float _distanceMultiplier = 1.5f;

        [Header("References")]
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Collider _collider;

        public override void Drop()
        {
            base.Drop();

            _rb.isKinematic = false;
            _collider.enabled = true;
            transform.parent = null;
        }

        public override void Interact()
        {
            base.Interact();

            throw new System.NotImplementedException();
        }

        public void Throw(Transform target)
        {
            _rb.isKinematic = false;
            transform.parent = null;
            IsFree = true;
            Invoke(nameof(EnableCollider), 0.25f);

            Vector3 direction = target.position - transform.position;
            float distance = Vector3.Distance(target.position, transform.position);

            _rb.AddForce(direction * _force * _distanceMultiplier, ForceMode.Impulse);
        }

        public override void Pick(Transform parent)
        {
            base.Pick(parent);

            _rb.isKinematic = true;
            _collider.enabled = false;
            transform.parent = parent;
        }

        private void EnableCollider()
        {
            _collider.enabled = true;
        }
    }
}