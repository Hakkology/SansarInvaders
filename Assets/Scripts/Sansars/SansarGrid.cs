using System.Collections.Generic;
using UnityEngine;

public class SansarGrid : MonoBehaviour
{
    [Header("Invader grid iþlemleri")]
    public List<SansarGridSO> sansarGridDataList = new List<SansarGridSO>();
    public int currentLevel = 0;
    //public SansarGridSO sansarGridData;

    private Vector3 _direction = Vector3.right;
    private float speed;

    [Header("Sansar roket mantýðý")]
    public Projectile SansarRoketi;
    public float SansarAtakHizi;

    public int OlenSansarlarSayisi { get; private set; }
    public int SansarSayisi => sansarGridDataList[currentLevel].rows * sansarGridDataList[currentLevel].columns;
    public int CanliSansarSayisi => SansarSayisi - OlenSansarlarSayisi;
    public float SansarYuzdesi => (float)OlenSansarlarSayisi / (float)SansarSayisi;

    private void Awake()
    {
        SansarlariDiz();
    }

    public void SansarlariDiz()
    {
        for (int satir = 0; satir < sansarGridDataList[currentLevel].rows; satir++)
        {
            float width = sansarGridDataList[currentLevel].satirAraligi * (sansarGridDataList[currentLevel].columns - 1);
            float height = sansarGridDataList[currentLevel].sutunAraligi * (sansarGridDataList[currentLevel].rows - 1);

            //Vector2 merkezilestirme = new Vector2(-width/2, -height/2);

            Vector3 satirKonumu = new Vector3(-width / 2, satir * sansarGridDataList[currentLevel].sutunAraligi, -height / 2);

            for (int sutun = 0; sutun < sansarGridDataList[currentLevel].columns; sutun++)
            {
                Sansar sansar = Instantiate(sansarGridDataList[currentLevel].sansarPrefab[satir], transform);
                Vector3 konum = satirKonumu;
                konum.x += sutun * sansarGridDataList[currentLevel].satirAraligi;
                sansar.transform.position = konum;
                sansar._killed += OnSansarKilled;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), SansarAtakHizi, SansarAtakHizi);
    }

    private void Update()
    {
        speed = Mathf.Lerp(sansarGridDataList[currentLevel].minSpeed, sansarGridDataList[currentLevel].maxSpeed, SansarYuzdesi);
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

    private void MissileAttack()
    {
        foreach (Transform sansar in transform)
        {
            if (!sansar.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Random.value < (1.0f / (float)CanliSansarSayisi))
            {
                Projectile missile = MissilePool.Instance.HavuzdanAl();
                if (missile != null)
                {
                    missile.transform.position = sansar.position;
                    missile.gameObject.SetActive(true);
                    missile.destroyed += OnMissileCrash;
                }
                //break;
            }
        }
    }

    private void OnSansarKilled()
    {
        OlenSansarlarSayisi++;
    }

    private void OnMissileCrash(Projectile projectile)
    {
        MissilePool.Instance.HavuzaDonder(projectile);
    }

    public void SansarlariTekrarDiz()
    {
        currentLevel = 0;
        foreach (Transform sansar in transform)
        {
            Destroy(sansar.gameObject);
        }

        SansarlariDiz();
    }

    public void SansarLevelAtla()
    {
        currentLevel++;

        if (currentLevel > sansarGridDataList.Count)
        {
            currentLevel = 0;
        }

        SansarlariDiz();
    }
}
