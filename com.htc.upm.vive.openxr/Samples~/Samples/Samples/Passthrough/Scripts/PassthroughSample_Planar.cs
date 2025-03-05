// Copyright HTC Corporation All Rights Reserved.

using UnityEngine;

#if UNITY_ANDROID
using VIVE.OpenXR.Passthrough;
using VIVE.OpenXR.Samples;
#endif

namespace VIVE.OpenXR.CompositionLayer.Samples.Passthrough
{
    public class PassthroughSample_Planar : MonoBehaviour
    {
#if UNITY_ANDROID
        private OpenXR.Passthrough.XrPassthroughHTC activePassthroughID = 0;
        private LayerType currentActiveLayerType = LayerType.Underlay;

        private void Update()
        {
            if (VRSInputManager.instance.GetButtonDown(VRSButtonReference.B)) //Set Passthrough as Overlay
            {
                SetPassthroughToOverlay();
            }
            if (VRSInputManager.instance.GetButtonDown(VRSButtonReference.A)) //Set Passthrough as Underlay
            {
                SetPassthroughToUnderlay();
            }
            if (VRSInputManager.instance.GetButtonDown(VRSButtonReference.GripR))
            {
                if (activePassthroughID == 0)
                {
                    StartPassthrough();
                }
            }
            if (VRSInputManager.instance.GetButtonDown(VRSButtonReference.GripL))
            {
                if(activePassthroughID != 0)
                {
                    PassthroughAPI.DestroyPassthrough(activePassthroughID);
                    activePassthroughID = 0;
                }
            }
        }
#endif
        public void SetPassthroughToOverlay()
        {
#if UNITY_ANDROID
            if (activePassthroughID != 0)
            {
                PassthroughAPI.SetPassthroughLayerType(activePassthroughID, LayerType.Overlay);
                currentActiveLayerType = LayerType.Overlay;
            }
#endif
        }

        public void SetPassthroughToUnderlay()
        {
#if UNITY_ANDROID
            if (activePassthroughID != 0)
            {
                PassthroughAPI.SetPassthroughLayerType(activePassthroughID, LayerType.Underlay);
                currentActiveLayerType = LayerType.Underlay;
            }
#endif
        }

        void StartPassthrough()
        {
#if UNITY_ANDROID
            PassthroughAPI.CreatePlanarPassthrough(out activePassthroughID, currentActiveLayerType, OnDestroyPassthroughFeatureSession);
#endif
        }

        void OnDestroyPassthroughFeatureSession(OpenXR.Passthrough.XrPassthroughHTC passthroughID)
        {
#if UNITY_ANDROID
            PassthroughAPI.DestroyPassthrough(passthroughID);
            activePassthroughID = 0;
#endif
        }
    }
}
