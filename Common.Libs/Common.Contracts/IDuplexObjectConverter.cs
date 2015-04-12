namespace Common.Contracts
{
    public interface IDuplexObjectConverter<TInterface1, TInterface2>
    {
        TInterface1 Convert(TInterface2 interface2);

        TInterface2 Convert(TInterface1 interface1);
    }
}