using UnityEngine;

namespace MJW.Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T m_instance;

        public static bool HasInstance => m_instance != null;

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = (T)FindObjectOfType(typeof(T));
                }

                return m_instance;
            }
        }
    }
}