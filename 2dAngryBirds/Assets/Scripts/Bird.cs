using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] private Color32 _newColor;
    [SerializeField] private Transform _initialPosition;
    [SerializeField] private string _tagNameForStartPoint = "StartPoint";
    [SerializeField] private float _lounchPower = 400f;
    [SerializeField] private LineRenderer _lineRender;
    [SerializeField] private Rigidbody2D _birdRigidbody;
    [SerializeField] private Camera _mainCamera;
    //[SerializeField] private Material _materialForArrow;

    private bool _birdWasLaunched = false;
    private float _timeSittingAround;

    private void Awake()
    {
        if (_initialPosition == null) _initialPosition = GameObject.FindGameObjectWithTag(_tagNameForStartPoint).transform;
        if (_lineRender == null) _lineRender = GetComponent<LineRenderer>();
        if (_birdRigidbody == null) _birdRigidbody = GetComponent<Rigidbody2D>();
        if (_mainCamera = null) _mainCamera = Camera.main;
        transform.position = _initialPosition.position;
        //gameObject.AddComponent<LineRenderer>();
        //gameObject.GetComponent<LineRenderer>().material = _materialForArrow;
    }

    private void Update()
    {
        print(Input.mousePosition);
        _lineRender.SetPosition(1, _initialPosition.position);
        _lineRender.SetPosition(0, transform.position);
        if (_birdWasLaunched &&
            _birdRigidbody.velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }
        if (transform.position.y > 10 ||
           transform.position.y < -10 ||
           transform.position.x > 10 ||
           transform.position.x < -14 ||
           _timeSittingAround > 2f)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = _newColor;
        _lineRender.enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition.position - transform.position;

        _birdRigidbody.AddForce(directionToInitialPosition * _lounchPower);
        _birdRigidbody.gravityScale = 1;
        _birdWasLaunched = true;
        _lineRender.enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D circle = _initialPosition.GetComponent<Collider2D>();
        newPosition = circle.ClosestPoint(newPosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ITakeDamage damageble = collision.collider.GetComponent<ITakeDamage>();
        if(damageble!=null) damageble.TakeDamage(5);
    }
}
