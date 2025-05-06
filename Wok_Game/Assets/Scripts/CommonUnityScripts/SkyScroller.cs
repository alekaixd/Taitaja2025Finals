using UnityEngine;

// makes a looping background start scrolling

public class SkyScroller : MonoBehaviour
{
    [Range(-10f, 10f)] // Makes so that there is a slider in the unity editor for the scrollSpeed part
    public float scrollSpeed;
    private float offset;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    void Update() //Offsets the texture every frame so that it seems like the background / road is moving
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
