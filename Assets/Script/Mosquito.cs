using Pyoro.Trajectories;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    [SerializeField] public TrajectoryType trajectoryType;
    [SerializeField] private PerlinTrajectory perlinTrajectory;
    [SerializeField] private EllipseTrajectory ellipseTrajectory;
    [SerializeField] private MosquitoTrajectory mosquitoTrajectory;

    public PerlinTrajectory PerlinTrajectory { get { return perlinTrajectory; } }

    private ITrajectory trajectory;

    public enum TrajectoryType
    {
        PerlinTrajectory,
        EllipseTrajectory,
        MosquitoTrajectory
    }

    private void Start()
    {
        if (trajectoryType == TrajectoryType.EllipseTrajectory)
            trajectory = ellipseTrajectory;
        else if (trajectoryType == TrajectoryType.PerlinTrajectory)
            trajectory = perlinTrajectory;
        else
            trajectory = mosquitoTrajectory;

        trajectory.Initialize();
    }

    private void Update()
    {
        trajectory.UpdatePosition();
    }
}