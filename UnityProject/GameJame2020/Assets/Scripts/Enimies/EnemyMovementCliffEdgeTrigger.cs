using UnityEngine;

public class EnemyMovementCliffEdgeTrigger : MonoBehaviour
{
    private EnemyMovement parentMovement;

    private void Start()
    {
        parentMovement = GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Environment")
        {
            parentMovement.ChangeDirection();
        }
    }
}
