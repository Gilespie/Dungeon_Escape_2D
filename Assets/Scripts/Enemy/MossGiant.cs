using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    protected override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        if (Health == 0) return;

        Health--;
        animator.SetTrigger("Hit");
        isHit = true;
        animator.SetBool("InCombat", true);

        if (Health <= 0)
        {
            Health = 0;
            animator.SetTrigger("Death");
            StartCoroutine(FadeInRoutine());
            SpawnDiamond();
        }
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