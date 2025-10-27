using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControll : MonoBehaviour
{
    public Rigidbody2D _rigidbody2d;
    Vector2 directionSpeed;
    [SerializeField] private SpriteRenderer Player;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector2 MovementDirection = Vector2.zero;
    public int moveNumber = 0;
    bool IsAnimationStart = false;
    
    void Start()
    {
        
        
    }

   
    void Update()
    {
        
        MoveSpeed(MovementDirection);
        MoveAnimation();
        Move();
    }
    
    public void MoveSpeed(Vector2 directionSpeed)
    {
        directionSpeed = directionSpeed * 5f;
        _rigidbody2d.velocity = directionSpeed;
    }
    public void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

       
        MovementDirection = new Vector2(x, y).normalized;
    }
    public void MoveAnimation()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.Play("right");
           return;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.Play("left");
            return;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            animator.Play("up");
            return;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            animator.Play("down");
            return;
        }
        else
        {
            animator.SetInteger("IsMoveAnimation", 0);
            animator.Play("idle");
        }
    }

}


