using TMPro;
using UnityEngine;

public class TenantDataBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Tenant tenant;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI aptNum;
    [SerializeField] TextMeshProUGUI enterLast;
    [SerializeField] TextMeshProUGUI exitLast;


    public void ShowData(Tenant data) {
        tenant = data;

        nameText.text       = tenant.name;
        aptNum.text         = tenant.apartmentNum.ToString();
        enterLast.text      = tenant.parsedLastEntrance.ToString("yyyy-MM-dd  HH:mm");
        exitLast.text       = tenant.parsedLastExit.ToString("yyyy-MM-dd  HH:mm");

        gameObject.SetActive(true);
    }
}
