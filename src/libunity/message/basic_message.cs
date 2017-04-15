namespace libunity.message {
  abstract public class basic_message : message_base {
    public basic_message(string name) {
      this.name = name;
    }

    public string get_name() {
      return name;
    }

    private string name;
  }
}
