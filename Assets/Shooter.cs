using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Vector3 origin = Vector2.zero;
    [SerializeField] float maxLength = 3f;

    Vector3 entryPoint;
    Vector3 exitPoint;
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
        var impactPoint = -(origin + (exitPoint - entryPoint));
        Shoot(impactPoint);
        PlayShootAnimation(impactPoint);
    }

    void PlayShootAnimation(Vector3 impactPoint)
    {
        lineRenderer.SetPosition(1, origin);
        Debug.DrawLine(origin, impactPoint, Color.red, 0.1f);
    }

    void Shoot(Vector3 impactPoint)
    {
        var hit = Physics2D.Raycast(origin, impactPoint, Vector3.Distance(origin, impactPoint));
        if (hit.collider != null)
        {
            var hitReceiver = hit.collider.gameObject.GetComponent<IHitReceiver>();
            if (hitReceiver != null)
                hitReceiver.OnRayHit();
        }
    }
}