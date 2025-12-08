using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class improvedMining : MonoBehaviour
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
            this.GlobalState.currentTech = "improvedMining";
            descText.text = "Price: $1,000\nNeed Improved Powerplants unlocked first\nEnergy Effect: +25";
            nameText.text = "Improved Mining";
        }
    }
}