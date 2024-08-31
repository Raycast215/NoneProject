
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


namespace Template.Utility
{
    public enum DataType
    {
        Csv,
        Json
    }
    
    public class CsvToJson : EditorWindow
    {
        private const string TileName = "Convert CSV to Json";
        private const int MaxWidth = 64;
        private const int FixSpaceValue = 10;
        private const string Meta = ".meta";
        
        private string csvPath;
        private string jsonPath;

        private GUIStyle _tileStyle;

        private void OnEnable()
        {
            csvPath = PlayerPrefs.GetString($"{DataType.Csv}_Path");
            jsonPath = PlayerPrefs.GetString($"{DataType.Json}_Path");
        }

        [MenuItem("Utility/Convert CSV to JSON")]
        private static void ConvertCsvToJson()
        {
            var window = (CsvToJson)GetWindow(typeof(CsvToJson));
            window.Show();
        }
        
        private void OnGUI()
        {
            UpdateTileGUI();
            UpdateGUI(DataType.Csv, ref csvPath);
            UpdateGUI(DataType.Json, ref jsonPath);
            UpdateConvertGUI();
            UpdateResetPathGUI();
        }

        private void UpdateTileGUI()
        {
            GUILayout.Space(FixSpaceValue);
            
            _tileStyle ??= new GUIStyle(GUI.skin.label);
            _tileStyle.fontSize = 20;
            _tileStyle.fontStyle = FontStyle.Bold;
            
            GUILayout.Label(TileName, _tileStyle);
            GUILayout.Space(FixSpaceValue);
        }
        
        private void UpdateGUI(DataType toType, ref string toPath)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"{toType} Path : {toPath}", EditorStyles.boldLabel);
            
            if (GUILayout.Button(".../", GUILayout.Width(MaxWidth)))
            {
                var tempPath = "";
                if (string.IsNullOrEmpty(toPath) is false)
                {
                    tempPath = toPath;
                }
                
                toPath = EditorUtility.OpenFolderPanel($"Open {toType} Folder", toPath, "");
                toPath = string.IsNullOrEmpty(toPath) ? tempPath : toPath;
                
                PlayerPrefs.SetString($"{toType}_Path", toPath);
                PlayerPrefs.Save();
            }
            
            if (GUILayout.Button("Open", GUILayout.Width(MaxWidth)))
            {
                EditorUtility.RevealInFinder($"{toPath}/");
            }
            
            GUILayout.EndHorizontal();
        }

        private void UpdateConvertGUI()
        {
            GUILayout.Space(FixSpaceValue);
            
            if (GUILayout.Button("Convert"))
            {
                //Convert(csvPath);
                
                List<string> csvLines = ConvertExcelToCSV($"{csvPath}/Data_Stage.xlsx");
                
                Debug.Log(csvLines.Count);
                
                string json = ConvertCSVToJson(csvLines);
                
                File.WriteAllText($"{jsonPath}/test.json", json);
            }
        }
        
        private void UpdateResetPathGUI()
        {
            if (GUILayout.Button("Reset Path"))
            {
                PlayerPrefs.DeleteKey($"{DataType.Csv}_Path");
                PlayerPrefs.DeleteKey($"{DataType.Json}_Path");

                csvPath = "";
                jsonPath = "";
                
                Debug.Log($"[{TileName}] Reset Path Finished...");
            }
        }

        List<string> ConvertExcelToCSV(string filePath)
        {
            List<string> csvLines = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    csvLines.Add(line);
                }
            }

            return csvLines;
        }

        string ConvertCSVToJson(List<string> csvLines)
        {
            List<Dictionary<string, object>> jsonData = new List<Dictionary<string, object>>();

            // 헤더 처리
            string[] headers = csvLines[0].Split(',');
            for (int i = 1; i < csvLines.Count; i++)
            {
                string[] values = csvLines[i].Split(',');

                // JSON 데이터 생성
                Dictionary<string, object> row = new Dictionary<string, object>();
                for (int j = 0; j < headers.Length; j++)
                {
                    if (values[j].StartsWith("[") && values[j].EndsWith("]"))
                    {
                        // 배열 형태인 경우
                        string[] arrayValues = values[j].Substring(1, values[j].Length - 2).Split(';');
                        int[] intArray = new int[arrayValues.Length];
                        for (int k = 0; k < arrayValues.Length; k++)
                        {
                            intArray[k] = int.Parse(arrayValues[k]);
                        }
                        row[headers[j]] = intArray;
                    }
                    else
                    {
                        // 배열이 아닌 경우
                        row[headers[j]] = values[j];
                    }
                }

                // JSON 데이터 리스트에 추가
                jsonData.Add(row);
            }

            // JSON으로 직렬화
            return JsonUtility.ToJson(jsonData, true);
        }
        
        
        
        private void Convert(string toPath)
        {
            const string separator = ",";
            const string arraySeparator = ".";
            
            var dir = new DirectoryInfo(toPath);
            foreach (var file in dir.GetFiles().Where(x => x.Name.EndsWith(Meta) is false))
            {
                if (File.Exists(file.FullName))
                { 
                    var data = new List<Dictionary<string, object>>();
                    var lines = File.ReadAllLines(file.FullName);
                    var headers = lines[0].Split(separator);
                    var exceptLines = new int[] { 1, 2 };

                    for (var i = 1; i < lines.Length; i++)
                    {
                        // 예외 라인 스킵.
                        if (exceptLines.Contains(i))
                            continue;
                        
                        var fields = lines[i].Split(separator);
                        var record = new Dictionary<string, object>();
                        for (var j = 0; j < headers.Length && j < fields.Length; j++)
                        {
                            var fieldName = headers[j];
                            var fieldValue = fields[j];
                            // 쉼표로 구분된 값을 하나의 필드로 처리.
                            if (fieldValue.Contains(arraySeparator))
                            {
                                fieldValue = "[" + fieldValue + "]";
                                fieldValue = fieldValue.Replace(arraySeparator, separator);
                            }
                            fieldValue = fieldValue.Replace("\"", "");
                            
                            Debug.Log($"\"{fieldName}\":{fieldValue}");
                            
                            record.Add(fieldName, fieldValue);
                        }
                        data.Add(record);
                    }

                    // Json으로 직렬화하여 파일에 저장.
                    var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                    var fileName = $"{jsonPath}/{file.Name}";
                    var replaceName = fileName.Replace($"{DataType.Json.ToString().ToLower()}", ".json");

                    File.WriteAllText(replaceName, jsonData);
                    Debug.Log($"[{TileName}] Convert Succeed...");
                }
                else
                {
                    Debug.Log($"[{TileName}] Convert Failed...");
                }
            }
        }
    }
}