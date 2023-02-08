﻿using UnityEngine;

using NEP.MonoDirector.Cameras;
using NEP.MonoDirector.State;

using System.Collections.Generic;
using NEP.MonoDirector.Actors;

namespace NEP.MonoDirector.Core
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Director : MonoBehaviour
    {
        public Director(System.IntPtr ptr) : base(ptr) { }

        public static Director instance { get; private set; }

        public Playback playback;
        public Recorder recorder;

        public static PlayState PlayState { get => playState; }
        public static CaptureState CaptureState { get => captureState; }

        public List<Actor> Cast;
        public List<ActorProp> WorldProps;
        public List<ActorProp> RecordedProps;

        public int WorldTick { get => worldTick; }
        public int CurrentTick { get => currentTick; }
        public int RecordedTicks { get => recordedTicks; }

        private static PlayState playState;
        private static CaptureState captureState;

        private CameraRig camera;

        private Coroutine playRoutine;
        private Coroutine recordRoutine;

        

        private int worldTick;
        private int currentTick;
        private int recordedTicks = 0;

        private void Awake()
        {
            instance = this;

            playback = new Playback();
            recorder = new Recorder();

            Cast = new List<Actor>();
            WorldProps = new List<ActorProp>();
            RecordedProps = new List<ActorProp>();
        }

        private void Start()
        {
            Events.OnPrePlayback += playback.OnPrePlayback;
            Events.OnPreRecord += recorder.OnPreRecord;

            Events.OnPlaybackTick += playback.OnPlaybackTick;
            Events.OnRecordTick += recorder.OnRecordTick;

            Events.OnStopPlayback += playback.OnStopPlayback;
            Events.OnStopRecording += recorder.OnStopRecording;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                Record();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Play();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Stop();
            }
        }

        public void Play()
        {
            if (playRoutine == null)
            {
                playRoutine = MelonLoader.MelonCoroutines.Start(playback.PlayRoutine()) as Coroutine;
            }
        }

        public void Pause()
        {

        }

        public void Record()
        {
            if (recordRoutine == null)
            {
                recordRoutine = MelonLoader.MelonCoroutines.Start(recorder.RecordRoutine()) as Coroutine;
            }
        }

        public void Stop()
        {
            playState = PlayState.Stopped;
        }

        public void SetCamera(CameraRig camera)
        {
            this.camera = camera;
        }

        public void RemoveActor(Actor actor)
        {
            for(int i = 0; i < Cast.Count; i++)
            {
                var castMember = Cast[i];

                if (castMember == actor)
                {
                    castMember.Delete();
                    Cast.Remove(actor);
                    return;
                }
            }
        }

        public void RemoveAllActors()
        {
            playState = PlayState.Stopped;

            for (int i = 0; i < Cast.Count; i++)
            {
                Cast[i].Delete();
            }

            Cast.Clear();
            recordedTicks = 0;
            worldTick = 0;
            currentTick = 0;
        }
        
        public void ClearScene()
        {
            RemoveAllActors();
            
            foreach(var prop in WorldProps)
            {
                prop.InteractableRigidbody.isKinematic = false;
                GameObject.Destroy(prop);
            }

            WorldProps.Clear();
        }
    }
}
   