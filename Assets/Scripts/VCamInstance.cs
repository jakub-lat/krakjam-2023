using Cinemachine;
using UnityEngine;

public class VCamInstance : MonoBehaviour
{
    public static CinemachineVirtualCamera Current { get; private set; }

    private void Awake()
    {
        Current = GetComponent<CinemachineVirtualCamera>();
    }
}
