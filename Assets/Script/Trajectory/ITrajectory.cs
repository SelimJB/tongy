namespace Pyoro.Trajectories
{
    public interface ITrajectory
    {
        void Initialize();
        void UpdatePosition();
        void ResetPosition();
    }
}