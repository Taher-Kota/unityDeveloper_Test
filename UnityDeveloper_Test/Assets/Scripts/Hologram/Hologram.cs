using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    private const string UPPERARROWKEY = "upArrow";
    private const string DOWNARROWKEY = "downArrow";
    private const string LEFTARROWKEY = "leftArrow";
    private const string RIGHTARROWKEY = "rightArrow";
    [SerializeField] private List<Quaternion> directionList;
    [SerializeField] private List<Vector3> positionList;
    [SerializeField] private GameObject holoGram;
    private void Start()
    {
        InputManager.Instance.OnHologramActivate += InputManager_ActivateHologram;
        InputManager.Instance.OnHologramDeacitvate += InputManager_DeactivateHologram;
    }

    private void InputManager_DeactivateHologram(object sender, System.EventArgs e)
    {
        if (holoGram.activeInHierarchy)
        {
            DeactivateHologram();
        }
    }

    private void InputManager_ActivateHologram(object sender, InputManager.OnHologramEventArgs e)
    {
        if (holoGram.activeInHierarchy) return;
        switch (e.keyName)
        {
            case UPPERARROWKEY:
                ActivateHologram(directionList[3], positionList[3]);
                break;
            case DOWNARROWKEY:
                ActivateHologram(directionList[2], positionList[2]);
                break;
            case LEFTARROWKEY:
                ActivateHologram(directionList[1], positionList[1]);
                break;
            case RIGHTARROWKEY:
                ActivateHologram(directionList[0], positionList[0]);
                break;
        }
    }

    private void ActivateHologram(Quaternion direction, Vector3 position)
    {
        holoGram.SetActive(true);
        holoGram.transform.localPosition = position;
        holoGram.transform.localRotation = direction;
    }
    
    private void DeactivateHologram()
    {
        holoGram.SetActive(false);
    }
}
