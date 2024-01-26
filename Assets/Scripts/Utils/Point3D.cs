
using UnityEngine;

namespace MJW.Utils
{
    public class Point3D : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Color _color;
        [SerializeField] private float _radius = 1f;

        #endregion

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}