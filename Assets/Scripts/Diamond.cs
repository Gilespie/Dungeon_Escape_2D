using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private int _gemCount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.CollectDiamon(_gemCount);
                Destroy(gameObject);
            }
        }
    }

    public void ChangeGemCount(int value)
    {
        _gemCount = value;
    }
}