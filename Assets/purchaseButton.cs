using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
public class purchaseButton : MonoBehaviour
{
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
            print("test");
            if (this.GlobalState.currentTech == "efficentCooling")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.purchaseEfficentCooling(gameObject);
                    print ("test");
                }
            }
            if (this.GlobalState.currentTech == "moreEfficentCooling")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.efficentCoolingPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.purchaseMoreEfficentCooling(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "mostEfficentCooling")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.moreEfficentCoolingPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.purchaseMostEfficentCooling(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "waterRecycling")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.mostEfficentCoolingPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseWaterRecycle(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "lakeCooling")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.waterRecyclePurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseLakeCooling(gameObject);
                }
            }
             if (this.GlobalState.currentTech == "improvedChillers")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseImprovedChillers(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "heatExchanger")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.improvedChillersPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseBetterHeatExchangers(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "betterThermalPaste")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.betterHeatExchangersPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseBetterThermalPaste(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "moreCoolent")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.betterThermalPastePurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseMoreCoolent(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "waterPipeline")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.moreCoolentPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseWaterPipeline(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "renewableEnergy")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseRenewablePower(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "betterRenewables")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.renewablePowerPurchase == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseWaterPipeline(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "nuclearPower")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.betterRenewablesPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseNuclearPower(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "recycledNuclearFuels")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.nuclearPowerPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseNuclearFuelRecycle(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "nuclearFusion")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.nuclearFuelRecyclePurchase == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseNuclearFusion(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "coalPower")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseCoalPower(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "improvedPowerplants")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.improvedPowerplantsPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseImprovedPowerplants(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "improvedMining")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.improvedPowerplantsPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseImprovedMining(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "cycloneFurnaces")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.improvedMiningPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseCycloneFurnace(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "cogeneration")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.cycloneFurnacePurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseCogeneration(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "heatEfficentChips")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseHeatEfficentChips(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "energyEfficentChips")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.heatEfficentChipsPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseEnergyEfficentChips(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "longerLastingChips")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.energyEfficentChipsPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseLongerLastingChips(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "highHeatEfficency")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.longerLastingChipsPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseHighHeatEfficentChips(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "highEnergyEfficency")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.highHeatEfficentChipsPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseHighEnergyEfficent(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "cycloneFurnaces")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.improvedMiningPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseCycloneFurnace(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "cycloneFurnaces")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.improvedMiningPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseCycloneFurnace(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "cycloneFurnaces")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.improvedMiningPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseCycloneFurnace(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "cycloneFurnaces")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.improvedMiningPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseCycloneFurnace(gameObject);
                }
            }
            if (this.GlobalState.currentTech == "cycloneFurnaces")
            {
                if (this.GlobalState.money >= this.GlobalState.techPrice && this.GlobalState.improvedMiningPurchased == true && this.GlobalState.money >= 100)
                {
                    this.GlobalState.money -= this.GlobalState.techPrice;
                    this.GlobalState.PurchaseCycloneFurnace(gameObject);
                }
            }
            






        }
    }
}
