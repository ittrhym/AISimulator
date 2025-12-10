using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    public State GlobalState;
    public GameObject endTitle;
    public GameObject endText;
    private bool Updated;

    void Update()
    {
        if (this.Updated) return;

        this.endTitle.GetComponent<TextMeshPro>().text = "You " + (
            /*this.GlobalState.purchasedAISingularity /*TODO*/ false
            ? "win"
            : "lose"
        );
        this.endText.GetComponent<TextMeshPro>().SetText(
            "Finished with $" +
                this.GlobalState.money +
            " and a pollution score of " + this.GlobalState.pollu
        );
        this.Updated = true;
    }
}
