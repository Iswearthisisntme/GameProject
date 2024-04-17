using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOutlet : MonoBehaviour
{
     public Material outlineMaterial; 
    public Color outlineColor = Color.red; 

    private GameObject outlineObject; 

    void Start()
    {
        outlineObject = Instantiate(gameObject, transform.position, transform.rotation, transform);
        outlineObject.GetComponent<Renderer>().material = outlineMaterial;

        outlineObject.SetActive(false);
    }

    public void ShowOutline(bool show)
    {
        outlineObject.SetActive(show);

        if (show)
        {
            outlineMaterial.SetColor("_OutlineColor", outlineColor);
        }
    }
}
