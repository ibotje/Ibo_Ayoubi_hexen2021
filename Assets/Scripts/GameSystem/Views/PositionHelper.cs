using BoardSystem;
using System;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultPositionHelper", menuName = "GameSystem/PositionHelper")]
    public class PositionHelper : ScriptableObject
    {
        [SerializeField]
        private float _tileSize = 1f;
        public float TileSize => _tileSize;

        public Position ToBoardPosition(Vector3 worldPosition)
        {
            var x = worldPosition.x;
            var y = worldPosition.z;
            var boardx = (Mathf.Sqrt(3f) / 3f * x - 0.33333333343f * y) / 1;
            var boardy =  0.6666666667f * y / 1;

            var xx= boardx;
            var yy = -boardx - boardy;
            var zz= boardy;

            float num1 = Mathf.Round(xx);
            float num2 = Mathf.Round(yy);
            float num3 = Mathf.Round(zz);

            float num4 = Mathf.Abs(num1 - xx);
            float num5 = Mathf.Abs(num2 - yy);
            float num6 = Mathf.Abs(num3 - zz);

            if (num4 > num5 && num4 > num6)
                num1 = -num2 - num3;
            else if (num5 <= num6)
                num3 = -num1 - num2;

            return new Position((int)num3, (int)num1);
        }

        internal Vector3 ToLocalPosition(Position position)
        {
            var localPosition = LocalPosCalc(position);

            return localPosition;
        }

        public Vector3 ToWorldPosition(Transform BoardTransform, Position boardPosition)
        {
            var localPosistion = LocalPosCalc(boardPosition);

            var worldPosition = BoardTransform.localToWorldMatrix * localPosistion;

            return worldPosition;
        }

        private Vector3 LocalPosCalc(Position Position)
        {
            var localPosX = _tileSize * (Mathf.Sqrt(3f) * Position.Z + Mathf.Sqrt(3f) / 2f * Position.X);
            var LocalPosZ = _tileSize * (1.5f * Position.X);

            return new Vector3(localPosX, 0, LocalPosZ);
        }
    }
}
