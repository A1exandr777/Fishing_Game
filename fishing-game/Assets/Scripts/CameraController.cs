using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 offset = new Vector3(0, 0, -1);
    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;
        // transform.position = Vector3.Lerp(transform.position, player.position + offset, 2 * Time.deltaTime);
    }
}
