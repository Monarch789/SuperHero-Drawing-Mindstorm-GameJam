using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress{

    public event EventHandler<OnProgressChangeEventAgs> OnProgressChanged;

    public class OnProgressChangeEventAgs : EventArgs {
        public float progressAmount;
    }

}
