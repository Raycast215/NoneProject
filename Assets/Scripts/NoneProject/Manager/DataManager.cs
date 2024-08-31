
using System.Collections.Generic;
using Newtonsoft.Json;
using NoneProject.Data;
using Template.Manager;
using UnityEngine;


namespace NoneProject.Manager
{
    public class DataManager : SingletonBase<DataManager>
    {
        public Stage StageData { get; private set; }

        [SerializeField] private Stage stage;

        [SerializeField] private Stage[] stageList;

        public void Load()
        {
            var stagePath = "Data_Stage";
            AddressableManager.Instance.Load<TextAsset>(stagePath, stage =>
            {
                stageList = JsonConvert.DeserializeObject<Stage[]>(stage.text);
            });
            
            var jsonData = JsonConvert.SerializeObject("data", Formatting.Indented);
            
            Debug.Log(jsonData);
        }
        
        protected override void Initialized()
        {
            IsInitialized = true;
        }
        
        // record = $"\"{fieldName}\":{fieldValue}";
    }
}