using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerEnemyView : MonoBehaviour
{
    [SerializeField] private EnemyView _enemyView;

    public EnemyView EnemyView => _enemyView;

    public void SetLocalPosition(Vector3 position) =>
        transform.localPosition = position;

    private void OnValidate()
    {
        if (_enemyView == null)
        {
            var topEnemy = transform.GetChild(0);
            if (topEnemy != null)
                _enemyView = topEnemy.GetComponent<EnemyView>();
        }
    }
}
