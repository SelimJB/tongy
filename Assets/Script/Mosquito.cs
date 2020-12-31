using Pyoro.Trajectories;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    public PerlinSwarmTrajectoryParameters Trajectory { get; set; }

    Vector3 initialPosition;
    Vector3 vibration;
    Vector3 progressionTrajectory;
    float perlinNoiseXSeed;
    float perlinNoiseYSeed;
    float magnitudeLimiter = 1f; // Is used when the mosquito has reached his goal, so as not to nomalize mangnitudes which are below 1, so that the attraction effect of the goal is quickly limited

    private void Start()
    {
        initialPosition = transform.position;
        progressionTrajectory = transform.position;
        vibration = Vector3.zero;
        perlinNoiseXSeed = Random.Range(0f, 10000f);
        perlinNoiseYSeed = Random.Range(0f, 10000f);
    }

    private void Update()
    {
        float pulsation = 0;
        if (Trajectory.easeType == PerlinSwarmTrajectoryParameters.EaseType.Cosinus)
            pulsation = (Mathf.Cos(Time.fixedTime * (Mathf.PI * 2) * (1 / Trajectory.pulsationSpeed)) + 1) / 2;
        else if (Trajectory.easeType == PerlinSwarmTrajectoryParameters.EaseType.Function)
            pulsation = Ease(Time.fixedTime, Trajectory.pulsationSpeed);
        else if (Trajectory.easeType == PerlinSwarmTrajectoryParameters.EaseType.Curve)
            pulsation = Trajectory.easeCurve.Evaluate((Time.fixedTime % Trajectory.pulsationSpeed) / Trajectory.pulsationSpeed);

        var perlinTrajectory = (Mathf.PerlinNoise(Time.time * Trajectory.perlinSpeed + perlinNoiseXSeed, 0) - 0.5f) * Trajectory.perlinAmplitude * Vector3.up
            + (Mathf.PerlinNoise(0, Time.time * Trajectory.perlinSpeed + perlinNoiseYSeed) - 0.5f) * Trajectory.perlinAmplitude * Vector3.right;

        if ((Trajectory.destination - transform.position).magnitude < magnitudeLimiter)
            magnitudeLimiter = (Trajectory.destination - transform.position).magnitude;

        progressionTrajectory += (Trajectory.destination - transform.position).normalized * Time.deltaTime * Trajectory.speed * magnitudeLimiter;

        vibration = (Vector3)Random.insideUnitCircle * Time.deltaTime * Trajectory.vibrationAmplitude;

        transform.position = progressionTrajectory + perlinTrajectory * (pulsation * Trajectory.pulsationIntensity + (1 - Trajectory.pulsationIntensity)) + vibration;
    }

    public void Reset()
    {
        transform.position = initialPosition;
        progressionTrajectory = initialPosition;
    }

    public static float EaseIn(float t) => 1 - Mathf.Pow(t, 2);

    public static float EaseOut(float t) => 1 - Mathf.Pow(t - 1, 2);

    public float Ease(float t, float pulsationSpeed)
    {
        var quotient = (int)(t / (pulsationSpeed / 2f));
        var remainder = t - quotient * (pulsationSpeed / 2f);
        if (quotient % 2 == 0)
            return EaseIn(remainder / (pulsationSpeed / 2f));
        else
            return EaseOut(remainder / (pulsationSpeed / 2f));
    }
}