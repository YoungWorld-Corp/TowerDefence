using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card5Panel : MonoBehaviour
{
    Image card1;
    Image card2;
    Image card3;
    Image card4;
    Image card5;

    List<Card> currentCardList;

    // Start is called before the first frame update
    void Start()
    {
        List<Card> currentCardList = new List<Card>();
        card1 = GameObject.Find("Card1").GetComponent<Image>();
        card2 = GameObject.Find("Card2").GetComponent<Image>();
        card3 = GameObject.Find("Card3").GetComponent<Image>();
        card4 = GameObject.Find("Card4").GetComponent<Image>();
        card5 = GameObject.Find("Card5").GetComponent<Image>();

        Button btnCard1 = GameObject.Find("Card1").GetComponent<Button>();
        Button btnCard2 = GameObject.Find("Card2").GetComponent<Button>();
        Button btnCard3 = GameObject.Find("Card3").GetComponent<Button>();
        Button btnCard4 = GameObject.Find("Card4").GetComponent<Button>();
        Button btnCard5 = GameObject.Find("Card5").GetComponent<Button>();
        btnCard1.onClick.AddListener(() => OnCardClicked(0));
        btnCard2.onClick.AddListener(() => OnCardClicked(1));
        btnCard3.onClick.AddListener(() => OnCardClicked(2));
        btnCard4.onClick.AddListener(() => OnCardClicked(3));
        btnCard5.onClick.AddListener(() => OnCardClicked(4));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCardImages(List<Card> cardList)
    {
        if (cardList.Count != 5)
        {
            Debug.LogWarning("Card5Panel::SetCardImages cardList length is not 5.");
            return;
        }
        currentCardList = cardList;

        card1.sprite = Resources.Load("Cards/" + cardList[0].ToResourceString(), typeof(Sprite)) as Sprite;
        card2.sprite = Resources.Load("Cards/" + cardList[1].ToResourceString(), typeof(Sprite)) as Sprite;
        card3.sprite = Resources.Load("Cards/" + cardList[2].ToResourceString(), typeof(Sprite)) as Sprite;
        card4.sprite = Resources.Load("Cards/" + cardList[3].ToResourceString(), typeof(Sprite)) as Sprite;
        card5.sprite = Resources.Load("Cards/" + cardList[4].ToResourceString(), typeof(Sprite)) as Sprite;
    }

    void OnCardClicked(int index)
    {
        if (currentCardList.Count != 5)
        {
            Debug.LogWarning("Card5Panel::OnCardClicked cardList length is not 5.");
        }

        List<Card> result = GlobalInstance.GetInstance().GetUser().ChangeOne(currentCardList, index);
        CardUtil.Evaluate(result);
        DeckMadeType type = CardUtil.Evaluate(result);
        GameObject.Find("Text_noti_pro").GetComponent<TMPro.TextMeshProUGUI>().text = type.ToString();
        
        SetCardImages(result);
    }
}
