using Scripts.Player;
using Scripts.Events.Level;
using UnityEngine;

public class DuelContainer : MonoBehaviour
{
    [SerializeField]
    private DuelResults duelResults;
    [SerializeField]
    private Transform invisibleWallLeft;
    [SerializeField]
    private Transform invisibleWallRight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            invisibleWallLeft.GetComponent<BoxCollider2D>().isTrigger = false;
            invisibleWallLeft.tag = "Borders";
            invisibleWallRight.GetComponent<BoxCollider2D>().isTrigger = false;
            invisibleWallRight.tag = "Borders";

            collision.GetComponent<Attack>().enabled = true;
        }
    }

    private void Update()
    {
        if(duelResults.hasWon)
            Destroy(gameObject);
    }
}
