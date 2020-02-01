using UnityEngine;

public class EnemyMovementWallTrigger : MonoBehaviour
{
    private EnemyMovement parentMovement;

    private void Start()
    {
        parentMovement = GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Environment" || collision.tag == "Enemy"/* || collision.tag == "Player"*/)
        {
            parentMovement.ChangeDirection();
        }
    }

}
