using System;
using System.Collections;
using System.Collections.Generic;

namespace LibUnity.Message {
  public class MessageDispatcher {
    public delegate void Handler();
    public delegate void Handler<T>(T message);

    public MessageDispatcher() {
      handler_table = new Dictionary<string, IList>();
    }

    public void AddListener(string name, Handler handler) {
      if (!handler_table.ContainsKey(name))
        handler_table[name] = new List<Handler>();
      handler_table[name].Add(handler);
    }

    public void AddListener<T>(string name, Handler<T> handler) {
      if (!handler_table.ContainsKey(name))
        handler_table[name] = new List<Handler<T>>();
      handler_table[name].Add(handler);
    }

    public void RemoveListener(string name, Handler handler) {
      if (handler_table.ContainsKey(name)) {
        handler_table[name].Remove(handler);
      }
    }

    public void RemoveListener<T>(string name, Handler<T> handler) {
      if (handler_table.ContainsKey(name)) {
        handler_table[name].Remove(handler);
      }
    }

    public void RemoveListener(string name) {
      if (handler_table.ContainsKey(name)) {
        handler_table[name].Clear();
      }
    }

    public void DispatchMessage<T>(string name, T message) {
      IList list;
      if (!handler_table.TryGetValue(name, out list))
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
      if (!handler_table.TryGetValue(name, out list))
        return;
      List<Handler> handlers = list as List<Handler>;
      if (null == handlers)
        return;
      for (int i = 0; i < handlers.Count; ++i) {
        handlers[i].Invoke();
      }
    }
    private Dictionary<string, IList> handler_table;
  }
}
