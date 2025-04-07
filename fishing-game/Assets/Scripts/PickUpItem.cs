using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickupDistance = 1.5f;
    [SerializeField] float ttl = 10f;

    private void Awake()
    {
        player = GameManager.instance.player.transform;
    }

    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0)
            Destroy(gameObject);

        var distance = Vector2.Distance(transform.position, player.position);
        if (distance > pickupDistance)
            return;

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (distance < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
