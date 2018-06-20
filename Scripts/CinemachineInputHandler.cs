using UnityEngine;
using Cinemachine;

public class CinemachineInputHandler : MonoBehaviour
{
    private CinemachineCore.AxisInputDelegate DefaultAxisInput;


    private void Awake()
    {
        DefaultAxisInput = CinemachineCore.GetInputAxis;
    }


    float DisableAxisInputDelegate(string axisName)
    {
        return 0f;
    }


    public void DisableInput()
    {
        CinemachineCore.GetInputAxis = DisableAxisInputDelegate;
    }


    public void EnableInput()
    {
        CinemachineCore.GetInputAxis = DefaultAxisInput;
    }
}
