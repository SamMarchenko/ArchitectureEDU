﻿using System.Linq;
using Logic;
using Logic.EnemySpawners;
using StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelStaticData))]
public class LevelStaticDataEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var levelData = (LevelStaticData) target;

        if (GUILayout.Button("Collect"))
        {
            levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
                .Select(x =>
                    new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.MonsterTypeId, x.transform.position))
                .ToList();

            levelData.LevelKey = SceneManager.GetActiveScene().name;
        }
        EditorUtility.SetDirty(target);
    }
}