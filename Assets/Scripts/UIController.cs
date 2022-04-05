using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image crossHair;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private GameOverPopup gameOverPopup;

    private int popupsOpen = 0;

    // update score display
    public void UpdateScore(int newScore){
        scoreValue.text = newScore.ToString();
    }

    public void SetGameActive(bool active)
    {
        if (active)
        {
            //Debug.Log("SetGameActive active");
            Time.timeScale = 1; // unpause the game
            Cursor.visible = false; // hide cursor
            Cursor.lockState = CursorLockMode.Locked; // hide the cursor
            crossHair.gameObject.SetActive(true); // show the crosshair
            Messenger.Broadcast(GameEvent.GAME_ACTIVE);
        }
        else
        {
            Debug.Log("SetGameActive inactive");
            Time.timeScale = 0; // pause the game
            Cursor.visible = true; // show cursor
            Cursor.lockState = CursorLockMode.None; // show the cursor
            crossHair.gameObject.SetActive(false); // turn off the crosshair

            Messenger.Broadcast(GameEvent.GAME_INACTIVE);
        }
    }

    private void Start()
    {
        SetGameActive(true);
        UpdateScore(0);
        healthBar.fillAmount = 1; 
        healthBar.color = Color.green;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (this.popupsOpen == 0)
            {
                optionsPopup.Open();
                //SetGameActive(true);
                //Messenger.Broadcast(GameEvent.GAME_INACTIVE);
            }
        }
    }

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.HEALTH_CHANGED, this.OnHealthChanged);
        Messenger.AddListener(GameEvent.POPUP_OPENED, this.OnPopupsOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, this.OnPopupsClosed);
    }

    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, this.OnPopupsOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, this.OnPopupsClosed);
    }

    private void OnHealthChanged(float healthPercentage)
    {
        Debug.Log("UI.OnHealthChanged(" + healthPercentage + ")");
        PlayerCharacter player = GetComponent<PlayerCharacter>();
        healthBar.color = Color.Lerp(Color.green, Color.red, 1-healthPercentage);
        healthBar.fillAmount = healthPercentage;
    }

    private void OnPopupsOpened()
    {
        Debug.Log("UI.OnPopupsOpened(" + this.popupsOpen + ")");

        if (this.popupsOpen == 0)
        {
            SetGameActive(false);
        }
        this.popupsOpen++;
        
    }

    private void OnPopupsClosed()
    {
        Debug.Log("UI.OnPopupsClosed(" + this.popupsOpen + ")");
        this.popupsOpen--;
        if (this.popupsOpen == 0)
        {
            SetGameActive(true);
        }

    }

    public void ShowGameOverPopup()
    {
        gameOverPopup.Open();
    }



}
