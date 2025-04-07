namespace Pedalo.Core.Application.Translations
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
        Button_RequestResetEmail,
        Button_Login,
        Button_Logout,
        Button_Save,
        Button_DownloadPdf,

        // Errors
        Error_InputRequired,
        Error_UsernameRequired,
        Error_PasswordRequired,
        Error_TenantNameRequired,
        Error_TenantIdRequired,
        Error_EmailRequired,
        Error_FirstnameRequired,
        Error_LastnameRequired,
        Error_UserClaimsRequired,
        Error_AccountPasswordResetIdRequired,
        Error_NewPasswordRequired,
        Error_NewPasswordConfirmationRequired,
        Error_PasswordsAreNotIdentical,
        Error_PasswordComplexityLength,
        Error_PasswordComplexityCharacters,
        Error_PasswordOriginalWrong,
        Error_ResetIdInvalidOrExpired,
        Error_UnknownErrorOccured,
        Error_GrantTypeRequired,
        Error_RefreshTokenRequired,

        // Success
        RecordDeleteSuccess,
        MFATokensResetSuccess,

        // Labels
        Label_AddressId,
        Label_CustomerId,
        Label_PedaloId,
        Label_BookingId,
        Label_ChangePassword,
        Label_DataSavedSuccess,
        Label_DateOfBirth,
        Label_Firstname,
        Label_Lastname,
        Label_Password,
        Label_Password_Existing,
        Label_Password_Forgot,
        Label_Password_New,
        Label_Password_NewConfirmation,
        Label_PasswordSaved,
        Label_Settings,
        Label_Tenant,
        Label_TenantId,
        Label_Tenant_Name,
        Label_Tenant_DateCreated,
        Label_Account,
        Label_AccountId,
        Label_Email,
        Label_LastLoginDate,
        Label_ManageUsers,
        Label_ManageMFATokens,
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
        Nav_Addresses,
        Nav_Customers,
        Nav_Pedalos,
        Nav_Bookings,
        Nav_Test2,

        // Texts
        Text_AddressIntro,
        Text_CustomersIntro,
        Text_PedalosIntro,
        Text_BookingsIntro,
        Text_Logout,
        Text_ResetPasswordInfo,
        Text_SetNewPasswordInfo,
        Text_TenantIntro,
        Text_AccountIntro,
        Text_Account_PasswordNotChangeable,
        Text_ConfirmDeleteEntry,

        // Titles
        Title_Addresses,
        Title_Customers,
        Title_Pedalos,
        Title_Bookings,
        Title_Address_Edit,
        Title_Address_New,
        Title_MFA_New,
        Title_ResetPassword,
        Title_SetNewPassword,
        Title_Tenant,
        Title_Tenant_New,
        Title_Tenant_Edit,
        Title_Account,
        Title_Account_Edit,
        Title_Account_New,
        Title_DeleteEntry,
    }
}
