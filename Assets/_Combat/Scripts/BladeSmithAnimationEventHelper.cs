using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;
using RootMotion;

namespace GerogeScripts
{
    [RequireComponent(typeof(BS_Weapon_Animation_Events))]
    public class BladeSmithAnimationEventHelper : MonoBehaviour
    {
        private PropRoot[] AllPropRootSlot;
        private PropRoot leftHandPropRoot;
        private PropRoot rightHandPropRoot;
        // now only set to 2 event for left hand and right hand incase there are two weapons for dual wielding
        private BS_Weapon_Animation_Events BS_animeEvent;
        private float timer = 1f;
        // Start is called before the first frame update
        void Start()
        {
            SearchForBS_AnimeEvent();
            if (SearchForPropRoot()){
                // now only check right hand slot, if is empty or the obj it hold is a weapon
                if (!CheckPropEmptyAndWeapon(rightHandPropRoot))
                {
                    // if is empty or hold item is not a weapon then turn off animeEvent scrip to avoid error
                    BS_animeEvent.enabled = false;
                    timer = 1f; // set the timer to 1s
                }
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            // if right hand weapon is already assigned to marker manage then return
            if (BS_animeEvent.Weapon1MarkerManger != null) return;
            // else check hand slot every 1s
            timer = timer - Time.deltaTime;
            if (timer <= 0)
            {
                if (!CheckPropEmptyAndWeapon(rightHandPropRoot)) timer = 1f;
            }
        }

        private bool SearchForPropRoot()
        {
            // search all PropRoot scripts and put in to array 
            PropRoot[] _propRoots = GetComponentsInChildren<PropRoot>();
            if (_propRoots != null)
            {
                foreach (var item in _propRoots)
                {
                    // search array for right hand prop and left hand prop
                    if (item.gameObject.name == "LeftHandSlot") leftHandPropRoot = item;
                    else if (item.gameObject.name == "RightHandSlot") rightHandPropRoot = item;
                    else Debug.LogError("There is no correct named slot, please check proproot obj's name");
                }
                return true;
            }
            else
            {
                // if there are no proproot scrip to put weapon then get error
                Debug.LogError("There is no proproot in this obj or child objs");
            }
            return false;
        }

        private bool CheckPropEmptyAndWeapon(PropRoot _pr)
        {
            if (_pr == null) return false;
            Prop _p = _pr.currentProp;
            
            if(_p != null && _p.gameObject.tag == "Weapon")
            {
                // enable the script
                BS_animeEvent.enabled = true;
                // put weapon slot 1 for right hand manager
                BS_animeEvent.Weapon1MarkerManger = rightHandPropRoot.currentProp.GetComponentInChildren<BS_Marker_Manager>();
                // set right hand weapon as default weapon
                BS_animeEvent.MarkerManager = BS_animeEvent.Weapon1MarkerManger;
                return true;
            }
            return false;
        }

        private bool SearchForBS_AnimeEvent()
        {
            // search for all bs_animeEvent scripts in attached obj
            BS_animeEvent = GetComponent<BS_Weapon_Animation_Events>(); 
            if (BS_animeEvent != null)
            {
                return true;
            }
            return false;
        }
    }
}