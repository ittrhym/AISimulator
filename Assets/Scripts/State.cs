using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "State", menuName = "Scriptable Objects/State")]
public class State : ScriptableObject
{
    public class Building
    {
        public GameObject gameObject;

        public float moneyModifier {get; private set;}
        public float waterModifier {get; private set;}
        public float powerModifier {get; private set;}
        public float polluModifier {get; private set;}
        private int upgradeCount = 0;

        public Building (
            GameObject GameObject,
            float baseMoneyMod,
            float baseWaterMod,
            float basePowerMod,
            float basePolluMod
        )
        {
            this.moneyModifier = 1/baseMoneyMod;
            this.waterModifier = 1/baseWaterMod;
            this.powerModifier = 1/basePowerMod;
            this.polluModifier = 1/basePolluMod;
            this.gameObject = GameObject;
        }

        public bool Upgrade(string type, float newValue)
        {
            if (upgradeCount >= 3)
            {
                return false;
            }
            switch (type)
            {
                case "money": this.moneyModifier = newValue;
                              break;
                case "water": this.waterModifier = newValue;
                              break;
                case "power": this.powerModifier = newValue;
                              break;
                case "pollu": this.polluModifier = newValue;
                              break;
            }
            return true;
        }
    }

    public Dictionary<Vector2,Building> Buildings;
    public bool InitBuildings()
    {
        if (this.Buildings == null)
        {
            this.Buildings = new Dictionary<Vector2, Building>();
            this.currentBuildingAddress = new Vector2(0, 0);
            this.inspectingBuilding = false;
            return true;
        }
        return false;
    }

    public GameObject EWastePrefab;
    public GameObject EWasteManagerObject;
    public Dictionary<Vector2,GameObject> EWaste;
    public bool InitEWaste()
    {
        // Apparently can't assign gameobject to scriptableobject persistently
        this.EWasteManagerObject = GameObject.Find("EWasteGrid");
        if (this.EWaste == null)
        {
            this.EWaste = new Dictionary<Vector2, GameObject>();
            return true;
        }
        return false;
    }

    public class BuildingsAvailable
    {
        public List<Building> available;
        public State GlobalState;
        public BuildingsAvailable(State state)
        {
            this.available = new List<Building>();
            this.GlobalState = state;
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
            Building building = new Building(
                buildingObject,
                1,
                this.GlobalState.waterMod,
                1,
                1
            );
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
        this.money = 100000;
        this.power = 0;
        this.pollu = 0;
        this.water = 0;
        if (this.PurchasedBuildings == null)
        {
            this.PurchasedBuildings = new Stack<PurchasedBuilding>();
            return true;
        }
        return false;
    }

    public Vector2 currentBuildingAddress;
    public bool inspectingBuilding = false;
    public bool DestroyBuilding (Vector2 buildingAddress)
    {
        Building building;
        if (this.Buildings.Remove(buildingAddress, out building))
        {
            Grid grid = this.EWasteManagerObject.GetComponent<Grid>();
            for (int i = -1; i<1; ++i)
            {
                GameObject newEWaste = Instantiate(
                    this.EWastePrefab,
                    new Vector3(
                        buildingAddress.x - (i==0?1:0)*grid.cellSize.x,
                        buildingAddress.y - (i==0?0:1)*grid.cellSize.y,
                        0
                    ),
                    Quaternion.identity
                );
                DontDestroyOnLoad(newEWaste);
                this.EWasteManagerObject.GetComponent<EWasteManager>().SnapObject(newEWaste);
                this.EWaste.Add(
                    new Vector2(
                        newEWaste.transform.position.x,
                        newEWaste.transform.position.y
                    ),
                    newEWaste
                );
                GameObject.Destroy(building.gameObject);
            }
            return true;
        }
        return false;
    }

    public BuildingsAvailable AvailableBuildings { get; private set; }
    public bool InitAvailableBuildings()
    {
        if (this.AvailableBuildings == null)
        {
            this.AvailableBuildings = new BuildingsAvailable(this);
            return true;
        }
        return false;
    }

    public float power { get; private set; }
    public float water { get; private set; }
    public float money { get; private set; }
    public float pollu { get; private set; }
    public float waterMod { get; private set; }

    public GameObject BuildingPrefab;

    public bool PurchaseBuilding(int cost, GameObject prefab)
    {
        if (this.money > cost)
        {
            this.money -= cost;
            this.PurchasedBuildings.Push(new PurchasedBuilding(prefab));
            return true;
        }
        return false;
    }

    public bool NewBuilding(GameObject buildingObject, Grid tilemapGrid, float x, float y, bool validPosition)
    {
        if (!validPosition)
        {
            return false;
        }
        int h = 0;
        int k = 0;
        while (h <= 1)
        {
            Grid grid = this.EWasteManagerObject.GetComponent<Grid>();
            Vector3 checkPosition = new Vector3(
                x - h*grid.cellSize.x,
                y - k*grid.cellSize.y,
                0
            );
            checkPosition = grid.GetCellCenterWorld(
                this.EWasteManagerObject
                    .transform
                    .GetChild(0)
                    .GetComponent<Tilemap>()
                    .WorldToCell(checkPosition)
            );
            if (this.EWaste.ContainsKey(
                new Vector2(checkPosition.x, checkPosition.y)
            ))
            {
                return false;
            }
            h += k>=1?1|(k=0):0&(k++);
        }
        buildingObject.transform.position = new Vector3(x, y, 0);
        Building building = new Building(
            buildingObject,
            1,
            this.waterMod,
            1,
            1
        );
        Vector2 position = new Vector2(x, y);
        if (this.Buildings.ContainsKey(position))
        {
            return false;
        }
        this.Buildings.Add(position, building);
        return true;
    }

    public bool efficentCoolingPurchased;
    public bool efficentCoolingRun;
    public void purchaseEfficentCooling(GameObject EfficentCoolingUnlocked)
    {
        if(efficentCoolingPurchased == true && efficentCoolingRun == false)
        {
            this.waterMod = 10;
            EfficentCoolingUnlocked.GetComponent<Renderer>().enabled = true;
            this.efficentCoolingRun = true;
        }
    }

    public void NextTurn()
    {
        foreach (Building building in this.Buildings.Values)
        {
            float waterCost = 5.0f * building.waterModifier;
            float powerCost = 5.0f * building.powerModifier;
            if (this.money < waterCost + powerCost)
            {
                return;
            }
            this.money -= waterCost;
            this.money -= powerCost;
            this.money += 100.0f * building.moneyModifier;
            this.pollu += 1.0f * building.polluModifier;
        }
    }
}
