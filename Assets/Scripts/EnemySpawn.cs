using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	public ObjectPool enemyPool;
	[Range(1f, 5f)]
	public float coolDownTime;
	
	void Start(){
		InvokeRepeating("SpawnEnemy", 1f, coolDownTime);
	}
	public void SpawnEnemy(){
		if(enemyPool != null){
			GameObject enemy = enemyPool.GetGameObject();
			enemy.transform.position = this.transform.position;
			enemy.GetComponent<Enemy>().Spawn = this;
		}                  
	}
	
	public void RemoveEnemy(GameObject enemy){
		if(enemyPool != null){
			enemy.transform.position = enemyPool.transform.position;
			enemyPool.ReleaseGameObject(enemy);
		}  
	}
}