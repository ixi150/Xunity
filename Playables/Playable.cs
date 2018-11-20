using System;
using System.Collections;
using UnityEngine;
using Xunity.ReferenceVariables;

namespace Xunity.Playables
{
    public abstract class Playable : GameBehaviour, IPlayable
    {
        [Header("Playable"), Tooltip("In seconds")] [SerializeField]
        FloatReference duration;

        [SerializeField] BoolReference playOnEnable;
        [SerializeField] BoolReference isLooping;

        public event Action StartedPlaying = () => { };
        public event Action StoppedPlaying = () => { };
        public event Action FinishedPlaying = () => { };

        public bool IsPlaying { get; private set; }

        public float Duration
        {
            get { return duration; }
        }

        public bool IsLooping
        {
            get { return isLooping; }
        }

        public bool PlayOnEnable
        {
            get { return playOnEnable; }
        }

        public virtual bool CanPlay
        {
            get { return !IsPlaying && gameObject.activeInHierarchy; }
        }

        public virtual bool CanStop
        {
            get { return IsPlaying && gameObject.activeInHierarchy; }
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
            StartCoroutine(StartPlaying());
            StartedPlaying();
        }

        protected void ForceStop()
        {
            IsPlaying = false;
            StopAllCoroutines();
            StoppedPlaying();
        }

        protected virtual void OnStartPlaying() { }
        protected virtual void OnStoppedPlaying() { }
        protected virtual void OnFinishPlaying() { }
        protected virtual void OnPlayUpdate(float progress) { }

        protected override void Awake()
        {
            base.Awake();
            StartedPlaying += OnStartPlaying;
            StoppedPlaying += OnStoppedPlaying;
            FinishedPlaying += OnFinishPlaying;
        }

        protected virtual void OnEnable()
        {
            if (playOnEnable)
                Play();
        }

        protected virtual void OnDestroy()
        {
            StartedPlaying -= OnStartPlaying;
            StoppedPlaying -= OnStoppedPlaying;
            FinishedPlaying -= OnFinishPlaying;
        }

        IEnumerator StartPlaying()
        {
            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                if (isLooping)
                    elapsed %= duration;
                OnPlayUpdate(Mathf.Clamp01(elapsed / duration));
                yield return null;
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