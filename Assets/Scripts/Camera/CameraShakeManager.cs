using DG.Tweening;
using MJW.Utils;
using UnityEngine;

namespace MJW.MyCamera
{
    public class CameraShakeManager : Singleton<CameraShakeManager>
    {
        [SerializeField] Camera _cam;

        [SerializeField] private float _str = 1;
        [SerializeField] private float _time = 0.1f;

        private bool _isShaking = false;

        private void Awake()
        {
            
        }

        private void OnDestroy()
        {
            
        }

        private void Shake()
        {
            if (!_isShaking)
            {
                _isShaking = true;
                _cam.DOShakePosition(_time, _str).OnComplete(() => { _isShaking = false; }).Play();
            }
            
        }
    }
}