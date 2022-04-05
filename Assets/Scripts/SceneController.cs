using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;
    private Vector3 spawnPoint = new Vector3(0, 0, 5);
    private int numEnemies = 3;
    private GameObject[] arrEnemies;

    [SerializeField] private GameObject iguanaPrefab;
    private GameObject iguana;
    private Vector3 spawnPointIguana = new Vector3(0, 0, 2);
    private int numIguanas = 7;
    private GameObject[] arrIguana;

    [SerializeField] private UIController ui;
    private int score = 0;

    private void Start()
    {
        arrEnemies = new GameObject[numEnemies];
        arrIguana = new GameObject[numIguanas];

        for (int i = 0; i < numIguanas; i++)
        {
            if (arrIguana[i] == null)
            {
                iguana = Instantiate(iguanaPrefab) as GameObject;
                iguana.transform.position = spawnPointIguana;
                float angle = Random.Range(0, 360);
                iguana.transform.Rotate(0, angle, 0);
                arrIguana[i] = iguana;
            }
        }
    }

    void Update()
    {

        for (int i = 0; i < numEnemies; i++)
        {
            if (arrEnemies[i] == null)
            {
                enemy = Instantiate(enemyPrefab) as GameObject;
                enemy.transform.position = spawnPoint;
                float angle = Random.Range(0, 360);
                enemy.transform.Rotate(0, angle, 0);
                arrEnemies[i] = enemy;
                WanderingAI ai = arrEnemies[i].GetComponent<WanderingAI>();
                ai.SetDifficulty(GetDifficulty());
            }
        }

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
        score++;
        ui.UpdateScore(score);
    }

    private void OnDifficultyChanged(int newDifficulty) 
    {
        Debug.Log("Scene.OnDifficultyChanged(" + newDifficulty + ")");
        for (int i = 0; i < arrEnemies.Length; i++)
        {
            WanderingAI ai = arrEnemies[i].GetComponent<WanderingAI>();
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
