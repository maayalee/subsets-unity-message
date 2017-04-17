using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace libunity.message {
  /**
   */
  public class basic_message_dispatcher : MonoBehaviour {
    public basic_message_dispatcher() : base() {
      dispatcher = new message_dispatcher(); 
    }

    public void add_listener<T>(string message_name, message_dispatcher.handler<T> callback) {
      dispatcher.add_listener<T>(message_name, callback);
    }

    public void add_listener(string message_name, message_dispatcher.handler callback) {
      dispatcher.add_listener(message_name, callback);
    }

    public void remove_listener(string message_name) {
      dispatcher.remove_listener(message_name);
    }

    public void remove_listener(string message_name, message_dispatcher.handler callback) {
      dispatcher.remove_listener(message_name, callback);
    }

    public void dispatch_message(string message_name, object message) {
      dispatcher.dispatch_message(message_name, message);
    }

    public void dispatch_message(string message_name) {
      dispatcher.dispatch_message(message_name);
    }

    public void dispatch_message(message_base message) {
      dispatcher.dispatch_message(message);
    }

    protected message_dispatcher dispatcher = null;
  }
}
