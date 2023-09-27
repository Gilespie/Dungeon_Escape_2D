using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canAttack = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();

        if(hit != null && _canAttack == true)
        {
            hit.Damage();
            _canAttack = false;
            StartCoroutine(ResetValueRoutine());
        }
    }

    private IEnumerator ResetValueRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _canAttack = true;
    }
}