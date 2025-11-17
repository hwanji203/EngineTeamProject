using UnityEngine;

namespace Member.Kimyongmin._02.Code
{
    public static class KimComponent
    {
        public static bool TryGetcomponentInParent<T>(this Component component, out T result) where T : Component
        {
            result = component.GetComponentInParent<T>();
            return result != null;
        }
        
        public static T GetComponentInDirectChildren<T>(this Transform parent) where T : Component
        {
            foreach (Transform child in parent)
            {
                if (child.TryGetComponent<T>(out T component))
                {
                    return component;
                }
            }

            return null;
        }
    }
}
