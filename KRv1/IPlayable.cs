using System;

namespace KRv1;

public interface IPlayable
{
    bool IsPlayable { get; set; }
    public bool Playable();
}