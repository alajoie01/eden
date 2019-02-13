using UnityEngine;
using System.Collections;

public class PushToFront : MonoBehaviour
{
    public string layerToPushTo;

    void Start()
    {
        GetComponent<MeshRenderer>().sortingLayerName = layerToPushTo;
        //Debug.Log(GetComponent<MeshRenderer>().sortingLayerName);
    }
}