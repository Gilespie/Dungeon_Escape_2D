using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }
    [SerializeField] private GameObject _acidPrefab;

    protected override void Init()
    {
        base.Init();
        Health = base.health;
    }

    protected override void Movement()
    {
        //sit
    }

    public void Damage()
    {
        if (Health == 0) return;

        Health--;

        if (Health <= 0)
        {
            Health = 0;
            animator.SetTrigger("Death");
            StartCoroutine(FadeInRoutine());
            SpawnDiamond();
        }
    }

    public void Attack()
    {
        Instantiate(_acidPrefab, transform.position, Quaternion.identity);
    }

    private IEnumerator FadeInRoutine()
    {
        Color a = sprite.color;

        while (sprite.color.a > 0)
        {
            a.a -= 0.001f;
            sprite.color = a;
            yield return null;
        }

        Destroy(gameObject);
    }
}