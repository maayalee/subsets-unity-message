using System;
using System.Collections.Generic;

namespace LibUnity.Message {
  public class MessageDispatcher {
    public delegate void Handler();
    public delegate void Handler<T>(T message);

    public MessageDispatcher() {
      handler_table = new Dictionary<string, List<Delegate>>();
    }

    public void AddListener(string message_name, Handler handler) {
      AddHandler(message_name, handler);
    } 

    public void AddListener<T>(string message_name, Handler<T> handler) {
      AddHandler(message_name, handler);
    }

    private void AddHandler(string message_name, Delegate handler) {
      if (!handler_table.ContainsKey(message_name))
        handler_table[message_name] = new List<Delegate>();
      handler_table[message_name].Add(handler);
    }

    public void RemoveListener(string message_name, Handler handler) {
      RemoveHandler(message_name, handler);
    }

    public void RemoveListener<T>(string message_name, Handler<T> handler) {
      RemoveHandler(message_name, handler);
    } 

    public void RemoveListener(string message_name, Delegate handler) {
      RemoveHandler(message_name, handler);
    }

    private void RemoveHandler(string message_name, Delegate handler) {
      if (handler_table.ContainsKey(message_name)) {
        handler_table[message_name].Remove(handler);
      }
    }

    public void RemoveListener(string message_name) {
      if (handler_table.ContainsKey(message_name)) {
        handler_table[message_name].Clear();
      }
    }

    public void DispatchMessage(MessageBase message) {
      foreach (Delegate handler in GetHandlers(message.GetName())) {
        handler.DynamicInvoke(message);
      }
    }

    public void DispatchMessage(string name, object message) {
      foreach (Delegate handler in GetHandlers(name)) {
        handler.DynamicInvoke(message);
      }
    }

    public void DispatchMessage(string name) {
      foreach (Delegate handler in GetHandlers(name)) {
        handler.DynamicInvoke();
      }
    }

    private List<Delegate> GetHandlers(string name) {
      if (handler_table.ContainsKey(name)) {
        return handler_table[name];
      }
      return new List<Delegate>();
    }

    private Dictionary<string, List<Delegate>> handler_table;
  }
}
