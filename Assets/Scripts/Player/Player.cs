using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float _speedMovement = 3f;
    [SerializeField] private float _powerJump = 5f;
    [SerializeField] private int _diamondCount = 0;
    public int Health { get; set; }

    private float _rayDistance = 0.6f;
    private bool _isGrounded = false;
    private bool _resetJump = false;
    private Rigidbody2D _rigidBody;
    private PlayerAnimation _playerAnimation;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordSprite;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    
    private void Update()
    {
        Movement();
        Attack();
    }

    public void Damage()
    {
        Health--;

        if (Health == 0)
        {
            _playerAnimation.Death();
        }
    }

    public void CollectDiamon(int amount)
    {
        if (amount <= 0) return;

        _diamondCount += amount;
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal") * _speedMovement;
        _isGrounded = CheckGrounded();

        Flip(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space) && CheckGrounded() == true)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _powerJump);
            _playerAnimation.Jump(true);
            StartCoroutine(ResetJumpRoutine());
        }

        _rigidBody.velocity = new Vector2(horizontalInput, _rigidBody.velocity.y);

        _playerAnimation.Move(horizontalInput);
    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down * _rayDistance, Color.red);

        if (hit.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnimation.Jump(false);
                return true;
            }
        }

        return false;
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && CheckGrounded() == true)
        {
            _playerAnimation.Attack();
        }
    }

    private void Flip(float speedX)
    {
        if (speedX > 0)
        {
            _playerSprite.flipX = false;
            _swordSprite.flipX = false;
            _swordSprite.flipY = false;

            Vector3 newPos = _swordSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordSprite.transform.localPosition = newPos;
        }
        else if (speedX < 0)
        {
            _playerSprite.flipX = true;
            _swordSprite.flipX = true;
            _swordSprite.flipY = true;

            Vector3 newPos = _swordSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordSprite.transform.localPosition = newPos;
        }
    }

    private IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
}