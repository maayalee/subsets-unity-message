using System;
using UnityEngine;

namespace Subsets.Message {
  public class BasicMessageDispatcher : MonoBehaviour {
    public void AddListener(string message_name, MessageDispatcher.Handler handler) {
      dispatcher.AddListener(message_name, handler);
    }

    public void AddListener<T>(string message_name, MessageDispatcher.Handler<T> handler) {
      dispatcher.AddListener(message_name, handler);
    }

    public void RemoveListener(string message_name, MessageDispatcher.Handler handler) {
      dispatcher.AddListener(message_name, handler);
    }

    public void RemoveListener<T>(string message_name, MessageDispatcher.Handler<T> handler) {
      dispatcher.AddListener(message_name, handler);
    }

    public void RemoveListener(string message_name) {
      dispatcher.RemoveListener(message_name);
    }

    public void DispatchMessage<T>(string name, T message) {
      dispatcher.DispatchMessage(name, message);
    }

    public void DispatchMessage(string name) {
      dispatcher.DispatchMessage(name);
    }

    public void Broadcast<MessageType>(string name, MessageType message) {
      BasicMessageDispatcher[] behaviours = GetComponentsInChildren<BasicMessageDispatcher>();
      foreach (BasicMessageDispatcher behaviour in behaviours) {
        behaviour.DispatchMessage(name, message);
      }
    }

    public void Broadcast<MessageType>(string name, MessageType message, Type receiver) {
      Component[] behaviours = GetComponentsInChildren(receiver);
      foreach (BasicMessageDispatcher behaviour in behaviours) {
        behaviour.DispatchMessage(name, message);
      }
    }

    public void Broadcast(MessageBase message) {
      BasicMessageDispatcher[] behaviours = GetComponentsInChildren<BasicMessageDispatcher>();
      foreach (BasicMessageDispatcher behaviour in behaviours) {
        behaviour.DispatchMessage(message.GetName(), message);
      }
    }

    public void Broadcast(MessageBase message, Type receiver) {
      Component[] behaviours = GetComponentsInChildren(receiver);
      foreach (BasicMessageDispatcher behaviour in behaviours) {
        behaviour.DispatchMessage(message.GetName(), message);
      }
    }

    public void BroadcastWithTag<MessageType>(string name, MessageType message, string tag) {
      BasicMessageDispatcher[] behaviours = GetComponentsInChildren<BasicMessageDispatcher>();
      foreach (BasicMessageDispatcher behaviour in behaviours) {
        if (behaviour.tag == tag) {
          behaviour.DispatchMessage(name, message);
        }
      }
    }

    public void BroadcastWithTag(MessageBase message, string tag) {
      BasicMessageDispatcher[] behaviours = GetComponentsInChildren<BasicMessageDispatcher>();
      foreach (BasicMessageDispatcher behaviour in behaviours) {
        if (behaviour.tag == tag) {
          behaviour.DispatchMessage(message.GetName(), message);
        }
      }
    }

    private MessageDispatcher dispatcher = new MessageDispatcher();
  }
}
