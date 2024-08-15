using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonPick : MonoBehaviour, IPointerDownHandler
{
    public Card5Panel card5Panel;

    public void Awake()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        RefreshCard();
    }

    public void RefreshCard()
    {
        //Output the name of the GameObject that is being clicked
        List<Card> result = GlobalInstance.GetInstance().GetUser().BatchDraw(5);
        Debug.Log(" Game Object Click in Progress");


        string resStr = "";
        foreach (Card card in result)
        {
            resStr += " \n"+ card;
        }
        card5Panel.SetCardImages(result);

        DeckMadeType type = CardUtil.Evaluate(ref result).deckMadeType;

        GameObject.Find("Text_noti_pro").GetComponent<TMPro.TextMeshProUGUI>().text = type.ToString();
        GameObject.Find("Text_Remain").GetComponent<TMPro.TextMeshProUGUI>().text = "Remain : " + GlobalInstance.GetInstance().GetUser().remainChance;

    }
    void Start()
    {
        RefreshCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
