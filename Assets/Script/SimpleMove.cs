using System;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float velocity;

    int way = 1;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (ReachEdge(transform.position))
            way = -way;
        transform.position += Time.deltaTime * direction.normalized * velocity * way;
    }

    private bool ReachEdge(Vector2 pos)
    {
        var size = cam.orthographicSize;
        if (Math.Abs(pos.x) > size || Math.Abs(pos.y) > size)
            return true;
        return false;
    }
}