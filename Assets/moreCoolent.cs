using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class moreCoolent : MonoBehaviour
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
            this.GlobalState.currentTech = "moreCoolent";
            descText.text = "Price: $10,000\nNeed Better Thermal Paste unlocked first\nCooling Effect: +50";
            nameText.text = "More Coolent";
        }
    }
}

