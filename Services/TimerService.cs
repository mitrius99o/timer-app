using TimerApp.Models;
using System.Timers;

namespace TimerApp.Services
{
    public class TimerService
    {
        private readonly Dictionary<int, TimerModel> _timers = new();
        private readonly Dictionary<int, System.Timers.Timer> _systemTimers = new();
        private int _nextId = 1;
        private readonly object _lock = new();

        public List<TimerResponse> GetAllTimers()
        {
            lock (_lock)
            {
                return _timers.Values.Select(t => MapToResponse(t)).ToList();
            }
        }

        public TimerResponse? GetTimer(int id)
        {
            lock (_lock)
            {
                return _timers.TryGetValue(id, out var timer) ? MapToResponse(timer) : null;
            }
        }

        public TimerResponse CreateTimer(TimerRequest request)
        {
            lock (_lock)
            {
                var duration = new TimeSpan(request.Hours, request.Minutes, request.Seconds);
                var timer = new TimerModel
                {
                    Id = _nextId++,
                    Name = request.Name,
                    Duration = duration,
                    RemainingTime = duration,
                    IsRunning = false,
                    IsCompleted = false
                };

                _timers[timer.Id] = timer;
                return MapToResponse(timer);
            }
        }

        public TimerResponse? StartTimer(int id)
        {
            lock (_lock)
            {
                if (!_timers.TryGetValue(id, out var timer) || timer.IsCompleted)
                    return null;

                if (timer.IsRunning)
                    return MapToResponse(timer);

                timer.IsRunning = true;
                timer.StartTime = DateTime.Now;
                timer.EndTime = timer.StartTime + timer.RemainingTime;

                var systemTimer = new System.Timers.Timer(1000); // Обновление каждую секунду
                systemTimer.Elapsed += (sender, e) => UpdateTimer(id);
                systemTimer.Start();

                _systemTimers[id] = systemTimer;

                return MapToResponse(timer);
            }
        }

        public TimerResponse? StopTimer(int id)
        {
            lock (_lock)
            {
                if (!_timers.TryGetValue(id, out var timer))
                    return null;

                if (!timer.IsRunning)
                    return MapToResponse(timer);

                timer.IsRunning = false;
                timer.StartTime = null;
                timer.EndTime = null;

                if (_systemTimers.TryGetValue(id, out var systemTimer))
                {
                    systemTimer.Stop();
                    systemTimer.Dispose();
                    _systemTimers.Remove(id);
                }

                return MapToResponse(timer);
            }
        }

        public TimerResponse? ResetTimer(int id)
        {
            lock (_lock)
            {
                if (!_timers.TryGetValue(id, out var timer))
                    return null;

                StopTimer(id);
                timer.RemainingTime = timer.Duration;
                timer.IsCompleted = false;

                return MapToResponse(timer);
            }
        }

        public TimerResponse? UpdateTimer(int id, TimerRequest request)
        {
            lock (_lock)
            {
                if (!_timers.TryGetValue(id, out var timer))
                    return null;

                var wasRunning = timer.IsRunning;
                if (wasRunning)
                    StopTimer(id);

                var newDuration = new TimeSpan(request.Hours, request.Minutes, request.Seconds);
                timer.Name = request.Name;
                timer.Duration = newDuration;
                timer.RemainingTime = newDuration;
                timer.IsCompleted = false;

                if (wasRunning)
                    StartTimer(id);

                return MapToResponse(timer);
            }
        }

        public bool DeleteTimer(int id)
        {
            lock (_lock)
            {
                if (!_timers.ContainsKey(id))
                    return false;

                StopTimer(id);
                _timers.Remove(id);
                return true;
            }
        }

        private void UpdateTimer(int id)
        {
            lock (_lock)
            {
                if (!_timers.TryGetValue(id, out var timer) || !timer.IsRunning)
                    return;

                var now = DateTime.Now;
                if (timer.EndTime.HasValue && now >= timer.EndTime.Value)
                {
                    // Таймер завершен
                    timer.IsRunning = false;
                    timer.IsCompleted = true;
                    timer.RemainingTime = TimeSpan.Zero;
                    timer.StartTime = null;
                    timer.EndTime = null;

                    if (_systemTimers.TryGetValue(id, out var systemTimer))
                    {
                        systemTimer.Stop();
                        systemTimer.Dispose();
                        _systemTimers.Remove(id);
                    }
                }
                else if (timer.EndTime.HasValue)
                {
                    // Обновляем оставшееся время
                    timer.RemainingTime = timer.EndTime.Value - now;
                }
            }
        }

        private TimerResponse MapToResponse(TimerModel timer)
        {
            string status;
            if (timer.IsCompleted)
                status = "Завершен";
            else if (timer.IsRunning)
                status = "Работает";
            else
                status = "Остановлен";

            return new TimerResponse
            {
                Id = timer.Id,
                Name = timer.Name,
                Duration = timer.Duration,
                RemainingTime = timer.RemainingTime,
                IsRunning = timer.IsRunning,
                IsCompleted = timer.IsCompleted,
                Status = status
            };
        }
    }
} 