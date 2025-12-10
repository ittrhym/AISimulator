using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class lakeCooling : MonoBehaviour
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
            this.GlobalState.currentTech = "lakeCooling";
            this.GlobalState.techPrice = 30000;
            descText.text = "Price: $30,000\nNeed Water Recycling unlocked first\nCooling Effect: +100";
            nameText.text = "Lake Cooling";
        }
    }
}

