using PluginAPI.Core;

namespace SCP1162
{
    using AdminToys;
    using Mirror;
    using UnityEngine;

    public class SimplifiedToy
    {
        public PrimitiveObjectToy Base;
        private PrimitiveObjectToy ToyPrefab
        {
            get
            {
                if (Base == null)
                {
                    foreach (var gameObject in NetworkClient.prefabs.Values)
                        if (gameObject.TryGetComponent<PrimitiveObjectToy>(out var component))
                            Base = component;
                }

                return Base;
            }
        }

        public PrimitiveType Type;
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
        public Color PrimitiveColor;
        public float Alpha = 1f;
        public Transform Parent;

        public SimplifiedToy(PrimitiveType type ,Vector3 position, Vector3 rotation, Vector3 scale, Color color, Transform parent = null, float alpha = 1f)
        {
            this.Type = type;
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
            this.PrimitiveColor = color;
            this.Alpha = alpha;
            this.Parent = parent;
        }

        public GameObject Spawn()
        {
            var toy = Object.Instantiate(ToyPrefab);

            toy.NetworkPrimitiveType = Type;

            if (Parent != null) toy.transform.parent = Parent;

            toy.transform.localPosition = Position;
            toy.transform.localEulerAngles = Rotation;
            toy.transform.localScale = Scale;

            toy.NetworkScale = Scale;
            toy.NetworkMaterialColor = PrimitiveColor * Alpha;
            toy.NetworkMovementSmoothing = 60;

            NetworkServer.Spawn(toy.gameObject);
            
            return toy.gameObject;
        }
    }
}