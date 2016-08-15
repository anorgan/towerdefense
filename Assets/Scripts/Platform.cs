using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public Color hoverColor;
    public GameObject platformGO;
    private Color defaultColor;
    private Renderer rend;

    private GameObject turret;

    public Canvas towerMenu;

    public void BuildTurret()
    {
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        Vector3 offset = new Vector3(0f, -.7f, 0f);
        turret = (GameObject)Instantiate(turretToBuild, transform.position + offset, transform.rotation);

        towerMenu.GetComponent<Canvas>().enabled = false;
    }

	void Start ()
    {
        rend = platformGO.GetComponent<Renderer>();
        towerMenu.GetComponent<Canvas>().enabled = false;
        defaultColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build, turret already exists");
            return;
        }

        rend.enabled = false;

        // Display menu
        towerMenu.GetComponent<Canvas>().enabled = true;

        // Should be called on button click
        BuildTurret();
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
