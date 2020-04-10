using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

	[SerializeField]
	private float _speed = 10.0f;
	[SerializeField][Range(0,.3f)]
	private float _smoothSpeed = 0.1f;
	[SerializeField]
	private float _jumpForce = 1.5f;
	[SerializeField]
	private Animator _animatorP;

	private Rigidbody2D _rgdPlayer;

	[Header("Active Moviment")]
	[SerializeField]
	private bool _transformMoviment = false;
	[SerializeField]
	private bool _rgdMoviment = false;

	private float _horizontalMove;
	private Vector3 _velocity = Vector3.zero;

	//Jump Variable
	private float _jumpMove;
	[SerializeField]
	private bool _isGrounded = true;
	[SerializeField]
	private LayerMask _lGround;
	[SerializeField][Range(0,.2f)]
	private float _distanceCast;
	[SerializeField][Range(-0.3f,.3f)]
	private float _radiusGain;

	private GameObject _sceneManager;

	void Awake(){
		_rgdPlayer = GetComponent<Rigidbody2D>();
		//link with SceneManager
		_sceneManager = GameObject.FindGameObjectWithTag("SM");
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update(){
		AnimationControl();
		DetectGround();
		DetectOutWorld();

		if(transform.position.x > 13f){
			transform.position = new Vector2(-13f,transform.position.y);
		}else if(transform.position.x < -13f){
			transform.position = new Vector2(13f,transform.position.y);
		}
		
	}
	void FixedUpdate () {
		Movement();
		Jump();
	}

	private void Movement(){
		
		//movimento Através da posição
		if(_transformMoviment){
			Vector3 movement =  new Vector3(_speed*Input.GetAxisRaw("Horizontal"),0f,0f);
			_animatorP.SetFloat("speed",Mathf.Abs(movement.x));
			transform.position += movement * Time.deltaTime;
		}
		//movimento Através do rigidbody
		if(_rgdMoviment && _isGrounded){
			_horizontalMove = Input.GetAxisRaw("Horizontal")*_speed*Time.deltaTime;
			Vector3 targetMovement = new Vector2(_horizontalMove*2f,_rgdPlayer.velocity.y);
			_animatorP.SetFloat("speed",Mathf.Abs(_horizontalMove));
			_rgdPlayer.velocity = Vector3.SmoothDamp(_rgdPlayer.velocity,targetMovement, ref _velocity,_smoothSpeed);
		}
		
		
	}
	private void Jump(){
		_jumpMove = Input.GetAxisRaw("Jump");
		
		if(Input.GetButtonDown("Jump")){
			
			if(_isGrounded){
			
				_isGrounded = false;
				_animatorP.SetBool("isJump",true);
				_rgdPlayer.AddForce(new Vector2(0f,_jumpForce*_jumpMove));
			}
		}
		

	}

	private void DetectGround(){
		RaycastHit2D hit2d = Physics2D.CircleCast(transform.position,GetComponent<CircleCollider2D>().radius+_radiusGain,Vector2.down,_distanceCast,_lGround);
		if(hit2d){
			Debug.Log("in Ground");
			_isGrounded = true;
			_animatorP.SetBool("isJump",false);
		}else{
			Debug.Log("in Air");
			_isGrounded = false;
		}
	}

	private void AnimationControl(){
		if(Input.GetKeyDown(KeyCode.D)){
			GetComponent<SpriteRenderer>().flipX = false;
		}else if(Input.GetKeyDown(KeyCode.A)){
			GetComponent<SpriteRenderer>().flipX = true;
		}
	}
	
	private void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Coin"){
			Debug.Log("Colidi com a coin");
			_sceneManager.GetComponent<SceneController>().ActiveObject();
			GameManager.instance.AddScore();
			Destroy(col.gameObject);
		}
		if(col.tag == "Enemy"){
			//_sceneManager.GetComponent<SceneController>()
			Destroy(gameObject);
		}
	}

	private void DetectOutWorld(){
		if(transform.position.y < -15f){
			Destroy(gameObject);
		}
	}

	public void SetRgdMoviment(bool setRgd){
		_rgdMoviment = setRgd;
	}
}
