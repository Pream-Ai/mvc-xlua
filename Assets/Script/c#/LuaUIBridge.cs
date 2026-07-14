using XLua;
using UnityEngine;
using System;

[LuaCallCSharp]
public class LuaUIBridge : MonoBehaviour
{
    [Serializable]
    public struct Injection
    {
        public string name;
        public GameObject value;
    }
    public Injection[] injections;

    private LuaTable _luaTable; 
    private Action<LuaTable> _luaStart;
    private Action<LuaTable> _luaOnDestroy;

    public void Init(LuaTable luaTable)
    {
        _luaTable = luaTable;
        foreach (var injection in injections)
        {
            _luaTable.Set(injection.name, injection.value);
        }
        _luaStart = _luaTable.Get<Action<LuaTable>>("Start");
        _luaOnDestroy = _luaTable.Get<Action<LuaTable>>("OnDestroy");
    }

    private void Start() => _luaStart?.Invoke(_luaTable);

    private void OnDestroy()
    {
        _luaOnDestroy?.Invoke(_luaTable);
        _luaStart = null;
        _luaOnDestroy = null;
        _luaTable?.Dispose();
        _luaTable = null;
    }
}