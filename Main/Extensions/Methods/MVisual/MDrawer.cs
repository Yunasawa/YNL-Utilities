#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace YNL.Utilities.Extensions
{
    public static class MDrawer
    {
        #region ▶ BoxCast Visualizer Utilities
        public static void DrawWireBox(Vector3 origin, Vector3 end, float thickness = 1)
            => MBox.DrawBox(new MBox(origin, end), thickness);
        public static void DrawWireBox(Vector3 center, float xLength, float ylength, float zLength, float thickness = 1)
            => MBox.DrawBox(new MBox(center, xLength, ylength, zLength), thickness);

        public static void DrawWireBoxGizmos(Vector3 center, float xLength, float ylength, float zLength, float thickness = 1)
            => MBox.DrawBoxGizmos(new MBox(center, xLength, ylength, zLength), thickness);
        #endregion

        public static Vector3 ReverseX(this Vector3 vector)
            => new Vector3(-vector.x, vector.y, vector.z);
        public static Vector3 ReverseY(this Vector3 vector)
            => new Vector3(vector.x, -vector.y, vector.z);
        public static Vector3 ReverseZ(this Vector3 vector)
            => new Vector3(vector.x, vector.y, -vector.z);
    }

    public struct MBox
    {
        public Vector3 FrontTopLeft { get { return FrontTopRight.SetX(BackBottomLeft.x); } }
        public Vector3 FrontTopRight { get; private set; }
        public Vector3 FrontBottomLeft { get { return BackBottomLeft.SetZ(FrontTopRight.z); } }
        public Vector3 FrontBottomRight { get { return FrontTopRight.SetY(BackBottomLeft.y); } }
        public Vector3 BackTopLeft { get { return BackBottomLeft.SetY(FrontTopRight.y); } }
        public Vector3 BackTopRight { get { return FrontTopRight.SetZ(BackBottomLeft.z); } }
        public Vector3 BackBottomLeft { get; private set; }
        public Vector3 BackBottomRight { get { return BackBottomLeft.SetX(FrontTopRight.x); } }

        public MBox(Vector3 backBottomLeft, Vector3 frontTopRight)
        {
            FrontTopRight = frontTopRight;
            BackBottomLeft = backBottomLeft;
        }

        public MBox(Vector3 center, float xLength, float yLength, float zLength)
        {
            FrontTopRight = new Vector3(center.x + xLength, center.y + yLength, center.z + zLength);
            BackBottomLeft = new Vector3(center.x - xLength, center.y - yLength, center.z - zLength);
        }

        public static void DrawBox(MBox box, float thickness)
        {
            Handles.DrawLine(box.FrontTopLeft, box.FrontTopRight, thickness);
            Handles.DrawLine(box.FrontTopRight, box.FrontBottomRight, thickness);
            Handles.DrawLine(box.FrontBottomRight, box.FrontBottomLeft, thickness);
            Handles.DrawLine(box.FrontBottomLeft, box.FrontTopLeft, thickness);

            Handles.DrawLine(box.BackTopLeft, box.BackTopRight, thickness);
            Handles.DrawLine(box.BackTopRight, box.BackBottomRight, thickness);
            Handles.DrawLine(box.BackBottomRight, box.BackBottomLeft, thickness);
            Handles.DrawLine(box.BackBottomLeft, box.BackTopLeft, thickness);

            Handles.DrawLine(box.FrontTopLeft, box.BackTopLeft, thickness);
            Handles.DrawLine(box.FrontTopRight, box.BackTopRight, thickness);
            Handles.DrawLine(box.FrontBottomRight, box.BackBottomRight, thickness);
            Handles.DrawLine(box.FrontBottomLeft, box.BackBottomLeft, thickness);
        }

        public static void DrawBoxGizmos(MBox box, float thickness)
        {
            Gizmos.DrawLine(box.FrontTopLeft, box.FrontTopRight);
            Gizmos.DrawLine(box.FrontTopRight, box.FrontBottomRight);
            Gizmos.DrawLine(box.FrontBottomRight, box.FrontBottomLeft);
            Gizmos.DrawLine(box.FrontBottomLeft, box.FrontTopLeft);

            Gizmos.DrawLine(box.BackTopLeft, box.BackTopRight);
            Gizmos.DrawLine(box.BackTopRight, box.BackBottomRight);
            Gizmos.DrawLine(box.BackBottomRight, box.BackBottomLeft);
            Gizmos.DrawLine(box.BackBottomLeft, box.BackTopLeft);

            Gizmos.DrawLine(box.FrontTopLeft, box.BackTopLeft);
            Gizmos.DrawLine(box.FrontTopRight, box.BackTopRight);
            Gizmos.DrawLine(box.FrontBottomRight, box.BackBottomRight);
            Gizmos.DrawLine(box.FrontBottomLeft, box.BackBottomLeft);
        }
    }
}
#endif