using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;
using System.Collections.Specialized;
using MJW.Utils;
using MJW.Game;
using MJW.Instruments;

public class InstrumentSpriteManager : Singleton<InstrumentSpriteManager>
{
    public Sprite xilo;
    public Sprite micro;    
    public Sprite tambor;
    public Sprite banjo;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float _seconds;
    [SerializeField] private float _waitSeconds;
    [SerializeField] private float _hideSeconds;
    [SerializeField] private float _bigScale = 1.4f;

    #region Unity events

    private void Awake()
    {
        spriteRenderer.gameObject.SetActive(false);
        spriteRenderer.transform.localScale = Vector3.zero;

        GameEvents.OnSheetGenerated += OnSheetGenerated;
    }

    private void OnDestroy()
    {
        GameEvents.OnSheetGenerated -= OnSheetGenerated;
    }

    #endregion

    #region Callbacks

    private void OnSheetGenerated(int notes, InstrumentType instrument)
    {
        switch (instrument)
        {
            case InstrumentType.Banjo:
                PonerInstr_banjo();
                break;

            case InstrumentType.Tambor:
                PonerInstr_tambor();
                break;

            case InstrumentType.Trompeta:
                PonerInstr_xilo();
                break;

            case InstrumentType.Vocal:
                PonerInstr_micro();
                break;

            default:
                break;
        }
    }

    #endregion


    void PonerInstr(int i)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.gameObject.SetActive(true);

            // Create a new sequence
            Sequence scaleSequence = DOTween.Sequence();

            // Add the first tween to scale up
            scaleSequence.Append(spriteRenderer.transform.DOScale(_bigScale, _seconds).SetEase(Ease.InOutBounce));

            scaleSequence.Append(spriteRenderer.transform.DOScale(1, _seconds).SetEase(Ease.Linear));

            scaleSequence.AppendInterval(_waitSeconds);

            // Add the second tween to scale back down
            scaleSequence.Append(spriteRenderer.transform.DOScale(0, _hideSeconds).SetEase(Ease.InOutBounce));

            scaleSequence.AppendCallback(() => spriteRenderer.gameObject.SetActive(false));

            // Play the sequence
            scaleSequence.Play();

            // Set the sprite based on the integer value
            switch (i)
            {
                case 0:
                    spriteRenderer.sprite = xilo;
                    break;
                case 1:
                    spriteRenderer.sprite = micro;
                    break;
                case 2:
                    spriteRenderer.sprite = tambor;
                    break;
                case 3:
                    spriteRenderer.sprite = banjo;
                    break;
                default:
                    // Handle the default case or leave it empty if not needed
                    break;
            }

        }
        else
        {
            //Debug.LogError("SpriteRenderer component not initialized");
        }
    }

    public void PonerInstr_xilo() {
        PonerInstr(2);
    }
    public void PonerInstr_micro()
    {
        PonerInstr(3);
    }
    public void PonerInstr_tambor()
    {
        PonerInstr(1);
    }
    public void PonerInstr_banjo()
    {
        PonerInstr(0);
    }

}