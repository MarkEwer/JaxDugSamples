namespace Convert_Procedure_To_Rules_Engine.ObjectVersion
{
    public abstract class Step
    {
        protected IGateway _gateway;
        protected Step(IGateway gateway)
        {
            this._gateway = gateway;
        }
    }
}
