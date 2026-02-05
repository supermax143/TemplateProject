using System;
using System.Linq;
using Common.Localization;
using Common.Session;
using TMPro;
using UnityEngine;
using Zenject;

namespace View
{
	public class MainMenu : MonoBehaviour
	{
		[Inject] private Session _session;
		[Inject] private ILocalization _localization;
		
		[SerializeField]
		private TMP_Dropdown _languageSelector;

		private void Start()
		{
			_languageSelector.options = _localization.LanguageCodes.
				Select(code => new TMP_Dropdown.OptionData { text = code } ).ToList();
			_languageSelector.onValueChanged.AddListener(OnLangugeChanged);
		}

		private void OnLangugeChanged(int value)
		{
			 _localization.SetLanguage(_languageSelector.options[value].text);
		}

		public void StartGame()
		{
			_session.CurrentState.StartGame();
		}
	}
}