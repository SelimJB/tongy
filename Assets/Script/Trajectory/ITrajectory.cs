namespace Pyoro.Trajectory
{
    public interface ITrajectory
    {
        void Initialize();
        void UpdatePosition();
        void ResetPosition();
    }
}