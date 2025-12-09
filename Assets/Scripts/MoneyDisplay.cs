using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    public State GlobalState;
    public GameObject moneyDisplay;
    void Update()
    {
        float status = this.GlobalState.money;
        int amt;
        string suffix = "";
        if (status > 1000000000)
        {
            amt = (int)(status/1000000000);
            suffix = "B";
        }
        else if (status > 1000000)
        {
            amt = (int)(status/1000000);
            suffix = "M";
        }
        else if (status > 1000)
        {
            amt = (int)(status/1000);
            suffix = "K";
        }
        else
        {
            amt = (int)status;
        }
        this.moneyDisplay.GetComponent<TextMeshPro>().text = "" + amt + suffix;
    }
}
