using UnityEngine;
using UnityEngine.SceneManagement;

public class Building : MonoBehaviour
{
    public Scene overlayScene;
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
            if (
                this.GlobalState.inspectingBuilding
                ||
                !this.GlobalState.Buildings.ContainsKey(new Vector2(
                    gameObject.transform.position.x,
                    gameObject.transform.position.y
                ))
            )
            {
                return;
            }
            this.GlobalState.inspectingBuilding = true;
            this.GlobalState.currentBuildingAddress = new Vector2(
                gameObject.transform.position.x,
                gameObject.transform.position.y
            );
            SceneManager.LoadSceneAsync("BuildingInspector", LoadSceneMode.Additive);
        }
    }
}
