﻿using System.Reflection;
using System.IO;

using MelonLoader;
using UnityEngine;

using BoneLib.BoneMenu;

using NEP.MonoDirector.Audio;
using NEP.MonoDirector.Cameras;
using NEP.MonoDirector.Core;
using BoneLib.BoneMenu.Elements;
using NEP.MonoDirector.UI;
using NEP.MonoDirector.State;

namespace NEP.MonoDirector
{
    public static class BuildInfo
    {
        public const string Name = "MonoDirector"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "Camera dolly system for Boneworks. Take cool shots and whatnot!"; // Description for the Mod.  (Set as null if none)
        public const string Author = "Not Enough Photons"; // Author of the Mod.  (MUST BE SET)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "0.0.1"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class Main : MelonMod
    {
        internal static MelonLogger.Instance Logger;

        public static Main instance;

        public static Director director;

        public static FreeCamera camera;

        public static FeedbackSFX feedbackSFX;

        public static AssetBundle bundle;

        public override void OnInitializeMelon()
        {
            Logger = new MelonLogger.Instance("MonoDirector", System.ConsoleColor.Magenta);

            instance = this;

            bundle = GetEmbeddedBundle();

            BoneLib.Hooking.OnLevelInitialized += (info) => MonoDirectorInitialize();

            MDMenu.Initialize();
        }

        private void MonoDirectorInitialize()
        {
            ResetInstances();
            CreateCameraManager();
            CreateDirector();
            CreateSFX();
            CreateUI();
        }

        private void ResetInstances()
        {
            Events.FlushActions();
            director = null;
            camera = null;
            feedbackSFX = null;
            PropMarkerManager.CleanUp();
        }

        private void CreateCameraManager()
        {
            new CameraRigManager();
        }

        private void CreateDirector()
        {
            GameObject directorObject = new GameObject("Director");
            director = directorObject.AddComponent<Director>();
            director.SetCamera(camera);
        }

        private void CreateSFX()
        {
            GameObject feedback = new GameObject("Feedback SFX");
            feedbackSFX = feedback.AddComponent<FeedbackSFX>();
        }

        private void CreateUI()
        {
            PropMarkerManager.Initialize();
            InfoInterfaceManager.Initialize();
        }

        private static AssetBundle GetEmbeddedBundle()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string fileName = "md_resources.pack";

            using (Stream resourceStream = assembly.GetManifestResourceStream("NEP.MonoDirector.Resources." + fileName))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    resourceStream.CopyTo(memoryStream);
                    return AssetBundle.LoadFromMemory(memoryStream.ToArray());
                }
            }
        }

        
    }
}