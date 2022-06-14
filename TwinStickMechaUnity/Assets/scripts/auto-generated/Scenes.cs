//This class is auto-generated do not modify
namespace k
{
	public static class Scenes
	{
		public const string SCENE_SPLASH = "Scene_Splash";
		public const string SCENE_HARNESS = "Scene_Harness";
		public const string SCENE_MAIN_MENU = "Scene_MainMenu";
		public const string SCENE_MISSION_BOARD = "Scene_MissionBoard";
		public const string SCENE_GARAGE = "Scene_Garage";
		public const string SCENE_BARRACKS = "Scene_Barracks";
		public const string SCENE_SHOP = "Scene_Shop";
		public const string SCENE_JUNKYARD = "Scene_Junkyard";
		public const string SCENE_PRE_GAME = "Scene_PreGame";
		public const string SCENE_IN_GAME = "Scene_InGame";
		public const string SCENE_POST_GAME = "Scene_PostGame";

		public const int TOTAL_SCENES = 11;


		public static int nextSceneIndex()
		{
			var currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
			if( currentSceneIndex + 1 == TOTAL_SCENES )
				return 0;
			return currentSceneIndex + 1;
		}
	}
}