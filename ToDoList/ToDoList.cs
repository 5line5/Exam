using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList
{
    class UnknownActionException: Exception { }

    public class User
    {
        public int Id { get; }
        public bool IsAllowed { get; private set; } = true;
        
        public HashSet<int> UserEntries { get; } = new HashSet<int>();

        public User(int id) => Id = id;

        public void SetAllowance(bool allowance) => IsAllowed = allowance;
    }

    public enum OperationsType
    {
        Create = 0,
        Done = 1,
        Undone = 2,
        Remove = 3
    }

    public struct UserAction<T>
    {
        public User User { get; }
        public int EntryId { get; }
        public OperationsType OperationsType { get; }
        public long Timestamp { get; }
        public Func<T, T> Action { get; }

        public UserAction(User user, int entryId, OperationsType operationType, 
            long timestamp, Func<T, T> action)
        {
            User = user;
            EntryId = entryId;
            OperationsType = operationType;
            Timestamp = timestamp;
            Action = action;
        }
    }

    public interface IHistory<TKey, TValue>
    {
        void MergeBranch(int branchId);

        void AddAction(UserAction<TValue> userAction);

        int ElementsCount();

        Dictionary<TKey, TValue> GetActualTasks();
    }

    public class EntryHistory: IHistory<int, Entry>
    {
        private readonly Dictionary<int, Entry> _actualEntries = new Dictionary<int, Entry>();
        private readonly Dictionary<int, Entry> _removedEntries = new Dictionary<int, Entry>();
        private readonly Dictionary<int, List<UserAction<Entry>>> _history 
            = new Dictionary<int, List<UserAction<Entry>>>();

        public void MergeBranch(int branchId)
        {
            RemoveCurrentTask(branchId);
            _history[branchId].ForEach(MergeActions);
        }
        
        public void AddAction(UserAction<Entry> userAction)
        {
            InitializeBranch(userAction.EntryId);
            _history[userAction.EntryId].Add(userAction);
            SortBranchByPriority(userAction.EntryId);
            MergeBranch(userAction.EntryId);
        }
        
        public int ElementsCount() => _actualEntries.Count;

        public Dictionary<int, Entry> GetActualTasks() => _actualEntries;
        
        private void MergeActions(UserAction<Entry> action)
        {
            if (!action.User.IsAllowed)
                return;
            
            switch (action.OperationsType)
            {
                case OperationsType.Create:
                    CreateActionHandler(action);
                    break;
                
                case OperationsType.Done:
                    ChangeStateActionHandler(action);
                    break;
                
                case OperationsType.Undone:
                    ChangeStateActionHandler(action);
                    break;
                    
                case OperationsType.Remove:
                    RemoveActionHandler(action.EntryId);
                    break;
                
                default:
                    throw new UnknownActionException();
            }
        }

        private void CreateActionHandler(UserAction<Entry> userAction)
        {
            if (_actualEntries.ContainsKey(userAction.EntryId))
            {
                var newEntry = userAction.Action(_actualEntries[userAction.EntryId]);
                _actualEntries[userAction.EntryId] = newEntry;
            }
            else if (_removedEntries.ContainsKey(userAction.EntryId))
            {
                var newEntry = userAction.Action(_removedEntries[userAction.EntryId]);
                _actualEntries[userAction.EntryId] = newEntry;
                _removedEntries.Remove(userAction.EntryId);
            }
            else
            {
                var newEntry = userAction.Action(Entry.Undone(userAction.EntryId, string.Empty));
                _actualEntries.Add(userAction.EntryId, newEntry);
            }
        }

        private void ChangeStateActionHandler(UserAction<Entry> userAction)
        {
            if (_actualEntries.ContainsKey(userAction.EntryId))
            {
                var newEntry = userAction.Action(_actualEntries[userAction.EntryId]);
                _actualEntries[userAction.EntryId] = newEntry;
            }
            else if (_removedEntries.ContainsKey(userAction.EntryId))
            {
                var newEntry = userAction.Action(_removedEntries[userAction.EntryId]);
                _removedEntries[userAction.EntryId] = newEntry;
            }
            else
            {
                var newEntry = userAction.Action(Entry.Undone(userAction.EntryId, string.Empty));
                _removedEntries.Add(userAction.EntryId, newEntry);
            }
        }

        private void RemoveActionHandler(int entryId)
        {
            if (!_actualEntries.ContainsKey(entryId)) return;
            
            _removedEntries.Add(entryId, _actualEntries[entryId]);
            _actualEntries.Remove(entryId); 
        }

        private void InitializeBranch(int entryId)
        {
            if (!_history.ContainsKey(entryId))
                _history.Add(entryId, new List<UserAction<Entry>>());
        }

        private void RemoveCurrentTask(int entryId)
        {
            if (_actualEntries.ContainsKey(entryId))
                _actualEntries.Remove(entryId);

            if (_removedEntries.ContainsKey(entryId))
                _removedEntries.Remove(entryId);
        }

        private void SortBranchByPriority(int entryId)
        {
            _history[entryId] = _history[entryId]
                .OrderBy(x => x.Timestamp)
                .ThenBy(x => x.OperationsType)
                .ThenByDescending(x => x.User.Id)
                .ToList();
        }
    }

    public class ToDoList : IToDoList
    {
        private readonly Dictionary<int, User> _users = new Dictionary<int, User>();
        private readonly IHistory<int, Entry> _history = new EntryHistory();

        public void AddEntry(int entryId, int userId, string name, long timestamp)
        {
            var action = new Func<Entry, Entry>(entry => new Entry(entryId, name, entry.State));
            AddUserActionInHistory(userId, entryId, OperationsType.Create, timestamp, action);
        }

        public void RemoveEntry(int entryId, int userId, long timestamp) 
            => AddUserActionInHistory(userId, entryId, OperationsType.Remove, timestamp, null);

        public void MarkDone(int entryId, int userId, long timestamp)
        {
            var action = new Func<Entry, Entry>(entry => entry.MarkDone());
            AddUserActionInHistory(userId, entryId, OperationsType.Done, timestamp, action);
        }

        public void MarkUndone(int entryId, int userId, long timestamp)
        {
            var action = new Func<Entry, Entry>(entry => entry.MarkUndone());
            AddUserActionInHistory(userId, entryId, OperationsType.Undone, timestamp, action);
        }
        
        public void DismissUser(int userId) => ChangeUserAllowance(userId, false);

        public void AllowUser(int userId) => ChangeUserAllowance(userId, true);

        private void AddUserActionInHistory(int userId, int entryId, OperationsType type, 
            long timestamp, Func<Entry, Entry> action)
        {
            AddUserIfNotExist(userId);
            _users[userId].UserEntries.Add(entryId);
            var userAction = new UserAction<Entry>(_users[userId], entryId, type, timestamp, action);
            _history.AddAction(userAction);
        }

        private void ChangeUserAllowance(int userId, bool allowance)
        {
            AddUserIfNotExist(userId);
            _users[userId].SetAllowance(allowance);
            _users[userId].UserEntries
                                     .ToList()
                                     .ForEach(_history.MergeBranch);
        }

        private void AddUserIfNotExist(int userId)
        {
            if (!_users.ContainsKey(userId))
                _users[userId] = new User(userId);
        }
        
        public IEnumerator<Entry> GetEnumerator() => _history.GetActualTasks().Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public int Count => _history.ElementsCount();
    }
}