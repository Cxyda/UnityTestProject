using Managers;
using Utility;
using Zenject;

namespace Installers
{
	/// <summary>
	/// Main Zenject installer class
	/// </summary>
	public class GameInstaller : MonoInstaller<GameInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<GameModeManager>().FromComponentSibling().AsSingle();
			Container.BindInterfacesAndSelfTo<UiManager>().FromComponentSibling().AsSingle();
			Container.Bind<InputManager>().FromComponentSibling().AsSingle();

			Container.Bind<PrefabFactory>().AsSingle();
		}
	}
}