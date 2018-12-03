using System;
using System.Collections;
using UnityEngine;
using Xunity.Behaviours;
using Xunity.ScriptableReferences;

namespace Xunity.Playables
{
    public abstract class Playable : GameBehaviour, IPlayable
    {
        /// <summary>
        /// Duration of effect in seconds.
        /// </summary>
        [SerializeField] FloatReference duration = FloatReference.New(true, 1);
        [SerializeField] BoolReference playOnEnable = BoolReference.New(true, true);
        [SerializeField] BoolReference isLooping = BoolReference.New(true, true);
        [SerializeField] BoolReference usesFixedUpdate = BoolReference.New(true);

        public event Action StartedPlaying = EmptyAction;
        public event Action StoppedPlaying = EmptyAction;
        public event Action FinishedPlaying = EmptyAction;

        public bool IsPlaying { get; private set; }

        /// <summary>
        /// Duration of effect in seconds.
        /// </summary>
        public float Duration
        {
            get { return duration; }
        }

        public bool IsLooping
        {
            get { return isLooping; }
        }

        public bool UsesFixedUpdate
        {
            get { return usesFixedUpdate; }
        }

        public bool PlayOnEnable
        {
            get { return playOnEnable; }
        }

        public virtual bool CanPlay
        {
            get { return !IsPlaying && ((Component) this).gameObject.activeInHierarchy; }
        }

        public virtual bool CanStop
        {
            get { return IsPlaying && ((Component) this).gameObject.activeInHierarchy; }
        }

        protected YieldInstruction WaitForInstruction
        {
            get { return usesFixedUpdate ? waitForFixedUpdate : waitForEndOfFrame; }
        }

        public bool Play()
        {
            if (!CanPlay)
                return false;

            ForcePlay();
            return true;
        }

        public bool Stop()
        {
            if (!CanStop)
                return false;

            ForceStop();
            return true;
        }

        public virtual bool Play(Vector3 position)
        {
            if (!CanPlay)
                return false;

            Position = position;
            ForcePlay();
            return true;
        }

        protected void ForcePlay()
        {
            IsPlaying = true;
            StartedPlaying();
            StartCoroutine(StartPlaying());
        }

        protected void ForceStop()
        {
            IsPlaying = false;
            StopAllCoroutines();
            StoppedPlaying();
        }

        protected abstract void OnStartPlaying();
        protected abstract void OnPlayUpdate(float progress);
        protected abstract void OnStoppedPlaying();
        protected abstract void OnFinishPlaying();

        protected override void Awake()
        {
            base.Awake();
            StartedPlaying += OnStartPlaying;
            StoppedPlaying += OnStoppedPlaying;
            FinishedPlaying += OnFinishPlaying;
        }

        protected virtual void OnDestroy()
        {
            StartedPlaying -= OnStartPlaying;
            StoppedPlaying -= OnStoppedPlaying;
            FinishedPlaying -= OnFinishPlaying;
        }

        protected virtual void OnEnable()
        {
            if (playOnEnable)
                Play();
        }

        IEnumerator StartPlaying()
        {
            var elapsed = 0f;
            while (elapsed < duration || isLooping)
            {
                elapsed += Time.deltaTime;
                if (isLooping)
                    elapsed %= duration;
                OnPlayUpdate(Mathf.Clamp01(elapsed / duration));
                yield return WaitForInstruction;
            }

            Finish();
        }

        void Finish()
        {
            IsPlaying = false;
            FinishedPlaying();
        }
    }
}