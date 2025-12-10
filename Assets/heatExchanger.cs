using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class heatExchanger : MonoBehaviour
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
            this.GlobalState.currentTech = "heatExchanger";
            this.GlobalState.techPrice = 500;
            descText.text = "Price: $500\nNeed Improved Chillers unlocked first\nCooling Effect: +20";
            nameText.text = "Better Heat Exchangers";
        }
    }
}

