using UnityEngine;
using LibUnity.Message;

public class TestReceiver : MonoBehaviour {
  public int TRY_COUNT = 1000;

  delegate void Handler(float a);

  Handler handlers;


	// Use this for initialization
	void Start () {
    message_server = GameObject.FindObjectOfType<MessageServer>();
    message_server.AddListener<float>(this, "Test", TestMethod);

    handlers += TestMethod;

    double t1 = LibUnity.Core.Time.GetTick();
    for (int i = 0; i < TRY_COUNT; i++) {
      SendMessage("TestMethod", 3.0f);
    }
    Debug.Log("SendMessage:" + (LibUnity.Core.Time.GetTick() - t1));

    t1 = LibUnity.Core.Time.GetTick();
    for (int i = 0; i < TRY_COUNT; i++) {
      MonoBehaviour[] bs = this.GetComponentsInChildren<MonoBehaviour>();
      for (int j = 0; j < bs.Length; j++) {
        bs[j].SendMessage("TestMethod", 3.0f,  SendMessageOptions.DontRequireReceiver);
      }
    }
    Debug.Log("GetComponentInChildren and SendMessage:" + (LibUnity.Core.Time.GetTick() - t1));

    t1 = LibUnity.Core.Time.GetTick();
    for (int i = 0; i < TRY_COUNT; i++) {
      TestReceiver[] bs2 = this.GetComponentsInChildren<TestReceiver>();
      for (int j = 0; j < bs2.Length; j++) {
        bs2[j].SendMessage("TestMethod", 3.0f,  SendMessageOptions.DontRequireReceiver);
      }
    }
    Debug.Log("GetComponentInChildren<T> and SendMessage:" + (LibUnity.Core.Time.GetTick() - t1));

    t1 = LibUnity.Core.Time.GetTick();
    for (int i = 0; i < TRY_COUNT; i++) {
      BroadcastMessage("TestMethod", 3.0f);
    }
    Debug.Log("BroadcastMessage:" + (LibUnity.Core.Time.GetTick() - t1));

    t1 = LibUnity.Core.Time.GetTick();
    for (int i = 0; i < TRY_COUNT; i++) {
      message_server.DispatchMessage(this, "Test", 3.0f);
    }
    Debug.Log("MessageDispatcher:" + (LibUnity.Core.Time.GetTick() - t1));

    t1 = LibUnity.Core.Time.GetTick();
    for (int i = 0; i < TRY_COUNT; i++) {
      message_server.Broadcast(this, "Test", 3.0f);
    }
    Debug.Log("MessageDispatcherBroadcast:" + (LibUnity.Core.Time.GetTick() - t1));

    t1 = LibUnity.Core.Time.GetTick();
    for (int i = 0; i < TRY_COUNT; i++) {
      message_server.Broadcast(this, "Test", 3.0f, typeof(TestReceiver));
    }
    Debug.Log("MessageDispatcherBroadcast<T, U>:" + (LibUnity.Core.Time.GetTick() - t1));

    t1 = LibUnity.Core.Time.GetTick();
    for (int i = 0; i < TRY_COUNT; i++) {
      TestMethod(3.0f);
    }
    Debug.Log("FunctionCall:" + (LibUnity.Core.Time.GetTick() - t1));

    t1 = LibUnity.Core.Time.GetTick();
    for (int i = 0; i < TRY_COUNT; i++) {
      handlers(3.0f);
    }
    Debug.Log("Delegate:" + (LibUnity.Core.Time.GetTick() - t1));
	}

	// Update is called once per frame
	void Update () {
	}

  public void TestMethod(float a) {
    //Debug.Log(a);
    int n = 3;
  }

  public void TestMethod2() {
    //Debug.Log("TestMethod2");
    int a = 3;
  }



  public MessageServer message_server;
}
