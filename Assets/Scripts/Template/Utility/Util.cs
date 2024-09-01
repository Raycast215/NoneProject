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
                ret = max;

            if (index > max)
                ret = min;

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
        
        /// GameObject를 생성하는 함수.
        public static GameObject CreateObject(string gameObjectName, Transform parent, Vector3 position = new Vector3())
        {
            var go = new GameObject(gameObjectName);
            
            go.transform.SetParent(parent);
            go.transform.position = position;
            return go;
        }

        /// GameObject를 생성하고 T 컴포넌트를 추가하는 함수.
        public static T CreateObject<T>(string gameObjectName, Transform parent, T addComponent, Vector3 position = new Vector3()) where T : Component
        {
            var go = new GameObject(gameObjectName);
            var ret = go.AddComponent<T>();
            
            go.transform.SetParent(parent);
            go.transform.position = position;
            return ret;
        }

        /// 조건에 따라 True = 1을, False = -1을 반환하는 함수.
        public static int GetToggleOne(bool isCondition)
        {
            return isCondition 
                ? 1 
                : -1;
        }
        
        /// 랜덤으로 위치를 반환하는 함수.
        public static Vector2 GetRandomDirVec(Vector2 curPos, float rangeX, float rangeY)
        {
            var seed = Random.Range(0, int.MaxValue);
            
            Random.InitState(seed);

            var toX = Random.Range(-rangeX, rangeX) + curPos.x;
            var toY = Random.Range(-rangeY, rangeY) + curPos.y;
            var toPos = new Vector2(toX, toY);
            
            return toPos;
        }
    }
}