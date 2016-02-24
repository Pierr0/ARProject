using UnityEngine;

public class GetMouseInput : MonoBehaviour
{

    [SerializeField]
    private GameRuler gameruler;

    void Update()
    {
        // Méthode 1: utiliser le collider
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            RaycastHit hitInfo;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Specify the ray to be casted from the position of the mouse click

            // Raycast and verify that it collided
            if (Physics.Raycast(ray, out hitInfo))
            {
                // Select the object if it has the good Tag
                if (hitInfo.collider.gameObject.tag == ("ObjectTag"))
                {
                    // Do something
                }
                if (hitInfo.collider.gameObject.tag == ("Demon"))
                {
                    // Do something

                }
                if (hitInfo.collider.gameObject.tag == ("Golem"))
                {
                    // Do something

                }
            }
        }

        // Méthode 2 - sans collider - pas appropriée
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 attractionPoint = Vector3.zero;
            Plane plane = new Plane(Vector3.forward, 0);
            float dist;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out dist))
            {
                attractionPoint = ray.GetPoint(dist);
            }
        }
    }
}