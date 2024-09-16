using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]float MouseSensitivityY = 5f;
    [SerializeField] float SmoothDampY = 0.2f;
    [SerializeField]
    [Tooltip("Altura minima da camera")]
    float minCameraY = 0.5f;
    [SerializeField]
    [Tooltip("Altura maxima da camera")]
    float maxCameraY = 5f;

    CinemachineVirtualCamera _cm;
    CinemachineTransposer _tp;
    public bool Invertido=false;


    // Start is called before the first frame update
    void Start()
    {
        //esconder e bloquear o rato
        Cursor.lockState = CursorLockMode.Locked;
        _cm = FindObjectOfType<CinemachineVirtualCamera>();
        _tp = _cm.GetCinemachineComponent<CinemachineTransposer>();
        // ler a opcao do rato
        Invertido = PlayerPrefs.GetInt("'mouse invertido", 0) == 0 ? false : true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale==0) return;

        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;
        mouseY=mouseY*MouseSensitivityY;
        if (Invertido==false)
        _tp.m_FollowOffset.y = Mathf.SmoothStep(_tp.m_FollowOffset.y, _tp.m_FollowOffset.y + mouseY, SmoothDampY);
        else
            _tp.m_FollowOffset.y = Mathf.SmoothStep(_tp.m_FollowOffset.y, _tp.m_FollowOffset.y - mouseY, SmoothDampY);

        _tp.m_FollowOffset.y = Mathf.Clamp(_tp.m_FollowOffset.y, minCameraY, maxCameraY);

    }
}
