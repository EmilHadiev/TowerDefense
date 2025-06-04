using UnityEngine;

public interface IGunFactory
{
    Gun Create(Gun prefab);
}