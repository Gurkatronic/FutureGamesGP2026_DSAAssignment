using Environment.Tasks;
using System.Collections;
using UnityEngine;

namespace Environment {
    public class Computer : MonoBehaviour, IInteractable {
        [SerializeField]
        private Task testTask = null; // Put your stack/queue here :)
        public PopText popText;
        public Stack testStack;

        public void OnInteract() {
            testTask.Execute();
        }
    }
}
