using Environment.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment {
    public class Computer : MonoBehaviour, IInteractable {
        [SerializeField]
        private Task testTask = null; // Put your stack/queue here :)
        [SerializeField] public Stack<PersonClass> testStack = new Stack<PersonClass>();
        public PopText popText;


        private void Awake() {
            CreateStack();
        }

        void CreateStack() {
            testStack.Push(new(30, "Anton", "gerklev"));
            testStack.Push(new(22, "Dnton", "Cerklev"));
            testStack.Push(new(33, "Onton", "Eerklev"));
            testStack.Push(new(32, "Cnton", "Kerklev"));
            testStack.Push(new(31, "Bnton", "Oerklev"));
        }
        public void OnInteract() {
            testTask.Execute();
        }
    }
}
