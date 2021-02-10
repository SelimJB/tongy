using System;
using System.Linq;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
	public event Action OnGameOver;
	
	private int health;
	private LifeReceptacle[] receptacles;
	
	private void Start()
	{
		receptacles = FindObjectsOfType<LifeReceptacle>();
		health = receptacles.Length;
	}

	public void LoseHealth()
	{
		health -= 1;
		if (health == 0)
			GameOver();
	}

	private void GameOver()
	{
		Debug.Log("Game Over");
		OnGameOver?.Invoke();
	}

	public LifeReceptacle FindNearestActiveLifeReceptacle(Vector2 position)
	{
		return receptacles?.Where(r => !r.IsInert)
			.OrderBy(r => Vector2.Distance(r.Position, position))
			.FirstOrDefault();
	}
}