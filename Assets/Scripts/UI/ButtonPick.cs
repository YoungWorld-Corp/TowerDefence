using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPick : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update

    public void Awake()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        Debug.Log(name + "Game Object Click in Progress");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
