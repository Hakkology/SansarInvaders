using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public TextMeshProUGUI skorText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        health1.SetActive(true);
        health2.SetActive(true);
        health3.SetActive(true);
    }
    public void UpdateSkorText(int skor)
    {
        skorText.text = "SKOR: " + skor;
    }

    public void DrawHealthImage(int health)
    {
        if (health < 1)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
        } 
        else if (health < 2)
        {
            health2.SetActive(false);
            health3.SetActive(false);
        }
        else if (health < 3)
        {
            health3.SetActive(false);
        }
    }


}
