using System.Collections;
using UnityEngine;

namespace libunity.message {
  /**
   * @class message_target
   *
   * @brief 이벤트 콜백 등록을 제공하는 컴포넌트

   * @author Lee, Hyeon-gi
   */
  public class message_target<T> : MonoBehaviour where T : message {
    public const float MESSAGE_UPDATE_TIME = 0.016f;

    public message_target() : base() {
      message_dispatcher = new message_dispatcher<T>(); 
    }

    protected void OnEnable() {
      StartCoroutine(update_process_message());
    }

    virtual public void add_listener(string message_name,
      message_dispatcher<T>.message_callback callback) {
      message_dispatcher.add_listener(message_name, callback);
    }

    virtual public void remove_listener(string message_name) {
      message_dispatcher.remove_listener(message_name);
    }

    virtual public void remove_listener(string message_name, 
      message_dispatcher<T>.message_callback callback) {
      message_dispatcher.remove_listener(message_name, callback);
    }

    /**
     * 
     *  @param immediately 이벤트 처리를 Update 루프를 통해 처리하지 않고 곧바로 처리할지 여부
     */ 
    virtual public void dispatch_message(T evt, bool immediately = false) {
      message_dispatcher.dispatch_message(evt);
      if (immediately) {
        message_dispatcher.update_message();
      }
    }

    protected IEnumerator update_process_message() {
      while (true) {
        message_dispatcher.update_message();
        yield return new WaitForSeconds(MESSAGE_UPDATE_TIME);
      }
    }

    protected message_dispatcher<T> message_dispatcher = null;
  }
}
