using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class recycledNuclearFuel : MonoBehaviour
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
            print("test");
            this.GlobalState.currentTech = "recycledNuclearFuels";
            this.GlobalState.techPrice = 15000;
            descText.text = "Price: $15,000\nNeed Nuclear Energy unlocked first\nEnergy Effect: +50";
            nameText.text = "Recycled Nuclear Fuels";
        }
    }
}