using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateBuilding : MonoBehaviour
{

    public State GlobalState;
    public GameObject BuildingPrefab;
    public GameObject Tilemap;
    public GameObject BuildingsGrid;
    public Texture2D[] BuildingSprites;
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
            Texture2D randomTexture = this.BuildingSprites[Random.Range(0,this.BuildingSprites.Length)];
            newAvailableBuilding.GetComponent<SpriteRenderer>().sprite = Sprite.Create(
                randomTexture,
                new Rect(0, 0, randomTexture.width, randomTexture.height),
                new Vector2(0.5f, 0.5f)
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
        // Left button clicked this frame
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
            // Left button not released yet
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = -1;
                // Avoid positions outside of viewport
                Vector3 viewportPos = Camera.main.WorldToViewportPoint(pos);
                if (0 > viewportPos.x || 1 < viewportPos.x)
                {
                    pos.x = this.current.transform.position.x;
                }
                if (0 > viewportPos.y || 1 < viewportPos.y)
                {
                    pos.y = this.current.transform.position.y;
                }
                this.current.transform.position = pos;
            }
            // Left button released this frame
            if (Input.GetMouseButtonUp(0))
            {
                float x = this.current.transform.position.x - 0.5f;
                float y = this.current.transform.position.y - 0.5f;
                Grid tilemapGrid = this.BuildingsGrid.GetComponent<Grid>();
                Vector3Int tpos = this.Tilemap.GetComponent<Tilemap>().WorldToCell(this.current.transform.position);
                Vector3 pos = tilemapGrid.GetCellCenterWorld(tpos);
                pos.z = -1;
                bool result = this.GlobalState.NewBuilding(
                    this.current,
                    tilemapGrid,
                    pos.x,
                    pos.y,
                    // No placing underneath ui
                    tpos.y >= -2 && tpos.y <= 3 && tpos.x <= 5 && tpos.x >= -5
                );
                if (result)
                {
                    GameObject tmp = new GameObject();
                    State.Building target = new State.Building(tmp,0,0,0,0);
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
                }
                this.current = null;
                this.fixAvailableBuildings = true;
            }
        }
    }
}
