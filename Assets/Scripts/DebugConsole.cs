using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    bool showDebugConsole;
    bool showHelp;

    bool keyDown = false;

    string inputText;

    public static DebugCommand KILL_ALL;
    public static DebugCommand HELP;

    public static DebugCommand<int> ADD_CUBE;

    public List<object> commandList;

    Vector2 scrollVec;

    private void Awake()
    {
        KILL_ALL = new DebugCommand("kill_all", "Remove all maters form the scene", "kill_all", () =>
        {
            GameManager.Instance.RemoveAllMaters();
        });

        ADD_CUBE = new DebugCommand<int>("add_cube", "Add some cubes into the scene", "add_cube 10", (int num) =>
        {

        });

        HELP = new DebugCommand("help", "Show a list of commands", "help", () =>
        {
            showHelp = true;
        });

        commandList = new List<object>
        {
            KILL_ALL,
            ADD_CUBE,
            HELP
        };
    }

    public void ToggleDebug()
    {
        showDebugConsole = !showDebugConsole;

        if (!showDebugConsole && showHelp)
        {
            showHelp = false;
        }
    }

    private void OnGUI()
    {
        if (!showDebugConsole)
        {
            return;
        }

        float y = 0f;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100f), "");

            Rect viewPoint = new Rect(0, y, Screen.width - 30, 20 * commandList.Count);

            scrollVec = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90f), scrollVec, viewPoint);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                var label = $"{command.CommandFormat} - {command.CommandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewPoint.width - 100, 20);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += 100.0f;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);

        inputText = GUI.TextField(new Rect(10.0f, y + 5f, Screen.width - 20f, 20f), inputText);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!keyDown && Input.GetKeyDown(KeyCode.BackQuote))
        {
            keyDown = true;

            ToggleDebug();
        }

        if (keyDown && Input.GetKeyUp(KeyCode.BackQuote))
        {
            keyDown = false;
        }

        if (showDebugConsole && Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(inputText))
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        for(int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase command = commandList[i] as DebugCommandBase;

            if (inputText.Contains(command.CommandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();

                    break;
                }
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    var paras = inputText.Split(' ');

                    if (paras.Length < 2)
                    {
                        continue;
                    }

                    int num = 0;

                    if(!int.TryParse(paras[1], out num)){
                        continue;
                    }

                    (commandList[i] as DebugCommand<int>).Invoke(num);
                }
            }
        }

        inputText = "";
    }
}
