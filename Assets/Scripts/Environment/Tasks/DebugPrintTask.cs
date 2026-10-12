using UnityEngine;

namespace Environment.Tasks {
    [CreateAssetMenu(fileName = "DebugPrint Task", menuName = "Tasks/DebugLog Task")]
    public class DebugPrintTask : Task {
        [SerializeField]
        private string textToPrint = "Hi, I'm a test task!";

        public override void Execute() {
            Debug.Log(textToPrint);
        }
    }
}
