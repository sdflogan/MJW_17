
using MJW.Utils;
using System.Drawing;
using UnityEngine;

namespace MJW.MyCamera
{
    public class GameCamera : MonoBehaviour
    {
        #region Inspector

        [SerializeField] private Point3D _gameplayPosition;
        [SerializeField] private Quaternion _gameplayRotation;

        #endregion

        #region Private properties

        private Vector3 _menuPosition;
        private Quaternion _menuRotation;

        #endregion

        #region Unity events

        private void Awake()
        {
            _menuPosition = transform.position;
            _menuRotation = transform.rotation;
        }

        private void OnDestroy()
        {
            
        }

        #endregion
    }
}