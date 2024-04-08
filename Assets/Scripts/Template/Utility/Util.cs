

using System.Linq;
using NoneProject.Common;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace Template.Utility
{
    public static class Util
    {
        public static int ClampIndex(int index, int min, int max)
        {
            var ret = index;

            if (index < min)
            {
                ret = max;
            }

            if (index > max)
            {
                ret = min;
            }

            return ret;
        }

        public static float GetScreenWidth()
        {
            var aspect = (float)Screen.width / Screen.height;
            var worldHeight = Camera.main!.orthographicSize * 2.0f;
            var worldWidth = worldHeight * aspect;

            return worldWidth;
        }

        public static int GetPosYDepth(float toValue)
        {
            var toList = Define.PosYOffset.ToList();
            var index = toList.IndexOf(toValue);

            return index + 4;
        }
    }
}