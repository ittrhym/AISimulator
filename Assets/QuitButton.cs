using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class QuitButton : MonoBehaviour
{
    private UIDocument document;
    private UnityEngine.UIElements.Button button;

//    private void OnEnable()
//    {
//        document = GetComponent<UIDocument>();
//        button = document.rootVisualElement.Q("#quit") as Button;
//        Console.WriteLine(button);
//        button.registerCallback<ClickEvent>(Click);
//    }
//
//    private void Click()
//    {
//        Console.WriteLine("Quit");
//    }
}
