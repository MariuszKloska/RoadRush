using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadInfiniteScroll : MonoBehaviour
{
    public float scrollSceed;
    private Vector2 offset;

    private void Update()
    {
        offset = new Vector2(0, Time.time * scrollSceed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
