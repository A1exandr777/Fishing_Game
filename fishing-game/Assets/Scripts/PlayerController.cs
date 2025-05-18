using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public PlayerInventory Inventory;

    public Rigidbody2D rigidbody;
    public Animator animator;
    public float speed = 2f;
    
    public Vector2 motionVector;
    public Vector2 lastMotionVector;
    public bool anchored = false;
    public bool moving;

    public Dictionary<string, int> caughtFish = new();

    public float health = 100f;
    public int money = 100;
    
    // public VectorValue startingPosition;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
        
        
        animator = GetComponent<Animator>();
        // transform.position = startingPosition.initialValue;

        // var data = SaveSystem.LoadGame(1);
        // if (data != null)
        // {
        //     foreach (var item in data.playerInventory.items)
        //     {
        //         GameManager.Instance.Inventory.Add(ItemDatabase.GetItem(item.itemID), item.quantity);
        //     }
        //     var pos = data.playerPosition;
        //
        //     transform.position = new Vector3(pos[0], pos[1], pos[2]);
        // }
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        if (anchored)
        {
            horizontal = 0;
            vertical = 0;
        }

        motionVector = new Vector2(horizontal, vertical).normalized;
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);

        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);

        if (moving)
        {
            lastMotionVector = motionVector;
            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody.linearVelocity = motionVector * speed;
    }
    
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void AnchorPlayer(bool state)
    {
        anchored = state;
    }

    public void SetMoney(int value)
    {
        money = value;
        Events.MoneyChanged.Invoke(money);
    }
    
    public void TakeMoney(int amount)
    {
        if (!EnoughMoney(amount))
            return;
        money -= amount;
        // Events.MoneyChanged.Invoke(money);
    }

    public void GiveMoney(int amount)
    {
        money += amount;
    }

    public bool EnoughMoney(int needed)
    {
        return money >= needed;
    }
}
