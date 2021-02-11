using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector2 origin = Vector2.zero; // FIXME : origin = transform position ? 
    [SerializeField] private float maxLength = 6f;

    public event Action<List<Target>> OnHitTargets;

    private Vector2 sweepStartPoint;
    private Vector2 sweepEndPoint;
    private Vector2 sweepVector;
    private Camera mainCamera;
    private Vector2 impactPoint;

    private Color lineRendererColor;
    public Vector2 ImpactPoint => impactPoint;
    public Vector2 Origin => origin;

    private void Start()
    {
        mainCamera = Camera.main;
        lineRenderer.SetPosition(0, origin);
        OnHitTargets += HitTargets;
        lineRendererColor = lineRenderer.startColor;
    }

    private void OnDestroy()
    {
        OnHitTargets -= HitTargets;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnShootInit();
        if (Input.GetMouseButton(0))
            OnShootPrep();
        if (Input.GetMouseButtonUp(0))
            OnShootRelease();
    }

    private void OnShootInit()
    {
        sweepStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnShootPrep()
    {
        sweepEndPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sweepVector = Vector2.ClampMagnitude(sweepEndPoint - sweepStartPoint, maxLength);
        lineRenderer.SetPosition(1, origin + sweepVector);
        lineRenderer.endWidth = 1f / (sweepVector.magnitude + 1);
    }

    private void OnShootRelease()
    {
        impactPoint = origin - sweepVector;
        var targetsHit = Shoot(impactPoint);
        StartCoroutine(PlayShootAnimation(impactPoint));
        if (targetsHit.Count > 0)
            OnHitTargets?.Invoke(targetsHit);
    }

    IEnumerator PlayShootAnimation(Vector3 impactPoint)
    {
        lineRenderer.SetPosition(1, impactPoint);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endColor = lineRenderer.startColor = Color.magenta;
        yield return new WaitForSeconds(0.1f);
        lineRenderer.endColor = lineRenderer.startColor = lineRendererColor;
        lineRenderer.startWidth = 1f;
        lineRenderer.widthCurve.keys[0].value = 1f;
        lineRenderer.SetPosition(1, origin);
    } 
    private List<Target> Shoot(Vector2 impactPoint)
    {
        var direction = impactPoint - origin;
        var hits = Physics2D.RaycastAll(origin, direction.normalized, direction.magnitude);
        var targetsHit = new List<Target>();

        if (hits.Length != 0)
        {
            foreach (var hit in hits)
            {
                if (hit.collider != null)
                {
                    var target = hit.collider.gameObject.GetComponent<Target>();
                    if (target != null)
                    {
                        targetsHit.Add(target);
                    }
                }
            }
        }

        return targetsHit;
    }

    private void HitTargets(List<Target> targets)
    {
        foreach (var target in targets)
        {
            target.OnHit();
        }
    }
}