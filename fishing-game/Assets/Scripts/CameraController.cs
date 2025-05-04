using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    public Transform player;
    private Vector3 offset = new Vector3(0, 0, -1);
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
    }

    void LateUpdate()
    {
        if (player is null)
            return;
        
        transform.position = player.position + offset;
        // transform.position = Vector3.Lerp(transform.position, player.position + offset, 2 * Time.deltaTime);
    }
}
