using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class longerLastingChips : MonoBehaviour
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
            this.GlobalState.currentTech = "longerLastingChips";
            descText.text = "Price: $10,000\nNeed Energy Efficent Chips unlocked first\nMoney effect: +$2,500";
            nameText.text = "Longer Lasting Chips";
        }
    }
}


