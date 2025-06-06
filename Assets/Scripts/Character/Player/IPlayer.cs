﻿using UnityEngine;

public interface IPlayer
{
    Transform Transform { get; }
    IHealth Health { get; }
    IResurrectable Resurrectable { get; }
    IGunPlace GunPlace { get; }
}