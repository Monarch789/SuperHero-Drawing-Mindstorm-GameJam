using System;

public interface IHasProgress{

    public event EventHandler<OnProgressChangeEventAgs> OnProgressChanged;

    public class OnProgressChangeEventAgs : EventArgs {
        public float progressAmount;
    }

}
