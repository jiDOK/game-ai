using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveDemo : MonoBehaviour
{
  [SerializeField] AnimationCurve curve;

  float health;

  void Start()
  {
    health = 50f;
  }

  void Update()
  {
    float healthUtility = curve.Evaluate(health);
  }
}
