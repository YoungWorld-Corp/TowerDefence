using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState instance = null;

    public int nextTowerLevel { get; set; }

    public GameObject UICanvas;
    private bool _bPickingPhase = false;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            // don't destory even if scene changes
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // don't make new instance even if scene changes.
            Destroy(this.gameObject);
        }
    }

    public static GameState Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void SetPickingPhase(bool bPickingPhase)
    {
        _bPickingPhase = bPickingPhase;

        if (_bPickingPhase)
        {
            UICanvas.SetActive(true);
        }
        else
        {
            UICanvas.SetActive(false);
        }
    }
}