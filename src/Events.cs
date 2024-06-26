﻿using NEP.MonoDirector.Actors;
using NEP.MonoDirector.State;

using System;

namespace NEP.MonoDirector
{
    public static class Events
    {
        public static void FlushActions()
        {
            OnActorCasted = null;

            OnSessionBegin = null;
            OnSessionEnd = null;

            OnPrePlayback = null;
            OnPreRecord = null;

            OnPlay = null;
            OnPause = null;
            OnStopPlayback = null;

            OnStartRecording = null;
            OnStopRecording = null;

            OnPlaybackTick = null;
            OnRecordTick = null;

            OnPreSnapshot = null;

            OnPrePhotograph = null;
            OnPostPhotograph = null;
            OnPhotograph = null;

            OnPlayStateSet = null;
            OnCameraModeSet = null;

            OnTimerCountdown = null;
        }

        public static Action<Actor> OnActorCasted;
        public static Action<Actor> OnActorUncasted;

        public static Action<Prop> OnPropCreated;
        public static Action<Prop> OnPropRemoved;

        public static Action OnSessionBegin;
        public static Action OnSessionEnd;

        public static Action OnPrePlayback;
        public static Action OnPreRecord;

        public static Action OnPlay;
        public static Action OnPause;
        public static Action OnStopPlayback;

        public static Action OnStartRecording;
        public static Action OnStopRecording;

        public static Action OnPlaybackTick;
        public static Action OnRecordTick;

        public static Action OnPrePhotograph;
        public static Action OnPhotograph;
        public static Action OnPostPhotograph;

        public static Action OnTimerCountdown;

        public static Action<PlayState> OnPlayStateSet;
        public static Action<CameraMode> OnCameraModeSet;

        public static Action OnPreSnapshot;
    }
}
