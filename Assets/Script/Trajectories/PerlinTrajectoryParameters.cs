using UnityEngine;

namespace TongueShooter.Trajectories
{
	[CreateAssetMenu(menuName = "TongueShooter/PrelinTrajectoryParameters")]
	public class PerlinTrajectoryParameters : ScriptableObject
	{
		[Range(0f, 10f)] [SerializeField] private float speed = 5f;
		[Range(0f, 3f)] [SerializeField] private float vibrationAmplitude = 1.7f;
		[Range(0f, 1)] [SerializeField] private float pulsationIntensity = 1f;
		[Range(0.1f, 4f)] [SerializeField] private float pulsationSpeed = 1f;
		[Range(0f, 4f)] [SerializeField] private float perlinSpeed = 1f;
		[Range(0f, 3f)] [SerializeField] private float perlinSpeedVariation = 1f;
		[Range(0f, 20f)] [SerializeField] private float perlinAmplitude = 5f;
		[SerializeField] private AnimationCurve easeCurve;
		[SerializeField] private EaseType easeType;

		public float Speed => speed;
		public float VibrationAmplitude => vibrationAmplitude;
		public float PulsationIntensity => pulsationIntensity;
		public float PulsationSpeed => pulsationSpeed;
		public float PerlinSpeed => perlinSpeed;
		public float PerlinSpeedVariation => perlinSpeedVariation;
		public float PerlinAmplitude => perlinAmplitude;
		public AnimationCurve EaseCurve => easeCurve;
		public EaseType Ease => easeType;

		public enum EaseType
		{
			Cosinus,
			Function,
			Curve
		}
	}
}