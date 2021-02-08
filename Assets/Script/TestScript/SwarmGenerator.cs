using System.Collections;
using UnityEngine;

public class SwarmGenerator : MonoBehaviour
{
    [SerializeField] GameObject SwarmPrefab;

    void Start()
    {
        StartCoroutine(GenerateSwarm());
    }

    IEnumerator GenerateSwarm()
    {
        var pos = new Vector3(Random.Range(0f, 1f) > 0.5f ? -22 : 22, Random.Range(-15f, 15f), 0);
        Instantiate(SwarmPrefab, pos, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        yield return GenerateSwarm();
    }
}
