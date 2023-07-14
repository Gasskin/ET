//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Text.Json;

namespace cfg
{ 

   
public sealed partial class Tables
{
    public AITable AITable {get; }
    public StartMachineTable StartMachineTable {get; }
    public StartProcessTable StartProcessTable {get; }
    public StartZoneTable StartZoneTable {get; }
    public StartSceneTable StartSceneTable {get; }

    public Tables(System.Func<string, JsonElement> loader)
    {
        var tables = new System.Collections.Generic.Dictionary<string, object>();
        AITable = new AITable(loader("aitable")); 
        tables.Add("AITable", AITable);
        StartMachineTable = new StartMachineTable(loader("startmachinetable")); 
        tables.Add("StartMachineTable", StartMachineTable);
        StartProcessTable = new StartProcessTable(loader("startprocesstable")); 
        tables.Add("StartProcessTable", StartProcessTable);
        StartZoneTable = new StartZoneTable(loader("startzonetable")); 
        tables.Add("StartZoneTable", StartZoneTable);
        StartSceneTable = new StartSceneTable(loader("startscenetable")); 
        tables.Add("StartSceneTable", StartSceneTable);
        PostInit();

        AITable.Resolve(tables); 
        StartMachineTable.Resolve(tables); 
        StartProcessTable.Resolve(tables); 
        StartZoneTable.Resolve(tables); 
        StartSceneTable.Resolve(tables); 
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        AITable.TranslateText(translator); 
        StartMachineTable.TranslateText(translator); 
        StartProcessTable.TranslateText(translator); 
        StartZoneTable.TranslateText(translator); 
        StartSceneTable.TranslateText(translator); 
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
