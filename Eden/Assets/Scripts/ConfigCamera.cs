using UnityEngine;
using System.Collections;

public class ConfigCamera : MonoBehaviour
{

    public float maxPixelHeight = 428f;

    void Awake()
    {
        float scale = Mathf.Ceil(Screen.height / maxPixelHeight);
        Camera.main.orthographicSize = Screen.height / 2f / scale;
    }

}