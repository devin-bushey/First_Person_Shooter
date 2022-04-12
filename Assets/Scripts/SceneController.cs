using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;
    //private Vector3 spawnPoint = new Vector3(0, 0, 5);
    //private int numEnemies = 3;
    private GameObject[] arrEnemies;
    public int enemiesAlive = 2;

    [SerializeField] private UIController ui;
    private int score = 0;

    private void Start()
    {
        enemiesAlive = 2;
    }

    //public void SpawnEnemies(GameObject spawnPt, int numEnemies)
    //{
    //    arrEnemies = new GameObject[numEnemies];
    //    enemiesAlive += numEnemies;

    //    for (int i = 0; i < numEnemies; i++)
    //    {
    //        enemy = Instantiate(enemyPrefab) as GameObject;
    //        enemy.transform.position = spawnPt.transform.position;
    //        Debug.Log("Enemy position is: " + enemy.transform.position);
    //        float angle = Random.Range(0, 360);
    //        enemy.transform.Rotate(0, angle, 0);
    //        arrEnemies[i] = enemy;
    //        //WanderingAI ai = arrEnemies[i].GetComponent<WanderingAI>();
    //        //ai.SetDifficulty(GetDifficulty());
    //        Enemy ai = arrEnemies[i].GetComponent<Enemy>();
    //        ai.SetDifficulty(GetDifficulty());
    //    }
    //}

    void Update()
    {

    }

    
    void Awake() 
    {
        Messenger<int>.AddListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.AddListener(GameEvent.ENEMY_DEAD, this.OnEnemyDead);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.AddListener(GameEvent.RESTART_GAME, OnRestartGame);
    }
    void OnDestroy() 
    {
        Messenger<int>.RemoveListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
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

    private void OnDifficultyChanged(int newDifficulty) 
    {
        Debug.Log("Scene.OnDifficultyChanged(" + newDifficulty + ")");
        for (int i = 0; i < arrEnemies.Length; i++)
        {
            //WanderingAI ai = arrEnemies[i].GetComponent<WanderingAI>();
            //ai.SetDifficulty(newDifficulty);
            Enemy ai = arrEnemies[i].GetComponent<Enemy>();
            ai.SetDifficulty(newDifficulty);
        }
    }

    int GetDifficulty()
    {
        return PlayerPrefs.GetInt("difficulty", 1);
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
