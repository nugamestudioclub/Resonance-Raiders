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

    public DeflectorPlacer constructor;
    public ShootProjectile player;

    // Start is called before the first frame update
    void Start()
    {
        int _r = 0;
        foreach(RoundData r in roundDataList)
        {
            if (_r != 0)
            {
                r.pathCreator.gameObject.SetActive(false);
            }
            _r++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(state)
        {
            case GameState.COMBAT:
                constructor.enabled = false;
                player.enabled = true;
                if (spawner.FinishedSpawning())
                {
                    constructor.enabled = true;
                    constructor.Clear();
                    
                    NextRound();
                    //ChangeState(GameState.PRELIMINARY);
                }
                break;
            case GameState.PRELIMINARY:
                player.enabled = false;
                constructor.enabled = true;
                break;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextRound();
        }
    }

    public void NextRound()
    {
        switch(state)
        {
            case GameState.PRELIMINARY:
                
                ChangeState(GameState.COMBAT);
                roundDataList[round].pathCreator.GetComponent<DisplayPath>().RemoveColliders();
              
                spawner.SpawnEnemies(roundDataList[round]);
                
                break;
            case GameState.COMBAT:
              
                ChangeState(GameState.PRELIMINARY);
                roundDataList[round].pathCreator.gameObject.SetActive(false);
                round = round + 1;
                if (round >= roundDataList.Count)
                {
                    //Scene Transition
                    //Win screen
                }
                roundDataList[round].pathCreator.gameObject.SetActive(true);
                //Debug.Log("Can't start a round while a round is running");
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
