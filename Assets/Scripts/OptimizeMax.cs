using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizeMax : MonoBehaviour
{
    private Renderer objRenderer;
    private Camera mainCam;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        mainCam = Camera.main;
    }

    void Update()
    {
        if (objRenderer == null || mainCam == null) return;
        
        bool isVisible = GeometryUtility.TestPlanesAABB(
            GeometryUtility.CalculateFrustumPlanes(mainCam),
            objRenderer.bounds
        );

        gameObject.SetActive(isVisible);
    }
}
