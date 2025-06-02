using UnityEngine;

public class TreeSway : MonoBehaviour
{
    [SerializeField] private float swayAmount = 0.5f;
    [SerializeField] private float swaySpeed = 1f;
    
    private float randomSeed;
    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
        randomSeed = Random.Range(0f, 100f);
    }

    private void Update()
    {
        var sway = Mathf.PerlinNoise(Time.time * swaySpeed, randomSeed) * 2f - 1f;
        var swayAngle = sway * swayAmount;
        transform.rotation = originalRotation * Quaternion.Euler(0, 0, swayAngle);
    }
}