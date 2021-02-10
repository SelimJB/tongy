using UnityEngine;

namespace Pyoro.Trajectory
{
    public class EllipseTrajectory : Trajectory
    {
        [SerializeField] float speed = 0f;
        [SerializeField] float vibrationAmplitude = 3f;
        [SerializeField] float minEllipseSize = 0.5f;
        [SerializeField] float maxEllipseAxisLength = 4f;
        [SerializeField] float minEllipseSpeed = 1f;
        [SerializeField] float maxEllipseSpeed = 5f;

        float ellipseSpeed;
        float ellipseRotation;
        Vector2 ellipseDimension;
        Vector3 progressionTrajectory;


        public override void Initialize(Vector3 destination)
        {
            this.destination = destination;
            progressionTrajectory = transform.position;

            var ellipseAxesLengthsSum = Random.Range(1f, maxEllipseAxisLength + 1f);
            var ellipseSize = Random.Range(0, ellipseAxesLengthsSum); // Not the real size since we're adding minEllipseSize
            ellipseDimension = new Vector2(ellipseSize + minEllipseSize, ellipseAxesLengthsSum - ellipseSize + minEllipseSize);

            ellipseSpeed = Random.Range(minEllipseSpeed, maxEllipseSpeed);
            ellipseRotation = Random.Range(0f, 90f);
        }

        public override void ResetPosition()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdatePosition()
        {
            // FIXME : replace cos by the same "Pulsation" that we have in the perlin swarm trajectory (add pulsation speed, and pulsation intensity); add the magnitude limiter
            var ellipseTrajectory = Quaternion.Euler(0, 0, ellipseRotation)
                * (Vector3)(ellipseDimension * new Vector2(Mathf.Cos(Time.fixedTime * ellipseSpeed), Mathf.Sin(Time.fixedTime * ellipseSpeed)))
                * (((1 + Mathf.Cos(Time.fixedTime * 3.5f)) / 2) * 0.75f + 0.25f);

            progressionTrajectory += (destination - transform.position).normalized * Time.deltaTime * speed + (Vector3)Random.insideUnitCircle * Time.deltaTime * vibrationAmplitude;

            transform.position = progressionTrajectory + ellipseTrajectory;
        }
    }
}