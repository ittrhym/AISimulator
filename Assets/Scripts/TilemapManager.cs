using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public State GlobalState;
    public GameObject BuildingPrefab;
    private Tilemap tilemap;
    private bool mouseDown;

    void Start()
    {
        this.tilemap = gameObject.GetComponent<Tilemap>();
    }

    void Update()
    {
        /*
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePosition = this.tilemap.WorldToCell(position);
        if (Input.GetMouseButtonDown(0) && !mouseDown)
        {
            this.mouseDown = true;
            GameObject building = Instantiate(
                this.BuildingPrefab,
                tilePosition,
                Quaternion.identity
            );
            if (!this.GlobalState.NewBuilding(building, position.x, position.y))
            {
                UnityEngine.Object.Destroy(building);
            }
        }
        else
        {
            this.mouseDown = false;
        }
        */
    }
}
