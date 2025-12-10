using UnityEngine;

public class highHeatEfficentChipUnlock : MonoBehaviour
{
    public GameObject icon;
    public State GlobalState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon.GetComponent<Renderer>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
