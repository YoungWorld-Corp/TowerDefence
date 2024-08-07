using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHP(float HpRatio)
    {
        Slider slider = GetComponent<Slider>();
        slider.value = HpRatio;
    }
}
