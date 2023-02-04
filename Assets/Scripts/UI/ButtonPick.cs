using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonPick : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update

    public void Awake()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        List<Card> result = GlobalInstance.GetInstance().GetUser().BatchDraw(5);
        Debug.Log(" Game Object Click in Progress");


        string resStr = "";
        foreach (Card card in result)
        {
            resStr += " \n"+ card.ToString();
        }

        GameObject.Find("Text_noti_pro").GetComponent<TMPro.TextMeshProUGUI>().text = resStr;
        //1 -> 여기서 텍스트 박스 컴포넌트 어케들고옴???? 야발

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
