using System;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    private int maxHealth;
    private int health;
    private LifeReceptacle[] receptacles;

    public event Action OnGameOver;

    private void Start()
    {
        receptacles = FindObjectsOfType<LifeReceptacle>();
        maxHealth = receptacles.Length;
        health = maxHealth;
        OnGameOver += GameOver;
        foreach (var receptacle in receptacles)
            receptacle.OnVitalityLoss += LoseHealth;
    }

    private void OnDestroy()
    {
        OnGameOver -= GameOver;
        foreach (var receptacle in receptacles)
            receptacle.OnVitalityLoss -= LoseHealth;
    }

    void LoseHealth()
    {
        health -= 1;
        if (health == 0)
            GameOver();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}