
using UnityEngine;

namespace MJW.Utils
{
    public class DepawnPool : MonoBehaviour
    {
        public float LifeSeconds = 10;

        private void Start()
        {
            Invoke(nameof(Depawn), LifeSeconds);
        }

        private void Depawn()
        {
            SimplePool.Despawn(gameObject);
        }
    }
}