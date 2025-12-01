using UnityEngine;

public class efficentCoolingUnlocked : MonoBehaviour
{
    public bool efficentCoolingPurchased;
    public GameObject icon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(efficentCoolingPurchased == false)
        {
            return;
        }
        icon.GetComponent<Renderer>().enabled = true;
    }
}
