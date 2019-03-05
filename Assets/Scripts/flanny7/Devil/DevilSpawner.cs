using UnityEngine;

public sealed class DevilSpowner
{
    [SerializeField]
    private Transform[] popAreas = null;
    [SerializeField]
    private GameObject enemyPrefab = null;
    [SerializeField]
    private float offsetY = 0.0f;

    public void Spown()
    {
        var gPos = this.GeneratPopPos();
        var pos = new Vector3(gPos.x, this.offsetY, gPos.y);

        var obj = GameObject.Instantiate(this.enemyPrefab);
        var trans = obj.transform;
        trans.position = pos;
        trans.rotation = this.GeneratAngle();
    }

    private Vector2 GeneratPopPos(int _index = -1, float _padding = 0)
    {
        var maxIndex = this.popAreas.Length - 1;
        var index = (_index < 0 || maxIndex < _index) ? Random.Range(0, maxIndex) : _index;

        var targetArea = this.popAreas[index];
        var centerPos = targetArea.position;
        var width = targetArea.localScale.x;
        var height = targetArea.localScale.z;

        var minX = centerPos.x - (width / 2) + _padding;
        var maxX = centerPos.x + (width / 2) - _padding;
        var minZ = centerPos.z - (height / 2) + _padding;
        var maxZ = centerPos.z + (height / 2) - _padding;

        var x = Random.Range(minX, maxX);
        var z = Random.Range(minZ, maxZ);

        return new Vector2(x, z);
    }

    private Quaternion GeneratAngle()
    {
        return Quaternion.AngleAxis(Random.Range(0, 360 - 1), Vector3.up);
    }
}