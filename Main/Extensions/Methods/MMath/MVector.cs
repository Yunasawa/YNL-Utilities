using System;
using System.Collections.Generic;
using UnityEngine;

namespace YNL.Extensions.Methods
{
    public static class MVector
    {
        #region 🔁 Converting Vector
        /// <summary> 
        /// Convert from Vector3 into Vector2 
        /// </summary>
        public static Vector2 ToVector2(this Vector3 input)
            => new Vector2(input.x, input.y);

        /// <summary> 
        /// Convert from Vector2 float into Vector2 integer. 
        /// </summary>
        public static Vector2Int ToVector2Int(this Vector2 input)
            => new Vector2Int((int)input.x, (int)input.y);

        /// <summary> 
        /// Convert from Vector3 float into Vector3 integer. 
        /// </summary>
        public static Vector3Int ToVector3Int(this Vector3 input)
            => new Vector3Int((int)input.x, (int)input.y, (int)input.z);
        #endregion

        #region 📠 Calculating Vector

        /// <summary> 
        /// Make a vector3 with random value betwwen <b><i> x </i></b> and <b><i> y </i></b>. 
        /// </summary>
        public static Vector3 Vector3Random(this Vector3 point, float x, float y)
            => new Vector3(point.x + UnityEngine.Random.Range(x, y), point.y + UnityEngine.Random.Range(x, y), point.z + UnityEngine.Random.Range(x, y));

        /// <summary>
        /// Make a division of two Vector3s.
        /// </summary>
        public static Vector3 DividedBy(this Vector3 vector1, Vector3 vector2)
            => new(vector1.x / vector2.x, vector1.y / vector2.y, vector1.z / vector2.z);

        /// <summary> 
        /// Returns the vector2 with FPS's change with default scale of 120 
        /// </summary>
        public static Vector2 Oscillate(this Vector2 vector)
            => new(vector.x.Oscillate(), vector.y.Oscillate());

        /// <summary> 
        /// Returns the vector2 with FPS's change with scale of 'target'.
        /// </summary>
        public static Vector2 Oscillate(this Vector2 vector, float target)
            => new(vector.x.Oscillate(target), vector.y.Oscillate(target));

        /// <summary> 
        /// Returns the vector3 with FPS's change with default scale of 120 
        /// </summary>
        public static Vector3 Oscillate(this Vector3 vector)
            => new(vector.x.Oscillate(), vector.y.Oscillate(), vector.z.Oscillate());

        /// <summary> 
        /// Returns the vector3 with FPS's change with scale of 'target'.
        /// </summary>
        public static Vector3 Oscillate(this Vector3 vector, float target)
            => new(vector.x.Oscillate(target), vector.y.Oscillate(target), vector.z.Oscillate(target));

        /// <summary> 
        /// Returns the vector4 with FPS's change with default scale of 120 
        /// </summary>
        public static Vector4 Oscillate(this Vector4 vector)
            => new(vector.x.Oscillate(), vector.y.Oscillate(), vector.z.Oscillate(), vector.w.Oscillate());

        /// <summary> 
        /// Returns the vector4 with FPS's change with scale of 'target'.
        /// </summary>
        public static Vector4 Oscillate(this Vector4 vector, float target)
            => new(vector.x.Oscillate(target), vector.y.Oscillate(target), vector.z.Oscillate(target), vector.w.Oscillate(target));

        #endregion

        #region 📐 Set / Add / Lock Vector
        public static Vector2 SetX(this Vector2 vector, float x) => new Vector2(x, vector.y);
        public static Vector2 SetY(this Vector2 vector, float y) => new Vector2(vector.x, y);

        public static Vector2 AddX(this Vector2 vector, float x) => new Vector2(vector.x + x, vector.y);
        public static Vector2 AddY(this Vector2 vector, float y) => new Vector2(vector.x, vector.y + y);

        public static Vector3 SetX(this Vector3 vector, float x) => new Vector3(x, vector.y, vector.z);
        public static Vector3 SetY(this Vector3 vector, float y) => new Vector3(vector.x, y, vector.z);
        public static Vector3 SetZ(this Vector3 vector, float z) => new Vector3(vector.x, vector.y, z);

        public static Vector3 AddX(this Vector3 vector, float x) => new Vector3(vector.x + x, vector.y, vector.z);
        public static Vector3 AddY(this Vector3 vector, float y) => new Vector3(vector.x, vector.y + y, vector.z);
        public static Vector3 AddZ(this Vector3 vector, float z) => new Vector3(vector.x, vector.y, vector.z + z);
        #endregion

        #region 📝 Get Closest / Farthest Vector
        /// <summary>
        /// Get the closest Vector2 from an array to the original Vector2.
        /// </summary>
        public static Vector2 GetClosestVector2From(this Vector2 vector, Vector2[] otherVectors)
        {
            if (otherVectors.Length == 0) throw new Exception("The array of other vectors is empty");
            var minDistance = Vector2.Distance(vector, otherVectors[0]);
            var minVector = otherVectors[0];
            for (var i = otherVectors.Length - 1; i > 0; i--)
            {
                var newDistance = Vector2.Distance(vector, otherVectors[i]);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    minVector = otherVectors[i];
                }
            }
            return minVector;
        }

