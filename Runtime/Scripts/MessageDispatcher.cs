using System;
using System.Collections;
using System.Collections.Generic;

namespace Subsets.Message {
  public class MessageDispatcher {
    public delegate void Handler();
    public delegate void Handler<T>(T message);

    public MessageDispatcher() {
      handlerTable = new Dictionary<string, IList>();
    }

    public void AddListener(string name, Handler handler) {
      if (!handlerTable.ContainsKey(name))
        handlerTable[name] = new List<Handler>();
      handlerTable[name].Add(handler);
    }

    public void AddListener<T>(string name, Handler<T> handler) {
      if (!handlerTable.ContainsKey(name))
        handlerTable[name] = new List<Handler<T>>();
      handlerTable[name].Add(handler);
    }

    public void RemoveListener(string name, Handler handler) {
      if (handlerTable.ContainsKey(name)) {
        handlerTable[name].Remove(handler);
      }
    }

    public void RemoveListener<T>(string name, Handler<T> handler) {
      if (handlerTable.ContainsKey(name)) {
        handlerTable[name].Remove(handler);
      }
    }

    public void RemoveListener(string name) {
      if (handlerTable.ContainsKey(name)) {
        handlerTable[name].Clear();
      }
    }

    public void DispatchMessage<T>(string name, T message) {
      IList list;
      if (!handlerTable.TryGetValue(name, out list))
        return;
      List<Handler<T>> handlers = list as List<Handler<T>>;
      if (null == handlers)
        return;
      for (int i = 0; i < handlers.Count; ++i) {
        handlers[i].Invoke(message);
      }
    }

    public void DispatchMessage(string name) {
      IList list;
      if (!handlerTable.TryGetValue(name, out list))
        return;
      List<Handler> handlers = list as List<Handler>;
      if (null == handlers)
        return;
      for (int i = 0; i < handlers.Count; ++i) {
        handlers[i].Invoke();
      }
    }
    private Dictionary<string, IList> handlerTable;
  }
}
