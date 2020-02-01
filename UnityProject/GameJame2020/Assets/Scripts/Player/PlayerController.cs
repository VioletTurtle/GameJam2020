using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the original player controller that we made in class.
public class PlayerController : MonoBehaviour
{
    public int damageTaken = 5;                    //The amount of damage the character takes every hit
    private int originalDmgTaken;                   //The original amount of damage, incase you want just a boss or something to deal more damage
    public float timeBtwDmgTaken = 2.5f;            //How often the player can be hit (in seconds)
    private float startTimeBtwDmgTaken;
    public float kbx, kby;                          //Knockback values for when the play is hit
    public float flashTime = .5f;                   //how long the player flashes for when hit
    private Color origColor, flashColor = Color.red;
    private SpriteRenderer playerSprite;

    public float walkSpeed = 15f;                   //how fast a character moves left and right while walking
    public float jumpSpeed = 10f;                    //how much force is put into a jump when holding down the jump button
    public float gravity = 2.5f;
    public float maxJumpTime = 0.1f;                //how long force can be applied while jumping by holding down the jump button
    public float physicsUpdateFrequency = 60f;

    private Rigidbody2D _playerRigidBody;           //a 2 dimmensional rigid body for movement physics

    private bool _isOnGround = true;                       //a boolean to represent if a character is currently on the ground
    private float _currentJumpTime = 0f;            //a float value that accumulates time while the character is jumping

    private bool _isJumping = false;

    private Transform _spriteTransform;
    private Transform _attackObjectTransform;
    private bool _facingRight = true;

    public string upButton = "w";
    public string leftButton = "a";
    public string rightButton = "d";

    public Animator movementAnimator;
    private bool inAir = false;   

    // Start is called before the first frame update
    void Start()
    {
        _spriteTransform = GameObject.FindGameObjectWithTag("Player Sprite").GetComponent<Transform>();
        _attackObjectTransform = GameObject.FindGameObjectWithTag("Attack Object").GetComponent<Transform>();
        _playerRigidBody = GetComponent<Rigidbody2D>();
        InvokeRepeating("PhysicsUpdate", 0f, 1f / physicsUpdateFrequency);
        InvokeRepeating("UpdateFacing", 0f, 1f / physicsUpdateFrequency);
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        origColor = playerSprite.color;
        originalDmgTaken = damageTaken;
        startTimeBtwDmgTaken = timeBtwDmgTaken;
    }

    public void copyScript(PlayerController copiedScript)
    {
        //grabs the first child object on the player
        _spriteTransform = GetComponent<Transform>().GetChild(0);
        //grabs the fourth child object on the player
        _attackObjectTransform = GetComponent<Transform>().GetChild(3);
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _isOnGround = copiedScript._isOnGround;
        _isJumping = copiedScript._isJumping;
        //if the player is facing a different direction before switching
        if (!copiedScript._facingRight && _facingRight)
        {
            _facingRight = false;
        }
        else if (copiedScript._facingRight && !_facingRight)
        {
            _facingRight = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyBindsManager.KBM.Jump) && !_isJumping && _isOnGround)
        {
            _isJumping = true;
        }
        else if (Input.GetKeyUp(KeyBindsManager.KBM.Jump) && _isJumping)
        {
            _isJumping = false;
        }
        AnimateCharacter();
        timeBtwDmgTaken -= Time.deltaTime;
    }

    private void AnimateCharacter()
    {
        if (_isJumping || !_isOnGround)
            inAir = true;
        else if (!_isJumping && _isOnGround)
            inAir = false;
        movementAnimator.SetFloat("moveSpeed", Mathf.Abs(_playerRigidBody.velocity.x));
        movementAnimator.SetBool("isJumping", inAir);
        movementAnimator.SetBool("isGrounded", _isOnGround);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(timeBtwDmgTaken <= 0)
        {
            if (other.gameObject.tag == "Enemy")
            {
                PlayerTakeDamage();
                timeBtwDmgTaken = startTimeBtwDmgTaken;
            }
        }
    }

    public void PlayerTakeDamage()
    {
        //Debug.Log("I touched something!");
        StaticPlayerController.Instance.RemoveHealthFromPlayer(damageTaken);
        //Debug.Log("I took damage!");
        StartCoroutine(Flash());
        _playerRigidBody.velocity += new Vector2(kbx, kby);
        damageTaken = originalDmgTaken;
    }

    IEnumerator Flash()
    {
        playerSprite.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        playerSprite.color = origColor;
    }

    private void PhysicsUpdate()
    {
        if (_isJumping && _currentJumpTime < maxJumpTime)
        {
            _currentJumpTime += 1f / physicsUpdateFrequency;
            _playerRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
        if (!_isOnGround)
        {
            _playerRigidBody.velocity -= new Vector2(0f, gravity);
        }

        if (_facingRight && Input.GetKey(KeyBindsManager.KBM.Left))
        {
            _facingRight = false;
            UpdateFacing();
        }
        else if (!_facingRight && Input.GetKey(KeyBindsManager.KBM.Right))
        {
            _facingRight = true;
            UpdateFacing();
        }

        float walkDirection = (Input.GetKey(KeyBindsManager.KBM.Right) ? 1f : 0f) - (Input.GetKey(KeyBindsManager.KBM.Left) ? 1f : 0f);
        _playerRigidBody.velocity = new Vector2(walkDirection * walkSpeed, _playerRigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Environment" && _playerRigidBody.velocity.y <= 0f) // the second part is to make sure the player is falling onto the ground
        {
            //set the player to be on the ground
            _isOnGround = true;
            _currentJumpTime = 0f;

            // the next line will make running up slopes slower 
            // but disallow the player to charge up a slope and end up in the air
            // it also fixes a bug that causes the player to jump extra high sometimes
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, 0f);
        }
    }

    //if the player continues to collide... 
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Environment" && _playerRigidBody.velocity.y <= 0f) // the second part is to make sure the player is falling onto the ground
        {
            //set the player to be on the ground
            _isOnGround = true;
            _currentJumpTime = 0f;

            // the next line will make running up slopes slower 
            // but disallow the player to charge up a slope and end up in the air
            // it also fixes a bug that causes the player to jump extra high sometimes
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, 0f);
        }

    }

    //when the player is no longer colliding...
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Environment" /*&& _playerRigidBody.velocity.y >= 0f*/)
            //set the player to no longer be on the ground
            _isOnGround = false;
    }


    //checks to see if the player needs to be flipped and flips him
    private void UpdateFacing()
    {
        Transform PlayerTransform = GetComponent<Transform>();

        //create a vectors used for flipping transforms and colliders
        Vector3 transformMod = new Vector3(Mathf.Abs(PlayerTransform.localScale.x) * 2, 0, 0);

        if (!_facingRight)
        {
            //if the player is facing right and needs to be facing left...
            if (PlayerTransform.localScale.x > 0)
            {
                PlayerTransform.localScale -= transformMod;
            }
        }
        else if (_facingRight)
        {
            //if the player is facing left and needs to be facing right...
            if (PlayerTransform.localScale.x < 0)
            {
                PlayerTransform.localScale += transformMod;
            }
        }
    }

}
