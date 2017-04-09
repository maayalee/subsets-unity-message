using System.Collections.Generic;

namespace libunity.message {
  public class message_dispatcher<T> where T : message {
    public delegate void message_callback(T evt);

    public message_dispatcher() {
      message_callbacks = new Dictionary<string, List<message_callback>>();
    }

    public virtual bool is_null() {
      return false;
    }

    public void add_listener(string message_name, message_callback callback) {
      if (!message_callbacks.ContainsKey(message_name))
        message_callbacks[message_name] = new List<message_callback>();
      message_callbacks[message_name].Add(callback);
    }

    public void remove_listener(string message_name) {
      if (message_callbacks.ContainsKey(message_name)) {
        message_callbacks[message_name].Clear();
      }
    }

    public void remove_listener(string message_name, message_callback callback) {
      if (message_callbacks.ContainsKey(message_name)) {
        List<message_callback> callbacks = message_callbacks[message_name];
        for (int i = 0; i < callbacks.Count; ++i) {
          if (callbacks[i] == callback) {
            callbacks.RemoveAt(i);
            return;
          }
        }
      }
    }

    virtual public void dispatch_message(T evt) {
      message_queue.Enqueue(evt);
    }

    public void update_message() {
      if (message_queue.Count == 0)
        return;
      T evt = message_queue.Dequeue();

      List<message_callback> register_messages;
      if (message_callbacks.ContainsKey("*")) {
        register_messages = message_callbacks["*"];
        for (int i = 0; i < register_messages.Count; i++) {
          register_messages[i](evt);
        }
      }
      if (!message_callbacks.ContainsKey(evt.get_name()))
        return;
      register_messages = message_callbacks[evt.get_name()];
      for (int i = 0; i < register_messages.Count; i++) {
        register_messages[i](evt);
      }
    }

    private Queue<T> message_queue = new Queue<T>();
    private Dictionary<string, List<message_callback>> message_callbacks;
  }
}
