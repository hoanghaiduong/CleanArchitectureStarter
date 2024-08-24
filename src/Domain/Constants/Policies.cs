namespace MyWebApi.Domain.Constants;

public abstract class Policies
{
    //staff required
    public const string Manager = nameof(Manager);
    public const string Receptionist = nameof(Receptionist);
    //End Staff required

    // public const string CanCreate = nameof(CanCreate);
    // public const string CanRead = nameof(CanRead);
    // public const string CanUpdate = nameof(CanUpdate);
    // public const string CanDelete = nameof(CanDelete);
    // public const string FullAccessCRUD = nameof(FullAccessCRUD);

    // public const string Under18 = nameof(Under18);
    // public const string EmailConfirmed = nameof(EmailConfirmed);
}