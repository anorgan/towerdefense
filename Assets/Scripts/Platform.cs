using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public Color hoverColor;
    public GameObject platformGO;
    private Color defaultColor;
    private Renderer rend;

    private GameObject turret;

    GameObject towerMenu;

	void Start ()
    {
        rend = platformGO.GetComponent<Renderer>();
        defaultColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build, turret already exists");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        Vector3 offset = new Vector3(0f, -.7f, 0f);
        turret = (GameObject) Instantiate(turretToBuild, transform.position + offset, transform.rotation);

        rend.enabled = false;

        // towerMenu.SetActive(true);
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = defaultColor;
    }
}
