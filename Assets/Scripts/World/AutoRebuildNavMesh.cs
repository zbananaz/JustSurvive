using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Extensions;
using NavMeshPlus.Components;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class AutoRebuildNavMesh : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface Surface;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private float UpdateRate = 0.1f;
    [SerializeField]
    private float MovementThreshold = 3;
    [SerializeField]
    private Vector3 NavMeshSize = new(40,40,40);

    private Vector3 WorldAnchor;
    private NavMeshData NavMeshData;
    private readonly List<NavMeshBuildSource> Sources = new();

    private void Start()
    {
        NavMeshData = new NavMeshData();
        NavMesh.AddNavMeshData(NavMeshData);
        BuildNavMesh(false);
        StartCoroutine(CheckPlayerMovement());
    }

    private IEnumerator CheckPlayerMovement()
    {

        WaitForSeconds Wait = new(UpdateRate);
        while (true)
        {
            if (Vector3.Distance(WorldAnchor, Player.transform.position) > MovementThreshold)
            {
                BuildNavMesh(true);
                WorldAnchor = Player.transform.position;
            }

            yield return Wait;
        }
    }

    private void BuildNavMesh(bool Async)
    {
        Bounds navMeshBounds = new(Player.transform.position, NavMeshSize);
        List<NavMeshBuildMarkup> markups = new();
        List<NavMeshModifier> modifiers;

        if (Surface.collectObjects == CollectObjects.Children)
        {
            modifiers = new List<NavMeshModifier>(Surface.GetComponentsInChildren<NavMeshModifier>());

        }
        else
        {
            modifiers = NavMeshModifier.activeModifiers;
        }

        for (int i = 0; i < modifiers.Count; i++)
        {
            if(((Surface.layerMask & (1 << modifiers[i].gameObject.layer)) == 1)
                && modifiers[i].AffectsAgentType(Surface.agentTypeID))
            {
                markups.Add(new NavMeshBuildMarkup()
                {
                    root = modifiers[i].transform,
                    overrideArea = modifiers[i].overrideArea,
                    area = modifiers[i].area,
                    ignoreFromBuild = modifiers[i].ignoreFromBuild
                });
            }
        }

        if (Surface.collectObjects == CollectObjects.Children)
        {
            NavMeshBuilder.CollectSources(Surface.transform, Surface.layerMask, Surface.useGeometry, Surface.defaultArea, markups, Sources);
            Debug.Log("Build CollectSources1");

        }
        else
        {
            NavMeshBuilder.CollectSources(navMeshBounds, Surface.layerMask, Surface.useGeometry, Surface.defaultArea, markups, Sources);
            Debug.Log("Build CollectSources2");

        }

        Sources.RemoveAll(source => source.component != null && source.component.gameObject.GetComponent<NavMeshAgent>() != null);

        if(Async)
        {
            NavMeshBuilder.UpdateNavMeshDataAsync(NavMeshData, Surface.GetBuildSettings(), Sources, new Bounds(Player.transform.position, NavMeshSize));
            Debug.Log("Build UpdateNavMeshDataAsync1");

        }
        else
        {
            NavMeshBuilder.UpdateNavMeshData(NavMeshData, Surface.GetBuildSettings(), Sources, new Bounds(Player.transform.position, NavMeshSize));
            Debug.Log("Build UpdateNavMeshDataAsync2");

        }
    }

}
