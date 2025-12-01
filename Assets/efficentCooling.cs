using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class efficentCooling : MonoBehaviour
{
    public TMP_Text descText;
    public TMP_Text nameText;
    public int efficentCoolingPrice;
    void Start()
    {
        efficentCoolingPrice = 300;
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
            descText.text = "Price: $" + efficentCoolingPrice + "\nCooling Effect: +5";
            nameText.text = "Efficent Cooling";

        }
    }
}

