using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    static GeneralManager _instance;
    static GeneralManager Instance {  get { Init(); return _instance; } }

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    
    void Start()
    {
        Init();
    }

    // Update is called once per frame
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

            _instance._sound.Init();
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Scene.Clear();
        Sound.Clear();
        UI.Clear();
    }
}
