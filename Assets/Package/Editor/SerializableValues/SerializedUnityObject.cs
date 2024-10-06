using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal struct SerializedUnityObject
    {
        public bool IsNull;
        public string AssetGuid;
        public string ComponentTypeFullName;
        public long ComponentFileId;

        public void SetValue(UnityEngine.Object value)
        {
            if (value == null)
            {
                IsNull = true;
                return;
            }

            var assetGuid = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(value)).ToString();

            if (!IsValidAssetGuid(assetGuid))
                throw new ArgumentException("Object is not a valid asset");

            if (value is Component)
            {
                ComponentTypeFullName = value.GetType().FullName;
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(value, out string _, out ComponentFileId);
            }

            AssetGuid = assetGuid;
        }

        private bool IsValidAssetGuid(string assetGuid)
        {
            return !assetGuid.All(character => character == '0');
        }

        public UnityEngine.Object GetValue()
        {
            if (IsNull)
                return null;

            var assetPath = AssetDatabase.GUIDToAssetPath(new GUID(AssetGuid));
            var obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);

            if (!string.IsNullOrEmpty(ComponentTypeFullName))
                obj = GetComponentFromFileId(obj as GameObject);

            return obj;
        }

        private UnityEngine.Object GetComponentFromFileId(GameObject gameObject)
        {
            var allPossibleComponents = gameObject.GetComponents(ComponentTypeNameToType());

            foreach (var component in allPossibleComponents)
            {
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(component, out string _, out long fileId);

                if (fileId == ComponentFileId)
                    return component;
            }

            return gameObject;
        }

        private Type ComponentTypeNameToType()
        {
            foreach (var type in TypeCache.GetTypesDerivedFrom<UnityEngine.Object>())
            {
                if (type.FullName == ComponentTypeFullName)
                    return type;
            }

            return null;
        }
    }
}
