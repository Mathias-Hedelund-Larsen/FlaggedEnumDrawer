﻿using System;

[Flags]
public enum FlaggedEnumValue : long
{
    First = 1 << 0,
    Second = 1 << 1,
    Third = 1 << 2,
    Fourth = 1 << 3,
    Fifth = 1 << 4
}
