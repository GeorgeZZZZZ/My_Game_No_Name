using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using Pathfinding;
/*  vr:
 *  - 0.0.1
 * 
 */

public class SelectableUnit : MonoBehaviour {

    public GameObject selectionCircle;  //	use for add circle above obj after been selected
    public float walk_Speed = 1;
    public float run_Speed = 2;
    public float walk_turn_Speed = 180;
    public float run_turn_Speed = 720;

    
    private MouseFunctions _mousFun;    // detect mouse click behavior
    private ILowLevelCameraController _CC;  // cam control local value
    private BehaviorTree _bt;   //  behaviortree local value
    private RichAI _RichAI;
    private AIPath _AIPath;

    // Use this for initialization
    void Start ()
    {
        _mousFun = MouseFunctions.instance;
        if (_mousFun == null)
            Debug.LogWarning("Mouse function not assign in scenes!!");
        _mousFun.Right_Click_Once_Event += _mousFun_Right_Click_Once_Event;
        _mousFun.Right_Double_Click_Event += _mousFun_Right_Double_Click_Event;
        _mousFun.Left_Click_Once_Event += _mousFun_Left_Click_Once_Event;
        _mousFun.Left_Double_Click_Event += _mousFun_Left_Double_Click_Event;
        
        _bt = GetComponent<BehaviorTree>();
        if (_bt == null)
        {
            Debug.LogWarning("No BehaviorTree attach!!!");
            return;
        }

        _CC = Camera_Controller.Instance;
        if (_CC == null)
            Debug.LogWarning("No camera controller!!!");
        _CC.Camera_Mode_Change_Event += _Camera_Mode_Changed;

        _RichAI = GetComponent<RichAI>();
        _AIPath = GetComponent<AIPath>();
    }

    void OnDestroy()
    {
        _mousFun.Right_Click_Once_Event -= _mousFun_Right_Click_Once_Event;
        _mousFun.Right_Double_Click_Event -= _mousFun_Right_Double_Click_Event;
        _mousFun.Left_Click_Once_Event -= _mousFun_Left_Click_Once_Event;
        _mousFun.Left_Double_Click_Event -= _mousFun_Left_Double_Click_Event;

        _CC.Camera_Mode_Change_Event -= _Camera_Mode_Changed;
    }
    
    private void _mousFun_Left_Double_Click_Event(Vector3 obj)
    {
    }

    private void _mousFun_Left_Click_Once_Event(Vector3 obj)
    {
    }

    private void _mousFun_Right_Double_Click_Event(Vector3 obj)
    {
        if (selectionCircle == null) return;    // only execute if unit has been selected
        //_bt.SetVariableValue("IsRun", true);    // set run bool in behavior tree
        _bt.SetVariableValue("NewPos", obj);    // give position to behavior tree
        if (_AIPath != null)
        {
            _AIPath.maxSpeed = run_Speed;
            _AIPath.rotationSpeed = run_turn_Speed;
        }
        if (_RichAI != null)
        {
            _RichAI.maxSpeed = run_Speed;
            _RichAI.rotationSpeed = run_turn_Speed;
        }
    }

    private void _mousFun_Right_Click_Once_Event(Vector3 obj)
    {
        if (selectionCircle == null) return;    // only execute if unit has been selected
        //_bt.SetVariableValue("IsRun", false);   // set run bool in behavior tree
        _bt.SetVariableValue("NewPos", obj);    // give position to behavior tree
        if (_AIPath != null)
        {
            _AIPath.maxSpeed = walk_Speed;
            _AIPath.rotationSpeed = walk_turn_Speed;
        }
        if (_RichAI != null)
        {
            _RichAI.maxSpeed = walk_Speed;
            _RichAI.rotationSpeed = walk_turn_Speed;
        }

    }

    private Camera_Controller_Mode _ccMode;
    private void _Camera_Mode_Changed(Camera_Controller_Mode _m)
    {
        _ccMode = _m;
    }

    // Update is called once per frame
    void Update () {
        // if is not in rts mode and has been selected
        if (_ccMode != Camera_Controller_Mode.RTS && selectionCircle)
        {
            Destroy(selectionCircle.gameObject);    // deselecte
            selectionCircle = null; //
        }
    }
}
