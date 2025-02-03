using Template.Pool;
using UnityEngine;

namespace NoneProject.Interface
{
    // Scripted by Raycast
    // 2025.01.17
    // 오브젝트 풀을 사용하는 매니저 클래스에서 공통으로 사용할 함수를 묶어놓은 인터페이스입니다.
    public interface IObjectPoolManager<in T, TU> where T : PoolBase<TU> where TU : Component
    {
        public void Release(T poolObject);
    }
}