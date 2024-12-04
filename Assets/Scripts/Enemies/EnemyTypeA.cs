using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeA : MonoBehaviour
{
    public float speed = 1f;
    public float minX, maxX;
    public float waitingTime = 2f;

    private GameObject _target; //el lugal al que se mueve

    // Start is called before the first frame update
    void Start()
    {
        UpdateTarget();
        StartCoroutine("PatrolToTarget");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateTarget()
    {
        //If first time, create target in the left
        if (_target == null)
        {
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            return;
        }

        //If we are in the left, change target to the right
        if (_target.transform.position.x == minX)
        {
            _target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        //If we are in the rigth, change target to the left
        else if (_target.transform.position.x == maxX)
        {
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private IEnumerator PatrolToTarget()
    {
        //Corutine to move the enemy
        while (Vector2.Distance(transform.position, _target.transform.position) > 0.05f)
        {
            //let's move to the target
            Vector2 direction = _target.transform.position - transform.position;
            float xDirection = direction.x;

            transform.Translate(direction.normalized * speed * Time.deltaTime);
            //IMPORTANT
            yield return null; //yield return hace que se repita el while hasta que sea falso sin leer lo demas de abajo
        }

        //At this point i've reached the target, let's set our position to the target's one
        Debug.Log("Target reached");
        transform.position = new Vector2(_target.transform.position.x, transform.position.y);

        //And lets wait for a moment
        Debug.Log("Waiting for " + waitingTime + "seconds");
        yield return new WaitForSeconds(waitingTime); //Important

        //once waited, let's restore the patrol behavior
        Debug.Log("waited enough, let's update the target and move again");
        UpdateTarget();
        StartCoroutine("PatrolToTarget");
    }
}
