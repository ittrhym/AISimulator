using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class imageGeneration : MonoBehaviour
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
            this.GlobalState.currentTech = "imageGeneration";
            descText.text = "Price: $25,000\nNeed AI Specific Tech unlocked first\nMoney effect: +$5,000";
            nameText.text = "Image Generation";
        }
    }
}


