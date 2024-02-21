using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 jumpForce;
    public Vector2 jumpForceUp;
    public float minForceX;
    public float minForceY;
    public float maxForceX;
    public float maxForceY;

    [HideInInspector]

    public int lastPlatformId;

    private bool m_didJump;
    private bool m_PowerSetted;

    private Rigidbody2D m_rb;
    private Animator m_anim;

    private float m_curPowerBarVal = 0f;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(GameManager.Ins.IsGameStarted)
        {
            SetPower();

            if (Input.GetMouseButtonDown(0))
            {
                SetPower(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                SetPower(false);
            }
        }    
    }

    private void SetPower()
    {
        if(m_PowerSetted && !m_didJump)
        {
            jumpForce.x += jumpForceUp.x * Time.deltaTime;
            jumpForce.y += jumpForceUp.y * Time.deltaTime;

            jumpForce.x = Mathf.Clamp(jumpForce.x,minForceX,maxForceX);

            jumpForce.y = Mathf.Clamp(jumpForce.y,minForceY,maxForceY);

            m_curPowerBarVal += GameManager.Ins.powerBarUp * Time.deltaTime;

            GameGUIManager.Ins.UpdatePowerBar(m_curPowerBarVal, 1);
        }
    }

    public void SetPower(bool isHoldingMouse)
    {
        m_PowerSetted = isHoldingMouse;

        if(!m_PowerSetted && !m_didJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!m_rb || jumpForce.x <= 0 || jumpForce.y <= 0) return;

        m_rb.velocity = jumpForce;

        m_didJump = true;

        if(m_anim)
        {
            m_anim.SetBool("didJump", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagConsts.GROUND))
        {
            Platform p = collision.transform.root.GetComponent<Platform>();

            if(m_didJump)
            {
                m_didJump = false;

                if(m_anim)
                {
                    m_anim.SetBool("didJump", false);
                }    

                if(m_rb)
                {
                    m_rb.velocity = Vector2.zero;
                }

                jumpForce = Vector2.zero;

                m_curPowerBarVal = 0;

                GameGUIManager.Ins.UpdatePowerBar(m_curPowerBarVal, 1);
            }    

            if(p && p.id != lastPlatformId)
            {
                GameManager.Ins.CreatePlatformandLerp(transform.position.x);

                lastPlatformId = p.id;

                GameManager.Ins.AddScore();
            }    
        }

        if(collision.CompareTag(TagConsts.DEAD_ZONE))
        {
            GameGUIManager.Ins.ShowGameOverDialog();
            Destroy(gameObject);
        }    
    }
}
