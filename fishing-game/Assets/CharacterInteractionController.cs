using System;
using UnityEngine;

public class CharacterInteractionController : MonoBehaviour
{
    CharacterController2D character;
    Rigidbody2D rigidbody2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float interactionRadius = 1.2f;

    private void Awake()
    {
        character = GetComponent<CharacterController2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UseTool();
        }
    }

    private void UseTool()
    {
        var position = rigidbody2d.position + character.lastMotionVector * offsetDistance;
        var colliders = Physics2D.OverlapCircleAll(position, interactionRadius);
        foreach (var collider in colliders)
        {
            ToolHit hit = collider.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                return;
            }
        }
    }
}
