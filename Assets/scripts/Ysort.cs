using UnityEngine;

public class YSort : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Multiply by -100 to keep integers and reverse order (lower y = higher order)
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}