using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sansar : MonoBehaviour
{
    public SansarSO sansarData;

    // Referanslar
    private SpriteRenderer _spriteRenderer;
    private int _animasyonKaresi;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        InvokeRepeating(nameof(ResimlerinAnimasyonunuCiz), sansarData.animasyonSuresi, sansarData.animasyonSuresi);
    }

    private void ResimlerinAnimasyonunuCiz()
    {
        _animasyonKaresi++;

        if (_animasyonKaresi >= sansarData.animasyonSprites.Length)
        {
            _animasyonKaresi = 0;
        }

        _spriteRenderer.sprite = sansarData.animasyonSprites[_animasyonKaresi];
    }
}
