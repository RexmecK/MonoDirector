using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;
using System;

namespace NEP.MonoDirector.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class MenuUI : MonoBehaviour
    {
        public MenuUI(System.IntPtr ptr) : base(ptr) { }

        public static MenuUI Instance { get; private set; }

        public MenuPageView MenuPageView => menuPage.GetComponent<MenuPageView>();
        public CasterPageView CasterPageView => castPage.GetComponent<CasterPageView>();
        public ActorSettingsView ActorSettingsView => actorSettingsPage.GetComponent<ActorSettingsView>();

        public string LastPage { get; private set; }
        public string CurrentPage { get; private set; }

        private Page menuPage;
        private Page castPage;
        private Page castListPage;
        private Page playheadPage;
        private Page actorSettingsPage;
        private Page settingsPage;

        private Transform footer => transform.Find("Footer");
        private Transform pageList => transform.Find("Content/Pages");

        private TextMeshProUGUI pageText => footer.Find("PageText").GetComponent<TextMeshProUGUI>();
        private Button previousPageButton => footer.Find("PreviousPage").GetComponent<Button>();
        private Button nextPageButton => footer.Find("NextPage").GetComponent<Button>();
        private Button goBackButton => footer.Find("GoBack").GetComponent<Button>();

        private bool traversed;

        private void Awake()
        {
            Instance = this;

            previousPageButton.onClick.AddListener(new System.Action(() => CasterPageView.PreviousPage()));
            nextPageButton.onClick.AddListener(new System.Action(() => CasterPageView.NextPage()));
            goBackButton.onClick.AddListener(new System.Action(() => SetPage(LastPage)));

            ActorSettingsView.gameObject.SetActive(false);

            gameObject.SetActive(false);

            menuPage = new Page("Menu", pageList.transform.GetChild(0));
            castPage = new Page("Cast", pageList.transform.GetChild(1));
            playheadPage = new Page("Playhead", pageList.transform.GetChild(2));
            settingsPage = new Page("Settings", pageList.transform.GetChild(3));
        }

        private void Start()
        {
            SetPage("Menu");
            gameObject.SetActive(false);
        }

        private void Update()
        {
            var physicsRig = Constants.rigManager.physicsRig;

            transform.position = Vector3.Lerp(transform.position, physicsRig.m_chest.position + physicsRig.m_chest.forward * 1.25f, 16f * Time.deltaTime);
            transform.LookAt(physicsRig.m_chest);
        }

        public void UpdatePageCounter(int currentPage, int maxPages)
        {
            pageText.text = $"Page {currentPage} / {maxPages}";
        }

        public void SetPage(string page)
        {
            if (page == "Menu")
            {
                menuPage.SetActive(true);
                castPage.SetActive(false);
                castListPage.SetActive(false);
                playheadPage.SetActive(false);
                actorSettingsPage.SetActive(false);
                settingsPage.SetActive(false);

                pageText.gameObject.SetActive(false);
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(false);
                goBackButton.gameObject.SetActive(true);

                LastPage = "Menu";
            }

            if (page == "Cast")
            {
                menuPage.SetActive(false);
                castPage.SetActive(true);
                castListPage.SetActive(true);
                playheadPage.SetActive(false);
                actorSettingsPage.SetActive(false);
                settingsPage.SetActive(false);

                pageText.gameObject.SetActive(true);
                previousPageButton.gameObject.SetActive(true);
                nextPageButton.gameObject.SetActive(true);
                goBackButton.gameObject.SetActive(true);

                LastPage = "Menu";
            }

            if(page == "Playhead")
            {
                menuPage.SetActive(false);
                castPage.SetActive(false);
                castListPage.SetActive(false);
                playheadPage.SetActive(true);
                actorSettingsPage.SetActive(false);
                settingsPage.SetActive(false);

                pageText.gameObject.SetActive(false);
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(false);
                goBackButton.gameObject.SetActive(true);

                LastPage = "Menu";
            }

            if (page == "Settings")
            {
                menuPage.SetActive(false);
                castPage.SetActive(false);
                castListPage.SetActive(false);
                playheadPage.SetActive(false);
                actorSettingsPage.SetActive(false);
                settingsPage.SetActive(true);

                pageText.gameObject.SetActive(false);
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(false);
                goBackButton.gameObject.SetActive(true);

                LastPage = "Menu";
            }

            if (page == "ActorSettings")
            {
                menuPage.SetActive(false);
                castPage.SetActive(true);
                castListPage.SetActive(false);
                playheadPage.SetActive(false);
                actorSettingsPage.SetActive(true);
                settingsPage.SetActive(false);

                pageText.gameObject.SetActive(false);
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(false);
                goBackButton.gameObject.SetActive(true);

                LastPage = "Cast";
            }

            if (page == "Settings/Option")
            {
                LastPage = "Settings";
            }

            Main.Logger.Msg(LastPage);
        }

        public void DisplayMenu(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}