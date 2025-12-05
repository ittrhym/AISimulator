using UnityEngine;
using UnityEngine.Tilemaps;

public class EWasteManager : MonoBehaviour
{
    public State GlobalState;
    public Tilemap tilemap;
    public Tilemap BuildingsTilemap;
    public GameObject BuildingsGrid;
    private GameObject current;

    void Start()
    {
        this.GlobalState.InitEWaste();
    }

    void Update()
    {
        if (this.current != null)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = -1;
                Vector3 viewportPos = Camera.main.WorldToViewportPoint(pos);
                if (1 < viewportPos.x || viewportPos.x < 0)
                {
                    pos.x = this.current.transform.position.x;
                }
                if (1 < viewportPos.y || viewportPos.y < 0)
                {
                    pos.y = this.current.transform.position.y;
                }
                this.current.transform.position = pos;
            }
            if (Input.GetMouseButtonUp(0))
            {
                this.SnapObject(this.current);
                foreach (Vector2 address in this.GlobalState.EWaste.Keys)
                {
                    if (this.GlobalState.EWaste[address] == this.current)
                    {
                        Vector3 checkPosition = this.BuildingsGrid.GetComponent<Grid>().GetCellCenterWorld(
                            this.BuildingsTilemap
                                .GetComponent<Tilemap>()
                                .WorldToCell(
                                    new Vector3(
                                        this.current.transform.position.x,
                                        this.current.transform.position.y,
                                        0
                                    )
                                )
                        );
                        if (
                            this.GlobalState.EWaste.ContainsKey(new Vector2(
                                this.current.transform.position.x,
                                this.current.transform.position.y
                            ))
                            ||
                            this.GlobalState.Buildings.ContainsKey(
                                new Vector2(checkPosition.x, checkPosition.y)
                            )
                        )
                        {
                            this.current.transform.position = address;
                        }
                        else
                        {
                            this.GlobalState.EWaste.Remove(address);
                            this.GlobalState.EWaste.Add(
                                new Vector2(
                                    this.current.transform.position.x,
                                    this.current.transform.position.y
                                ),
                                this.current
                            );
                        }
                        break;
                    }
                }
                this.current = null;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero
            );
            if (hit == false)
            {
                return;
            }
            if (this.GlobalState.EWaste.ContainsKey(new Vector2(
                    hit.collider.gameObject.transform.position.x,
                    hit.collider.gameObject.transform.position.y
                )
            ))
            this.current = hit.collider.gameObject;
        }
    }
    public void SnapObject(GameObject targetObject)
    {
        Vector3 pos = targetObject.transform.position;
        Grid tilemapGrid = gameObject.GetComponent<Grid>();
        Vector3Int tilePos = this.tilemap.GetComponent<Tilemap>().WorldToCell(pos);
        pos = gameObject.GetComponent<Grid>().GetCellCenterWorld(tilePos);
        pos.z = 0;
        targetObject.transform.position = pos;
    }
}
