using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private EdgeCollider2D platformTop;
    private void Start()
    {
        platformTop = GetComponent<EdgeCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision, platformTop, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision, platformTop, false);
        }
    }
}
