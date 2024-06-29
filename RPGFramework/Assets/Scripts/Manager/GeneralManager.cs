using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    static GeneralManager _instance;
    static GeneralManager Instance {  get { Init(); return _instance; } }

    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    
    void Start()
    {
        Init();
    }
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("GeneralManager");
            if (go == null)
            {
                go = new GameObject { name = "GeneralManager" };
                go.AddComponent<GeneralManager>();
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<GeneralManager>();

            _instance._data.Init();
            _instance._pool.Init();
            _instance._sound.Init();
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Pool.Clear();
        Scene.Clear();
        Sound.Clear();
        UI.Clear();
    }
}
