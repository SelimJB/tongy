using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField]
    private float perlinMovementAmplitude;
    [SerializeField]
    private float perlinMovementSpeed;
    [SerializeField]
    private float perlinScaleAmplitude;
    [SerializeField]
    private float perlinScaleSpeed;


    private float[] perlinSeed = new float[3];
    private Vector3 initialPosition;
    private Vector3 initialScale;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialScale = transform.localScale;

        for (int i = 0; i < perlinSeed.Length; i++)
        {
            perlinSeed[i] = Random.Range(0f, 10f);
        }
    }

    void Update()
    {
        var floatX = Mathf.PerlinNoise(Time.fixedTime * perlinMovementSpeed + perlinSeed[0], 0) * 2 - 1;
        var floatY = Mathf.PerlinNoise(Time.fixedTime * perlinMovementSpeed + perlinSeed[1], 0) * 2 - 1;
        transform.localPosition = initialPosition + new Vector3(floatX, floatY, 0) * perlinMovementAmplitude;

        var scaleFactor = Mathf.PerlinNoise(Time.fixedTime * perlinScaleSpeed + perlinSeed[1], 0) * 2 - 1;
        transform.localScale = initialScale * (1 + scaleFactor * perlinScaleAmplitude);
    }
}
