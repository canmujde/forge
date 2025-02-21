using UnityEngine;

namespace Extensions
{
    public static class CameraExtensions
    {
        public static void SetOrthographicSize(this Camera camera, float size) => camera.orthographicSize = size;
        public static void FitToChildRenderers(this Camera camera, Transform parentTransform, float offset)
        {
            var childRenderers = parentTransform.GetComponentsInChildren<Renderer>();

            if (childRenderers.Length == 0)
            {
                Debug.LogWarning("No child renderers found.");
                return;
            }

            var bounds = childRenderers[0].bounds;
            for (var i = 1; i < childRenderers.Length; i++)
            {
                bounds.Encapsulate(childRenderers[i].bounds);
            }

            bounds.Expand(offset);

            var cameraSize = bounds.size.y / 2f;
            var cameraAspect = camera.aspect;

            if (cameraAspect >= 1f)
            {
                camera.orthographicSize = cameraSize;
            }
            else
            {
                var targetWidth = bounds.size.x / 2f / cameraAspect;
                camera.orthographicSize = Mathf.Max(cameraSize, targetWidth);
            }
        }

    }
}