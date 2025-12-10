using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "State", menuName = "Scriptable Objects/State")]
public class State : ScriptableObject
{
    public float power = 0f;
    public float water = 0f;
    public float money = 0f;
    public float pollu = 0f;
    public float waterMod = 5f;
    public float powerMod = 5f;
    public float polluMod = 1f;
    public float moneyMod = 100f;
    public string currentTech;
    public float techPrice;

    public GameObject BuildingPrefab;

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

        if (this.PurchasedBuildings == null)
        {
        this.money = 500;
        this.power = 0;
        this.pollu = 0;
        this.water = 0;
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

    public bool efficentCoolingPurchased = false;
    public bool efficentCoolingRun = false;
    public void purchaseEfficentCooling(GameObject EfficentCoolingUnlocked)
    {
        if(efficentCoolingRun == false)
        {
            this.waterMod = 10;
            this.polluMod -=0.1f;
            EfficentCoolingUnlocked.GetComponent<Renderer>().enabled = true;
            this.efficentCoolingRun = true;
            this.efficentCoolingPurchased = true;
        }
    }
    public bool moreEfficentCoolingPurchased = false;
    public bool moreEfficentCoolingRun = false;
    public void purchaseMoreEfficentCooling(GameObject moreEfficentCoolingUnlocked)
    {
        if(moreEfficentCoolingRun == false)
        {
            this.waterMod = 15;
            this.polluMod -=0.25f;
            moreEfficentCoolingUnlocked.GetComponent<Renderer>().enabled = true;
            this.moreEfficentCoolingRun = true;
            this.moreEfficentCoolingPurchased = true;
        }
    }
    public bool mostEfficentCoolingPurchased = false;
    public bool mostEfficentCoolingRun = false;
    public void purchaseMostEfficentCooling(GameObject mostEfficentCoolingUnlocked)
    {
        if(mostEfficentCoolingRun == false)
        {
            
            this.waterMod = 30;
            this.polluMod -=0.5f;
            this.money -= this.techPrice;
            mostEfficentCoolingUnlocked.GetComponent<Renderer>().enabled = true;
            this.mostEfficentCoolingRun = true;
            this.mostEfficentCoolingPurchased = true;
        }
    }
    public bool waterRecyclePurchased = false;
    public bool waterRecycleRun = false;
    public void PurchaseWaterRecycle(GameObject waterRecycleIcon)
    {
        if(waterRecycleRun == false)
        {
            
            this.waterMod = 60;
            this.polluMod -=1f;
            this.money -= this.techPrice;
            waterRecycleIcon.GetComponent<Renderer>().enabled = true;
            this.mostEfficentCoolingRun = true;
            this.waterRecyclePurchased = true;
        }
    }
    public bool lakeCoolingPurchased = false;
    public bool lakeCoolingRun = false;
    public void PurchaseLakeCooling(GameObject lakeCoolingIcon)
    {
        if(lakeCoolingRun == false)
        {
            
            this.waterMod = 100;
            this.polluMod -=1.25f;
            this.money -= this.techPrice;
            lakeCoolingIcon.GetComponent<Renderer>().enabled = true;
            this.lakeCoolingRun = true;
            this.lakeCoolingPurchased = true;
        }
    }
    public bool improvedChillersPurchased = false;
    public bool improvedChillersRun = false;
    public void PurchaseImprovedChillers(GameObject improvedChillersIcon)
    {
        if(lakeCoolingRun == false)
        {
            
            this.waterMod = 10;
            this.polluMod +=0.1f;
            this.money -= this.techPrice;
            improvedChillersIcon.GetComponent<Renderer>().enabled = true;
            this.improvedChillersRun = true;
            this.improvedChillersPurchased = true;
        }
    }
    public bool betterHeatExchangersPurchased = false;
    public bool betterHeatExchangersRun = false;
   public void PurchaseBetterHeatExchangers(GameObject betterHeatExchangersIcon)
    {
        if(betterHeatExchangersRun == false)
        {
            
            this.waterMod = 20;
            this.polluMod +=0.25f;
            this.money -= this.techPrice;
            betterHeatExchangersIcon.GetComponent<Renderer>().enabled = true;
            this.betterHeatExchangersRun = true;
            this.betterHeatExchangersPurchased = true;
        }
    }
     public bool betterThermalPastePurchased = false;
    public bool betterThermalPasteRun = false;
   public void PurchaseBetterThermalPaste(GameObject betterHeatExchangersIcon)
    {
        if(betterHeatExchangersRun == false)
        {
            
            this.waterMod = 25;
            this.polluMod +=0.5f;
            this.money -= this.techPrice;
            betterHeatExchangersIcon.GetComponent<Renderer>().enabled = true;
            this.betterThermalPasteRun = true;
            this.betterThermalPastePurchased = true;
        }
    }
    public bool moreCoolentPurchased = false;
    public bool moreCoolentRun = false;
   public void PurchaseMoreCoolent(GameObject moreCoolentIcon)
    {
        if(moreCoolentPurchased == false)
        {
            
            this.waterMod = 50;
            this.polluMod +=0.75f;
            this.money -= this.techPrice;
            moreCoolentIcon.GetComponent<Renderer>().enabled = true;
            this.moreCoolentPurchased = true;
            this.moreCoolentRun = true;
        }
    }
    public bool waterPipelinePurchased = false;
    public bool waterPipelineRun = false;
   public void PurchaseWaterPipeline(GameObject waterPipelineIcon)
    {
        if(betterHeatExchangersRun == false)
        {
            
            this.waterMod = 75;
            this.polluMod +=1f;
            this.money -= this.techPrice;
            waterPipelineIcon.GetComponent<Renderer>().enabled = true;
            this.waterPipelineRun = true;
            this.waterPipelinePurchased = true;
        }
    }
    public bool renewablePowerPurchased = false;
    public bool renewablePowerRun = false;
    public void PurchaseRenewablePower(GameObject renewablePowerIcon)
    {
        if(renewablePowerRun == false)
        {
            
            this.powerMod = 10;
            this.polluMod -=0.1f;
            this.money -= this.techPrice;
            renewablePowerIcon.GetComponent<Renderer>().enabled = true;
            this.renewablePowerPurchased = true;
            this.renewablePowerRun = true;
        }
    }
    public bool betterRenewablesPurchased = false;
    public bool betterRenewablesRun = false;
    public void PurchaseBetterRenewable(GameObject betterRenewablesIcon)
    {
        if(betterRenewablesRun == false)
        {
            
            this.powerMod = 15;
            this.polluMod -=0.25f;
            this.money -= this.techPrice;
            betterRenewablesIcon.GetComponent<Renderer>().enabled = true;
            this.renewablePowerPurchased = true;
            this.renewablePowerRun = true;
        }
    }
    public bool nuclearPowerPurchased = false;
    public bool nuclearPowerRun = false;
    public void PurchaseNuclearPower(GameObject nuclearPowerIcon)
    {
        if(nuclearPowerRun == false)
        {
            
            this.powerMod = 20;
            this.polluMod -=0.5f;
            this.money -= this.techPrice;
            nuclearPowerIcon.GetComponent<Renderer>().enabled = true;
            this.nuclearPowerPurchased = true;
            this.nuclearPowerRun = true;
        }
    }
    public bool nuclearFuelRecyclePurchased = false;
    public bool nuclearFuelRecycleRun = false;
    public void PurchaseNuclearFuelRecycle(GameObject nuclearFuelIcon)
    {
        if(nuclearFuelRecycleRun == false)
        {
            
            this.powerMod = 50;
            this.polluMod -=1f;
            this.money -= this.techPrice;
            nuclearFuelIcon.GetComponent<Renderer>().enabled = true;
            this.nuclearFuelRecyclePurchased = true;
            this.nuclearFuelRecycleRun = true;
        }
    }
    public bool nuclearFusionPurchased = false;
    public bool nuclearFusionRun = false;
    public void PurchaseNuclearFusion(GameObject nuclearFusionIcon)
    {
        if(betterRenewablesRun == false)
        {
            
            this.powerMod = 100;
            this.polluMod -=1.25f;
            this.money -= this.techPrice;
            nuclearFusionIcon.GetComponent<Renderer>().enabled = true;
            this.nuclearFusionRun = true;
            this.nuclearFusionPurchased = true;
        }
    }
    public bool coalPowerPurchased = false;
    public bool coalPowerRun = false;
    public void PurchaseCoalPower(GameObject coalPowerIcon)
    {
        if(betterRenewablesRun == false)
        {
            
            this.powerMod = 10;
            this.polluMod +=0.1f;
            this.money -= this.techPrice;
            coalPowerIcon.GetComponent<Renderer>().enabled = true;
            this.coalPowerPurchased = true;
            this.coalPowerRun = true;
        }
    }
    public bool improvedPowerplantsPurchased = false;
    public bool improvedPowerplantsRun = false;
    public void PurchaseImprovedPowerplants(GameObject improvedPowerplantIcon)
    {
        if(improvedPowerplantsRun == false)
        {
            
            this.powerMod = 15;
            this.polluMod +=0.25f;
            this.money -= this.techPrice;
            improvedPowerplantIcon.GetComponent<Renderer>().enabled = true;
            this.improvedChillersPurchased = true;
            this.improvedPowerplantsRun = true;
        }
    }
    public bool improvedMiningPurchased = false;
    public bool improvedMiningRun = false;
    public void PurchaseImprovedMining(GameObject improvedMiningIcon)
    {
        if(improvedMiningRun == false)
        {
            
            this.powerMod = 25;
            this.polluMod +=0.5f;
            this.money -= this.techPrice;
            improvedMiningIcon.GetComponent<Renderer>().enabled = true;
            this.improvedMiningRun = true;
            this.improvedMiningPurchased = true;
        }
    }
    public bool cycloneFurnacePurchased = false;
    public bool cycloneFurnaceRun = false;
    public void PurchaseCycloneFurnace(GameObject cycloneFurnaceIcon)
    {
        if(cycloneFurnaceRun == false)
        {
            
            this.powerMod = 10;
            this.polluMod +=0.1f;
            this.money -= this.techPrice;
            cycloneFurnaceIcon.GetComponent<Renderer>().enabled = true;
            this.cycloneFurnacePurchased = true;
            this.cycloneFurnaceRun = true;
        }
    }
    public bool cogenerationPurchased = false;
    public bool cogenerationRun = false;
    public void PurchaseCogeneration(GameObject cogenerationIcon)
    {
        if(betterRenewablesRun == false)
        {
            
            this.powerMod = 75;
            this.polluMod +=1f;
            this.money -= this.techPrice;
            cogenerationIcon.GetComponent<Renderer>().enabled = true;
            this.cogenerationPurchased = true;
            this.cogenerationRun = true;
        }
    }
    public bool heatEfficentChipsPurchased = false;
    public bool heatEfficentChipsRun = false;
    public void PurchaseHeatEfficentChips(GameObject heatEfficentChipsIcon)
    {
        if(heatEfficentChipsRun == false)
        {
            
            this.moneyMod = 500;
            this.polluMod -=0.1f;
            this.money -= this.techPrice;
            heatEfficentChipsIcon.GetComponent<Renderer>().enabled = true;
            this.heatEfficentChipsRun = true;
            this.heatEfficentChipsPurchased = true;
        }
    }
    public bool energyEfficentChipsPurchased = false;
    public bool energyEfficentChipsRun = false;
    public void PurchaseEnergyEfficentChips(GameObject energyEfficentChipIcon)
    {
        if(energyEfficentChipsRun == false)
        {
            
            this.moneyMod = 1000;
            this.polluMod -=0.25f;
            this.money -= this.techPrice;
            energyEfficentChipIcon.GetComponent<Renderer>().enabled = true;
            this.energyEfficentChipsRun = true;
            this.energyEfficentChipsPurchased = true;
        }
    }
    public bool longerLastingChipsPurchased = false;
    public bool longerLastingChipsRun = false;
    public void PurchaseLongerLastingChips(GameObject longerLastingChipsIcon)
    {
        if(longerLastingChipsRun == false)
        {
            
            this.moneyMod = 2500;
            this.polluMod -=0.5f;
            this.money -= this.techPrice;
            longerLastingChipsIcon.GetComponent<Renderer>().enabled = true;
            this.longerLastingChipsRun = true;
            this.longerLastingChipsPurchased = true;
        }
    }
    public bool highHeatEfficentChipsPurchased = false;
    public bool highHeatEfficentChipsRun = false;
    public void PurchaseHighHeatEfficentChips(GameObject highHeatEfficentChipsIcon)
    {
        if(highHeatEfficentChipsRun == false)
        {
            
            this.moneyMod = 10000;
            this.polluMod -=1f;
            this.money -= this.techPrice;
            highHeatEfficentChipsIcon.GetComponent<Renderer>().enabled = true;
            this.highHeatEfficentChipsRun = true;
            this.highHeatEfficentChipsPurchased = true;
        }
    }
    public bool highEnergyEfficentChipsPurchased = false;
    public bool highEnergyEfficentChipsRun = false;
    public void PurchaseHighEnergyEfficent(GameObject highEnergyEfficentChipsIcon)
    {
        if(highEnergyEfficentChipsRun== false)
        {
            
            this.moneyMod = 20000;
            this.polluMod -=1.25f;
            this.money -= this.techPrice;
            highEnergyEfficentChipsIcon.GetComponent<Renderer>().enabled = true;
            this.highEnergyEfficentChipsRun = true;
            this.highHeatEfficentChipsPurchased = true;
        }
    }
    public bool fasterCPUsPurchased = false;
    public bool fasterCPUsRun = false;
    public void PurchasefasterCPU(GameObject fasterCPUsIcon)
    {
        if(fasterCPUsRun== false)
        {
            
            this.moneyMod = 750;
            this.polluMod +=0.1f;
            this.money -= this.techPrice;
            fasterCPUsIcon.GetComponent<Renderer>().enabled = true;
            this.fasterCPUsPurchased = true;
            this.fasterCPUsRun = true;
        }
    }
    public bool newGPUsPurchased = false;
    public bool newGPUsRun = false;
    public void PurchaseNewGPUs(GameObject newGPUsIcon)
    {
        if(newGPUsRun == false)
        {
            
            this.moneyMod = 1500;
            this.polluMod +=0.25f;
            this.money -= this.techPrice;
            newGPUsIcon.GetComponent<Renderer>().enabled = true;
            this.newGPUsPurchased = true;
            this.newGPUsRun = true;
        }
    }
    public bool aiSpecificTechPurchased = false;
    public bool aiSpecificTechRun = false;
    public void PurchaseAiSpecificTech(GameObject aiSpecificTechIcon)
    {
        if(highEnergyEfficentChipsRun== false)
        {
            
            this.moneyMod = 2500;
            this.polluMod += 0.5f;
            this.money -= this.techPrice;
            aiSpecificTechIcon.GetComponent<Renderer>().enabled = true;
            this.aiSpecificTechRun = true;
            this.aiSpecificTechPurchased = true;
        }
    }
    public bool imageGenerationPurchased = false;
    public bool imageGenerationRun = false;
    public void PurchaseImageGeneration(GameObject imageGenerationIcon)
    {
        if(highEnergyEfficentChipsRun== false)
        {
            
            this.moneyMod = 5000;
            this.polluMod +=0.75f;
            this.money -= this.techPrice;
            imageGenerationIcon.GetComponent<Renderer>().enabled = true;
            this.imageGenerationRun = true;
            this.imageGenerationPurchased = true;
        }
    }
    public bool videoGenerationPurchased = false;
    public bool videoGenerationRun = false;
    public void PurchaseVideoGeneration(GameObject videoGenerationIcon)
    {
        if(videoGenerationRun == false)
        {
            
            this.moneyMod = 10000;
            this.polluMod +=1f;
            this.money -= this.techPrice;
            videoGenerationIcon.GetComponent<Renderer>().enabled = true;
            this.videoGenerationPurchased = true;
            this.videoGenerationRun = true;
        }
    }
    public bool aiSingularityPurchased = false;

    public void NextTurn()
    {
        foreach (Building building in this.Buildings.Values)
        {
            float waterCost = 5.0f * building.waterModifier;
            float powerCost = 5.0f * building.powerModifier;
            if (this.water < waterCost || this.power < powerCost)
            {
                return;
            }
            this.water -= waterCost;
            this.power -= powerCost;
            this.money += this.moneyMod * building.moneyModifier;
            this.pollu += 1.0f * building.polluModifier * this.polluMod;
        }
        if (this.pollu > 1500)
        {
            SceneManager.LoadScene("Ending", LoadSceneMode.Additive);
        }
    }

}
