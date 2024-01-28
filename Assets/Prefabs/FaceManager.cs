using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;
using System.Collections.Specialized;

public class FaceManager : MonoBehaviour
{
    public Sprite cara_seria;
    public Sprite cara_carcajada;
    public Sprite cara_sonrisa;
    public Sprite cara_enfadada;
    public Sprite cara_orden;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming your GameObject has a SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            // If there's no SpriteRenderer, you may need to add one or modify accordingly
            //Debug.LogError("SpriteRenderer component not found on GameObject: " + gameObject.name);
        }

        PonerCara_orden();
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any additional update logic if needed
    }

    void PonerCara(int i)
    {
        if (spriteRenderer != null)
        {
            Vector3 originalScale = transform.localScale;
            Vector3 targetScale = originalScale * 1.4f;

            // Create a new sequence
            Sequence scaleSequence = DOTween.Sequence();

            // Add the first tween to scale up
            scaleSequence.Append(transform.DOScale(targetScale, 0.4f).SetEase(Ease.Linear));

            // Add the second tween to scale back down
            scaleSequence.Append(transform.DOScale(originalScale, 0.4f).SetEase(Ease.Linear));

            // Play the sequence
            scaleSequence.Play();



            // Set the sprite based on the integer value
            switch (i)
            {
                case 0:
                    spriteRenderer.sprite = cara_seria;
                    break;
                case 1:
                    spriteRenderer.sprite = cara_carcajada;
                    break;
                case 2:
                    spriteRenderer.sprite = cara_sonrisa;
                    break;
                case 3:
                    spriteRenderer.sprite = cara_enfadada;
                    break;
                case 4:
                    spriteRenderer.sprite = cara_orden;
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
    }
    public void PonerCara_carcajada()
    {
        PonerCara(1);
    }
    public void PonerCara_sonrisa()
    {
        PonerCara(2);
    }
    public void PonerCara_enfadada()
    {
        PonerCara(3);
    }
    public void PonerCara_orden()
    {
        PonerCara(4);
    }
}