using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseBuildingInspector : MonoBehaviour
{
    public Scene overlayScene;
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
            this.ExitInspector();
        }
    }

    public void ExitInspector()
    {
        SceneManager.UnloadSceneAsync("BuildingInspector");
        //AsyncOperation unload = SceneManager.UnloadSceneAsync(this.overlayScene);
        //while (!unload.Done)
        //{
        //    yield return null;
        //}
        //return true;
    }
}
