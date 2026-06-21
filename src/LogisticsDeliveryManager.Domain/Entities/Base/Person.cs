using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities.Base;

public abstract class Person : EntityBase
{
    public string Name { get; private set; } = string.Empty;
    public string Document { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;

    protected Person() { }

    public Person(string name, string document, string phoneNumber, Email email)
    {
        Validate(name, document, phoneNumber, email);

        Name = name;
        Document = document;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    private static void Validate(string name, string document, string phoneNumber, Email email)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add("Name cannot be empty.");

        if (string.IsNullOrWhiteSpace(document))
            errors.Add("Document cannot be empty.");

        if (string.IsNullOrWhiteSpace(phoneNumber))
            errors.Add("Phone number cannot be empty.");

        if (email is null)
            errors.Add("Email cannot be null.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }
}
