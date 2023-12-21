using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    [Header("Bullet Info")]
    public GameObject bullet;
    public float bulletSpeed;
    Transform bulletTransform;
    Rigidbody bulletRb;
    bullet bulletScript;

    [Header("Player Info")]
    public GameObject playerObject;
    SpringJoint springJoint;

    [Header("Gun Info")]
    public Transform barrel;

    /*
    [Header("Debug Info")]
    public GameObject attached;
    public float spring;
    public float damper;
    public float min;*/


    public bool shot {  get; set; }

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = bullet.GetComponent<Rigidbody>();
        bulletTransform = bullet.transform;
        bulletScript = bullet.GetComponent<bullet>();
        //Swing();
    }

    bool IsGrounded()
    {
        Collider c = playerObject.GetComponentInChildren<Collider>();
        Physics.Raycast(c.transform.position, -Vector3.up, out var hit, c.bounds.extents.y + 0.1f);
        return hit.collider.gameObject.tag == "Ground";
    }

// Update is called once per frame
    void Update()
    {
        if (!shot)
        {
            bulletTransform.position = barrel.position;
            bulletTransform.forward = barrel.forward;
            bulletScript.DestroyJoint();
        }
        if (!IsGrounded())
        {
            playerObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
        } else
        {
            playerObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
        }
    }

    public void Fire()
    {
        shot = true;
        bulletTransform.position = barrel.position;
        bulletRb.velocity = barrel.forward * bulletSpeed;
    }

    public void CancelFire()
    {
        shot = false;
        bulletScript.DestroyJoint();
        Destroy(springJoint);
    }

    public void Swing()
    {
        if (springJoint != null)
        {
            Destroy(springJoint);
        }
        springJoint = playerObject.AddComponent<SpringJoint>();
        springJoint.connectedBody = bulletScript.collisionObject.GetComponent<Rigidbody>();
        //springJoint.connectedBody = attached.GetComponent<Rigidbody>();
        springJoint.autoConfigureConnectedAnchor = false;
        springJoint.connectedAnchor = bulletScript.collisionObject.transform.InverseTransformPoint(bulletScript.hitPoint);
        //springJoint.connectedAnchor = attached.transform.InverseTransformPoint(attached.GetComponent<Transform>().position - new Vector3(0, 0, 0.35f));
        //springJoint.anchor = new Vector3(0, (playerObject.transform.position.y - attached.transform.position.y) * 1f, 0);
        //springJoint.anchor = playerObject.transform.InverseTransformPoint(attached.transform.position) - new Vector3(0.5f, 0.5f, 0.5f); 
        springJoint.anchor = Vector3.zero;

        float disJointToPlayer = Vector3.Distance(playerObject.transform.position, bulletTransform.position);
        //springJoint.maxDistance = disJointToPlayer * 0.9f;
        springJoint.minDistance = 0;
        springJoint.damper = 175;
        springJoint.spring = 350;
    }
}
