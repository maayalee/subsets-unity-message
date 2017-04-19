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

    public void AddListener<T>(string message_name, message_dispatcher.handler<T> callback) {
      dispatcher.AddListener<T>(message_name, callback);
    }

    public void AddListener(string message_name, message_dispatcher.handler callback) {
      dispatcher.AddListener(message_name, callback);
    }

    public void RemoveListener(string message_name) {
      dispatcher.RemoveListener(message_name);
    }

    public void RemoveListener(string message_name, message_dispatcher.handler callback) {
      dispatcher.RemoveListener(message_name, callback);
    }

    public void DispatchMessage(string message_name, object message) {
      dispatcher.DispatchMessage(message_name, message);
    }

    public void DispatchMessage(string message_name) {
      dispatcher.DispatchMessage(message_name);
    }

    public void DispatchMessage(message_base message) {
      dispatcher.DispatchMessage(message);
    }

    protected message_dispatcher dispatcher = null;
  }
}
