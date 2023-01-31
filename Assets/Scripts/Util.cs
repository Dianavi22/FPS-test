using UnityEngine;

public class Util 
{
   
    public static void SetLayerRecurcively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecurcively(child.gameObject, newLayer);
        }
    }
}
