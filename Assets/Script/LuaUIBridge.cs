using UnityEngine;
using XLua;
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
    public LuaTable _luaTable;

    private Action<LuaTable> _luaStart;
    private Action<LuaTable> _luaDestroy;

    public void init(LuaTable luaTable)
    {
        _luaTable = luaTable;
        foreach (var injection in injections)
        {
            _luaTable.Set(injection.name,injection.value);
        }
        _luaStart = _luaTable.Get<Action<LuaTable>>("Start");
        _luaDestroy = _luaTable.Get<Action<LuaTable>>("OnDestroy");
    }
    void Start()
    {
        _luaStart?.Invoke(_luaTable);
    }
    void OnDestroy()
    {
        _luaDestroy?.Invoke(_luaTable);
        _luaStart = null;
        _luaDestroy = null;
        _luaTable?.Dispose();
        _luaTable = null;
    }
}
