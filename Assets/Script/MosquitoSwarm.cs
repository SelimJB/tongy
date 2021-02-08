using Pyoro.Trajectory;
using UnityEngine;

public class MosquitoSwarm : MonoBehaviour
{
    [SerializeField] int swarmSize = 10;
    [SerializeField] Vector3 destination;
    [SerializeField] Mosquito mosquitoPrefab;
    [SerializeField] PerlinTrajectoryParameters trajectoryParameters;

    void Start()
    {
        for (int i = 0; i < swarmSize; i++)
        {
            var enemy = Instantiate(mosquitoPrefab);
            enemy.PerlinTrajectory.TrajectoryParameters = trajectoryParameters;
            enemy.PerlinTrajectory.Destination = destination;
            enemy.transform.position = transform.position;
            enemy.transform.parent = transform;
        }
    }
}
