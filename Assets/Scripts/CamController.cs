using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float lerpTime;
    public float xOffset;

    private bool m_canLerp;
    private float m_lerpxDist;

    private void Update()
    {
        if(m_canLerp)
        {
            MoveLerp();
        }
    }

    private void MoveLerp()
    {
        float xPos = transform.position.x;
        xPos = Mathf.Lerp(xPos, m_lerpxDist, lerpTime * Time.deltaTime);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        if(transform.position.x >= (m_lerpxDist - xOffset))
        {
            m_canLerp = false;
        }
    }

    public void LerpTrigger(float dist)
    {
        m_canLerp = true;
        m_lerpxDist = dist;
    }
}
