namespace Client.Listeners
{
    public interface IOnBarcodeReadListener
    {
        void OnBarcodeRead(string data);
    }
}