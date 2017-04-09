using System.Collections.Generic;

namespace libunity.message {

  /**
   * @ingroup vtech.message
   *
   * @class message
   *
   * @brief 메시지 객체. 컴포넌트 간의 이벤트 등록 및 메시지 전달시에 사용된다.
   * @author Lee Hyeon-gi
   */
  public class message {
    public message() : base() {
      properties = new Dictionary<string, object>();
    }

    public message(string name) {
      properties = new Dictionary<string, object>();
      properties["name"] = name;
    }

    public message(string name, object data) {
      properties = new Dictionary<string, object>();
      properties["name"] = name;
      properties["data"] = data;
    }

    public string get_name() {
      if (!properties.ContainsKey("name"))
        return "";
      return (string)properties["name"];
    }

    protected void set_name(string name) {
      properties["name"] = name;
    }

    public void set_data<T>(T data) {
      properties["data"] = (T)data;
    }

    public T get_data<T>() {
      if (!properties.ContainsKey("data"))
        return default(T);
      return (T)properties["data"];
    }

    public bool has_data() {
      return properties.ContainsKey("data");
    }

    private Dictionary<string, object> properties;
  }
}
