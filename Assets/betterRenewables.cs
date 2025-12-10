using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class betterRenewables : MonoBehaviour
{
    public TMP_Text descText;
    public TMP_Text nameText;
    public State GlobalState;
    
    void Start()
    {

    }
    void Update()
    {
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
            this.GlobalState.currentTech = "betterRenewables";
            this.GlobalState.techPrice = 600;
            descText.text = "Price: $600\nNeed Renewable Energy unlocked first\nEnergy Effect: +15";
            nameText.text = "Better Renewables";
        }
    }
}


