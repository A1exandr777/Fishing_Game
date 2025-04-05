using UnityEngine;

public class TreeCuttable : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;

    public override void Hit()
    {
        for (var i = 0; i < dropCount; i++)
        {
            var position = transform.position;
            position.x += spread * Random.value - spread / 2;
            position.y += spread * Random.value - spread / 2;
            var drop = Instantiate(pickUpDrop);
            drop.transform.position = position;
        }

        Destroy(gameObject);
    }
}
