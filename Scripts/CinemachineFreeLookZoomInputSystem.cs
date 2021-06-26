//#define CM_2_1_10_OR_EARLIER // comment this out for later versions of Cinemachine

using System;
using UnityEngine;

/// <summary>
/// Zooms a CinemachineFreeLook camera in or out based on Mouse ScrollWheel input
/// </summary>
/// <seealso cref="https://forum.unity.com/threads/cinemachine-how-to-add-zoom-control-to-freelook-camera.505541/#post-3510212"/>
/// <remarks>
/// This code is modified from a post in the Unity forums:
/// https://forum.unity.com/threads/cinemachine-how-to-add-zoom-control-to-freelook-camera.505541/#post-3510212
/// From the author, @Gregoryl
/// I've made a version of the FreeLookZoom script that's compatible with CM 2.1.10 and Unity 2017. 
/// However, it's a little different: Once you add it to your FreeLook, you have to use the FreeLookZoom 
/// fields to define the original orbits, the FreeLook orbits themselves will be always overwritten.
/// It works, though.
/// </remarks>
/// 


namespace Cinemachine
{
    [SaveDuringPlay]
    [RequireComponent(typeof(CinemachineFreeLook), typeof(CinemachineInputProvider))]
    class CinemachineFreeLookZoomInputSystem : MonoBehaviour
    {
        private CinemachineFreeLook freelook;
        private CinemachineInputProvider inputProvider;
        public CinemachineFreeLook.Orbit[] originalOrbits = new CinemachineFreeLook.Orbit[0];

        [Tooltip("The minimum scale for the orbits")]
        [Range(0.01f, 5f)]
        public float minScale = 0.5f;

        [Tooltip("The maximum scale for the orbits")]
        [Range(1f, 25f)]
        public float maxScale = 1;

        private float zInput = 0f;
        [Range(0.01f, 100f)] public float zoomSensitiity = 10f;

        //        [Tooltip("The Vertical axis.  Value is 0..1.  How much to scale the orbits")]
        //#if CM_2_1_10_OR_EARLIER
        //        public AxisState zAxis = new AxisState(50f, 0.1f, 0.1f, 1, "Mouse ScrollWheel", false);
        //#else
        //        [AxisStateProperty]
        //        public AxisState zAxis = new AxisState(0, 1, false, true, 50f, 0.1f, 0.1f, "Mouse ScrollWheel", false);
        //#endif
        void OnValidate()
        {
            minScale = Mathf.Max(0.01f, minScale);
            maxScale = Mathf.Max(minScale, maxScale);
        }

        void Awake()
        {
            //#if true // make this true for CM 2.1.10 and earlier, false otherwise
            //            zAxis.SetThresholds(0, 1, false);
            //#endif
            freelook = GetComponent<CinemachineFreeLook>();
            inputProvider = GetComponent<CinemachineInputProvider>();
            if (freelook != null && originalOrbits.Length == 0)
            {
                zInput -= inputProvider.GetAxisValue(2) * Time.deltaTime * (zoomSensitiity / 1000f);
                zInput = Mathf.Clamp(zInput, -1f, 1f);
                float scale = Mathf.Lerp(minScale, maxScale, zInput);
                for (int i = 0; i < Mathf.Min(originalOrbits.Length, freelook.m_Orbits.Length); i++)
                {
                    freelook.m_Orbits[i].m_Height = originalOrbits[i].m_Height * scale;
                    freelook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * scale;
                }
            }
        }

        void Update()
        {
            if (freelook != null)
            {
                if (originalOrbits.Length != freelook.m_Orbits.Length)
                {
                    originalOrbits = new CinemachineFreeLook.Orbit[freelook.m_Orbits.Length];
                    Array.Copy(freelook.m_Orbits, originalOrbits, freelook.m_Orbits.Length);
                }
                zInput -= inputProvider.GetAxisValue(2) * Time.deltaTime * (zoomSensitiity / 1000f);
                zInput = Mathf.Clamp(zInput, -1f, 1f);
                float scale = Mathf.Lerp(minScale, maxScale, zInput);
                for (int i = 0; i < Mathf.Min(originalOrbits.Length, freelook.m_Orbits.Length); i++)
                {
                    freelook.m_Orbits[i].m_Height = originalOrbits[i].m_Height * scale;
                    freelook.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * scale;
                }
            }
        }
    }
}