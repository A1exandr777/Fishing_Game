using UnityEngine;

public class PivotSway : MonoBehaviour
{
    [Header("Sway Settings")]
    public float maxSwayAngle = 15f;
    public float swaySpeed = 8f;
    public float returnSpeed = 4f;
    public float swayDuration = 0.3f;
    
    private float currentSwayTime;
    private bool isSwaying;
    private Quaternion originalRotation;
    private Transform playerTransform;
    private Vector2 swayDirection;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isSwaying = true;
            currentSwayTime = 0f;
            playerTransform = other.transform;
            
            // Calculate direction from grass to player
            swayDirection = (transform.position - playerTransform.position).normalized;
        }
    }

    private void Update()
    {
        if (isSwaying)
        {
            currentSwayTime += Time.deltaTime;
            
            if (currentSwayTime < swayDuration)
            {
                // Sway phase
                var progress = currentSwayTime / swayDuration;
                var angle = Mathf.Sin(progress * Mathf.PI) * maxSwayAngle;
                
                // Rotate away from player (around Z-axis for 2D)
                transform.rotation = originalRotation * 
                    Quaternion.Euler(0, 0, angle * -swayDirection.x);
            }
            else
            {
                // Return phase
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    originalRotation,
                    returnSpeed * Time.deltaTime
                );
                
                // Check if we've basically returned
                if (Quaternion.Angle(transform.rotation, originalRotation) < 0.1f)
                {
                    transform.rotation = originalRotation;
                    isSwaying = false;
                }
            }
        }
    }
}