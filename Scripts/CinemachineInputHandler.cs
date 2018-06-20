using UnityEngine;
using Cinemachine;

/// <summary>
/// Component that exposes methods for enabling and disabling CinemachineFreeLook Input
/// </summary>
/// <seealso cref="https://forum.unity.com/threads/disable-freelook-input.536999/#post-3538277"/>
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
