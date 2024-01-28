using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Garza : MonoBehaviour
{
    [SerializeField] private Transform _posInit;
    [SerializeField] private Transform _posFinal;
    [SerializeField] private GameObject _impactPrefab;

    private void Start()
    {
        // Invoke(nameof(GarzaAttack), 2f);
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2f);
        sequence.Append(transform.DOMoveY(_posFinal.position.y, 1f));
        sequence.AppendCallback((() =>
        {
            Instantiate(_impactPrefab, gameObject.transform);
        }));
    }

    
}
