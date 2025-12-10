using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class mostEfficentCooling : MonoBehaviour
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
            this.GlobalState.currentTech = "mostEfficentCooling";
            this.GlobalState.techPrice = 5000;
            descText.text = "Price: $5,000\nNeed More Efficent Cooling unlocked first\nCooling Effect: +30";
            nameText.text = "Most Efficent Cooling";
        }
    }
}

