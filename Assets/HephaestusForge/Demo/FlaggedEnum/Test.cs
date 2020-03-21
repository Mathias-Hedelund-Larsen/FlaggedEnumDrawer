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
            int index = (Conditions.Second | Conditions.Fifth).GetEnumValueIndex();

            Debug.Log(index);

            _flags = _flags.Add(Conditions.Fifth);

            _flags.ForEach(e => Debug.Log(e));
        }
    }
}