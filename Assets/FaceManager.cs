using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;
using System.Collections.Specialized;
using MJW.Utils;
using MJW.Game;
using MJW.Instruments;
using MJW.Audio;

public class FaceManager : Singleton<FaceManager>
{
    public Sprite cara_seria;
    public Sprite cara_carcajada;
    public Sprite cara_sonrisa;
    public Sprite cara_enfadada;
    public Sprite cara_orden;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _seconds;
    [SerializeField] private AnimationCurve _customCurve;

    [SerializeField] private List<SoundType> _laughs;
    [SerializeField] private List<SoundType> _angry;
    [SerializeField] private List<SoundType> _ok;
    [SerializeField] private List<SoundType> _orden;

    private int _successCount = 0;

    #region Unity events

    private void Awake()
    {
        GameEvents.OnGameReady += OnGameReady;
        GameEvents.OnSimonStart += OnSimonStarted;
        GameEvents.OnSimonEnd += OnSimonEnd;
        GameEvents.OnNoteFailed += OnNoteFailed;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameReady -= OnGameReady;
        GameEvents.OnSimonStart -= OnSimonStarted;
        GameEvents.OnSimonEnd -= OnSimonEnd;
        GameEvents.OnNoteFailed -= OnNoteFailed;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_spriteRenderer == null)
        {
            // If there's no SpriteRenderer, you may need to add one or modify accordingly
            //Debug.LogError("SpriteRenderer component not found on GameObject: " + gameObject.name);
        }
    }

    #endregion

    #region Callbacks

    private void OnGameReady()
    {

    }

    private void OnSimonStarted()
    {
        PonerCara_orden();
    }

    private void OnSimonEnd(int errors)
    {
        if (errors > 0 || errors < 0)
        {
            PonerCara_seria();
        }
        else
        {
            // no errors
            _successCount++;

            if (_successCount > 2)
            {
                PonerCara_sonrisa();
            }
            else
            {
                PonerCara_carcajada();
            }
        }
    }

    private void OnNoteFailed(InstrumentType type)
    {
        PonerCara_enfadada();
    }

    #endregion

    void PonerCara(int i)
    {
        if (_spriteRenderer != null)
        {
            Vector3 originalScale = _spriteRenderer.transform.localScale;
            Vector3 targetScale = originalScale * 1.2f;

            // Create a new sequence
            Sequence scaleSequence = DOTween.Sequence();

            // Add the first tween to scale up
            scaleSequence.Append(_spriteRenderer.transform.DOScale(targetScale, _seconds).SetEase(_customCurve));

            // Add the second tween to scale back down
            scaleSequence.Append(_spriteRenderer.transform.DOScale(originalScale, _seconds).SetEase(_customCurve));

            // Play the sequence
            scaleSequence.Play();

            Camera.main.DOShakePosition(0.1f, 1f).Play();

            // Set the sprite based on the integer value
            switch (i)
            {
                case 0:
                    _spriteRenderer.sprite = cara_seria;
                    break;
                case 1:
                    _spriteRenderer.sprite = cara_carcajada;
                    break;
                case 2:
                    _spriteRenderer.sprite = cara_sonrisa;
                    break;
                case 3:
                    _spriteRenderer.sprite = cara_enfadada;
                    break;
                case 4:
                    _spriteRenderer.sprite = cara_orden;
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

    public void PonerCara_seria() {
        PonerCara(0);

        int randomIndex = Random.Range(0, _angry.Count);
        AudioManager.Instance.PlaySFX(_angry[randomIndex], true);
    }
    public void PonerCara_carcajada()
    {
        PonerCara(1);

        int randomIndex = Random.Range(0, _laughs.Count);
        AudioManager.Instance.PlaySFX(_laughs[randomIndex], true);
    }
    public void PonerCara_sonrisa()
    {
        PonerCara(2);

        int randomIndex = Random.Range(0, _ok.Count);
        AudioManager.Instance.PlaySFX(_ok[randomIndex], true);
    }
    public void PonerCara_enfadada()
    {
        PonerCara(3);

        int randomIndex = Random.Range(0, _angry.Count);
        AudioManager.Instance.PlaySFX(_angry[randomIndex], true);
    }
    public void PonerCara_orden()
    {
        PonerCara(4);

        int randomIndex = Random.Range(0, _orden.Count);
        AudioManager.Instance.PlaySFX(_orden[randomIndex], true);
    }
}