using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisibleObj : MonoBehaviour
{
    public Camera targetCamera;
    
    public float checkInterval = 0.25f;
    
    public float boundsPadding = 0.0f;

    private Renderer[] renderers;
    private float timer;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
        if (targetCamera == null) targetCamera = Camera.main;
        if (renderers == null || renderers.Length == 0)
            Debug.LogWarning($"[{name}] No Renderer");
    }

    void Update()
    {
        if (targetCamera == null || renderers == null || renderers.Length == 0) return;

        timer += Time.unscaledDeltaTime;
        if (timer < checkInterval) return;
        timer = 0f;
        
        Bounds b = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
            b.Encapsulate(renderers[i].bounds);
        if (boundsPadding != 0f) b.Expand(boundsPadding);

        var planes = GeometryUtility.CalculateFrustumPlanes(targetCamera);
        bool visible = GeometryUtility.TestPlanesAABB(planes, b);
        
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].enabled = visible;
    }
}
