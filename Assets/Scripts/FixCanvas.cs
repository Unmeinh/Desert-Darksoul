using UnityEngine;
using UnityEngine.UI;

public class FixCanvas
{
    public static void Execute()
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        CanvasScaler scaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
        
        Debug.Log("Canvas renderMode: " + canvas.renderMode);
        Debug.Log("Canvas worldCamera: " + canvas.worldCamera);
        Debug.Log("CanvasScaler uiScaleMode: " + scaler.uiScaleMode);
        Debug.Log("CanvasScaler matchWidthOrHeight: " + scaler.matchWidthOrHeight);
    }
}