using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Simple interface that ensures an OnPossession method is implemented
    to be called right after possession (Mostly for animations or post op stuff)
 */
public interface IOnPossessionController
{
    void OnPossession();
}
