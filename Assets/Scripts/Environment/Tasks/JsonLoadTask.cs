using Environment;
using Environment.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "JsonLoadTask", menuName = "Tasks/JsonLoadTask")]
public class JsonLoadTask : Task
{
    [SerializeField] TextAsset jsonFile;
    [SerializeField] TenantsData testShow;
  
    public override void Execute() {
        string jsonText = jsonFile.text;
        TenantsData tenantsData = JsonUtility.FromJson<TenantsData>(jsonText);

        for(int i = 0; i < tenantsData.allTenants.Count; i++) {
            tenantsData.allTenants[i].InitializeData();
        }

        testShow = tenantsData;
        Computer.Instance.ReciveTenants(tenantsData);

    }
}
