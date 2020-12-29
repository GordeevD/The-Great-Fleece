using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _anim;
    private Vector3 _target;
    public GameObject coinPrefab;
    public AudioClip coinSound;
    private bool _haveCoin = true;

    public static Action<Vector3> OnCoinToss;
   // public Vector3 coinPosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
            //    Debug.Log("Hit " + hitInfo.point);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = hitInfo.point;
                cube.SetActive(false);
                _agent.SetDestination(hitInfo.point);
                _anim.SetBool("Walk", true);
                _target = hitInfo.point;
            }
        }

        float distance = Vector3.Distance(transform.position, _target);
        if(distance < 1.0f)
         _anim.SetBool("Walk", false);

        if (Input.GetMouseButtonDown(1) && _haveCoin)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(rayOrigin, out hitInfo))
            {
                StartCoroutine(ThrowCoin(hitInfo));
            }

        }

    }

    IEnumerator ThrowCoin(RaycastHit hitInfo)
    {
        _anim.SetTrigger("Throw");
        _haveCoin = false;
        yield return new WaitForSeconds(2f);
        Instantiate(coinPrefab, hitInfo.point, Quaternion.identity);
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
        SendAIGuardToCoinSpot(hitInfo.point);
    }

    void SendAIGuardToCoinSpot(Vector3 coinPosition)
    {
        if (OnCoinToss != null)
            OnCoinToss(coinPosition);
    }
}
