using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public gun gun;
    FixedJoint fixedJoint;

    [HideInInspector]
    public GameObject collisionObject;
    public Vector3 hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" && gun.shot) 
        {
            hitPoint = collision.contacts[0].point;
            collisionObject = collision.gameObject;
            if (fixedJoint != null)
            {
                Destroy(fixedJoint);
            }
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = collisionObject.GetComponent<Rigidbody>();
            gun.Swing();
        }
    }

    public void DestroyJoint()
    {
        Destroy(fixedJoint);
    }
}
