using UnityEngine;

public class SansarGrid : MonoBehaviour
{
    public Sansar[] sansarPrefab;
    public int rows = 5;
    public int columns = 11;

    public float satirAraligi = 2.0f;
    public float sutunAraligi = 1.5f;
    public float yOffset = 3f;

    private Vector3 _direction = Vector3.right;
    private float speed = 2.0f;
    public float minSpeed = .5f;
    public float maxSpeed = 2f;

    private void Awake()
    {
        for (int satir = 0; satir < rows; satir++)
        {
            float width = satirAraligi * (columns - 1);
            float height = sutunAraligi * (rows - 1);

            //Vector2 merkezilestirme = new Vector2(-width/2, -height/2);

            Vector3 satirKonumu = new Vector3(-width/2, satir * sutunAraligi, -height/2);

            for (int sutun = 0; sutun < columns; sutun++)
            {
                Sansar sansar = Instantiate(sansarPrefab[satir], transform);
                Vector3 konum = satirKonumu;
                konum.x += sutun * satirAraligi;
                sansar.transform.position = konum;
            }
        }
    }

    private void Update()
    {
        //speed = .6f * (speed - minSpeed) / (maxSpeed - minSpeed);
        //speed = Mathf.Lerp(minSpeed, maxSpeed, speed);

        transform.position += _direction * speed * Time.deltaTime;

        Vector3 solKenar = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 sagKenar = Camera.main.ViewportToWorldPoint(Vector3.right);

        Transform enSolNokta = EnSoldakiSansar();
        Transform enSagNokta = EnSagdakiSansar();

        if (_direction == Vector3.right && enSagNokta != null && enSagNokta.position.x >= sagKenar.x -1)
        {
            AsagiKay();
        }

        if (_direction == Vector3.left && enSolNokta != null && enSolNokta.position.x <= solKenar.x + 1)
        {
            AsagiKay();
        }
    }

    private void AsagiKay()
    {
        _direction = _direction == Vector3.right ? Vector3.left : Vector3.right;

        //if (_direction == Vector3.right)
        //{
        //    _direction = Vector3.left;
        //}
        //else
        //{
        //    _direction = Vector3.right;
        //}

        Vector3 pos = this.transform.position;
        pos.y -= 1.0f;
        this.transform.position = pos;
    }

    private Transform EnSoldakiSansar()
    {
        Transform sol = null;
        foreach (Transform sansar in transform)
        {
            if (!sansar.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (sol == null || sansar.position.x < sol.position.x)
            {
                sol = sansar;
            }
        }
        return sol;
    }

    private Transform EnSagdakiSansar()
    {
        Transform sag = null;
        foreach (Transform sansar in transform)
        {
            if (!sansar.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (sag == null || sansar.position.x > sag.position.x)
            {
                sag = sansar;
            }
        }
        return sag;
    }
}
