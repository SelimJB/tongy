using UnityEngine;

namespace Pyoro.Trajectories
{
    public class MosquitoTrajectory : MonoBehaviour, ITrajectory
    {
        [SerializeField] Vector3 destination;
        [Range(0f, 10f)]
        [SerializeField] float speed = 3.5f;
        [Range(0f, 10f)]
        [SerializeField] float vibrationAmplitude = 3f;

        private Vector3 initialPosition;

        public void Initialize()
        {
            initialPosition = transform.position;
        }

        public void ResetPosition()
        {
            transform.position = initialPosition;
        }

        public void UpdatePosition()
        {
            transform.position += (Vector3)Random.insideUnitCircle * Time.deltaTime * vibrationAmplitude
                + (destination - transform.position).normalized * Time.deltaTime * speed;
        }
    }
}