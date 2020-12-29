using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public GameObject gameOverCutscene;
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MeshRenderer render = GetComponent<MeshRenderer>();
            render.material.SetColor("_TintColor", new Color(0.6f,.1f,.1f,.3f));

            anim.enabled = false;
            StartCoroutine(AlertRoutine());
            
        }
    }

    IEnumerator AlertRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        gameOverCutscene.SetActive(true);
    }
}
