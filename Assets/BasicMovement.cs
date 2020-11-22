using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    void Start()
    {

    }

    int right = 1;
    void Update()
    {
        if (transform.position.x < -9)
            right = 1;
        else if (transform.position.x > 9)
            right = -1;
        transform.position += right * Time.deltaTime * Vector3.right * 10;
    }
}
