using UnityEngine;

namespace Pyoro.Trajectories
{
    [System.Serializable]
    public class PerlinSwarmTrajectoryParameters
    {
        [SerializeField] public Vector3 destination;
        [Range(0f, 10f)]
        [SerializeField] public float speed = 5f;
        [Range(0f, 3f)]
        [SerializeField] public float vibrationAmplitude = 1.7f;
        [Range(0f, 1)]
        [SerializeField] public float pulsationIntensity = 1f;
        [Range(0.1f, 4f)]
        [SerializeField] public float pulsationSpeed = 1f;
        [Range(0f, 4f)]
        [SerializeField] public float perlinSpeed = 1f;
        [Range(0f, 20f)]
        [SerializeField] public float perlinAmplitude = 5f;
        [SerializeField] public AnimationCurve easeCurve;
        [SerializeField] public EaseType easeType;

        public enum EaseType
        {
            Cosinus,
            Function,
            Curve
        }
    }
}