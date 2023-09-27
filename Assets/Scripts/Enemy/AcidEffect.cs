using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    [SerializeField] private float _speedMovement = 3f;
    [SerializeField] private float _timeToDestroy = 5f;
    private void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }

    private void Update()
    {
        Vector3 direction = Vector3.right * _speedMovement * Time.deltaTime;
        transform.Translate(direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            IDamageable hit = other.gameObject.GetComponent<IDamageable>();

            if (hit != null)
            {
                hit.Damage();
                Destroy(gameObject);
            }
        }
    }
}