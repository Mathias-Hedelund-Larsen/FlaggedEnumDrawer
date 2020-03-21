using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HephaestusForge.FlaggedEnum
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private NonFlaggedEnum _non;

        [SerializeField]
        private Conditions _flags;

        [NonSerialized] //Unity doesnt like another underlying value than int wtf?
        private FlaggedEnumValue _flagsTwo;

        private void Awake()
        {
            var index = Conditions.L;

            Debug.Log(index);

            _flags = _flags.Add(Conditions.Fifth);

            _flags.ForEachContained(e => Debug.Log(e));

            index.ForEach(c =>
            {
                index |= c;

                Debug.Log(index);

                Debug.Log((int)index);
            });
        }
    }
}