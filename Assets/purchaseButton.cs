using UnityEngine;
using UnityEngine.SceneManagement;

public class purchaseButton : MonoBehaviour
{
    public int currentCooling;
    public int currentMoney;
    public bool efficentCoolingPurchased;
    public GameObject efficentCoolingButton;
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
        }
    }
}
