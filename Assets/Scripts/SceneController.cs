using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public int enemiesAlive;

    [SerializeField] private UIController ui;
    private int score = 0;


    private void Start()
    {
        enemiesAlive = 5;
        
    }
    
    void Awake() 
    {
        Messenger.AddListener(GameEvent.ENEMY_DEAD, this.OnEnemyDead);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.AddListener(GameEvent.RESTART_GAME, OnRestartGame);
    }
    void OnDestroy() 
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEAD, this.OnEnemyDead);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.RemoveListener(GameEvent.RESTART_GAME, OnRestartGame);
    }
    void OnEnemyDead()
    {
        Debug.Log("Enemy Dead. " + enemiesAlive + " alive");
        enemiesAlive--;
        score++;
        ui.UpdateScore(score);
    }

    private void OnPlayerDead()
    {
        ui.ShowGameOverPopup();
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
