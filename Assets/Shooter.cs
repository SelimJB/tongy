using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Vector3 origin = Vector2.zero;
    [SerializeField] float maxLength = 3f;

    Vector3 entryPoint;
    Vector3 exitPoint;
    Vector3 position;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnShootInit();
        if (Input.GetMouseButton(0))
            OnShootPrep();
        if (Input.GetMouseButtonUp(0))
            OnShootRelease();
    }

    void OnShootInit()
    {
        entryPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnShootPrep()
    {
        exitPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(1, origin + (exitPoint - entryPoint));
    }

    void OnShootRelease()
    {
        lineRenderer.SetPosition(1, origin);
        Shoot();
    }

    void Shoot()
    {
        Debug.Log("Shoot");
    }
}