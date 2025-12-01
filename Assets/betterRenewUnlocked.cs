using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class betterRenewUnlocked : MonoBehaviour
{
    public TMP_Text CanvasText;
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
            CanvasText.text = "test";

        }
    }
}
