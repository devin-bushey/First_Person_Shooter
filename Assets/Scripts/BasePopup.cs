using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{

    public virtual void Open()
    {
        this.gameObject.SetActive(true);
        Messenger.Broadcast(GameEvent.POPUP_OPENED);
    }

    public void Close()
    {

        gameObject.SetActive(false);
        Messenger.Broadcast(GameEvent.POPUP_CLOSED);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
