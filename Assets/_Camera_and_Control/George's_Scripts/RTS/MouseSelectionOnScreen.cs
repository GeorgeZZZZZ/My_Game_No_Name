﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  vr:
 *  - 0.0.1
 *  selecet selectable unit by single click and box drag selection
 *  only start when camera_controller is in rts view
 */

public class MouseSelectionOnScreen : MonoBehaviour {
    public Camera playerCam;
    public GameObject Select_Circle_Prefab;

    private bool mouseAreaSelec = false;    //	mouse area selecting flag
    private Vector3 curMousPos;

    private ILowLevelCameraController _cameraController;
    private bool _rpgMode = false;
    private bool _rtsMode = false;

    private Camera_Controller_Mode _ccMode;

    void OnGUI()
    {

        //	RTS selection function, mouse click then start draw selection area
        if (mouseAreaSelec)
        {
            // Create a rect from both mouse positions
            var rect = Utils_RTS_Draw.GetScreenRect(curMousPos, Input.mousePosition);
            Utils_RTS_Draw.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils_RTS_Draw.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    // Use this for initialization
    void Start () {
        _cameraController = Camera_Controller.Instance;
        _cameraController.Camera_Mode_Change_Event += _Camera_Mode_Cahanged;
    }

    private void OnDestroy()
    {
        _cameraController.Camera_Mode_Change_Event -= _Camera_Mode_Cahanged;
    }

    // Update is called once per frame
    void Update () {
    }

    private void FixedUpdate()
    {

        // only execute if camera view is in rts mode
        //if (!_rtsMode) return;
        if (_ccMode != Camera_Controller_Mode.RTS) return;

        bool mousLefButt = Input.GetMouseButton(0);

        RTS_Point_Selec(mousLefButt);

        RTS_Area_Selec(mousLefButt);
    }

    //	RTS style Point Selection
    private void RTS_Point_Selec(bool mousLB)
    {

        //	search obj which contain Selectable_Unit_Controller.cs
        if (mousLB & !mouseAreaSelec)
        {

            //	cast a ray from camera and go through mouse position
            Ray camMousRay = playerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit selectionHit;

            //	cast a ray from camera and go through mouse position
            foreach (var selectableObj in FindObjectsOfType<SelectableUnit>())
            {

                /******************
				 * may need add code for selected obj in to a global list for AI control at here
				 ******************/

                if (Physics.Raycast(camMousRay, out selectionHit, 50f))
                {
                    if (selectableObj.GetComponent<Collider>().bounds.Contains(selectionHit.point))
                    {
                        if (selectableObj.selectionCircle == null)
                        {
                            selectableObj.selectionCircle = Instantiate(Select_Circle_Prefab);
                            selectableObj.selectionCircle.transform.SetParent(selectableObj.transform, false);
                            selectableObj.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                        }
                    }
                    else
                    {
                        if (selectableObj.selectionCircle != null)
                        {
                            Destroy(selectableObj.selectionCircle.gameObject);
                            selectableObj.selectionCircle = null;
                        }
                    }
                }
            }
        }
    }

    //	RTS style Area Selection
    private void RTS_Area_Selec(bool mousLB)
    {

        //	if press left mouse button start draw square
        if (mousLB & !mouseAreaSelec)
        {
            mouseAreaSelec = true;
            curMousPos = Input.mousePosition;
        }

        //	if release left mouse button stop draw square
        //	original detect mousLefButtUp, but some times can't get release signal
        if (!mousLB)
        {
            mouseAreaSelec = false;
        }

        //	use projector give a circle under selected unity
        if (mouseAreaSelec)
        {
            //	search obj which contain component Selectable_Unit_Controller.cs
            foreach (var selectableObj in FindObjectsOfType<SelectableUnit>())
            {
                //	call judgement function and see if obj is in selection area
                if (IsWithinSelectionBounds(selectableObj.gameObject))
                {
                    if (selectableObj.selectionCircle == null)
                    {
                        selectableObj.selectionCircle = Instantiate(Select_Circle_Prefab);
                        selectableObj.selectionCircle.transform.SetParent(selectableObj.transform, false);
                        selectableObj.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                    }
                }
            }
        }
    }

    //	RTS selection function, judgement for selectable obj in or not in selction area from camera view angle 
    public bool IsWithinSelectionBounds(GameObject gameObject)
    {

        if (!mouseAreaSelec)
            return false;

        var cam = playerCam;
        var viewportBounds =
            Utils_RTS_Draw.GetViewportBounds(cam, curMousPos, Input.mousePosition);

        return viewportBounds.Contains(
            cam.WorldToViewportPoint(gameObject.transform.position));   //	use bounds() search if obj is in selection area
    }

    // Get event indicate whether camera is in rts mode or not
    private void _cameraController_RTS_Mode(bool obj)
    {
        _rtsMode = obj;
    }

    // Get event indicate whether camera is in rpg mode or not
    private void _cameraController_RPG_Mode(bool obj)
    {
        _rpgMode = obj;
    }

    private void _Camera_Mode_Cahanged(Camera_Controller_Mode _mode)
    {
        _ccMode = _mode;
    }

}
