using UnityEngine;

public class CreateBuilding : MonoBehaviour
{

    public State GlobalState;
    public GameObject BuildingPrefab;
    private GameObject current;
    private bool fixAvailableBuildings;

    void Start()
    {
        this.GlobalState.InitAvailableBuildings();
        this.GlobalState.InitPurchasedBuildings();
        this.GlobalState.InitBuildings();
    }

    void Update()
    {
        // Distribute buildings in center
        while (this.GlobalState.PurchasedBuildings.Count > 0)
        {
            GameObject newAvailableBuilding = Instantiate(
                this.GlobalState.PurchasedBuildings.Pop().prefab,
                new Vector3(0, 0, 0),
                Quaternion.identity
            );
            this.GlobalState.AvailableBuildings.New(newAvailableBuilding);
            this.fixAvailableBuildings = true;
        }
        if (this.fixAvailableBuildings)
        {
            Renderer renderer = gameObject.transform.GetComponent<Renderer>();
            float dx = renderer.bounds.size.x/(this.GlobalState.AvailableBuildings.available.Count+1);
            float x = renderer.bounds.center.x-renderer.bounds.extents.x;
            float y = gameObject.transform.position.y;
            foreach (State.Building availableBuilding in this.GlobalState.AvailableBuildings.available)
            {
                x += dx;
                availableBuilding.gameObject.transform.position = new Vector3(x, y, -1);
            }
            this.fixAvailableBuildings = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero
            );
            if (hit == false)
            {
                return;
            }
            if (
                this.current == null
                &&
                this.GlobalState.AvailableBuildings.IsAvailable(hit.collider.gameObject)
            )
            {
                this.current = hit.collider.gameObject;
            }
        }
        if (this.current != null)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = -1;
                this.current.transform.position = pos;
            }
            if (Input.GetMouseButtonUp(0))
            {
                float x = this.current.transform.position.x - 0.5f;
                float y = this.current.transform.position.y - 0.5f;
                GameObject tilemapGrid = GameObject.Find("Grid");
                bool result = this.GlobalState.NewBuilding(
                    this.current,
                    tilemapGrid,
                    x + ((x%tilemapGrid.GetComponent<Grid>().cellSize.x > 0.5) ? 0.5f : 0.0f),
                    y + ((y%tilemapGrid.GetComponent<Grid>().cellSize.y > 0.5) ? 0.5f : 0.0f)
                );
                if (!result)
                {
                    this.fixAvailableBuildings = true;
                }
                else
                {
                    GameObject tmp = new GameObject();
                    State.Building target = new State.Building(tmp);
                    foreach (State.Building availableBuilding in this.GlobalState.AvailableBuildings.available)
                    {
                        if (availableBuilding.gameObject == this.current)
                        {
                            target = availableBuilding;
                            break;
                        }
                    }
                    this.GlobalState.AvailableBuildings.available.Remove(target);
                    GameObject.Destroy(tmp);
                    this.current = null;
                }
            }
        }
    }
}
