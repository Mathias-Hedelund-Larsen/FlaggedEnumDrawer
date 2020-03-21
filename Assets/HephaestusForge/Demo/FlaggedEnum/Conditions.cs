using System;
/// <summary>
/// You can have 31 values with bitshifting and basetype of int in the enum.
/// </summary>
[Flags]
public enum Conditions 
{
    First = 1 << 0,
    Second = 1 << 1,
    Third = 1 << 2,
    Fourth = 1 << 3,
    Fifth = 1 << 4,
    Sixth = 1 << 5,
    Sveneth = 1 << 6,
    Eath = 1 << 7,
    What = 1 << 8,
    Evenr = 1 << 9,
    Names = 1 << 10,
    Dont = 1 << 11,
    Matter = 1 << 12,
    Anymore = 1 << 13,
    Well = 1 << 14,
    Damn = 1 << 15,
    Hi = 1 << 16,
    Hello = 1 << 17,
    A = 1 << 18,
    B = 1 << 19,
    C = 1 << 20,
    D = 1 << 21,
    E = 1 << 22,
    F = 1 << 23,
    G = 1 << 24,
    H = 1 << 25,
    I = 1 << 26,
    J = 1 << 28,
    K = 1 << 29,
    L = 1 << 30
}
