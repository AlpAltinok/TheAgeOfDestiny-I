using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }

    public Material SelectedHeaderMat;
    public Material UnselectedHeaderMat;

    public int CurrentActivePanel;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void OnSelectedHeader(int _index)
    {
        Transform buttonsParent = transform.GetChild(0).GetChild(0);
        Transform panelParent = transform.GetChild(0).GetChild(1);
        Transform target = panelParent.GetChild(_index);
        foreach (Transform item in buttonsParent)
        {
            item.GetComponent<Button>().interactable = true;
            item.GetComponent<Image>().material = UnselectedHeaderMat;
        }

        foreach (Transform item in panelParent)
        {
            if(item != target)
            CloseMenu(item,Vector3.right);
        }
        buttonsParent.GetChild(_index).GetComponent<Button>().interactable = false;
        buttonsParent.GetChild(_index).GetComponent<Image>().material = SelectedHeaderMat;
        CurrentActivePanel = _index;
        OpenMenu(target, Vector3.right);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (transform.GetChild(0).gameObject.activeInHierarchy)
            {
                CloseMenu(transform.GetChild(0), Vector3.one);
            }
            else
            {
                InventoryHandler.instance.RefreshInventoryUI();
                OpenMenu(transform.GetChild(0), Vector3.one);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            if (transform.GetChild(0).gameObject.activeInHierarchy)
                CloseMenu(transform.GetChild(0), Vector3.one);
    }

    Vector3 GetMirrorVector(Vector3 originalVector)
    {
        // Mirror the vector by replacing 0 with 1 and 1 with 0
        return new Vector3(
            originalVector.x == 0 ? 1 : 0,
            originalVector.y == 0 ? 1 : 0,
            originalVector.z == 0 ? 1 : 0
        );
    }

    public void OpenMenu(Transform _menu, Vector3 axis)
    {
        _menu.localScale = GetMirrorVector(axis);
        StopCoroutine(OpenMenuNumerator(_menu, axis));
        StopCoroutine(CloseMenuNumerator(_menu, axis));
        StartCoroutine(OpenMenuNumerator(_menu, axis));
    }

    IEnumerator OpenMenuNumerator(Transform _menu, Vector3 axis)
    {
        _menu.gameObject.SetActive(true);
        while ((axis.y == 1 && _menu.localScale.y < 1) || (axis.x == 1 && _menu.localScale.x < 1))
        {
            _menu.localScale += axis * Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        _menu.localScale = Vector3.one;
    }

    public void CloseMenu(Transform _menu, Vector3 axis)
    {
        _menu.localScale = Vector3.one;
        StopCoroutine(OpenMenuNumerator(_menu, axis));
        StopCoroutine(CloseMenuNumerator(_menu, axis));
        StartCoroutine(CloseMenuNumerator(_menu, axis));
    }

    public Transform GetPanelParent(Panels _target)
    {
        return transform.GetChild(0).GetChild(1).GetChild((int)_target);
    }

    IEnumerator CloseMenuNumerator(Transform _menu, Vector3 axis)
    {
        while ((axis.y == 1 && _menu.localScale.y > 0.25f) || (axis.x == 1 && _menu.localScale.x > 0.25f))
        {
            _menu.localScale -= axis * Time.deltaTime * 5;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        _menu.localScale = Vector3.zero;
        _menu.gameObject.SetActive(false);
    }

    public enum Panels
    {
        StatPanel,
        WeaponPanel,
        ArmourPanel,
        JevelriesPanel,
        RunesPanel,
        SkillsPanel,
        QuestsPanel,
        MapPanel,
        SettingsPanel
    }
}
