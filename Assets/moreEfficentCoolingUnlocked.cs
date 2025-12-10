using UnityEngine;

public class moreEfficentCoolingUnlocked : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject icon;
    public State GlobalState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon.GetComponent<Renderer>().enabled = this.GlobalState.moreEfficentCoolingPurchased;
    }


    // Update is called once per frame
    void Update()
    {
        icon.GetComponent<Renderer>().enabled = this.GlobalState.moreEfficentCoolingPurchased;
    }
}
