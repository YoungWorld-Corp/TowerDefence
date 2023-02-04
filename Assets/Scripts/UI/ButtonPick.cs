using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonPick : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public Card5Panel card5Panel;

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
        card5Panel.SetCardImages(result);

        GameObject.Find("Text_noti_pro").GetComponent<TMPro.TextMeshProUGUI>().text = resStr;
        //1 -> ���⼭ �ؽ�Ʈ �ڽ� ������Ʈ ���ɵ����???? �߹�

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
