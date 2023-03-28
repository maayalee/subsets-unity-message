namespace Subsets.Message {
  abstract public class BasicMessage : MessageBase {
    public BasicMessage(string name) {
      this.name = name;
    }

    public string GetName() {
      return name;
    }

    private string name;
  }
}
