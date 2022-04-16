using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{

    private Camera cam;
    //[SerializeField] private int aimSize = 16;
    //[SerializeField] private UIController ui;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPt;
    private float projectileForwardForce = 30;
    private float projectileUpForce = 7;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        // hide the mouse cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                Enemy target = hitObject.GetComponent<Enemy>();
                // is this object our Enemy?
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    // visually indicate where there was a hit
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }

        }
        else if (Input.GetMouseButtonDown(1))
        {
            ThrowGrenade();
        }
    }

    public void ThrowGrenade()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPt.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * projectileForwardForce, ForceMode.Impulse);
        rb.AddForce(transform.up * projectileUpForce, ForceMode.Impulse);
    }

    private IEnumerator SphereIndicator(Vector3 hitPosition)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); 
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.position = hitPosition; 

        yield return new WaitForSeconds(1); 

        Destroy(sphere);
    }

}
