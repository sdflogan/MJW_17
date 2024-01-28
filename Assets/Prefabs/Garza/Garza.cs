using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MJW.Audio;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Garza : MonoBehaviour
{
    [SerializeField] private GameObject visual;
    [SerializeField] private Transform _posInit;
    [SerializeField] private Transform _posFinal;
    [SerializeField] private GameObject _impactPrefab;
    [SerializeField] private float _timeDropping;
    [SerializeField] private float _timeToDestroy;

    private void Start()
    {
        AudioManager.Instance.PlaySFX(SoundType.garza_scream);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(1.5f);
        sequence.Append(visual.transform.DOMoveY(_posFinal.position.y, _timeDropping));
        sequence.InsertCallback(1.5f + _timeDropping - 0.2f, (() =>
        {
            Camera.main.DOShakePosition(0.1f, 1f).Play();
            AudioManager.Instance.PlaySFX(SoundType.garza_impact);
            Instantiate(_impactPrefab, gameObject.transform);
        }));
        sequence.Append(visual.transform.DOMoveY(_posInit.position.y, _timeDropping));
        sequence.AppendCallback(() => Destroy(gameObject));
        sequence.Play();
        
        //Destroy(gameObject, _timeToDestroy);
    }

    
}
