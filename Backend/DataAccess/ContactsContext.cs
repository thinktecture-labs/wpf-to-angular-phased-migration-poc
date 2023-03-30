using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Bogus;
using Serilog;

namespace Backend.DataAccess;

public sealed class ContactsContext
{
    private const string ContactsFilePath = "contacts.json";

    public ContactsContext(List<Contact> contacts, ILogger logger)
    {
        Contacts = contacts;
        ContactsLookup = contacts.ToDictionary(c => c.Id);
        Logger = logger;
    }

    private static JsonSerializerOptions JsonOptions { get; } =
        new ()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

    public List<Contact> Contacts { get; }
    public Dictionary<Guid, Contact> ContactsLookup { get; }
    private ILogger Logger { get; }
    public bool IsDirty { get; private set; }

    public void SaveChanges() => IsDirty = true;
    
    public static ContactsContext CreateDefault(ILogger logger,
                                                Faker<Contact> contactFaker)
    {
        var contacts = GetContactsFromFile(logger);
        if (contacts == null)
        {
            contacts = contactFaker.Generate(250);
            logger.Information("Contacts generated");
        }

        return new (contacts, logger);
    }

    private static List<Contact>? GetContactsFromFile(ILogger logger)
    {
        try
        {
            if (!File.Exists(ContactsFilePath))
                return null;

            using var stream = new FileStream(ContactsFilePath, FileMode.Open, FileAccess.Read);
            var contacts = JsonSerializer.Deserialize<List<Contact>>(stream, JsonOptions);
            logger.Information("Contacts loaded from {FilePath}", ContactsFilePath);
            return contacts;

        }
        catch (Exception exception)
        {
            logger.Warning(exception, "Could not load contacts from file");
            return null;
        }
    }

    public void WriteContactsToFileIfNecessary()
    {
        if (!IsDirty)
        {
            Logger.Information("Contacts did not change, saving them to {FilePath} is not necessary", ContactsFilePath);
            return;
        }

        try
        {
            using var stream = new FileStream(ContactsFilePath, FileMode.Create);
            JsonSerializer.Serialize(stream, Contacts, JsonOptions);
            Logger.Information("Contacts written to {FilePath}", ContactsFilePath);
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not save contacts");
        }
    }

    public void AddContact(Contact contact)
    {
        Contacts.Add(contact);
        ContactsLookup.Add(contact.Id, contact);
    }

    public void RemoveContact(Contact contact)
    {
        ContactsLookup.Remove(contact.Id);
        Contacts.Remove(contact);
    }
}