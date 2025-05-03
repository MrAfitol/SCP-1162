using AdminToys;
using Mirror;
using UnityEngine;

namespace SCP1162
{
    // The code was originally created by the brilliant Jesus-QC and has been slightly modified by me.
    public class SimplifiedToy
    {
        /// <summary>
        /// A primitive type
        /// </summary>
        public PrimitiveType Type;

        /// <summary>
        /// A primitive position 
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// A primitive rotation 
        /// </summary>
        public Vector3 Rotation;

        /// <summary>
        /// A primitive scale 
        /// </summary>
        public Vector3 Scale;

        /// <summary>
        /// A primitive color 
        /// </summary>
        public Color PrimitiveColor;

        /// <summary>
        /// A primitive transparency
        /// </summary>
        public float Alpha;

        /// <summary>
        /// A primitive parent
        /// </summary>
        public Transform Parent;

        /// <summary>
        /// A primitive base
        /// </summary>
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

        /// <summary>
        /// Create primitive object
        /// </summary>
        /// <param name="type">The primitive type</param>
        /// <param name="position">The primitive position</param>
        /// <param name="rotation">The primitive rotation</param>
        /// <param name="scale">The primitive scale</param>
        /// <param name="color">The primitive color</param>
        /// <param name="parent">The primitive parent</param>
        /// <param name="alpha">The primitive transparency</param>
        public SimplifiedToy(PrimitiveType type, Vector3 position, Vector3 rotation, Vector3 scale, Color color, Transform parent = null, float alpha = 1f)
        {
            this.Type = type;
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
            this.PrimitiveColor = color;
            this.Alpha = alpha;
            this.Parent = parent;
        }

        /// <summary>
        /// Spawn primitive
        /// </summary>
        /// <returns>Returns the created primitive</returns>
        public PrimitiveObjectToy Spawn()
        {
            var toy = Object.Instantiate(ToyPrefab);

            toy.NetworkPrimitiveType = Type;

            if (Parent != null) toy.transform.SetParent(Parent);

            toy.transform.localPosition = Position;
            toy.transform.localEulerAngles = Rotation;
            toy.transform.localScale = Scale;

            toy.NetworkScale = Scale;
            toy.NetworkMaterialColor = PrimitiveColor * Alpha;
            toy.NetworkMovementSmoothing = 60;

            NetworkServer.Spawn(toy.gameObject);

            return toy;
        }
    }
}