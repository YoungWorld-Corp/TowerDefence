using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGamePhase
{
    Picking = 0,
    Battle = 1,
    GameEnd = 2,
}

public class GameState : MonoBehaviour
{
    private static GameState instance = null;
    public MobSpawner mobSpawner;
    public ButtonPick buttonPick;

    public int nextTowerLevel { get; set; }

    public EGamePhase currentPhase;
    public int curStage;
    public int lifeRemain;

    // UI
    public GameObject UICanvas;
    public GameObject CardPickingPanel;
    public GameObject GameEndPanel;
    public GameObject Text_Lift;
    public GameObject Text_Stage;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            OnGameStart();

            // don't destory even if scene changes
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // don't make new instance even if scene changes.
            Destroy(this.gameObject);
        }
    }

    void OnGameStart()
    {
        curStage = 1;
        lifeRemain = 10;
        OnChangeLife(0);

        currentPhase = EGamePhase.Picking;
        GameEndPanel.SetActive(false);
    }

    public static GameState Instance
    {
        get
        {
            if (!instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void SetPhase(EGamePhase InPhase)
    {
        
    }
    public void SetGamePhase(EGamePhase InPhase)
    {
        currentPhase = InPhase;

        if (currentPhase == EGamePhase.Picking)
        {
            GlobalInstance.GetInstance().GetUser().OnNewPhase();
            UICanvas.SetActive(true);
            buttonPick.RefreshCard();
        }
        else if (currentPhase == EGamePhase.Battle)
        {
            UICanvas.SetActive(false);
            mobSpawner.StartPhase(curStage);
            OnIncreaseStage();
        }
        else if (currentPhase == EGamePhase.GameEnd)
        {
            UICanvas.SetActive(true);
            CardPickingPanel.SetActive(false);
            GameEndPanel.SetActive(true);
        }
    }

    public void OnChangeLife(int delta)
    {
        lifeRemain += delta;
        if (lifeRemain <= 0)
        {
            SetGamePhase(EGamePhase.GameEnd);
        }
        
        Text_Lift.GetComponent<TMPro.TextMeshProUGUI>().text = "life : " + lifeRemain.ToString();
    }

    public void OnIncreaseStage()
    {
        curStage++;
        
        Text_Stage.GetComponent<TMPro.TextMeshProUGUI>().text = "stage : " + curStage.ToString();
    }
}