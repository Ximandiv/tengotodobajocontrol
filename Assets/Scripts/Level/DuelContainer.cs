using Scripts.Player;
using Scripts.Events.Level;
using UnityEngine;
using Scripts.UI;

public class DuelContainer : MonoBehaviour
{
    [SerializeField]
    private DuelResults duelResults;
    [SerializeField]
    private DuelController duelController;
    [SerializeField]
    private Transform invisibleWallLeft;
    [SerializeField]
    private Transform invisibleWallRight;

    private void Awake()
    {
        duelController = GameObject.FindGameObjectWithTag("DuelController").GetComponent<DuelController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            duelController.DuelAppear();

            invisibleWallLeft.GetComponent<BoxCollider2D>().isTrigger = false;
            invisibleWallLeft.tag = "Borders";
            invisibleWallRight.GetComponent<BoxCollider2D>().isTrigger = false;
            invisibleWallRight.tag = "Borders";

            collision.GetComponent<Attack>().enabled = true;

            LevelOneEvents.InvokePartFinished("Duel");
        }
    }

    private void Update()
    {
        if(duelResults.hasWon)
            Destroy(gameObject);
    }
}
