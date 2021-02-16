﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Model.Managers;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        public TextMeshProUGUI text;

        // Start is called before the first frame update

        private void Awake()
        {
            text.text = $"골드: {GameManager.Instance.Gold}                  스테이지: {GameManager.Instance.stage}";
        }

        public void EnableMenu()
        {
            Debug.Log("Menu Enabled");
        }
    }
}