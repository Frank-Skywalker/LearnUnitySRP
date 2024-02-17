using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

public class MeshBall : MonoBehaviour
{
    [SerializeField] 
    private Mesh mesh = default;
    [SerializeField] 
    private Material material = default;
    
    private static int baseColorId = Shader.PropertyToID("_BaseColor");

    private Matrix4x4[] transforms = new Matrix4x4[1023];
    private Vector4[] baseColors = new Vector4[1023];

    private MaterialPropertyBlock block;

    void Awake () {
        for (int i = 0; i < transforms.Length; i++) {
            transforms[i] = Matrix4x4.TRS(
                Random.insideUnitSphere * 10f, Quaternion.identity, Vector3.one
            );
            baseColors[i] =
                new Vector4(Random.value, Random.value, Random.value, 1f);
        }
    }

    void Update () {
        if (block == null) {
            block = new MaterialPropertyBlock();
            block.SetVectorArray(baseColorId, baseColors);
        }
        Graphics.DrawMeshInstanced(mesh, 0, material, transforms, 1023, block);
    }
}
