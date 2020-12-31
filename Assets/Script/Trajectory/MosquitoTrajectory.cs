using UnityEngine;

namespace Trajectory
{
    public class MosquitoTrajectory : MonoBehaviour
    {
        [SerializeField] Vector3 destination;
        [Range(0f, 10f)]
        [SerializeField] float speed = 3.5f;
        [Range(0f, 10f)]
        [SerializeField] float vibrationAmplitude = 3f;

        void Update()
        {
            transform.position += (Vector3)Random.insideUnitCircle * Time.deltaTime * vibrationAmplitude
                + (destination - transform.position).normalized * Time.deltaTime * speed;
        }
    }
}