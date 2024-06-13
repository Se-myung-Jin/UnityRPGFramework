using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    static GeneralManager _instance;
    static GeneralManager Instance {  get { Init(); return _instance; } }

    InputManager _input = new InputManager();
    ResourceManager _resources = new ResourceManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resources; } }
    
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
            GameObject go = GameObject.Find("@GeneralManager");
            if (go == null)
            {
                go = new GameObject { name = "@GeneralManager" };
                go.AddComponent<GeneralManager>();
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<GeneralManager>();
        }
    }
}
