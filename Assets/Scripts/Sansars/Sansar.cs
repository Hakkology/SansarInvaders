using System;
using UnityEngine;

public class Sansar : MonoBehaviour
{
    public SansarSO sansarData;

    // Referanslar
    private SpriteRenderer _spriteRenderer;
    private int _animasyonKaresi;

    public Action _killed;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            _killed.Invoke();
            GameState.Instance.AddScore(sansarData.Skor);
            gameObject.SetActive(false);
        }
    }
}
