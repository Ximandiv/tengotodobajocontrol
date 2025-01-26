using UnityEngine;

public class DuelContainer : MonoBehaviour
{
    [SerializeField]
    private Transform invisibleWallLeft;
    [SerializeField]
    private Transform invisibleWallRight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            invisibleWallLeft.GetComponent<BoxCollider2D>().isTrigger = false;
            invisibleWallRight.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
