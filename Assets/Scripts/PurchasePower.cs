using UnityEngine;
using TMPro;

public class PurchasePower : MonoBehaviour
{
    public GameObject powerDisplay;
    public State GlobalState;

    void Update()
    {
        this.DisplayPowerCount();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero
            );
            if (hit == false || hit.collider.gameObject != gameObject)
            {
                return;
            }
            if (this.GlobalState.money > 100)
            {
                this.GlobalState.money -= 100;
                this.GlobalState.power += 100;
            }
        }
    }
    public void DisplayPowerCount()
    {
        float status = this.GlobalState.power;
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
        this.powerDisplay.GetComponent<TextMeshPro>().text = "" + amt + suffix;
    }
}
