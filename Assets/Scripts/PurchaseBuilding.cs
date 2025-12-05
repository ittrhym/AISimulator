using UnityEngine;
using UnityEngine.SceneManagement;

public class PurchaseBuilding : MonoBehaviour
{
    public State GlobalState;
    public int cost;
    public GameObject buildingPrefab;

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
            this.GlobalState.PurchaseBuilding(this.cost, this.buildingPrefab);
        }
    }
}
