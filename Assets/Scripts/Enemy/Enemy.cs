using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float speedMovement;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] private GameObject diamondPrefab;

    protected Vector3 currentTarget;
    protected Animator animator;
    protected SpriteRenderer sprite;
    protected Player player;
    protected bool isHit = false;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        Init();
    }

    protected virtual void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") == true && animator.GetBool("InCombat") == false) return;


        Movement();
        FlipSprite();

    }

    protected virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Movement()
    {
        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            animator.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            animator.SetTrigger("Idle");
        }

        if(isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speedMovement * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.position, player.transform.localPosition);

        if (distance >= 2.0f)
        {
            isHit = false;
            animator.SetBool("InCombat", false);
        }
    }

    protected virtual void SpawnDiamond()
    {
        GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
        diamond.GetComponent<Diamond>().ChangeGemCount(gems);
    }

    protected virtual void FlipSprite()
    {
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else if (currentTarget == pointB.position)
        {
            sprite.flipX = false;
        }

        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (direction.x > 0 && animator.GetBool("InCombat") == true)
        {
            sprite.flipX = false;
        }
        else if (direction.x < 0 && animator.GetBool("InCombat") == true)
        {
            sprite.flipX = true;
        }
    }
}