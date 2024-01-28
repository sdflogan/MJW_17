using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics;
using System.Collections.Specialized;

public class InstrumentSpriteManager : MonoBehaviour
{
    public Sprite xilo;
    public Sprite micro;    
    public Sprite tambor;
    public Sprite banjo;




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

    }

    // Update is called once per frame
    void Update()
    {
        // You can add any additional update logic if needed
    }

    void PonerInstr(int i)
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
        PonerInstr(0);
    }
    public void PonerInstr_micro()
    {
        PonerInstr(1);
    }
    public void PonerInstr_tambor()
    {
        PonerInstr(2);
    }
    public void PonerInstr_banjo()
    {
        PonerInstr(3);
    }

}