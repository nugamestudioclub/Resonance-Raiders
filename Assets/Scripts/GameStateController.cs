using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{

    public static GameStateController Controller { get; private set; }
    
    [field: SerializeField]
    public GameState state { get; private set; } = GameState.PRELIMINARY;
    [field: SerializeField]
    public int round { get; private set; } = 0;

    public EnemySpawner spawner;

    [SerializeField]
    List<RoundData> roundDataList = new List<RoundData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextRound();
        }
        switch(state)
        {
            case GameState.COMBAT:
                if (spawner.FinishedSpawning())
                {
                    ChangeState(GameState.PRELIMINARY);
                }
                break;
        }
    }

    public void NextRound()
    {
        switch(state)
        {
            case GameState.PRELIMINARY:
                ChangeState(GameState.COMBAT);
                spawner.SpawnEnemies(roundDataList[round++]);
                break;
            case GameState.COMBAT:
                Debug.Log("Can't start a round while a round is running");
                break;
        }
    }

    public void ChangeState(GameState state)
    {
        this.state = state;
        switch (state)
        {

        }
    }

}

public enum GameState
{
    PRELIMINARY, COMBAT
}
