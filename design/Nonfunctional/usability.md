# Usability
This is probably the most important one as other solutions have awful usability. This needs to be as transparent as possible to applications it just needs to appear like its not even there. But we need to also have a user friendy interface for setting configuration.

## Configuration Usability
The aim here is to have as little configuration as possible. Although we want to minimise configuration we need to ensure there is absolutely know hardcoding. It needs to be possible to change the functionality of these applications without a code deployment. Eventually over time we would like the user to configure all this.

## Proxy Usability
While a tenant may not know this service even exists, this is the best usabililty, complete transparency. This section does not configure setting configruation settings. THat should be handled in the configuration usability section.

### Questions
What is the absolute easiest way to specify the proxy config?
The absolute easiest way would be to take in an open API swagger spec with the various fields tagged with a custom attribute. Either way this will need to be converted to a proxy readable setting. So accepting both as an option seems resonable.

It should be possible to indicate if settings are case sensitive.

## Transparency
One key factor is to make this as transparent as possible, we want as litte intrusion in the tenant set up as possible. Literally just the proxy reverse proxy.
