using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    public Texture2D cursorTarget;
    public Texture2D cursorWalk;
    public Texture2D cursorUnknown;

    CameraRaycaster cameraRaycaster;
    private void Start()
    {

        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.onLayerChange += layerChanges;
    }

    private void layerChanges(Layer _Layer)
    {       

        switch (_Layer)
        {
            case Layer.Enemy:
                Cursor.SetCursor(cursorTarget, Vector2.zero, CursorMode.Auto);
                break;
            case Layer.Interactable://MISC
                Cursor.SetCursor(cursorTarget, Vector2.zero, CursorMode.Auto);
                break;

            case Layer.Walkable:
                Cursor.SetCursor(cursorWalk, Vector2.zero, CursorMode.Auto);
                break;

            default:
                Cursor.SetCursor(cursorUnknown, Vector2.zero, CursorMode.Auto);

                break;
        }
    }
    //TODO Consinder de-register layerChangeObservers


}
