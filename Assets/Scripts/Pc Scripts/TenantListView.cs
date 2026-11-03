using UnityEngine;

public class TenantListView : MonoBehaviour
{
    [SerializeField] GameObject contentParent;
    [SerializeField] TenantDataBox tenantDataPrefab;

    TenantsData tenantsData;

    [SerializeField] TenantDataBox[] createdBoxes;



    public void LoadData(TenantsData data) {
        tenantsData = data;
        FillList();
    }

    public void FillList() {
        createdBoxes = new TenantDataBox[tenantsData.allTenants.Count];
        for(int i = 0; i < tenantsData.allTenants.Count; i++) {
            TenantDataBox newBox = Instantiate(tenantDataPrefab, contentParent.transform);
            newBox.ShowData(tenantsData.allTenants[i]);
            createdBoxes[i] = newBox;
        }
    }

    void HideAll() {
        for(int i = 0; i < createdBoxes.Length; i++) {
            createdBoxes[i].gameObject.SetActive(false);
        }
    }

    void ShowAll() {
        for(int i = 0; i < createdBoxes.Length; i++) {
            createdBoxes[i].gameObject.SetActive(true);
        }
    }


}
