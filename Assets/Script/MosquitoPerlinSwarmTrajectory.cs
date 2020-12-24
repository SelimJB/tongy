using UnityEngine;

public class MosquitoPerlinSwarmTrajectory : MonoBehaviour
{
    [SerializeField] Vector3 destination;
    [Range(0f, 10f)]
    [SerializeField] float speed = 5f;
    [Range(0f, 3f)]
    [SerializeField] float vibrationAmplitude = 1.7f;
    [Range(0f, 1)]
    [SerializeField] float pulsationIntensity = 1f;
    [Range(0.1f, 4f)]
    [SerializeField] float pulsationSpeed = 1f;
    [Range(0f, 4f)]
    [SerializeField] float perlinSpeed = 1f;
    [Range(0f, 20f)]
    [SerializeField] float perlinAmplitude = 5f;
    [SerializeField] AnimationCurve easeCurve;
    [SerializeField] EaseType easeType;

    Vector3 initialPosition;
    Vector3 vibration;
    Vector3 progressionTrajectory;
    float perlinNoiseXSeed;
    float perlinNoiseYSeed;
    float magnitudeLimiter = 1f; // Is used when the mosquito has reached his goal, so as not to nomalize mangnitudes which are below 1, so that the attraction effect of the goal is quickly limited

    public enum EaseType
    {
        Cosinus,
        Function,
        Curve
    }

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
        if (easeType == EaseType.Cosinus)
            pulsation = (Mathf.Cos(Time.fixedTime * (Mathf.PI * 2) * (1 / pulsationSpeed)) + 1) / 2;
        else if (easeType == EaseType.Function)
            pulsation = Ease(Time.fixedTime, pulsationSpeed);
        else if (easeType == EaseType.Curve)
            pulsation = easeCurve.Evaluate((Time.fixedTime % pulsationSpeed) / pulsationSpeed);

        var perlinTrajectory = (Mathf.PerlinNoise(Time.time * perlinSpeed + perlinNoiseXSeed, 0) - 0.5f) * perlinAmplitude * Vector3.up
            + (Mathf.PerlinNoise(0, Time.time * perlinSpeed + perlinNoiseYSeed) - 0.5f) * perlinAmplitude * Vector3.right;

        if ((destination - transform.position).magnitude < magnitudeLimiter)
            magnitudeLimiter = (destination - transform.position).magnitude;

        progressionTrajectory += (destination - transform.position).normalized * Time.deltaTime * speed * magnitudeLimiter;

        vibration = (Vector3)Random.insideUnitCircle * Time.deltaTime * vibrationAmplitude;

        transform.position = progressionTrajectory + perlinTrajectory * (pulsation * pulsationIntensity + (1 - pulsationIntensity)) + vibration;
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