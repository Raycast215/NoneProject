using UnityEngine;

namespace NoneProject.GameSystem.Composition
{
    public class AssetRender
    {
        private readonly string _assetKey;

        public AssetRender(string assetKey)
        {
            _assetKey = assetKey;

            LoadAsset();
        }

        private void LoadAsset()
        {
            Debug.Log($"Load Asset key : {_assetKey}");
        }
    }
}