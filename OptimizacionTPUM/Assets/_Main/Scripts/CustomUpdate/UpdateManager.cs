using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.CustomUpdate
{
    public class UpdateManager : MonoBehaviour
    {
        public static UpdateManager Instance;

        [SerializeField] private float uiUpdateTime = 30f;
        [SerializeField] private float gamePlayUpdateTime = 60f;

        private List<IUpdateObject> m_updateObjectsGamePlay;
        private List<IUpdateObject> m_updateObjectsUI;
        private float m_uiUpdateTime;
        private float m_gameplayUpdateTime;
        private float m_uiTimer;
        private float m_gameplayTimer;

        private void Awake()
        {
            if (Instance != default)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            m_uiUpdateTime = 1f / uiUpdateTime;
            m_gameplayUpdateTime = 1f / gamePlayUpdateTime;
        }

        private void Update()
        {
            var l_deltaTime = Time.deltaTime;
            m_uiTimer += l_deltaTime;
            
            if (m_uiTimer >= m_uiUpdateTime)
            {
                for (var i = 0; i < m_updateObjectsUI.Count; i++)
                {
                    m_updateObjectsUI[i].MyUpdate();
                }
                
                m_uiTimer -= m_uiUpdateTime;
            }

            m_gameplayTimer += l_deltaTime;

            if (m_gameplayTimer >= m_gameplayUpdateTime)
            {
                for (var i = 0; i < m_updateObjectsGamePlay.Count; i++)
                {
                    m_updateObjectsGamePlay[i].MyUpdate();
                }

                m_gameplayTimer -= m_gameplayUpdateTime;
            }
        }

        public void AddListenerGamePlay(IUpdateObject p_updateObject)
        {
            if (m_updateObjectsGamePlay == default)
                m_updateObjectsGamePlay = new List<IUpdateObject>();
            
            if (!m_updateObjectsGamePlay.Contains(p_updateObject))
                m_updateObjectsGamePlay.Add(p_updateObject);
        }

        public void RemoveListenerGamePlay(IUpdateObject p_updateObject)
        {
            if (m_updateObjectsGamePlay.Contains(p_updateObject))
                m_updateObjectsGamePlay.Remove(p_updateObject);

            if (m_updateObjectsGamePlay.Count <= 0)
                m_updateObjectsGamePlay = default;
        }
        
        public void AddListenerUI(IUpdateObject p_updateObject)
        {
            if (m_updateObjectsUI == default)
                m_updateObjectsUI = new List<IUpdateObject>();
            
            if (!m_updateObjectsUI.Contains(p_updateObject))
                m_updateObjectsUI.Add(p_updateObject);
        }

        public void RemoveListenerUI(IUpdateObject p_updateObject)
        {
            if (m_updateObjectsUI.Contains(p_updateObject))
                m_updateObjectsUI.Remove(p_updateObject);
            
            if (m_updateObjectsUI.Count <= 0)
                m_updateObjectsUI = default;
        }
    }
}