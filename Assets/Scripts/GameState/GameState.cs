using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public SansarGrid sansarGrid;

    public int PlayerScore { get; private set; }
    public int PlayerHealth { get; private set; }
    public int startingHealth = 3;


    private void Awake()
    {
        // Ensure singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ResetPlayerStats();
    }

    public void AddScore(int points)
    {
        PlayerScore += points;
        HUDManager.Instance.UpdateSkorText(PlayerScore);
    }

    public void ReduceHealth()
    {
        PlayerHealth--;

        if (PlayerHealth <= 0)
        {
            ResetGame();
        }
        else
        {
            ResetBoard();
        }

        HUDManager.Instance.DrawHealthImage(PlayerHealth);
    }

    private void ResetPlayerStats()
    {
        PlayerScore = 0;
        PlayerHealth = startingHealth;
    }

    public void ResetBoard()
    {
        sansarGrid.SansarlariTekrarDiz();
    }

    public void ResetGame()
    {
        ResetPlayerStats();
        sansarGrid.SansarlariTekrarDiz();
    }
}
