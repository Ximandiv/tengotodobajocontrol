using UnityEngine;

[CreateAssetMenu(fileName = "GameStatus", menuName = "Scriptable Objects/GameStatus")]
public class GameStatus : ScriptableObject
{
    [SerializeField]
    private bool gameStarted = false;
    public bool GameStarted
    {
        get { return gameStarted; }
        set { gameStarted = value; }
    }

    [SerializeField]
    private bool startCutsceneEnd = false;
    public bool StartCutsceneEnd
    {
        get { return startCutsceneEnd; }
        set { startCutsceneEnd = value; }
    }

    [SerializeField]
    private bool gameFinished = false;
    public bool GameFinished
    {
        get { return gameFinished; }
        set { gameFinished = value; }
    }
}
