using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCamera : MonoBehaviour
{

    public Transform Pivot;
    public Bounds bound;
    public Vector2 screenSize;
    public Vector2 boardSize;

    public float minOrthoSize;
    public float Margin;

    public static Vector2 Limites;

    private void Start()
    {
        bound = Pivot.GetBounds();
        var center = bound.center;
        center.z = -10;
        Camera.main.transform.position = center;
        screenSize = new Vector2(Screen.width, Screen.height);
        boardSize = new Vector2(bound.size.x, bound.size.y);
        Limites = bound.extents;

        var screenRatio = screenSize.x / screenSize.y;
        var targetRatio = boardSize.x / boardSize.y;

        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = Mathf.Max((boardSize.y / 2) + Margin, minOrthoSize);
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = Mathf.Max((boardSize.y / 2 * differenceInSize) + Margin, minOrthoSize);
        }
    }
}
