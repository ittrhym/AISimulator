using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;
    public State GlobalState;

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
            foreach (GameObject ewaste in this.GlobalState.EWaste.Values)
            {
                ewaste.SetActive(!ewaste.activeSelf);
            }
            foreach (State.Building building in this.GlobalState.AvailableBuildings.available)
            {
                building.gameObject.SetActive(!building.gameObject.activeSelf);
            }
            foreach (State.Building building in this.GlobalState.Buildings.Values)
            {
                building.gameObject.SetActive(!building.gameObject.activeSelf);
            }
            SceneManager.LoadScene(this.SceneName, LoadSceneMode.Single);
        }
    }
}
