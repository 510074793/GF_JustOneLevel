using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

public class SurvivalGame {
    public bool GameOver {
        get;
        protected set;
    }

    private float m_ElapseSeconds = 0f;

    private Hero m_Hero = null;

    public void Initialize () {
        GameEntry.Event.Subscribe (ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        GameEntry.Event.Subscribe (ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);


        // 创建主角
        HeroData heroData = new HeroData (EntityExtension.GenerateSerialId (), 1, CampType.Player);
        heroData.Position = new Vector3(3, 0, 3);
        EntityExtension.ShowHero (typeof (Hero), "PlayerGroup", heroData);

        // 创建怪物
        MonsterData monsterData = new MonsterData (EntityExtension.GenerateSerialId (), 1, CampType.Enemy);
        monsterData.Position = new Vector3(10, 0, 10);
        EntityExtension.ShowMonster (typeof (Monster), "MonsterGroup", monsterData);

        // monsterData = new MonsterData (EntityExtension.GenerateSerialId (), 1, CampType.Enemy);
        // monsterData.Position = new Vector3(8, 0, 8);
        // EntityExtension.ShowMonster (typeof (Monster), "MonsterGroup", monsterData);
        
        // monsterData = new MonsterData (EntityExtension.GenerateSerialId (), 1, CampType.Enemy);
        // monsterData.Position = new Vector3(12, 0, 3);
        // EntityExtension.ShowMonster (typeof (Monster), "MonsterGroup", monsterData);
        
        // monsterData = new MonsterData (EntityExtension.GenerateSerialId (), 1, CampType.Enemy);
        // monsterData.Position = new Vector3(3, 0, 4);
        // EntityExtension.ShowMonster (typeof (Monster), "MonsterGroup", monsterData);

        GameOver = false;
        m_Hero = null;
    }

    public void Shutdown () {
        GameEntry.Event.Unsubscribe (ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        GameEntry.Event.Unsubscribe (ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
    }

    public void Update (float elapseSeconds, float realElapseSeconds) {
        if (m_Hero != null && m_Hero.IsDead) {
            GameOver = true;
            return;
        }

        m_ElapseSeconds += elapseSeconds;
        if (m_ElapseSeconds >= 1f) {
            m_ElapseSeconds = 0f;

            // float randomPositionX = m_SceneBackground.EnemySpawnBoundary.bounds.min.x + m_SceneBackground.EnemySpawnBoundary.bounds.size.x * (float) Utility.Random.GetRandomDouble ();
            // float randomPositionZ = m_SceneBackground.EnemySpawnBoundary.bounds.min.z + m_SceneBackground.EnemySpawnBoundary.bounds.size.z * (float) Utility.Random.GetRandomDouble ();

            // GameEntry.Entity.ShowEntity<DemoSF_Asteroid> (
            //     DemoSF_EntityExtension.GenerateSerialId (),
            //     "Assets/DemoStarForce/Prefabs/Asteroid01.prefab",
            //     "AsteroidGroup",
            //     new Vector3 (randomPositionX, 0f, randomPositionZ));
        }
    }

    protected void OnShowEntitySuccess (object sender, GameEventArgs e) {
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs) e;
        if (ne.EntityLogicType == typeof (Hero)) {
            m_Hero = (Hero) ne.Entity.Logic;
        }
    }

    protected void OnShowEntityFailure (object sender, GameEventArgs e) {
        ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs) e;
        Log.Warning ("Show entity failure with error message '{0}'.", ne.ErrorMessage);
    }
}