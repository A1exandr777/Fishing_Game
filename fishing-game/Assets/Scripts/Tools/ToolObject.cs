using System;
using UnityEngine;

public class ToolObject : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Init();
    }
    
    public virtual void Init()
    {
        
    }

    public virtual void OnDown()
    {
        
    }

    public virtual void OnUp()
    {
        
    }

    public virtual void UpdateDirection(Vector2 direction)
    {
        
    }
}