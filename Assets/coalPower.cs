using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class coalPower : MonoBehaviour
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
            this.GlobalState.currentTech = "coalPower";
            descText.text = "Price: $300\nEnergy Effect: +10";
            nameText.text = "Coal Power";
        }
    }
}