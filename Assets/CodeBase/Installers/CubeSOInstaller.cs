using Logic.StaticData;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CubeSOInstaller", menuName = "Installers/CubeSOInstaller")]
public class CubeSOInstaller : ScriptableObjectInstaller<CubeSOInstaller>
{
    [field: SerializeField] public CubeListSO List { get; private set; }

    public override void InstallBindings()
    {
        Container.BindInstance(List).AsSingle();
    }
}