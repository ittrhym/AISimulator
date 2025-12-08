using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseBuildingInspector : MonoBehaviour
{
    public State GlobalState;
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
            this.ExitInspector();
        }
    }

    public void ExitInspector()
    {
        SceneManager.UnloadSceneAsync("BuildingInspector");
        this.GlobalState.inspectingBuilding = false;
    }
}
