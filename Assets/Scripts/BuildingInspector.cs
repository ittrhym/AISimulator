using UnityEngine;
using TMPro;

public class BuildingInspector : MonoBehaviour
{
    public State GlobalState;
    public GameObject waterModText;
    public GameObject powerModText;

    void Update()
    {
        if (this.waterModText.GetComponent<TextMeshPro>().text.Equals(""))
        {
            State.Building currentBuilding = this.GlobalState.Buildings[this.GlobalState.currentBuildingAddress];
            this.waterModText.GetComponent<TextMeshPro>().SetText(currentBuilding.waterModifier + "x");
            this.powerModText.GetComponent<TextMeshPro>().SetText(currentBuilding.powerModifier + "x");
        }
    }
}
