using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopup : MonoBehaviour
{

    [SerializeField] private SettingsPopup settingsPopup;

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public bool IsActive() { 
        return gameObject.activeSelf; 
    }
    public void OnSettingsButton()
    {
        Close();
        settingsPopup.Open();
        Debug.Log("settings clicked");
    }
    public void OnExitGameButton()
    {
        Debug.Log("exit game");
        Application.Quit();
    }
    public void OnReturnToGameButton()
    {
        gameObject.SetActive(true);
        Debug.Log("return to game");
        Close();
    }
}
