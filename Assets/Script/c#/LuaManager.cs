using XLua;
using System.IO;
using UnityEngine;

public class LuaManager : MonoBehaviour
{
    private LuaEnv _luaEnv;
 
    void Awake()
    {
        _luaEnv=new LuaEnv();
        _luaEnv.AddLoader(CustomLoader);
        _luaEnv.DoString("require('Main')");
    }

    private byte[] CustomLoader(ref string filepath)
    {
        string path = Path.Combine(Application.dataPath, "Script", "Lua", filepath + ".lua");

        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        return null;
    }

    private void OnDestroy()
    {
        _luaEnv?.Dispose();
    }
}
