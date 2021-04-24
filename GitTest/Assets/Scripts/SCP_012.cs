using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_012 : MonoBehaviour
{
    private float effectDistance = 4f;
    private float damageDistance = 0.5f;
    public LayerMask entityLayers;

    void FixedUpdate()
    {
        PullEntity();
        DamageEntity();
    }

    private void DamageEntity()
    {
        Collider2D[] nearEntities = Physics2D.OverlapCircleAll(
                    new Vector2(transform.position.x, transform.position.y),
                    damageDistance,
                    entityLayers);
        foreach (var entity in nearEntities)
        {
            entity.GetComponent<Entity>().TakeDamage(0.5f);
        }
    }

    private void PullEntity()
    {
        Collider2D[] nearEntities = Physics2D.OverlapCircleAll(
                    new Vector2(transform.position.x, transform.position.y),
                    effectDistance,
                    entityLayers);
        foreach (var entity in nearEntities)
        {
            if (!transform.position.Equals(entity.transform.position))
            {
                float distance = Vector2.Distance(transform.position, entity.transform.position);
                entity.transform.position = Vector2.MoveTowards(
                    entity.transform.position,
                    transform.position,
                    0.04f / (distance * distance));
            }
        }
    }
}
