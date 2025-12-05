using UnityEngine;

public class DestroyBuilding : MonoBehaviour
{
    public State GlobalState;
    public GameObject closeButton;

    void Start()
    {
        this.GlobalState.InitEWaste();
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
            if (this.GlobalState.DestroyBuilding(this.GlobalState.currentBuildingAddress))
            {
                this.closeButton.GetComponent<CloseBuildingInspector>().ExitInspector();
                this.GlobalState.inspectingBuilding = false;
            }
        }
    }
}
