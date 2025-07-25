using A;
using UnityEngine;
//using A;

namespace A
{
    public class Item
    {
        public int id;
    }
}

namespace B
{
    public class Item
    {
        public int id2;
    }
}


public class BasicScript : MonoBehaviour
{
    void Start()
    {
        A.Item item = new A.Item();
    }

    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Debug.Log("OnBecameInvisible called on class 1");
    }
}