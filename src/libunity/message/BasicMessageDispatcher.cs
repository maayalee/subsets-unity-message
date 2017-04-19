using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace LibUnity.Message {
  /**
   */
  public class BasicMessageDispatcher : MonoBehaviour {
    public BasicMessageDispatcher() : base() {
      dispatcher = new MessageDispatcher(); 
    }

    public void AddListener<T>(string message_name, MessageDispatcher.Handler<T> callback) {
      dispatcher.AddListener<T>(message_name, callback);
    }

    public void AddListener(string message_name, MessageDispatcher.Handler callback) {
      dispatcher.AddListener(message_name, callback);
    }

    public void RemoveListener(string message_name) {
      dispatcher.RemoveListener(message_name);
    }

    public void RemoveListener(string message_name, MessageDispatcher.Handler callback) {
      dispatcher.RemoveListener(message_name, callback);
    }

    public void DispatchMessage(string message_name, object message) {
      dispatcher.DispatchMessage(message_name, message);
    }

    public void DispatchMessage(string message_name) {
      dispatcher.DispatchMessage(message_name);
    }

    public void DispatchMessage(MessageBase message) {
      dispatcher.DispatchMessage(message);
    }

    protected MessageDispatcher dispatcher = null;
  }
}
