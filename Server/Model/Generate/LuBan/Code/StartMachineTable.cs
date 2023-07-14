//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using System.Text.Json;



namespace cfg
{


public sealed partial class StartMachineTable
{
    private readonly Dictionary<int, StartMachineConfig> _dataMap;
    private readonly List<StartMachineConfig> _dataList;
    
    public StartMachineTable(JsonElement _json)
    {
        _dataMap = new Dictionary<int, StartMachineConfig>();
        _dataList = new List<StartMachineConfig>();
        
        foreach(JsonElement _row in _json.EnumerateArray())
        {
            var _v = StartMachineConfig.DeserializeStartMachineConfig(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, StartMachineConfig> DataMap => _dataMap;
    public List<StartMachineConfig> DataList => _dataList;

    public StartMachineConfig GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public StartMachineConfig Get(int key) => _dataMap[key];
    public StartMachineConfig this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    

    partial void PostInit();
    partial void PostResolve();
}

}