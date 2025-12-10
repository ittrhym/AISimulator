using UnityEngine;

public class moreCoolentUnlock : MonoBehaviour
{
    public GameObject icon;
    public State GlobalState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon.GetComponent<Renderer>().enabled = this.GlobalState.moreCoolentPurchased;
    }


    // Update is called once per frame
    void Update()
    {
        icon.GetComponent<Renderer>().enabled = this.GlobalState.moreCoolentPurchased;
    }
}
