using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Garza : MonoBehaviour
{
    [SerializeField] private GameObject visual;
    [SerializeField] private Transform _posInit;
    [SerializeField] private Transform _posFinal;
    [SerializeField] private GameObject _impactPrefab;

    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(1.5f);
        sequence.Append(visual.transform.DOMoveY(_posFinal.position.y, 1f));
        sequence.AppendCallback((() =>
        {
            Instantiate(_impactPrefab, gameObject.transform);
        }));
        sequence.Play();
        
        Destroy(gameObject, 4f);
    }

    
}
