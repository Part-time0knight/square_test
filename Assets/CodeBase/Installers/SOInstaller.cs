using Logic.StaticData;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CubeSOInstaller", menuName = "Installers/CubeSOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    [field: SerializeField] public CubeListSO CubeList { get; private set; }
    [field: SerializeField] public ActionsSO ActionList { get; private set; }

    public override void InstallBindings()
    {
        Container.BindInstance(CubeList).AsSingle();
        Container.BindInstance(ActionList).AsSingle();
    }
}