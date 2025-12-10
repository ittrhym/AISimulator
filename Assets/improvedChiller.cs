using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class improvedChiller : MonoBehaviour
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
            this.GlobalState.currentTech = "improvedChillers";
            this.GlobalState.techPrice = 300;
            descText.text = "Price: $300\nCooling Effect: +10";
            nameText.text = "Improved Chillers";
        }
    }
}

