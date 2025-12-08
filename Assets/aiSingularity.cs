using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class aiSingularity : MonoBehaviour
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
            this.GlobalState.currentTech = "aiSingularity";
            descText.text = "Price: $500,000\nNeed Lake Cooling or Water Pipeline, Nuclear Fusion or Cogeneration, and High Energy Efficency or Video Generation unlocked first\nWin the Game!";
            nameText.text = "AI Singularity";
        }
    }
}

