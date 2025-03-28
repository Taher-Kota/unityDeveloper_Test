using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCubes : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")){
            GameManager.Instance.IncremenetCubeCount();
            gameObject.SetActive(false);
        }
    }
}
