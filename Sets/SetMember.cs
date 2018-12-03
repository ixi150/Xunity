using System.Collections.Generic;
using UnityEngine;
using Xunity.Extensions;
using Xunity.Morphables;

namespace Xunity.Sets
{
    public class SetMember : MonoBehaviour
    {
        [SerializeField] SetMorph sets;
        [SerializeField] MembershipScope membershipScope;

        enum MembershipScope
        {
            WhileActive = 0,
            WhileExists = 1,
        }

        Transform t;

        public IEnumerable<SetCollection> Sets
        {
            get { return sets.NotNull(); }
        }

        void Awake()
        {
            t = transform;
            if (membershipScope == MembershipScope.WhileExists)
                Add();
        }

        void OnDestroy()
        {
            if (membershipScope == MembershipScope.WhileExists)
                Remove();
        }

        void OnEnable()
        {
            if (membershipScope == MembershipScope.WhileActive)
                Add();
        }

        void OnDisable()
        {
            if (membershipScope == MembershipScope.WhileActive)
                Remove();
        }

        void Add()
        {
            foreach (var set in Sets)
                set.Add(t);
        }

        void Remove()
        {
            foreach (var set in Sets)
                set.Remove(t);
        }
    }
}