using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections;

namespace Assets.Scripts.Namespaces {
    public class Que<T> {
        private List<T> queTest = new();
        public int Count { get { return queTest.Count; } }

        public Que() {
            queTest = new();
        }

        public void Enqueue(T que) {
            queTest.Add(que);
        }


        public T DeQueue() {

            T queNum = queTest[queTest.Count - 1];
            queTest.Remove(queNum);
            return queNum;
        }

        public T Peek() {
            T queNum = queTest[queTest.Count - 1];
            return queNum;
        }



    }
}
