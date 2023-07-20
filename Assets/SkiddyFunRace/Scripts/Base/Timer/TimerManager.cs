using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using GenBase.Utils;

namespace GenBase.Timer
{
    /// <summary>
    ///     Manages updating all the <see cref="Timer" />s that are running in the application.
    ///     This will be instantiated the first time you create a timer -- you do not need to add it into the
    ///     scene manually.
    /// </summary>
    public class TimerManager : SingletonMonoBehaviour<TimerManager>
    {
        private List<Timer> _timers = new List<Timer>();

        // buffer adding timers so we don't edit a collection during iteration
        private List<Timer> _timersToAdd = new List<Timer>();

        // update all the registered timers on every frame
        [UsedImplicitly]
        private void Update()
        {
            UpdateAllTimers();
        }

        public void RegisterTimer(Timer timer)
        {
            _timersToAdd.Add(timer);
        }

        public void CancelAllTimers()
        {
            foreach (var timer in _timers) timer.Cancel();

            _timers = new List<Timer>();
            _timersToAdd = new List<Timer>();
        }

        public void PauseAllTimers()
        {
            foreach (var timer in _timers) timer.Pause();
        }

        public void ResumeAllTimers()
        {
            foreach (var timer in _timers) timer.Resume();
        }

        private void UpdateAllTimers()
        {
            if (_timersToAdd.Count > 0)
            {
                _timers.AddRange(_timersToAdd);
                _timersToAdd.Clear();
            }

            foreach (var timer in _timers) timer.Update();

            _timers.RemoveAll(t => t.IsDone);
        }

        private Timer GetTimerByKey(string key)
        {
            return _timersToAdd.Find(t => t.Key.Equals(key));
        }

        public void AddListener(string key, Action onComplete, Action<float> onUpdate = null)
        {
            var timer = GetTimerByKey(key);

            if (timer == null)
                return;

            if (onComplete != null)
                timer.AddCompleteEvent(onComplete);

            if (onUpdate != null)
                timer.AddUpdateEvent(onUpdate);
        }

        public void RemoveListener(string key, Action onComplete, Action<float> onUpdate = null)
        {
            var timer = GetTimerByKey(key);

            if (timer == null)
                return;

            if (onComplete != null)
                timer.RemoveCompleteEvent(onComplete);

            if (onUpdate != null)
                timer.RemoveUpdateEvent(onUpdate);
        }
    }
}