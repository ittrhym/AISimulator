using UnityEngine;

public class betterThermalPasteUnlock : MonoBehaviour
{
    public GameObject icon;
    public State GlobalState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon.GetComponent<Renderer>().enabled = this.GlobalState.betterThermalPastePurchased;
    }


    // Update is called once per frame
    void Update()
    {
        icon.GetComponent<Renderer>().enabled = this.GlobalState.betterThermalPastePurchased;
    }
}
