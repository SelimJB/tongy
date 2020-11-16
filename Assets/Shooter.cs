using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Vector2 origin = Vector2.zero;
    [SerializeField] float maxLength = 6f;

    Vector2 sweepStartPoint;
    Vector2 sweepEndPoint;
    Vector2 sweepVector;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        lineRenderer.SetPosition(0, origin);
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
        sweepStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnShootPrep()
    {
        sweepEndPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sweepVector = Vector2.ClampMagnitude(sweepEndPoint - sweepStartPoint, maxLength);
        lineRenderer.SetPosition(1, origin + sweepVector);
        lineRenderer.endWidth = 1f / (sweepVector.magnitude + 1);
    }

    void OnShootRelease()
    {
        var impactPoint = origin - sweepVector;
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