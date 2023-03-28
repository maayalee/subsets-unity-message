using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subsets.Message {
  /* 
   * \class MessageServer
   *    
   * \brief Message transfer system component
   * \author Lee, Hyeon-gi
   */
  public class MessageServer : MonoBehaviour {
    private sealed class DispatcherContainer {
      public MonoBehaviour instance;
      public MessageDispatcher dispatcher;
    }

    public void AddListener<MessageType>(MonoBehaviour that, string name, MessageDispatcher.Handler<MessageType> handler) {
      Debug.Assert(null != that, "That is not null");
      int id = that.GetInstanceID();
      DispatcherContainer container;
      if (!objects.TryGetValue(id, out container)) {
        container = new DispatcherContainer();
        container.instance = that;
        container.dispatcher = new MessageDispatcher();
        objects[id] = container;
      }
      container.dispatcher.AddListener(name, handler);
    }

    public void RemoveListener(MonoBehaviour that, string name) {
      DispatcherContainer container;
      if (!objects.TryGetValue(that.GetInstanceID(), out container))
        return;
      container.dispatcher.RemoveListener(name);
    }

    public void RemoveListener<MessageType>(MonoBehaviour that, string name, MessageDispatcher.Handler<MessageType> handler) {
      DispatcherContainer container;
      if (!objects.TryGetValue(that.GetInstanceID(), out container))
        return;
      container.dispatcher.RemoveListener(name, handler);
    }

    public void DispatchMessage<MessageType>(MonoBehaviour that, string name, MessageType message) {
      DispatcherContainer container;
      if (!objects.TryGetValue(that.GetInstanceID(), out container))
        return;
      if (container.instance) {
        container.dispatcher.DispatchMessage(name, message);
      }
    }

    public void DispatchMessage<MessageType>(GameObject that, string name, MessageType message) {
      MonoBehaviour[] behaviours = that.GetComponents<MonoBehaviour>();
      for (int i = 0; i < behaviours.Length; ++i) {
        DispatchMessage(behaviours[i], name, message);
      }
    }

    public void Broadcast<MessageType>(MonoBehaviour that, string name, MessageType message, Type receiver = null) {
      if (null == receiver)
        receiver = typeof(MonoBehaviour);
      Component[] behaviours = that.GetComponentsInChildren(receiver, true);
      for (int i = 0; i < behaviours.Length; ++i) {
        if (null == behaviours[i])
          continue;
        DispatchMessage(behaviours[i] as MonoBehaviour, name, message);
      }
    }

    public void Broadcast<MessageType>(GameObject that, string name, MessageType message, Type receiver = null) {
      if (null == receiver)
        receiver = typeof(MonoBehaviour);
      Component[] behaviours = that.GetComponentsInChildren(receiver, true);
      for (int i = 0; i < behaviours.Length; ++i) {
        if (null == behaviours[i])
          continue;
        DispatchMessage(behaviours[i] as MonoBehaviour, name, message);
      }
    }

    public void BroadcastAll<MessageType>(string name, MessageType message, Type receiver = null) {
      List<int> keys = new List<int>(objects.Keys);
      for (int i = 0; i < keys.Count; ++i) {
        DispatcherContainer container;
        if (objects.TryGetValue(keys[i], out container)) {
          if (container.instance != null) {
            if (null == receiver) {
              container.dispatcher.DispatchMessage(name, message);
            }
            else {
              if (container.instance.GetType() == receiver) {
                container.dispatcher.DispatchMessage(name, message);
              }
            }
          }
        }
      }
    }

    public void BroadcastWithTag<MessageType>(MonoBehaviour that, string name, MessageType message, string tag) {
      MonoBehaviour[] behaviours = that.GetComponentsInChildren<MonoBehaviour>(true);
      foreach (MonoBehaviour behaviour in behaviours) {
        if (behaviour.tag == tag) {
          DispatchMessage(behaviour, name, message);
        }
      }
    }

    public void BroadcastWithTag<MessageType>(GameObject that, string name, MessageType message, string tag) {
      MonoBehaviour[] behaviours = that.GetComponentsInChildren<MonoBehaviour>(true);
      foreach (MonoBehaviour behaviour in behaviours) {
        if (behaviour.tag == tag) {
          DispatchMessage(behaviour, name, message);
        }
      }
    }

    public void OnEnable() {
      StartCoroutine(RemoveDestroyComponents());
    }

    /**
     * Don't put yield code in foreach.
     * Out of sync error when iterating dictionary
     */
    private IEnumerator RemoveDestroyComponents() {
      while (true) {
        List<int> keys = new List<int>(objects.Keys);
        for (int i = 0; i < keys.Count; ++i) {
          DispatcherContainer container;
          if (objects.TryGetValue(keys[i], out container)) {
            if (objects[keys[i]].instance == null) {
              objects.Remove(keys[i]);
            }
          }
        }
        yield return new WaitForSeconds(1.0f);
      }
    }

    private Dictionary<int, DispatcherContainer> objects = new Dictionary<int, DispatcherContainer>();
  }
}