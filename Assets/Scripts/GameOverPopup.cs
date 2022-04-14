using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopup : BasePopup
{
    [SerializeField] public TextMeshProUGUI results;

    public void ShowScore(string score)
    {
        results.text = "Game Over.\nYou destroyed " + score + " enemies";
    }

    public void OnExitGameButton()
    {
        Debug.Log("Exiting Game"); Application.Quit();
    }

    public void OnStartAgainButton()
    {
        Close();
        Messenger.Broadcast(GameEvent.RESTART_GAME);
    }
}
