using UnityEngine;
using System;

//KNOWN ISSUES:
//CHANGES DIRECITON WHEN WALKING DOWN INCLINE BECAUSE DOES NOT FALL FAST ENOUGH
//
//POSSIBLE FIX:
//HAVE MOBS ONLY WALK ON FLAT GROUND, NOT UP AND DOWN INCLINES

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _physicsUpdateFrequency = 60f;
    [SerializeField] private float gravity = 10f;

    // rigidbody to control velocity
    private Rigidbody2D _rb;

    // flag for direction the enemy is walking
    [SerializeField] private bool _walkingRight = true;

    public bool _isOnGround = false; 

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("PhysicsUpdate", 0f, 1f / _physicsUpdateFrequency);
    }

    private void FixedUpdate()
    {
        //if (_isOnGround)
        //{
        //    _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        //}

        if (_walkingRight)
        {
            _rb.velocity = new Vector2(_moveSpeed, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(-_moveSpeed, _rb.velocity.y);
        }
    }

    private void PhysicsUpdate()
    {
        if (!_isOnGround)
        {
            _rb.velocity -= new Vector2(0f, gravity);
        }
    }

    public void ChangeDirection()
    {
        transform.Rotate(transform.up, 180f, Space.Self);
        _walkingRight = !_walkingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isOnGround = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _isOnGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isOnGround = false;
    }

    public void SetWalkingRight(bool IsRight)
    {
        _walkingRight = IsRight;
    }
}
