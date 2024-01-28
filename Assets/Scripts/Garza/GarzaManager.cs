using MJW.Game;
using MJW.Instruments;
using MJW.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJW.Garza
{
    public class GarzaManager : MonoBehaviour
    {
        
        [Header("Config")]
        [SerializeField] private float _garzaMinStartSeconds;
        [SerializeField] private float _garzaMaxStartSeconds;
        [SerializeField] private GameObject _garzaPrefab;
        [SerializeField] private List<GameObject> _zonesGarzaFalling;

        private void Awake()
        {
            GameEvents.OnGameReady += OnGameReady;
            
        }

        private void OnDestroy()
        {
            GameEvents.OnGameReady -= OnGameReady;
        }

        public void OnGameReady()
        {
            float waitTime = Random.Range(_garzaMinStartSeconds, _garzaMaxStartSeconds);
            
            Invoke(nameof(GarzaAppear), waitTime);
        }

        private IEnumerator GarzaAppear()
        {
            int randomNumber = Random.Range(0, _zonesGarzaFalling.Count);
            Transform randomPosition = _zonesGarzaFalling[randomNumber].transform;

            Instantiate(_garzaPrefab, randomPosition);
            yield return new WaitForSeconds(1f);
        }
    }
}

