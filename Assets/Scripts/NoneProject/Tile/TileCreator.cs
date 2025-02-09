using Template.Manager;
using UnityEngine;

namespace NoneProject.Tile
{
    // Scripted by Raycast
    // 2025.01.25
    // Tile을 생성하는 클래스입니다.
    public class TileCreator
    {
        private const int WidthCount = 2;
        private const string Gird = "Grid";

        public bool IsInitialized { get; private set; }
        
        private Tile _tile;
        private Grid _grid;
        private readonly string _tileID;
        private readonly float _offset;

        public TileCreator(string tileID = "")
        {
            _offset = GameManager.Instance.Const.TileOffset;
            _tileID = string.IsNullOrEmpty(tileID) 
                ? GameManager.Instance.Const.DefaultTileID 
                : tileID;
            
            LoadGrid();
        }

        private async void LoadGrid()
        {
            await AddressableManager.Instance.LoadAsset<GameObject>(Gird, OnComplete);
            return;

            void OnComplete(GameObject asset)
            {
                _grid = Object.Instantiate(asset).GetComponent<Grid>();
                LoadTile();
            }
        }
        
        private async void LoadTile()
        {
            await AddressableManager.Instance.LoadAsset<GameObject>(_tileID, OnComplete);
            return;

            void OnComplete(GameObject asset)
            {
                for (var i = 0; i < WidthCount; i++)
                {
                    for (var j = 0; j < WidthCount; j++)
                    {
                        // Tile 생성.
                        var tile = Object.Instantiate(asset, _grid.transform).GetComponent<Tile>();
                        // 중앙 기준으로 위치 계산.
                        var centerOffset = (WidthCount - 1) / 2.0f;
                        // 실제 이동할 위치.
                        var pos = new Vector2(i - centerOffset, j - centerOffset) * _offset;

                        tile.SetPosition(pos);
                    }
                }

                IsInitialized = true;
            }
        }
    }
}