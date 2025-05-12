namespace PedaloWebApp.Core.Application.Translations
{
    /// <summary>
    /// TranslationCodeId enumeration to localize the application with JSON resource files.
    /// </summary>
    /// <remarks>
    /// A prefix is required for all TranslationCodeIds, followed by an underscore.
    /// Link => Text for Link
    /// Label => For example for Field Label
    /// Placeholder => HTML input attribute "placeholder"
    /// Text => generic text
    /// Error => Error message
    /// Info => Info message
    /// Button => Button Text
    /// Title => Text of title element h1,h2,h3
    /// Nav => For navigation in the menu
    /// </remarks>
    public enum TranslationCodeId
    {
        // Buttons
        Button_Back,
        Button_Save,
        Button_DownloadPdf,

        // Errors
        Error_InputRequired,
        Error_LastnameRequired,

        // Success
        RecordDeleteSuccess,

        // Labels
        Label_CustomerId,
        Label_PedaloId,
        Label_BookingId,
        Label_DataSavedSuccess,
        Label_DateOfBirth,
        Label_Firstname,
        Label_Lastname,
        Label_Settings,
        Label_SwitchLanguage,
        Label_Name,
        Label_Color,
        Label_Capacity,
        Label_HourlyRate,
        Label_PedaloName,
        Label_CustomerName,
        Label_BookingStartDate,
        Label_BookingEndDate,

        // Navigations
        Nav_Customers,
        Nav_Pedalos,
        Nav_Bookings,

        // Texts
        Text_CustomersIntro,
        Text_PedalosIntro,
        Text_BookingsIntro,
        Text_ConfirmDeleteEntry,

        // Titles
        Title_Customers,
        Title_Pedalos,
        Title_Bookings,
        Title_Customer_Edit,
        Title_Customer_New,
        Title_DeleteEntry,
    }
}
