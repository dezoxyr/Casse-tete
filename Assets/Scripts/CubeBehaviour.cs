using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CubeBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject medal;
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private GameObject endMenu;
    [SerializeField]
    private GameObject startMenu;

    private RaycastHit m_Hit;
    private int m_Action = 0;
    private int m_code = 1;
    private GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void launch()
    {
        startMenu.SetActive(false);
        cube.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) { return; }

        Ray ray = arCamera.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out m_Hit))
        {
            //Debug.Log(m_Hit.transform.gameObject.name);
            Debug.Log(m_Hit.collider.gameObject.name);
            Debug.Log(m_Action);
            if (m_Hit.collider.gameObject.name == "Bouton")
            {
                m_Action = 1;
                MeshRenderer renderer = m_Hit.collider.gameObject.GetComponent<MeshRenderer>();
                renderer.material.SetColor("_Color", Color.red);
            }

            if (m_Action == 1)
            {
                if (m_Hit.collider.gameObject.name == "Slider")
                {
                    m_Action = 2;
                    Animator anim = m_Hit.collider.gameObject.GetComponent<Animator>();
                    anim.SetBool("Slide", true);
                }
            }
            else if (m_Action == 2)
            {
                if (m_Hit.collider.gameObject.name == "Key")
                {
                    MeshRenderer renderer = m_Hit.collider.gameObject.GetComponent<MeshRenderer>();
                    renderer.material.SetColor("_Color", Color.red);
                    g = m_Hit.collider.gameObject;
                    m_Action = 3;
                }
            }
            else if (m_Action == 3)
            {
                if (m_Hit.collider.gameObject.name == "Hole")
                {
                    MeshRenderer renderer = m_Hit.collider.gameObject.GetComponent<MeshRenderer>();
                    renderer.material.SetColor("_Color", Color.red);
                    g.SetActive(false);
                    m_Action = 4;
                }
            }
            else if (m_Action == 4)
            {
                if (m_Hit.collider.gameObject.name == "B_ordre 1")
                {
                    if (m_code == 1)
                    {
                        m_code = 3;
                    }
                    else { m_code = 1; }

                }
                if (m_Hit.collider.gameObject.name == "B_ordre 3")
                {
                    if (m_code == 3)
                    {
                        m_code = 4;
                    }
                    else { m_code = 1; }

                }
                if (m_Hit.collider.gameObject.name == "B_ordre 4")
                {
                    if (m_code == 4)
                    {
                        m_code = 2;
                    }
                    else { m_code = 1; }
                }
                if (m_Hit.collider.gameObject.name == "B_ordre 2")
                {
                    if (m_code == 2)
                    {
                        m_Action = 5;
                    }
                    else { m_code = 1; }
                }
            }
            else if (m_Action == 5)
            {
                if (m_Hit.collider.gameObject.name == "End")
                {
                    m_Action = 6;
                    Animator anim = m_Hit.collider.gameObject.GetComponent<Animator>();
                    anim.SetBool("End", true);
                }
            }
            else if (m_Action == 6)
            {
                if (m_Hit.collider.gameObject.name == "Medal")
                {
                    Debug.Log("You win !");
                    cube.SetActive(false);
                    endMenu.SetActive(true);
                    medal.SetActive(true);
                    /*Animator anim = m_Hit.collider.gameObject.GetComponent<Animator>();
                    anim.Play("Start");*/
                }

            }
        }
    }
}
