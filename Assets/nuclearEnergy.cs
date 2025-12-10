using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class nuclearEnergy : MonoBehaviour
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
            this.GlobalState.currentTech = "nuclearEnergy";
            this.GlobalState.techPrice = 1500;
            descText.text = "Price: $1,500\nNeed Better Renewables unlocked first\nEnergy Effect: +20";
            nameText.text = "Nuclear Energy";
        }
    }
}