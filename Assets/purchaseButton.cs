using UnityEngine;
using UnityEngine.SceneManagement;

public class purchaseButton : MonoBehaviour
{
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
            if (currentMoney <= 300)
            {
                currentCooling = currentCooling + 5
            } 
        }
    }
}
