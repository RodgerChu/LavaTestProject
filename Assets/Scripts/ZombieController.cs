using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Transform ragdollParent;
    public Transform zombieStartPosition;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        OnReset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHit()
    {
        Time.timeScale = 0.5f;
        animator.enabled = false;
        ChangeGravityForJoint(ragdollParent, true);
    }

    public void OnReset()
    {
        Time.timeScale = 1;
        animator.enabled = true;
        gameObject.transform.localPosition = zombieStartPosition.position;
        gameObject.transform.localRotation = Quaternion.identity;
        ChangeGravityForJoint(ragdollParent, false);
    }

    private void ChangeGravityForJoint(Transform transform, bool enabled)
    {
        if (transform == null || transform.childCount == 0)
            return;

        var rb = transform.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = !enabled;
            rb.velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("Child without rb");
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            ChangeGravityForJoint(child, enabled);
        }
    }
}