        /// <summary>
        /// Get the closest Vector2 from a list to the original Vector2.
        /// </summary>
        public static Vector2 GetClosestVector2From(this Vector2 vector, List<Vector2> otherVectors)
        {
            if (otherVectors.Count == 0) throw new Exception("The list of other vectors is empty");
            var minDistance = Vector2.Distance(vector, otherVectors[0]);
            var minVector = otherVectors[0];
            for (var i = otherVectors.Count - 1; i > 0; i--)
            {
                var newDistance = Vector2.Distance(vector, otherVectors[i]);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    minVector = otherVectors[i];
                }
            }
            return minVector;
        }

        /// <summary>
        /// Get the farthest Vector2 from an array to the original Vector2.
        /// </summary>
        public static Vector2 GetFarthestVector2From(this Vector2 vector, Vector2[] otherVectors)
        {
            if (otherVectors.Length == 0) throw new Exception("The array of other vectors is empty");
            var maxDistance = Vector2.Distance(vector, otherVectors[0]);
            var maxVector = otherVectors[0];
            for (var i = otherVectors.Length - 1; i > 0; i--)
            {
                var newDistance = Vector2.Distance(vector, otherVectors[i]);
                if (newDistance > maxDistance)
                {
                    maxDistance = newDistance;
                    maxVector = otherVectors[i];
                }
            }
            return maxVector;
        }

        /// <summary>
        /// Get the farthest Vector2 from a list to the original Vector2.
        /// </summary>
        public static Vector2 GetFarthestVector2From(this Vector2 vector, List<Vector2> otherVectors)
        {
            if (otherVectors.Count == 0) throw new Exception("The list of other vectors is empty");
            var maxDistance = Vector2.Distance(vector, otherVectors[0]);
            var maxVector = otherVectors[0];
            for (var i = otherVectors.Count - 1; i > 0; i--)
            {
                var newDistance = Vector2.Distance(vector, otherVectors[i]);
                if (newDistance > maxDistance)
                {
                    maxDistance = newDistance;
                    maxVector = otherVectors[i];
                }
            }
            return maxVector;
        }

        /// <summary>
        /// Get the closest Vector3 from an array to the original Vector3.
        /// </summary>
        public static Vector3 GetClosestVector3From(this Vector3 vector, Vector3[] otherVectors)
        {
            if (otherVectors.Length == 0) throw new Exception("The list of other vectors is empty");
            var minDistance = Vector3.Distance(vector, otherVectors[0]);
            var minVector = otherVectors[0];
            for (var i = otherVectors.Length - 1; i > 0; i--)
            {
                var newDistance = Vector3.Distance(vector, otherVectors[i]);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    minVector = otherVectors[i];
                }
            }
            return minVector;
        }

        /// <summary>
        /// Get the closest Vector3 from a list to the original Vector3.
        /// </summary>
        public static Vector3 GetClosestVector3From(this Vector3 vector, List<Vector3> otherVectors)
        {
            if (otherVectors.Count == 0) throw new Exception("The list of other vectors is empty");
            var minDistance = Vector3.Distance(vector, otherVectors[0]);
            var minVector = otherVectors[0];
            for (var i = otherVectors.Count - 1; i > 0; i--)
            {
                var newDistance = Vector3.Distance(vector, otherVectors[i]);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    minVector = otherVectors[i];
                }
            }
            return minVector;
        }

        /// <summary>
        /// Get the farthest Vector3 from an array to the original Vector3.
        /// </summary>
        public static Vector3 GetFarthestVector3From(this Vector3 vector, Vector3[] otherVectors)
        {
            if (otherVectors.Length == 0) throw new Exception("The array of other vectors is empty");
            var maxDistance = Vector3.Distance(vector, otherVectors[0]);
            var maxVector = otherVectors[0];
            for (var i = otherVectors.Length - 1; i > 0; i--)
            {
                var newDistance = Vector3.Distance(vector, otherVectors[i]);
                if (newDistance > maxDistance)
                {
                    maxDistance = newDistance;
                    maxVector = otherVectors[i];
                }
            }
            return maxVector;
        }

        /// <summary>
        /// Get the farthest Vector3 from an list to the original Vector3.
        /// </summary>
        public static Vector3 GetFarthestVector3From(this Vector3 vector, List<Vector3> otherVectors)
        {
            if (otherVectors.Count == 0) throw new Exception("The array of other vectors is empty");
            var maxDistance = Vector3.Distance(vector, otherVectors[0]);
            var maxVector = otherVectors[0];
            for (var i = otherVectors.Count - 1; i > 0; i--)
            {
                var newDistance = Vector3.Distance(vector, otherVectors[i]);
                if (newDistance > maxDistance)
                {
                    maxDistance = newDistance;
                    maxVector = otherVectors[i];
                }
            }
            return maxVector;
        }
        #endregion

        #region Others
        /// <summary>
        /// Get distance between two vector3s.
        /// </summary>
        public static float Distance(this Vector3 from, Vector3 to) => (from - to).magnitude;
        #endregion
    }
}