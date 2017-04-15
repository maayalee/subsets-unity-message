using System;
using System.Collections.Generic;

namespace libunity.message {
  public class message_dispatcher {
    public delegate void handler();
    public delegate void handler<T>(T message);

    public message_dispatcher() {
      handler_table = new Dictionary<string, List<Delegate>>();
    }

    public void add_listener(string message_name, handler handler) {
      add_handler(message_name, handler);
    } 

    public void add_listener<T>(string message_name, handler<T> handler) {
      add_handler(message_name, handler);
    }

    public void add_listener(string message_name, Delegate handler) {
      add_handler(message_name, handler);
    }

    private void add_handler(string message_name, Delegate handler) {
      if (!handler_table.ContainsKey(message_name))
        handler_table[message_name] = new List<Delegate>();
      handler_table[message_name].Add(handler);
    }

    public void remove_listener(string message_name, handler handler) {
      remove_handler(message_name, handler);
    }

    public void remove_listener<T>(string message_name, handler<T> handler) {
      remove_handler(message_name, handler);
    } 

    public void remove_listener(string message_name, Delegate handler) {
      remove_handler(message_name, handler);
    }

    private void remove_handler(string message_name, Delegate handler) {
      if (handler_table.ContainsKey(message_name)) {
        handler_table[message_name].Remove(handler);
      }
    }

    public void remove_listener(string message_name) {
      if (handler_table.ContainsKey(message_name)) {
        handler_table[message_name].Clear();
      }
    }

    public void dispatch_message(message_base message) {
      foreach (Delegate handler in get_handlers(message.get_name())) {
        handler.DynamicInvoke(message);
      }
    }

    public void dispatch_message(string name, object message) {
      foreach (Delegate handler in get_handlers(name)) {
        handler.DynamicInvoke(message);
      }
    }

    public void dispatch_message(string name) {
      foreach (Delegate handler in get_handlers(name)) {
        handler.DynamicInvoke();
      }
    }

    private List<Delegate> get_handlers(string name) {
      if (handler_table.ContainsKey(name)) {
        return handler_table[name];
      }
      return new List<Delegate>();
    }

    private Dictionary<string, List<Delegate>> handler_table;
  }
}
