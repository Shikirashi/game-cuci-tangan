﻿using UnityEngine;

public class Target : MonoBehaviour{
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }

}
