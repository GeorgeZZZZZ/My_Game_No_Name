using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;

namespace GerogeScripts
{
    [RequireComponent(typeof(Animator))]
    public class PlayerPicking : MonoBehaviour
    {
        public PropRoot leftHandPropRoot;   //  use to grib obj in left hand
        public PropRoot rightHandPropRoot;   //  use to grib obj in right hand

        public List<Prop> InRangeItems = new List<Prop>();
        public float Pick_Frequency_Limit = 1f;
        private Animator anime;
        private float timer = 0;

        private AnimationEventMessage anime_event;

        // Start is called before the first frame update
        void Start()
        {
            SearchForPropRoot();
            anime = GetComponent<Animator>();
            anime_event = GetComponent<AnimationEventMessage>();
            if (anime_event == null) Debug.LogWarning("AnimationEventMessage not assign!");
            else
                anime_event.Animation_middle_00 += Anime_mid_00;
        }

        // Update is called once per frame
        void Update()
        {
            float _picking = Input.GetAxis("Grab");
            bool _droping = Input.GetKeyUp("x");

            if (timer > 0) timer -= Time.deltaTime;
            if (_picking != 0 && timer <= 0)
            {
                // if nothing in range then return
                if (InRangeItems.Count == 0) return;
                timer = Pick_Frequency_Limit;
                // start pick animation
                anime.SetTrigger("IsPicking");
            }

            if (_droping) DropItem();
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

        private void PickItem()
        {
            // if right hand and left hand are not empty then return
            if (rightHandPropRoot.currentProp != null && leftHandPropRoot.currentProp != null) return;
            // if right hand is occupied then put item to left hand
            else if (rightHandPropRoot.currentProp != null)
            {
                InRangeItems[0].gameObject.GetComponent<SphereCollider>().enabled = false;
                leftHandPropRoot.currentProp = InRangeItems[0];
                InRangeItems.RemoveAt(0);
            }
            // pick up by right hand
            else
            {
                // turn off the tigger collider
                InRangeItems[0].gameObject.GetComponent<SphereCollider>().enabled = false;
                // take in hand
                rightHandPropRoot.currentProp = InRangeItems[0];
                // remove item in list
                InRangeItems.RemoveAt(0);
            }
        }

        private void DropItem()
        {
            PropRoot _dorpProp = new PropRoot();
            // By setting the prop root's currentProp to null, the prop connected to it will be dropped.
            if (rightHandPropRoot != null)  //  if right hand has occupied then drop right hand first
            {
                _dorpProp = rightHandPropRoot;
            }
            else if (leftHandPropRoot != null)    //  if right hand is empty but left hand has occupied then dorp left hand obj
            {
                _dorpProp = leftHandPropRoot;
            }

            if (_dorpProp.currentProp != null)
            {
                // turn on tigger collider
                _dorpProp.currentProp.gameObject.GetComponent<SphereCollider>().enabled = true;
                _dorpProp.currentProp.gameObject.GetComponentInChildren<BS_Marker_Manager>()._markersAreEnabled = true;
                // drop item on hand
                _dorpProp.currentProp = null;
            }
        }
        private void OnTriggerEnter(Collider collider)
        {
            CheckAndAddItem(collider);
        }

        private void OnTriggerStay(Collider other)
        {
            // always check if in range item is in list
            // otherwise droping item is not goint to trigger OnTriggerEnter
            // look like droping item is trigger the OnTriggerEnter methord
            //CheckAndAddItem(other);
        }

        private void OnTriggerExit(Collider other)
        {
            // search all items in list
            for (int i = 0; i < InRangeItems.Count; i++)
            {
                // if exit item is in list then take it out from the list
                if (InRangeItems[i].gameObject.name == other.gameObject.name)
                    InRangeItems.RemoveAt(i);
            }
        }

        private void CheckAndAddItem(Collider _c)
        {
            if (_c.GetComponent<Prop>().isPickedUp) return;
            int _i = 0;
            // search all items in item list
            foreach (var item in InRangeItems)
            {
                // if this item is not match then count it
                if (item.gameObject.name != _c.name)
                {
                    _i++;
                }
            }
            // if all item has been count but no same item then add it to the list
            if (_i == InRangeItems.Count) InRangeItems.Add(_c.GetComponent<Prop>());

        }

        private void Anime_mid_00(string _s)
        {
            // received animation complete message then pick item
            PickItem();
        }

        private void OnDestroy()
        {
            anime_event.Animation_middle_00 -= Anime_mid_00;
        }
    }
}
