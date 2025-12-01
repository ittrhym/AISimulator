using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;

    void Start()
    {
        if (!gameObject.GetComponent<PolygonCollider2D>())
        {
            gameObject.AddComponent<PolygonCollider2D>();
        }
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
            SceneManager.LoadScene(this.SceneName, LoadSceneMode.Single);
        }
    }
}
