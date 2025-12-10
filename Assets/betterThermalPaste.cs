using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class betterThermalPaste : MonoBehaviour
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
            this.GlobalState.currentTech = "betterThermalPaste";
            this.GlobalState.techPrice = 5000;
            descText.text = "Price: $5,000\nNeed Better Heat Exchangers unlocked first\nCooling Effect: +25";
            nameText.text = "Better Thermal Paste";
        }
    }
}

