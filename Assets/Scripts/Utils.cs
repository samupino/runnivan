using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

    public static int GetRandomWeightedIndex(float[] weights) {
        if (weights == null || weights.Length == 0) throw new System.ArgumentException("Empty weights not allowed");

        float total = 0;
        for (int i = 0; i < weights.Length; i++) {
            float w = weights[i];
            if (w < 0) throw new System.ArgumentException("Negative weights not allowed");
            if (float.IsNaN(w)) throw new System.ArgumentException("NaN weights not allowed");
            total += w;
        }
        if (total == 0) throw new System.ArgumentException("Weights sum cannot be zero");

        float rand = Random.Range(0, total);
        float sum = 0f;

        for (int i = 0; i < weights.Length; i++) {
            sum += weights[i];
            if (sum >= rand) return i;
        }

        return -1;
    }
}