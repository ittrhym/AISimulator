using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class improvedPowerplants : MonoBehaviour
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
            this.GlobalState.currentTech = "improvedPowerplants";
            descText.text = "Price: $500\nNeed Coal Power unlocked first\nEnergy Effect: +15";
            nameText.text = "Improved Powerplants";
        }
    }
}