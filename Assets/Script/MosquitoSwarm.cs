using Pyoro.Trajectories;
using UnityEngine;

public class MosquitoSwarm : MonoBehaviour
{
    [SerializeField] int swarmSize = 10;
    [SerializeField] Mosquito mosquitoPrefab;
    [SerializeField] PerlinSwarmTrajectoryParameters trajectory;

    void Start()
    {
        for (int i = 0; i < swarmSize; i++)
        {
            var enemy = Instantiate(mosquitoPrefab);
            enemy.Trajectory = trajectory;
            enemy.transform.position = transform.position;
            enemy.transform.parent = transform;
        }
    }
}
