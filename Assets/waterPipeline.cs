using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class waterPipeline : MonoBehaviour
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
            this.GlobalState.currentTech = "waterPipeline";
            descText.text = "Price: $20,000\nNeed More Coolent unlocked first\nCooling Effect: +75";
            nameText.text = "Water Pipeline";
        }
    }
}

