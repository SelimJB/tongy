using UnityEngine;

public class Target : MonoBehaviour, IHitReceiver
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Color hitColor;

    public void OnRayHit()
    {
        sprite.color = hitColor;
    }
}