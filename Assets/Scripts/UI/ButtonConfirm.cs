using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonConfirm : MonoBehaviour, IPointerDownHandler
{
    public Card5Panel Card5Panel;

    public void Awake()
    {

    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        GlobalInstance instance = GlobalInstance.GetInstance();
        List<Card> hand = instance.GetUser().GetHand();
        
        DeckMadeType madeType = CardUtil.Evaluate(ref hand);
        GameState.Instance.nextTowerLevel = (int)madeType;
        GameState.Instance.SetPickingPhase(false);
    }
    
    void Start()
    {
        Card5Panel = GameObject.Find("Card5Panel").GetComponent<Card5Panel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


