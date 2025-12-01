using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "State", menuName = "Scriptable Objects/State")]
public class State : ScriptableObject
{
    public class Building
    {
        public GameObject gameObject;
        public Building (GameObject GameObject)
        {
            this.gameObject = GameObject;
        }
    }

    private Dictionary<Vector2,Building> Buildings;
    public bool InitBuildings()
    {
        if (this.Buildings == null)
        {
            this.Buildings = new Dictionary<Vector2, Building>();
            return true;
        }
        return false;
    }

    public class BuildingsAvailable
    {
        public List<Building> available;
        public BuildingsAvailable()
        {
            this.available = new List<Building>();
        }

        public bool IsAvailable(GameObject building)
        {
            foreach (Building availableBuilding in this.available)
            {
                if (availableBuilding.gameObject == building)
                {
                    return true;
                }
            }
            return false;
        }
        public bool New(GameObject buildingObject)
        {
            Building building = new Building(buildingObject);
            this.available.Add(building);
            return true;
        }
    }

    public class PurchasedBuilding
    {
        public GameObject prefab;
        public PurchasedBuilding(GameObject prefab)
        {
            this.prefab = prefab;
        }
    }

    public Stack<PurchasedBuilding> PurchasedBuildings;
    public bool InitPurchasedBuildings()
    {
        //TODO - temporary (remove later)
        this.money = 100;
        if (this.PurchasedBuildings == null)
        {
            this.PurchasedBuildings = new Stack<PurchasedBuilding>();
            return true;
        }
        return false;
    }

    public BuildingsAvailable AvailableBuildings { get; private set; }
    public bool InitAvailableBuildings()
    {
        if (this.AvailableBuildings == null)
        {
            this.AvailableBuildings = new BuildingsAvailable();
            return true;
        }
        return false;
    }

    public int power { get; private set; }
    public int water { get; private set; }
    public int money { get; private set; }
    public int pollu { get; private set; }

    public GameObject BuildingPrefab;

    public bool Purchase(int cost, GameObject prefab)
    {
        if (this.money > cost)
        {
            this.money -= cost;
            this.PurchasedBuildings.Push(new PurchasedBuilding(prefab));
            return true;
        }
        return false;
    }
    public bool NewBuilding(GameObject buildingObject, GameObject tilemapGrid, float x, float y)
    {
        x -= x%tilemapGrid.GetComponent<Grid>().cellSize.x;
        y -= y%tilemapGrid.GetComponent<Grid>().cellSize.y;
        buildingObject.transform.position = new Vector3(x, y, 0);
        Building building = new Building(buildingObject);
        Vector2 position = new Vector2(x, y);
        if (this.Buildings.ContainsKey(position))
        {
            return false;
        }
        this.Buildings.Add(position, building);
        return true;
    }
}
