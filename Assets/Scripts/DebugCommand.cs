using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandBase
{
    private string _commandId;
    private string _commandDescription;
    private string _commandFormat;

    public string CommandId => _commandId;
    public string CommandDescription => _commandDescription;
    public string CommandFormat => _commandFormat;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">command identify</param>
    /// <param name="description">command description</param>
    /// <param name="format">command usage</param>
    public DebugCommandBase(string id, string description, string format)
    {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action _action;

    public DebugCommand(string id, string description, string format, Action action): base(id, description, format)
    {
        _action = action;
    }

    public void Invoke()
    {
        _action.Invoke();
    }
}

public class DebugCommand<T> : DebugCommandBase
{
    private Action<T> _action;

    public DebugCommand(string id, string description, string format, Action<T> action) : base(id, description, format)
    {
        _action = action;
    }

    public void Invoke(T t)
    {
        _action.Invoke(t);
    }
}