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
    }
}
