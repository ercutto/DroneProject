using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    // Scroll main texture based on time

    float scrollSpeed = 0.1f;
    float offset;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset =scrollSpeed *Time.time;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
