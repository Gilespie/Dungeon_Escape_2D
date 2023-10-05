using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health = 4;
    [SerializeField] private float _speedMovement = 3f;
    [SerializeField] private float _powerJump = 5f;
    [SerializeField] private int _gemsCount = 0;

    public int Health { get; set; }
    public int GemsCount => _gemsCount;

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
        Health = _health;
    }
    
    private void Update()
    {
        Movement();
        Attack();
    }

    public void Damage()
    {
        if (Health <= 0) return;

        Health--;
        UIManager.Instance.UpdateLives(Health);

        if (Health <= 0)
        {
            _playerAnimation.Death();
        }
    }

    public void AddDiamond(int amount)
    {
        if (amount <= 0) return;

        _gemsCount += amount;
        UIManager.Instance.UpdateGemCount(_gemsCount);
    }

    public void SubstrucDiamond(int amount)
    {
        if (amount <= 0) return;

        _gemsCount -= amount;
        UIManager.Instance.UpdateGemCount(_gemsCount);
    }

    private void Movement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal") * _speedMovement; //Input.GetAxisRaw("Horizontal") * _speedMovement;
        _isGrounded = CheckGrounded();

        Flip(horizontalInput);

        if (CrossPlatformInputManager.GetButtonDown("B_Jump_Button") && CheckGrounded() == true) //Input.GetKeyDown(KeyCode.Space)
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
        if (CrossPlatformInputManager.GetButtonDown("A_Attack_Button") && CheckGrounded() == true) //(Input.GetMouseButtonDown(0) 
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