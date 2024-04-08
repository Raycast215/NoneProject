
using NoneProject.Common;
using NoneProject.Manager;
using Template.Manager;
using UnityEngine;


namespace NoneProject
{
    public class GameManager : SingletonBase<GameManager>
    {
        protected override void Initialized()
        {
            IsInitialized = true;
        }
    }
}