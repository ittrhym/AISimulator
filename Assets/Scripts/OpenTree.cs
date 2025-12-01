using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTree : MonoBehaviour
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
            SceneManager.LoadScene("Tree", LoadSceneMode.Single);
        }
    }
}
