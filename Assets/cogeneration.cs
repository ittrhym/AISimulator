using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class cogeneration : MonoBehaviour
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
            this.GlobalState.currentTech = "cogeneration";
            this.GlobalState.techPrice = 20000;
            descText.text = "Price: $20,000\nNeed Cyclone Furnace unlocked first\nEnergy Effect: +75";
            nameText.text = "Cogeneration";
        }
    }
}