namespace PrivateCertificateGenerator.CertificateBuilders;

public record DistinguishedName(
    string CountryName,
    string State,
    string City,
    string Organization,
    string OrganizationUnit,
    string CommonName);
