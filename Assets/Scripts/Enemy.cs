using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Rigidbody))]
public class Enemy : MonoBehaviour {
	[Range(1, 100)]
	public int maxHealth;
	public int currentHealth;
	[Range(1f, 50f)]
	public float speed;
	[Range(1f, 5f)]
	public float coolDownDamageTime;
	private EnemySpawn spawn;
	private bool followPlayer;
	private Transform player;
	
	void Start(){
		currentHealth = maxHealth;
		followPlayer = false;
		player = GameObject.FindWithTag("Player").transform;
		InvokeRepeating("TakingDefaultDamage", 1f, coolDownDamageTime);
	}
	
	void FixedUpdate(){
		if(followPlayer){
			if(player != null){
				Vector3 dir = (player.position - transform.position).normalized;
				GetComponent<Rigidbody>().velocity =  dir * speed * Time.fixedDeltaTime;
			}
		}
	}

	public EnemySpawn Spawn {
		get {
			return spawn;
		}
		set {
			spawn = value;
		}
	}

	public void TakingDefaultDamage(){
		if(!followPlayer){
			return;
		}
		if(currentHealth > 0){
			currentHealth -= 1;   
			return;
		}
		if(spawn != null){
			spawn.RemoveEnemy(gameObject);
		}
	}
	
	public void SetInitialState(){
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		currentHealth = maxHealth;
		followPlayer = false;
	}
	
	public void SetAwakeState(){
		followPlayer = true;
	}
}