namespace Convert_Procedure_To_Rules_Engine.ObjectVersion
{
    public interface IProcessStep
    {
        bool Execute(CreditAuthorizationContext context);
    }
}
