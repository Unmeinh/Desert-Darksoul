using UnityEngine;
using UnityEngine.UI;

public class FixCanvasScaler
{
    public static void Execute()
    {
        GameObject canvasObj = GameObject.Find("Canvas");
        if (canvasObj != null)
        {
            CanvasScaler scaler = canvasObj.GetComponent<CanvasScaler>();
            if (scaler != null)
            {
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);
                scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                scaler.matchWidthOrHeight = 0.5f;
                Debug.Log("CanvasScaler updated successfully.");
            }
            else
            {
                Debug.LogError("CanvasScaler component not found on Canvas.");
            }
        }
        else
        {
            Debug.LogError("Canvas GameObject not found.");
        }
    }
}