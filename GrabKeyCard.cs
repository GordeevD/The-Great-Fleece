using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKeyCard : MonoBehaviour
{
    public GameObject sleepingGuardCutscene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sleepingGuardCutscene.SetActive(true);
            GameManager.Instance.HasCard = true;
        }
    }
}